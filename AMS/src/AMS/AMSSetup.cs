using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Blockcore.Consensus.Checkpoints;
using Blockcore.P2P;
using NBitcoin;
using NBitcoin.Protocol;

namespace AMS
{
   public class AMSSetup
   {
      public const string FileNamePrefix = "ams";
      public const string ConfigFileName = "ams.conf";
      public const string Magic = "82-81-80-11";
      public const int CoinType = 524; // SLIP-0044: https://github.com/satoshilabs/slips/blob/master/slip-0044.md
      public const decimal PremineReward = 144754435m;
      public const decimal PoWBlockReward = 2m;
      public const decimal PoSBlockReward = 2m;
      public const int LastPowBlock = 2500;
      public const string GenesisText = "https://www.amsterdamcoin.com"; // The New York Times, 2020-04-16
      public static TimeSpan TargetSpacing = TimeSpan.FromSeconds(64);
      public const uint ProofOfStakeTimestampMask = 0x0000000F; // 0x0000003F // 64 sec
      public const int PoSVersion = 3;

      public class Main
      {
         public const string Name = "AMSMain";
         public const string RootFolderName = "ams";
         public const string CoinTicker = "AMS";
         public const int DefaultPort = 50000;
         public const int DefaultRPCPort = 51000;
         public const int DefaultAPIPort = 63000;
         public const int PubKeyAddress = 83; // https://en.bitcoin.it/wiki/List_of_address_prefixes
         public const int ScriptAddress = 125;
         public const int SecretAddress = 191;

         public const uint GenesisTime = 1585312300;
         public const uint GenesisNonce = 1904723;
         public const uint GenesisBits = 0x1e0fffff;
         public const int GenesisVersion = 1;
         public static Money GenesisReward = Money.Zero;
         public const string HashGenesisBlock = "0x18fccdeafad47d3e10a391d761881fca81c8ad32e3e6fa9576363712ab88982e";
         public const string HashMerkleRoot = "0x739b30bbbf51bc06f54c049db4bbf93747800dc6efcdcf29249c1cd6e19f2a36";

         public static List<DNSSeedData> DNS = new List<DNSSeedData>
         {
            // TODO: Add additional DNS seeds here
            new DNSSeedData("node1.amsterdamcoin.network", "node1.amsterdamcoin.network"),
            new DNSSeedData("node2.amsterdamcoin.network", "node2.amsterdamcoin.network"),
            new DNSSeedData("ams.seed.blockcore.net", "ams.seed.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("176.223.131.60"), AMSSetup.Main.DefaultPort),
            new NetworkAddress(IPAddress.Parse("85.214.223.236"), AMSSetup.Main.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.

                { 0, new CheckpointInfo(new uint256("0x18fccdeafad47d3e10a391d761881fca81c8ad32e3e6fa9576363712ab88982e"), new uint256("0x0000000000000000000000000000000000000000000000000000000000000000")) },
                { 2, new CheckpointInfo(new uint256("0x862e8e39013620d1f12b939d3036d1b29205858f159db0c97ee68604ebb72751"), new uint256("0x7889a02d48988af51ab85035d17185968bd403815d737638597234edb11ad7c2")) },
                { 500, new CheckpointInfo(new uint256("0x20423c8b1a3692c8340540548f4b16e0dc6b74faea68f5058832e0f6af99f41b"), new uint256("0x98f9f469f6abcfc9e196599b099d8c5a71b8240e8c1ca63b6fd1f269919f0519")) }
         };
      }

      public class RegTest
      {
         public const string Name = "AMSRegTest";
         public const string RootFolderName = "AMSRegTest";
         public const string CoinTicker = "TAMS";
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
            new DNSSeedData("seedregtest1.ams.blockcore.net", "seedregtest1.ams.blockcore.net"),
            new DNSSeedData("seedregtest2.ams.blockcore.net", "seedregtest2.ams.blockcore.net"),
            new DNSSeedData("seedregtest.ams.blockcore.net", "seedregtest.ams.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("176.223.131.60"), AMSSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("85.214.223.236"), AMSSetup.Test.DefaultPort),
         };

         public static Dictionary<int, CheckpointInfo> Checkpoints = new Dictionary<int, CheckpointInfo>
         {
            // TODO: Add checkpoints as the network progresses.
         };
      }

      public class Test
      {
         public const string Name = "AMSTest";
         public const string RootFolderName = "AMSTest";
         public const string CoinTicker = "TAMS";
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
            new DNSSeedData("seedtest1.ams.blockcore.net", "seedtest1.ams.blockcore.net"),
            new DNSSeedData("seedtest2.ams.blockcore.net", "seedtest2.ams.blockcore.net"),
            new DNSSeedData("seedtest.ams.blockcore.net", "seedtest.ams.blockcore.net"),
         };

         public static List<NetworkAddress> Nodes = new List<NetworkAddress>
         {
            // TODO: Add additional seed nodes here
            new NetworkAddress(IPAddress.Parse("176.223.131.60"), AMSSetup.Test.DefaultPort),
            new NetworkAddress(IPAddress.Parse("85.214.223.236"), AMSSetup.Test.DefaultPort),
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
