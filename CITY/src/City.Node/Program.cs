using System;
using System.Threading.Tasks;
using Blockcore;
using Blockcore.Builder;
using Blockcore.Configuration;
using Blockcore.Features.NodeHost;
using Blockcore.Features.BlockExplorer;
using Blockcore.Features.BlockStore;
using Blockcore.Features.ColdStaking;
using Blockcore.Features.Consensus;
using Blockcore.Features.Diagnostic;
using Blockcore.Features.MemoryPool;
using Blockcore.Features.Miner;
using Blockcore.Features.RPC;
using Blockcore.Features.WalletNotify;
using Blockcore.Utilities;
using NBitcoin.Protocol;
using Blockcore.Networks.City.Networks;

namespace City.Daemon
{
   public class Program
   {
      public static async Task Main(string[] args)
      {
         try
         {
            var nodeSettings = new NodeSettings(networksSelector: Networks.City, args: args);

            IFullNodeBuilder nodeBuilder = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings)
                .UseBlockStore()
                .UseBlockExplorer()
                .UsePosConsensus()
                .UseMempool()
                .UseColdStakingWallet()
                .AddPowPosMining()
                .UseNodeHost()
                .UseWalletNotify()
                .AddRPC()
                .UseDiagnosticFeature();

            IFullNode node = nodeBuilder.Build();

            if (node != null)
               await node.RunAsync();
         }
         catch (Exception ex)
         {
            Console.WriteLine("There was a problem initializing the node. Details: '{0}'", ex);
         }
      }
   }
}
