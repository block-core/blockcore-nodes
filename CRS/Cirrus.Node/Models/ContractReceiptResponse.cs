using NBitcoin;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.SmartContracts.Core.Receipts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cirrus.Node.Models
{
    public class ContractReceiptResponse : ReceiptResponse
    {
        public string MethodName { get; set; }
        public string ContractCodeType { get; set; }

        public ContractReceiptResponse(Receipt receipt, List<LogResponse> logs, Network network)
            : base(receipt, logs, network)
        {
            this.MethodName = receipt.MethodName;
        }
    }
}
