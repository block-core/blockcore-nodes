using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using NBitcoin;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.SmartContracts;
using Stratis.SmartContracts.CLR;
using Stratis.SmartContracts.Core.Receipts;
using Stratis.SmartContracts.Core.State;

namespace Cirrus.Node.Enrichment
{
    public class NonFungibleTokenLogEnrichment : ISmartContractLogEnrichment
    {
        private readonly IStateRepositoryRoot stateRoot;
        private readonly Network network;
        private readonly ISerializer serializer;
        private readonly ILogger logger;

        public NonFungibleTokenLogEnrichment(IStateRepositoryRoot stateRoot, Network network, ISerializer serializer, ILoggerFactory loggerFactory)
        {
            this.stateRoot = stateRoot;
            this.network = network;
            this.serializer = serializer;
            logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        private const string typeName = "NonFungibleToken";

        public bool IsSmartContractSupported(string typeName)
        {
            return typeName == NonFungibleTokenLogEnrichment.typeName;
        }

        public void EnrichLogs(Receipt receipt, List<LogResponse> logResponses)
        {
            if (!receipt.Success) return;

            switch (receipt.MethodName) //TODO this needs to move into a factory
            {
                case null:
                    AddCreateDataToLog(receipt, logResponses);
                    break;
                case "Mint":
                case "SafeMint":
                    AddMintTokenDataToLog(receipt, logResponses);
                    break;
                default:
                    logger.LogDebug($"Couldn't find a data to log method for {receipt.MethodName}");
                    break;
            }

        }

        private void AddCreateDataToLog(Receipt receipt, List<LogResponse> logResponses)
        {
            if (receipt.MethodName != null || receipt.NewContractAddress == null) return;

            // this is the constructor we want to fetch the name, symbol, decimals and total supply.
            IStateRepositoryRoot stateAtHeight = stateRoot.GetSnapshotTo(receipt.PostState.ToBytes());

            uint160 addressNumeric = receipt.NewContractAddress;

            byte[] nftName = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Name"));
            byte[] nftSymbole = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Symbol"));
            byte[] nftOwner = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Owner"));
            byte[] nftOwnerOnlyMinting =
                stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("OwnerOnlyMinting"));

            logResponses.Add(new LogResponse(new Log(addressNumeric, new List<byte[]>(), new byte[0]), this.network)
            {
                Log = new LogData("Constructor", new Dictionary<string, object>
                {
                    { "nftName", serializer.ToString(nftName) },
                    { "nftSymbol", serializer.ToString(nftSymbole) },
                    { "nftOwner", serializer.ToAddress(nftOwner).ToUint160().ToBase58Address(this.network) },
                    { "nftOwnerOnlyMinting", serializer.ToBool(nftOwnerOnlyMinting) }
                })
            });
        }

        private void AddMintTokenDataToLog(Receipt receipt, List<LogResponse> logResponses)
        {
            // this is the constructor we want to fetch the name, symbol, decimals and total supply.
            IStateRepositoryRoot stateAtHeight = stateRoot.GetSnapshotTo(receipt.PostState.ToBytes());

            uint160 addressNumeric = receipt.To;


            byte[] tokenUri = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes($"URI:{receipt.Result}"));

            logResponses.Add(new LogResponse(new Log(addressNumeric, new List<byte[]>(), new byte[0]), this.network)
            {
                Log = new LogData("MintExtract", new Dictionary<string, object>
                {
                    { "tokenUri", serializer.ToString(tokenUri) },
                })
            });
        }
    }
}
