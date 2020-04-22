using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NBitcoin;
using NBitcoin.Protocol;

namespace Stratis
{
   public class StratisSetup
   {
      public const string FileNamePrefix = "stratis";
      public const string ConfigFileName = "stratis.conf";
      public const string Magic = "70-35-22-05";
      public const int CoinType = 105; // SLIP-0044: https://github.com/satoshilabs/slips/blob/master/slip-0044.md
      public const decimal PremineReward = 98000000;
      public const decimal PoWBlockReward = 4;
      public const decimal PoSBlockReward = 1;
      public const int LastPowBlock = 12500;
      public const string GenesisText = "http://www.theonion.com/article/olympics-head-priestess-slits-throat-official-rio--53466"; // The New York Times, 2020-04-16
      public static TimeSpan TargetSpacing = TimeSpan.FromSeconds(64);
      public const uint ProofOfStakeTimestampMask = 0x0000000F; // 0x0000003F // 64 sec
      public const int PoSVersion = 3;

      public class Main
      {
         public const string Name = "StratisMain";
         public const string RootFolderName = "stratis";
         public const string CoinTicker = "STRAT";
         public const int DefaultPort = 16178;
         public const int DefaultRPCPort = 16174;
         public const int DefaultAPIPort = 37221;
         public const int DefaultSignalRPort = 38824;
         public const int PubKeyAddress = 63; // B https://en.bitcoin.it/wiki/List_of_address_prefixes
         public const int ScriptAddress = 125; // b
         public const int SecretAddress = 191;

         public const uint GenesisTime = 1470467000;
         public const uint GenesisNonce = 1831645;
         public const uint GenesisBits = 0x1E0FFFFF;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0000066e91e46e5a264d42c89e1204963b2ee6be230b443e9159020539d972af";
         public const string HashMerkleRoot = "65a26bc20b0351aebf05829daefa8f7db2f800623439f3c114257c91447f1518";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("mainnet1.stratisnetwork.com", "mainnet1.stratisnetwork.com"),
            new DNSSeedData("mainnet2.stratisnetwork.com", "mainnet2.stratisnetwork.com"),
            new DNSSeedData("strat.seed.blockcore.net", "strat.seed.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("138.68.145.243"), StratisSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("169.1.13.216"), StratisSetup.Test.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class RegTest
      {
         public const string Name = "StratisRegTest";
         public const string RootFolderName = "StratisRegTest";
         public const string CoinTicker = "TSTRAT";
         public const int DefaultPort = 18444;
         public const int DefaultRPCPort = 18442;
         public const int DefaultAPIPort = 38221;
         public const int DefaultSignalRPort = 14336;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1494909211;
         public const uint GenesisNonce = 2433759;
         public const uint GenesisBits = 0x207FFFFF;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "93925104d664314f581bc7ecb7b4bad07bcfabd1cfce4256dbd2faddcf53bd1f";
         public const string HashMerkleRoot = "65a26bc20b0351aebf05829daefa8f7db2f800623439f3c114257c91447f1518";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("seedregtest1.strat.blockcore.net", "seedregtest1.strat.blockcore.net"),
            new DNSSeedData("seedregtest2.strat.blockcore.net", "seedregtest2.strat.blockcore.net"),
            new DNSSeedData("seedregtest.strat.blockcore.net", "seedregtest.strat.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("138.68.145.243"), StratisSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("169.1.13.216"), StratisSetup.Test.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class Test
      {
         public const string Name = "StratisTest";
         public const string RootFolderName = "StratisTest";
         public const string CoinTicker = "TSTRAT";
         public const int DefaultPort = 26178;
         public const int DefaultRPCPort = 26174;
         public const int DefaultAPIPort = 38221;
         public const int DefaultSignalRPort = 39824;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1493909211;
         public const uint GenesisNonce = 2433759;
         public const uint GenesisBits = 0x1F00FFFF;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "00000e246d7b73b88c9ab55f2e5e94d9e22d471def3df5ea448f5576b1d156b9";
         public const string HashMerkleRoot = "65a26bc20b0351aebf05829daefa8f7db2f800623439f3c114257c91447f1518";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("testnet1.stratisnetwork.com", "testnet1.stratisnetwork.com"),
            new DNSSeedData("testnet2.stratisnetwork.com", "testnet2.stratisnetwork.com"),
            new DNSSeedData("seedtest.strat.blockcore.net", "seedtest.strat.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("51.140.231.125"), StratisSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("169.1.13.216"), StratisSetup.Test.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public static bool IsPoSv3()
      {
         return PoSVersion == 3;
      }

      public static bool IsPoSv4()
      {
         return PoSVersion == 4;
      }
   }
}
