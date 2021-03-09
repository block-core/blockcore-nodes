using System;
using System.Threading.Tasks;
using Blockcore;
using Blockcore.Builder;
using Blockcore.Configuration;
using Blockcore.Features.NodeHost;
using Blockcore.Features.BlockStore;
using Blockcore.Features.Consensus;
using Blockcore.Features.Wallet;
using Blockcore.Features.MemoryPool;
using Blockcore.Features.Miner;
using Blockcore.Features.RPC;
using Blockcore.Utilities;
using Blockcore.Networks.XRC;

namespace xRhodium.Daemon
{
   public class Program
   {
      public static async Task Main(string[] args)
      {
         try
         {
            var nodeSettings = new NodeSettings(networksSelector: Networks.XRC, args: args);

            IFullNodeBuilder nodeBuilder = new FullNodeBuilder()
                   .UseNodeSettings(nodeSettings)
                   .UseBlockStore()
                   .UsePowConsensus()
                   .UseMempool()
                   .AddMining()
                   .AddRPC()
                   .UseWallet()
                   .UseNodeHost();

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
