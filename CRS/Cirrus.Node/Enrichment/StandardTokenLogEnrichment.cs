using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.SmartContracts;
using Stratis.SmartContracts.Core.Receipts;
using Stratis.SmartContracts.Core.State;

namespace Cirrus.Node.Enrichment
{
    public class StandardTokenLogEnrichment : ISmartContractLogEnrichment
    {
        private readonly IStateRepositoryRoot stateRoot;
        private readonly Network network;
        private readonly ISerializer serializer;

        private const string TypeName = "StandardToken";

        public StandardTokenLogEnrichment(IStateRepositoryRoot stateRoot, Network network, ISerializer serializer)
        {
            this.stateRoot = stateRoot;
            this.network = network;
            this.serializer = serializer;
        }


        public bool IsSmartContractSupported(string typeName)
        {
            return typeName == TypeName;
        }

        public void EnrichLogs(Receipt receipt, List<LogResponse> logResponses)
        {
            if (!receipt.Success) return;
            if (receipt.MethodName != null || receipt.NewContractAddress == null) return;

            // this is the constructor we want to fetch the name, symbol, decimals and total supply.
            IStateRepositoryRoot stateAtHeight = this.stateRoot.GetSnapshotTo(receipt.PostState.ToBytes());

            uint160 addressNumeric = receipt.NewContractAddress;

            byte[] tokenName = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Name"));
            byte[] tokenSymbole = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Symbol"));
            byte[] tokenTotalSupply = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("TotalSupply"));
            byte[] tokenDecimals = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Decimals"));

            logResponses.Add(new LogResponse(new Log(addressNumeric, new List<byte[]>(), new byte[0]), this.network)
            {
                Log = new LogData("Constructor", new Dictionary<string, object>
                {
                    { "tokenName", Encoding.UTF8.GetString(tokenName)},
                    { "tokenSymbole", Encoding.UTF8.GetString(tokenSymbole)},
                    { "tokenTotalSupply", serializer.ToInt64(tokenTotalSupply)},
                    { "tokenDecimals", tokenDecimals?[0]}
                })
            });
        }
    }
}
