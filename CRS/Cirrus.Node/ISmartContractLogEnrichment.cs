using System.Collections.Generic;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.SmartContracts.Core.Receipts;

namespace Cirrus.Node
{
    public interface ISmartContractLogEnrichment
    {
        bool IsSmartContractSupported(string typeName);
        void EnrichLogs(Receipt receipt,List<LogResponse> logResponses);
    }
}
