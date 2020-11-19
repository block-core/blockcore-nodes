using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Blockcore.Consensus.Checkpoints;
using Blockcore.P2P;
using NBitcoin;
using NBitcoin.Protocol;

namespace Impleum
{
   public class ImpleumSetup
   {
      public const string FileNamePrefix = "impleum";
      public const string ConfigFileName = "impleum.conf";
      public const string Magic = "51-11-41-31";
      public const int CoinType = 769; // SLIP-0044: https://github.com/satoshilabs/slips/blob/master/slip-0044.md
      public const decimal PremineReward = 1000000;
      public const decimal PoWBlockReward = 48;
      public const decimal PoSBlockReward = 5;
      public const int LastPowBlock = 100000;
      public const string GenesisText = "https://cryptocrimson.com/news/apple-payment-request-api-ripple-interledger-protocol"; // The New York Times, 2020-04-16
      public static TimeSpan TargetSpacing = TimeSpan.FromSeconds(64);
      public const uint ProofOfStakeTimestampMask = 0x0000000F; // 0x0000003F // 64 sec
      public const int PoSVersion = 3;

      public class Main
      {
         public const string Name = "ImpleumMain";
         public const string RootFolderName = "impleum";
         public const string CoinTicker = "IMPL";
         public const int DefaultPort = 16171;
         public const int DefaultRPCPort = 16172;
         public const int DefaultAPIPort = 38222;
         public const int PubKeyAddress = 102; // B https://en.bitcoin.it/wiki/List_of_address_prefixes
         public const int ScriptAddress = 125; // b
         public const int SecretAddress = 191;

         public const uint GenesisTime = 1523364655;
         public const uint GenesisNonce = 2380297;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0x02a8be139ec629b13df22e7abc7f9ad5239df39efaf2f5bf3ab5e4d102425dbe";
         public const string HashMerkleRoot = "0xbd3233dd8d4e7ce3ee8097f4002b4f9303000a5109e02a402d41d2faf74eb244";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("impleum.com", "impleum.com"),
            new DNSSeedData("explorer.impleum.com", "explorer.impleum.com"),
            new DNSSeedData("impl.seed.blockcore.net", "impl.seed.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("109.108.77.134"), ImpleumSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("62.80.181.141"), ImpleumSetup.Test.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class RegTest
      {
         public const string Name = "ImpleumRegTest";
         public const string RootFolderName = "ImpleumRegTest";
         public const string CoinTicker = "TIMPL";
         public const int DefaultPort = 16171;
         public const int DefaultRPCPort = 16172;
         public const int DefaultAPIPort = 38222;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1523364655;
         public const uint GenesisNonce = 2380297;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0x02a8be139ec629b13df22e7abc7f9ad5239df39efaf2f5bf3ab5e4d102425dbe";
         public const string HashMerkleRoot = "0xbd3233dd8d4e7ce3ee8097f4002b4f9303000a5109e02a402d41d2faf74eb244";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("seedregtest1.impl.blockcore.net", "seedregtest1.impl.blockcore.net"),
            new DNSSeedData("seedregtest2.impl.blockcore.net", "seedregtest2.impl.blockcore.net"),
            new DNSSeedData("seedregtest.impl.blockcore.net", "seedregtest.impl.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("109.108.77.134"), ImpleumSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("62.80.181.141"), ImpleumSetup.Test.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class Test
      {
         public const string Name = "ImpleumTest";
         public const string RootFolderName = "ImpleumTest";
         public const string CoinTicker = "TIMPL";
         public const int DefaultPort = 16171;
         public const int DefaultRPCPort = 16172;
         public const int DefaultAPIPort = 38222;
         public const int PubKeyAddress = 111;
         public const int ScriptAddress = 196;
         public const int SecretAddress = 239;

         public const uint GenesisTime = 1523364655;
         public const uint GenesisNonce = 2380297;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0x02a8be139ec629b13df22e7abc7f9ad5239df39efaf2f5bf3ab5e4d102425dbe";
         public const string HashMerkleRoot = "0xbd3233dd8d4e7ce3ee8097f4002b4f9303000a5109e02a402d41d2faf74eb244";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("seedtest1.impl.blockcore.net", "seedtest1.impl.blockcore.net"),
            new DNSSeedData("seedtest2.impl.blockcore.net", "seedtest2.impl.blockcore.net"),
            new DNSSeedData("seedtest.impl.blockcore.net", "seedtest.impl.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("109.108.77.134"), ImpleumSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("62.80.181.141"), ImpleumSetup.Test.DefaultPort),
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
