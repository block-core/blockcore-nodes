using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
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
using Microsoft.Extensions.FileProviders;
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
            var nodeSettings = new NodeSettings(networksSelector: Blockcore.Networks.Xds.Networks.Xds, args: args);

            IFullNodeBuilder nodeBuilder = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings)
                .UseBlockStore()
                .UsePosConsensus()
                .UseMempool()
                .UseColdStakingWallet()
                .AddPowPosMining()
                .UseApi()
                .AddRPC();

            var window = new WebWindow("Blockcore");
            window.NavigateToString("<h1>Blockcore node loading....</h1> ");
            WriteResourceToFile("Xds-ui.favicon.ico", "Xds-ui.favicon.ico");
            window.SetIconFile("Xds-ui.favicon.ico");

            IFullNode node = null;

            Task.Run(() =>
            {
               try
               {
                  node = nodeBuilder.Build();
                  var nodeTask = node.RunAsync();
               }
               catch (Exception ex)
               {
                  window.NavigateToString("There was a problem initializing the node. <br> see the log file " + nodeSettings.DataFolder.LogPath + " " + ex.ToString());
                  return;
               }

               window.Title = node.Network.CoinTicker + " Node";
               window.NavigateToUrl(node.NodeService<ApiSettings>().ApiUri.ToString());
            });

            window.WaitForExit();
            node?.Dispose();
         }
         catch (Exception ex)
         {
            Console.WriteLine("There was a problem initializing the node. Details: '{0}'", ex.ToString());
         }
      }

      public static void WriteResourceToFile(string resourceName, string fileName)
      {
         using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
         {
            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
               resource.CopyTo(file);
            }
         }
      }
   }
}
