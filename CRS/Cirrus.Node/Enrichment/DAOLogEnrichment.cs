using System.Collections.Generic;
using System.Text;
using DBreeze.Utils;
using NBitcoin;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.SmartContracts;
using Stratis.SmartContracts.Core.Receipts;
using Stratis.SmartContracts.Core.State;

namespace Cirrus.Node.Enrichment
{
    public class DAOLogEnrichment: ISmartContractLogEnrichment
    {
        private const string TypeName = "DAOContract";

        private readonly IStateRepositoryRoot stateRoot;
        private readonly Network network;
        private readonly ISerializer serializer;

        public DAOLogEnrichment(IStateRepositoryRoot stateRoot, Network network, ISerializer serializer)
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

            // this is the constructor we want to fetch the name, symbol, decimals and total supply.
            IStateRepositoryRoot stateAtHeight = this.stateRoot.GetSnapshotTo(receipt.PostState.ToBytes());

            uint160 addressNumeric  =  receipt.MethodName != null || receipt.NewContractAddress == null ? receipt.To: receipt.NewContractAddress;

            byte[] owner = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("Owner"));
            byte[] whiteListCount = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("WhitelistedCount"));
            byte[] minVotingDuration = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("MinVotingDuration"));
            byte[] maxVotingDuration = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("MaxVotingDuration"));
            byte[] lastProposalId = stateAtHeight.GetStorageValue(addressNumeric, Encoding.UTF8.GetBytes("LastProposalId"));



            logResponses.Add(new LogResponse(new Log(addressNumeric, new List<byte[]>(), new byte[0]), this.network)
            {
                Log = new LogData("Constructor", new Dictionary<string, object>
                {
                    { "Owner", serializer.ToAddress(owner)},
                    { "WhitelistedCount", whiteListCount == null ? 0 : serializer.ToUInt32(whiteListCount)},
                    { "MinVotingDuration", serializer.ToUInt32(minVotingDuration)},
                    { "MaxVotingDuration", serializer.ToUInt32(maxVotingDuration)},
                    { "LastProposalId", serializer.ToUInt32(lastProposalId)},
                })
            });
        }
    }
}
