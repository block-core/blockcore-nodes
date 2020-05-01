using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NBitcoin;
using NBitcoin.Protocol;

namespace Solaris
{
   public class SolarisSetup
   {
      public const string FileNamePrefix = "solaris";
      public const string ConfigFileName = "solaris.conf";
      public const string Magic = "71-36-23-06";
      public const int CoinType = 450; // SLIP-0044: https://github.com/satoshilabs/slips/blob/master/slip-0044.md
      public const decimal PremineReward = 2032000;
      public const decimal PoWBlockReward = 0.25M;
      public const decimal PoSBlockReward = 0.25M;
      public const int LastPowBlock = 2500;
      public const string GenesisText = "https://www.solarisplatform.com"; // The New York Times, 2020-04-16
      public static TimeSpan TargetSpacing = TimeSpan.FromSeconds(64);
      public const uint ProofOfStakeTimestampMask = 0x0000000F; // 0x0000003F // 64 sec
      public const int PoSVersion = 3;

      public class Main
      {
         public const string Name = "SolarisMain";
         public const string RootFolderName = "solaris";
         public const string CoinTicker = "XLR";
         public const int DefaultPort = 60000;
         public const int DefaultRPCPort = 61000;
         public const int DefaultAPIPort = 62000;
         public const int DefaultSignalRPort = 63000;
         public const int PubKeyAddress = 75; // B https://en.bitcoin.it/wiki/List_of_address_prefixes
         public const int ScriptAddress = 125; // b
         public const int SecretAddress = 191;

         public const uint GenesisTime = 1572266171;
         public const uint GenesisNonce = 1834723;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0xa3a98f72634c7d098164926b83ff136b66d1cafbb9aeb5a3b8d18da02937f79f";
         public const string HashMerkleRoot = "0x64ebe67e26861a4608c4315a7cb5671e7d15fb7546989b2621bfb806bbc6ad08";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            new DNSSeedData("node1.solarisdns.network", "node1.solarisdns.network"),
            new DNSSeedData("node2.solarisdns.network", "node2.solarisdns.network"),
            new DNSSeedData("node3.solarisdns.network", "node3.solarisdns.network"),
            new DNSSeedData("node4.solarisdns.network", "node4.solarisdns.network"),
            new DNSSeedData("node5.solarisdns.network", "node5.solarisdns.network"),
            new DNSSeedData("xlr.seed.blockcore.net", "xlr.seed.blockcore.net")
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            new NetworkAddress(IPAddress.Parse("176.223.131.60"), 60000), //Official node 1
            new NetworkAddress(IPAddress.Parse("85.214.223.236"), 60000), //Official node 2
            new NetworkAddress(IPAddress.Parse("85.214.241.80"), 60000), //Official node 3
            new NetworkAddress(IPAddress.Parse("85.214.130.77"), 60000), //Official node 4
            new NetworkAddress(IPAddress.Parse("81.169.238.113"), 60000), //Official node 5
            new NetworkAddress(IPAddress.Parse("81.169.234.147"), 60000), //Official node 6
            new NetworkAddress(IPAddress.Parse("80.211.88.141"), 60000) //Trustaking node
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
                { 0, new CheckpointInfo(new uint256("0xa3a98f72634c7d098164926b83ff136b66d1cafbb9aeb5a3b8d18da02937f79f"), new uint256("0x0000000000000000000000000000000000000000000000000000000000000000")) },
                { 1409, new CheckpointInfo(new uint256("0xd2f9c43c57fbb066daf940f80e9ce1a63d5d444e9e337b1491f79c36288ab0da"), new uint256("0x602e263081a44650085947dbe99fd0c51041389d79fb9c3f379f8a403a74d977")) },
                { 2900, new CheckpointInfo(new uint256("0x21e7bce830a7646e2ea049b7f37a42f032e09e8b2526cbf8d69fb094014826eb"), new uint256("0x83590685c1d2dfcb0ca9220af9447677a375cbfaaaf4305be309bb7141e41a47")) }
         };
      }

      public class RegTest
      {
         public const string Name = "SolarisRegTest";
         public const string RootFolderName = "SolarisRegTest";
         public const string CoinTicker = "TXLR";
         public const int DefaultPort = 60008;
         public const int DefaultRPCPort = 61008;
         public const int DefaultAPIPort = 62008;
         public const int DefaultSignalRPort = 63008;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1566229188;
         public const uint GenesisNonce = 479144;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0x198559121e1a041779e58f332d643bb1f1d760d4ca1e5add6d48d2e9ff881b99";
         public const string HashMerkleRoot = "0x5d96c5816f511f09c90c86c576473b8836de0506ef2a3505685f3e146c7d4ad3";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("seedregtest1.xlr.blockcore.net", "seedregtest1.xlr.blockcore.net"),
            new DNSSeedData("seedregtest2.xlr.blockcore.net", "seedregtest2.xlr.blockcore.net"),
            new DNSSeedData("seedregtest.xlr.blockcore.net", "seedregtest.xlr.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("176.223.131.60"), SolarisSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("85.214.223.236"), SolarisSetup.Test.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class Test
      {
         public const string Name = "SolarisTest";
         public const string RootFolderName = "SolarisTest";
         public const string CoinTicker = "TXLR";
         public const int DefaultPort = 60009;
         public const int DefaultRPCPort = 61009;
         public const int DefaultAPIPort = 62009;
         public const int DefaultSignalRPort = 63009;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1566229188;
         public const uint GenesisNonce = 479144;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0x198559121e1a041779e58f332d643bb1f1d760d4ca1e5add6d48d2e9ff881b99";
         public const string HashMerkleRoot = "0x5d96c5816f511f09c90c86c576473b8836de0506ef2a3505685f3e146c7d4ad3";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("seedtest1.xlr.blockcore.net", "seedtest1.xlr.blockcore.net"),
            new DNSSeedData("seedtest2.xlr.blockcore.net", "seedtest2.xlr.blockcore.net"),
            new DNSSeedData("seedtest.xlr.blockcore.net", "seedtest.xlr.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("176.223.131.60"), SolarisSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("85.214.223.236"), SolarisSetup.Test.DefaultPort),
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
