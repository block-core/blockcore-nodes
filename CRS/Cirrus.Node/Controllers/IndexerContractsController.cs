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

            List<LogResponse> logResponses = new List<LogResponse>();

            if (receipt.Logs.Any())
            {
                var deserializer = new ApiLogDeserializer(this.primitiveSerializer, this.network, this.stateRoot, this.contractAssemblyCache);

                logResponses = deserializer.MapLogResponses(receipt.Logs);
            }

            EnrischLogs(receipt, typeName, logResponses);

            return this.Json(new ContractReceiptResponse(receipt, logResponses, this.network)
            {
                ContractCodeType = typeName,
            });
        }

        private void EnrischLogs(Receipt receipt, string typeName, List<LogResponse> logResponses)
        {
            if (typeName == "StandardToken")
            {
                if (receipt.Success)
                {
                    if (receipt.MethodName == null && receipt.NewContractAddress != null)
                    {
                        // this is the constructor we want to fetch the name, symbol, decimals and total supply.
                        IStateRepositoryRoot stateAtHeight = this.stateRoot.GetSnapshotTo(receipt.PostState.ToBytes());

                        uint160 addressNumeric = receipt.NewContractAddress;

                        byte[] tokenName = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Name"));
                        byte[] tokenSymbole = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Symbol"));
                        byte[] tokenTotalSupply = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("TotalSupply"));

                        logResponses.Add(new LogResponse(new Log(addressNumeric, new List<byte[]>(), new byte[0]), this.network)
                        {
                            Log = new LogData("Constructor", new Dictionary<string, object>
                            {
                                { "tokenName", Encoding.UTF8.GetString(tokenName)},
                                { "tokenSymbole", Encoding.UTF8.GetString(tokenSymbole)},
                                { "tokenTotalSupply", BitConverter.ToInt64(tokenTotalSupply)}
                            })
                        });
                    }
                }
            }
        }
    }
}
