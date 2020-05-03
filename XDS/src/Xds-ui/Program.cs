using System;
using Blockcore.Builder;
using Blockcore.Configuration;
using Blockcore.Features.Api;
using Blockcore.Features.BlockStore;
using Blockcore.Features.ColdStaking;
using Blockcore.Features.Consensus;
using Blockcore.Features.MemoryPool;
using Blockcore.Features.Miner;
using Blockcore.Features.RPC;
using Blockcore.Utilities;
using NBitcoin.Protocol;
using WebWindows;

namespace Blockcore
{
   public class Program
   {
      public static void Main(string[] args)
      {
         try
         {
            var nodeSettings = new NodeSettings(networksSelector: Blockcore.Networks.Xds.Networks.Xds,
                protocolVersion: ProtocolVersion.PROVEN_HEADER_VERSION,
                args: args);

            IFullNodeBuilder nodeBuilder = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings)
                .UseBlockStore()
                .UsePosConsensus()
                .UseMempool()
                .UseColdStakingWallet()
                .AddPowPosMining()
                .UseApi()
                .AddRPC();

            var node = nodeBuilder.Build();
            var nodeTask = node.RunAsync();

            //HideConsole();

            var window = new WebWindow(node.Network.CoinTicker + " Node");
            window.NavigateToUrl(node.NodeService<ApiSettings>().ApiUri.ToString());
            window.Size = new System.Drawing.Size(1050, 650);
            window.SetIconFile("favicon.ico");
            window.WaitForExit();

            node.Dispose();
         }
         catch (Exception ex)
         {
            Console.WriteLine("There was a problem initializing the node. Details: '{0}'", ex.ToString());
         }
      }
   }
}
