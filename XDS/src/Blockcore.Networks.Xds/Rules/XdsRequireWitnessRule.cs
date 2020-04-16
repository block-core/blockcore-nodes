using System.Threading.Tasks;
using Blockcore.Consensus.Rules;
using Microsoft.Extensions.Logging;

namespace Blockcore.Networks.Xds.Rules
{
   /// <summary>
   /// Checks if all transaction in the block have witness.
   /// </summary>
   public class XdsRequireWitnessRule : PartialValidationConsensusRule
   {
      public override Task RunAsync(RuleContext context)
      {
         NBitcoin.Block block = context.ValidationContext.BlockToValidate;

         foreach (NBitcoin.Transaction tx in block.Transactions)
         {
            if (!tx.HasWitness)
            {
               Logger.LogTrace($"(-)[FAIL_{nameof(XdsRequireWitnessRule)}]".ToUpperInvariant());

               XdsConsensusErrors.MissingWitness.Throw();
            }
         }

         return Task.CompletedTask;
      }
   }
}
