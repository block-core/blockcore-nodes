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
        public string ContractBytecode { get; set; }
        public string ContractCodeHash { get; set; }
        public string ContractCSharp { get; set; }
        public ulong GasPrice { get; set; }
        public ulong Amount { get; set; }
        public ulong ContractBalance { get; set; }

        public ContractReceiptResponse(Receipt receipt, List<LogResponse> logs, Network network)
            : base(receipt, logs, network)
        {
            this.MethodName = receipt.MethodName;
            this.GasPrice = receipt.GasPrice;
            this.Amount = receipt.Amount;
        }
    }
}
