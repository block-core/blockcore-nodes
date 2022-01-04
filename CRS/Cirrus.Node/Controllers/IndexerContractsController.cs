using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using Stratis.Bitcoin.Controllers;
using Stratis.Bitcoin.Features.SmartContracts.Models;
using Stratis.SmartContracts.CLR;
using Stratis.SmartContracts.CLR.Decompilation;
using Stratis.SmartContracts.Core;
using Stratis.SmartContracts.Core.State;

namespace Cirrus.Node.Controllers
{
    [ApiVersion("1")]
    [Route("api/indexer/contracts")]
    public class IndexerContractsController : FeatureController
    {
        private readonly Network network;
        private readonly IStateRepositoryRoot stateRoot;
        private readonly CSharpContractDecompiler contractDecompiler;

        public IndexerContractsController(
            Network network,
            IStateRepositoryRoot stateRoot,
            CSharpContractDecompiler contractDecompiler
        )
        {
            this.network = network;
            this.stateRoot = stateRoot;
            this.contractDecompiler = contractDecompiler;
        }

        [Route("code")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetDecompiledCode([FromQuery] string address)
        {
            uint160 addressNumeric = address.ToUint160(this.network);
            byte[] contractCode = this.stateRoot.GetCode(addressNumeric);

            if (contractCode == null || !contractCode.Any())
            {
                return this.Json(new GetCodeResponse
                {
                    Message = $"No contract execution code exists at {address}"
                });
            }

            string typeName = this.stateRoot.GetContractType(addressNumeric);

            Result<string> sourceResult = this.contractDecompiler.GetSource(contractCode);

            return this.Json(new GetCodeResponse
            {
                Message = $"Contract execution code retrieved at {address}",
                Bytecode = contractCode.ToHexString(),
                Type = typeName,
                CSharp = sourceResult.IsSuccess ? sourceResult.Value : sourceResult.Error // Show the source, or the reason why the source couldn't be retrieved.
            });
        }
    }
}
