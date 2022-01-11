using Microsoft.ApplicationInsights.NLogTarget;
using Microsoft.Extensions.Configuration;
using NBitcoin.Protocol;
using NLog;
using NLog.Config;
using Cirrus.Node.Features.KubernetesProbes;
using Stratis.Bitcoin;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Configuration;
using Stratis.Bitcoin.Consensus;
using Stratis.Bitcoin.Features.Api;
using Stratis.Bitcoin.Features.BlockStore;
using Stratis.Bitcoin.Features.MemoryPool;
using Stratis.Bitcoin.Features.Notifications;
using Stratis.Bitcoin.Features.RPC;
using Stratis.Bitcoin.Features.SmartContracts;
using Stratis.Bitcoin.Features.SmartContracts.PoA;
using Stratis.Bitcoin.Features.SmartContracts.Wallet;
using Stratis.Bitcoin.Networks;
using Stratis.Bitcoin.Utilities;
using Stratis.Features.Collateral;
using Stratis.Features.Collateral.CounterChain;
using Stratis.Features.SQLiteWalletRepository;
using Stratis.Sidechains.Networks;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cirrus.Node
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // IConfiguration configuration = BuildConfiguration();
            // SetupApplicationInsightsLogging(configuration["Azure:ApplicationInsights:InstrumentationKey"]);

            var isLaunchedWithDevMode = args.Any(a => a.Contains($"-{NodeSettings.DevModeParam}", StringComparison.InvariantCultureIgnoreCase));

            IFullNode fullNode = isLaunchedWithDevMode
                ? BuildCirrusDevNode(args)
                : BuildCirrusLiveNode(args);

            await fullNode.RunAsync();
        }

        private static IFullNode BuildCirrusDevNode(string[] args)
        {
            string[] devModeArgs = new[] { "-bootstrap=1", "-dbtype=rocksdb", "-defaultwalletname=cirrusdev", "-defaultwalletpassword=password" }.Concat(args).ToArray();

            var nodeSettings = new NodeSettings(new CirrusDev(), protocolVersion: ProtocolVersion.CIRRUS_VERSION, args: devModeArgs)
            {
                MinProtocolVersion = ProtocolVersion.ALT_PROTOCOL_VERSION
            };

            DbType dbType = nodeSettings.GetDbType();

            IFullNodeBuilder nodeBuilder = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings, dbType)
                .UseBlockStore(dbType)
                .AddPoAFeature()
                .UsePoAConsensus(dbType)
                .AddPoAMiningCapability<SmartContractPoABlockDefinition>()
                .UseTransactionNotification()
                .UseBlockNotification()
                .UseApi()
                .UseIndexerApi()
                .UseMempool()
                .AddRPC()
                .AddSmartContracts(options =>
                {
                    options.UseReflectionExecutor();
                    options.UsePoAWhitelistedContracts(true);
                })
                .UseSmartContractWallet()
                .AddSQLiteWalletRepository()
                .UseKubernetesProbes();

            return nodeBuilder.Build();
        }

        private static IFullNode BuildCirrusLiveNode(string[] args)
        {
            var nodeSettings = new NodeSettings(networksSelector: CirrusNetwork.NetworksSelector, protocolVersion: ProtocolVersion.CIRRUS_VERSION, args: args)
            {
                MinProtocolVersion = ProtocolVersion.ALT_PROTOCOL_VERSION
            };

            Console.Title = $"Cirrus Full Node {nodeSettings.Network.NetworkType}";

            DbType dbType = nodeSettings.GetDbType();

            IFullNodeBuilder nodeBuilder = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings, dbType)
                .UseBlockStore(dbType)
                .UseMempool()
                .AddSmartContracts(options =>
                {
                    options.UseReflectionExecutor();
                    options.UsePoAWhitelistedContracts();
                })
                .AddPoAFeature()
                .UsePoAConsensus(dbType)
                .CheckCollateralCommitment()
                .SetCounterChainNetwork(StraxNetwork.MainChainNetworks[nodeSettings.Network.NetworkType]())
                .UseSmartContractWallet()
                .AddSQLiteWalletRepository()
                .UseTransactionNotification()
                .UseBlockNotification()
                .UseApi()
                .UseIndexerApi()
                .AddRPC()
                .UseKubernetesProbes();
            // .AddSignalR(options =>
            // {
            //     DaemonConfiguration.ConfigureSignalRForCirrus(options);
            // })
            // .UseDiagnosticFeature();

            return nodeBuilder.Build();
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("configuration.json", optional: true, reloadOnChange: false)
                                             .AddUserSecrets<Program>()
                                             .AddEnvironmentVariables()
                                             .Build();
        }

        private static void SetupApplicationInsightsLogging(string appInsightsInstrumentationKey)
        {
            var loggingConfiguration = new LoggingConfiguration();

            ApplicationInsightsTarget appInsightsTarget = new ApplicationInsightsTarget
            {
                InstrumentationKey = appInsightsInstrumentationKey
            };

            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", LogLevel.Warn, appInsightsTarget));

            LogManager.Configuration = loggingConfiguration;
        }
    }
}
