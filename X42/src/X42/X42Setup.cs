using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NBitcoin;
using NBitcoin.Protocol;

namespace X42
{
   public class X42Setup
   {
      public const string FileNamePrefix = "x42";
      public const string ConfigFileName = "x42.conf";
      public const string Magic = "42-66-52-03";
      public const int CoinType = 424242; // SLIP-0044: https://github.com/satoshilabs/slips/blob/master/slip-0044.md
      public const decimal PremineReward = 10500000;
      public const decimal PoWBlockReward = 0;
      public const decimal PoSBlockReward = 20;
      public const int LastPowBlock = 523;
      public const string GenesisText = "On Emancipation Day, we are fighting to maintain our democratic freedom at various levels - https://www.stabroeknews.com/2018/opinion/letters/08/01/on-emancipation-day-we-are-fighting-to-maintain-our-democratic-freedom-at-various-levels/ | popó & lita - 6F3582CC2B720980C936D95A2E07F809"; // The New York Times, 2020-04-16
      public static TimeSpan TargetSpacing = TimeSpan.FromSeconds(64);
      public const uint ProofOfStakeTimestampMask = 0x0000003F; // 0x0000003F // 64 sec
      public const int PoSVersion = 3;

      public class Main
      {
         public const string Name = "X42Main";
         public const string RootFolderName = "x42";
         public const string CoinTicker = "x42";
         public const int DefaultPort = 52342;
         public const int DefaultRPCPort = 52343;
         public const int DefaultAPIPort = 42220;
         public const int PubKeyAddress = 75; // B https://en.bitcoin.it/wiki/List_of_address_prefixes
         public const int ScriptAddress = 125; // b
         public const int SecretAddress = 203;

         public const uint GenesisTime = 1533106324;
         public const uint GenesisNonce = 246101626;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0x04ffe583707a96c1c2eb54af33a4b1dc6d9d8e09fea8c9a7b097ba88f0cb64c4";
         public const string HashMerkleRoot = "0x6e3439a32382f83dee4f94a6f8bdd38908bcf0c82ec09aba85c5321357f01f67";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("mainnet1.x42seed.host", "mainnet1.x42seed.host"),
            new DNSSeedData("mainnetnode1.x42seed.host", "mainnetnode1.x42seed.host"),
            new DNSSeedData("x42.seed.blockcore.net", "x42.seed.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("34.255.35.42"), X42Setup.Main.DefaultPort),
            new NetworkAddress(IPAddress.Parse("52.211.235.48"), X42Setup.Main.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class RegTest
      {
         public const string Name = "X42RegTest";
         public const string RootFolderName = "X42RegTest";
         public const string CoinTicker = "Tx42";
         public const int DefaultPort = 9333;
         public const int DefaultRPCPort = 9332;
         public const int DefaultAPIPort = 9331;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1587118950;
         public const uint GenesisNonce = 41450;
         public const uint GenesisBits = 0x1F00FFFF;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "00008af47a491ddf7251cc05cd48f0272a9cfad8540de9400ea0506850f5ed93";
         public const string HashMerkleRoot = "344960deee772f2b6f8cdc5f6fe86e0fe3146f43430849d8ca5b9b851bdcc58c";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("seedregtest1.x42.blockcore.net", "seedregtest1.x42.blockcore.net"),
            new DNSSeedData("seedregtest2.x42.blockcore.net", "seedregtest2.x42.blockcore.net"),
            new DNSSeedData("seedregtest.x42.blockcore.net", "seedregtest.x42.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("34.255.35.42"), X42Setup.RegTest.DefaultPort),
            new NetworkAddress(IPAddress.Parse("52.211.235.48"), X42Setup.RegTest.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class Test
      {
         public const string Name = "X42Test";
         public const string RootFolderName = "X42Test";
         public const string CoinTicker = "Tx42";
         public const int DefaultPort = 9333;
         public const int DefaultRPCPort = 9332;
         public const int DefaultAPIPort = 9331;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1587118953;
         public const uint GenesisNonce = 4834;
         public const uint GenesisBits = 0x1F0FFFFF;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0009066a2c5b01b1b5ecb41267ce79a94954b75a5906a83221c587428f1e0bcd";
         public const string HashMerkleRoot = "7d197d7cf32b63e01b1cf0279b5d3c8ed733770284554881038d253b6c34b3a8";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("seedtest1.x42.blockcore.net", "seedtest1.x42.blockcore.net"),
            new DNSSeedData("seedtest2.x42.blockcore.net", "seedtest2.x42.blockcore.net"),
            new DNSSeedData("seedtest.x42.blockcore.net", "seedtest.x42.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("34.255.35.42"), X42Setup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("52.211.235.48"), X42Setup.Test.DefaultPort),
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
