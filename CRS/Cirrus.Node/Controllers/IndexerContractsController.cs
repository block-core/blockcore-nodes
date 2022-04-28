using Cirrus.Node.Enrichment;
using Cirrus.Node.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NBitcoin;
using Stratis.Bitcoin.Controllers;
using Stratis.Bitcoin.Features.SmartContracts;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.Bitcoin.Utilities.JsonErrors;
using Stratis.SmartContracts;
using Stratis.SmartContracts.CLR.Caching;
using Stratis.SmartContracts.CLR.Decompilation;
using Stratis.SmartContracts.CLR.Serialization;
using Stratis.SmartContracts.Core;
using Stratis.SmartContracts.Core.Receipts;
using Stratis.SmartContracts.Core.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Cirrus.Node.Controllers
{
    [ApiVersion("1")]
    [Route("api/indexer/contract")]
    public class IndexerContractsController : FeatureController
    {
        private readonly Network network;
        private readonly IStateRepositoryRoot stateRoot;
        private readonly CSharpContractDecompiler contractDecompiler;
        private readonly IReceiptRepository receiptRepository;
        private readonly IContractPrimitiveSerializer primitiveSerializer;
        private readonly IContractAssemblyCache contractAssemblyCache;
        private readonly ISerializer serializer;
        private readonly ISmartContractEnrichmentFactory contractEnrichmentFactory;
        private readonly ILogger logger;

        public IndexerContractsController(
            Network network,
            ILoggerFactory loggerFactory,
            IStateRepositoryRoot stateRoot,
            CSharpContractDecompiler contractDecompiler,
            IReceiptRepository receiptRepository,
            IContractPrimitiveSerializer primitiveSerializer,
            IContractAssemblyCache contractAssemblyCache,
            ISmartContractEnrichmentFactory contractEnrichmentFactory,
            ISerializer serializer)
        {
            this.network = network;
            this.stateRoot = stateRoot;
            this.contractDecompiler = contractDecompiler;
            this.receiptRepository = receiptRepository;
            this.primitiveSerializer = primitiveSerializer;
            this.contractAssemblyCache = contractAssemblyCache;
            this.serializer = serializer;
            this.contractEnrichmentFactory = contractEnrichmentFactory;
            this.logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        [Route("info")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetContractInfo([FromQuery] string txHash)
        {
            try
            {
                uint256 txHashNum = new uint256(txHash);
                Receipt receipt = this.receiptRepository.Retrieve(txHashNum);

                if (receipt == null)
                {
                    return null;
                }

                uint160 address = receipt.NewContractAddress ?? receipt.To;

                string typeName = null;
                byte[] contractCode = null;
                uint256 codeHash = null;
                string csharpCode = null;
                ulong balance = 0;

                if (address != null)
                {
                    IStateRepositoryRoot stateAtHeight = this.stateRoot.GetSnapshotTo(receipt.PostState.ToBytes());
                    AccountState accountState = stateAtHeight.GetAccountState(address);

                    if (accountState != null)
                    {
                        typeName = accountState.TypeName;
                        balance = stateAtHeight.GetCurrentBalance(address);

                        if (receipt.NewContractAddress != null)
                        {
                            codeHash = new uint256(accountState.CodeHash);
                            contractCode = this.stateRoot.GetCode(receipt.NewContractAddress);
                            Result<string> sourceResult = this.contractDecompiler.GetSource(contractCode);
                            csharpCode = sourceResult.IsSuccess ? sourceResult.Value : sourceResult.Error;
                        }
                    }
                }

                List<LogResponse> logResponses = new List<LogResponse>();

                if (receipt.Logs.Any())
                {
                    var deserializer = new ApiLogDeserializer(this.primitiveSerializer, this.network, this.stateRoot, this.contractAssemblyCache);

                    logResponses = deserializer.MapLogResponses(receipt.Logs);
                }

                var logEnrichment = contractEnrichmentFactory.GetLogEnrichment(typeName);

                logEnrichment?.EnrichLogs(receipt, logResponses);

                return this.Json(new ContractReceiptResponse(receipt, logResponses, this.network)
                {
                    ContractCodeType = typeName,
                    ContractBalance = balance,
                    ContractCodeHash = codeHash?.ToString(),
                    ContractBytecode = contractCode?.ToHexString(),
                    ContractCSharp = csharpCode
                });
            }
            catch (Exception e)
            {
                this.logger.LogError("Exception occurred: {0}", e.ToString());
                return ErrorHelpers.BuildErrorResponse(HttpStatusCode.BadRequest, e.Message, e.ToString());
            }
        }
    }
}
