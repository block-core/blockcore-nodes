using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Cirrus.Node.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using Stratis.Bitcoin.Controllers;
using Stratis.Bitcoin.Features.SmartContracts;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.SmartContracts.CLR;
using Stratis.SmartContracts.CLR.Caching;
using Stratis.SmartContracts.CLR.Decompilation;
using Stratis.SmartContracts.CLR.Serialization;
using Stratis.SmartContracts.Core;
using Stratis.SmartContracts.Core.Receipts;
using Stratis.SmartContracts.Core.State;

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

        public IndexerContractsController(
            Network network,
            IStateRepositoryRoot stateRoot,
            CSharpContractDecompiler contractDecompiler,
            IReceiptRepository receiptRepository,
            IContractPrimitiveSerializer primitiveSerializer,
            IContractAssemblyCache contractAssemblyCache
        )
        {
            this.network = network;
            this.stateRoot = stateRoot;
            this.contractDecompiler = contractDecompiler;
            this.receiptRepository = receiptRepository;
            this.primitiveSerializer = primitiveSerializer;
            this.contractAssemblyCache = contractAssemblyCache;
        }

        [Route("info")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetContractInfo([FromQuery] string txHash)
        {
            uint256 txHashNum = new uint256(txHash);
            Receipt receipt = this.receiptRepository.Retrieve(txHashNum);

            if (receipt == null)
            {
                return null;
            }

            uint160 address = receipt.NewContractAddress ?? receipt.To;

            string typeName = null;

            if (address != null)
            {
                typeName = this.stateRoot.GetContractType(address);
            }

            List<LogResponse> logResponses = null;

            if (receipt.Logs.Any())
            {
                var deserializer = new ApiLogDeserializer(this.primitiveSerializer, this.network, this.stateRoot, this.contractAssemblyCache);

                logResponses = deserializer.MapLogResponses(receipt.Logs);
            }

            return this.Json(new ContractReceiptResponse(receipt, logResponses ?? new List<LogResponse>(), this.network)
            {
                ContractCodeType = typeName,
            });
        }

    }
}
