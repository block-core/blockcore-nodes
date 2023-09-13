using System;
using Solaris.Networks.Policies;
using Blockcore.NBitcoin;
using Blockcore.NBitcoin.BouncyCastle.math;
using Blockcore.NBitcoin.DataEncoders;
using Blockcore.Networks;
using Blockcore.Consensus.BlockInfo;
using Blockcore.Consensus;
using Blockcore.Base.Deployments;

namespace Solaris.Networks
{
   public class SolarisTest : SolarisMain
   {
      public SolarisTest()
      {
         NetworkType = NetworkType.Testnet;

         Name = SolarisSetup.Test.Name;
         CoinTicker = SolarisSetup.Test.CoinTicker;
         Magic = ConversionTools.ConvertToUInt32(SolarisSetup.Magic, true);
         RootFolderName = SolarisSetup.Test.RootFolderName;
         DefaultPort = SolarisSetup.Test.DefaultPort;
         DefaultRPCPort = SolarisSetup.Test.DefaultRPCPort;
         DefaultAPIPort = SolarisSetup.Test.DefaultAPIPort;

         var consensusFactory = new PosConsensusFactory();

         Block genesisBlock = CreateGenesisBlock(consensusFactory,
            SolarisSetup.Test.GenesisTime,
            SolarisSetup.Test.GenesisNonce,
            SolarisSetup.Test.GenesisBits,
            SolarisSetup.Test.GenesisVersion,
            SolarisSetup.Test.GenesisReward,
            SolarisSetup.GenesisText);

         Genesis = genesisBlock;

         // Taken from StratisX.
         var consensusOptions = new PosConsensusOptions
         {
            MaxBlockBaseSize = 1_000_000,
            MaxStandardVersion = 2,
            MaxStandardTxWeight = 100_000,
            MaxBlockSigopsCost = 20_000,
            MaxStandardTxSigopsCost = 20_000 / 5,
            WitnessScaleFactor = 4
         };

         var buriedDeployments = new BuriedDeploymentsArray
         {
            [BuriedDeployments.BIP34] = 0,
            [BuriedDeployments.BIP65] = 0,
            [BuriedDeployments.BIP66] = 0
         };

         Consensus = new Consensus(
             consensusFactory: consensusFactory,
             consensusOptions: consensusOptions,
             coinType: SolarisSetup.CoinType,
             hashGenesisBlock: genesisBlock.GetHash(),
             subsidyHalvingInterval: 260000,
             majorityEnforceBlockUpgrade: 750,
             majorityRejectBlockOutdated: 950,
             majorityWindow: 1000,
             buriedDeployments: buriedDeployments,
             bip9Deployments: new NoBIP9Deployments(),
             bip34Hash: null,
             minerConfirmationWindow: 2016, // nPowTargetTimespan / nPowTargetSpacing
             maxReorgLength: 500,
             defaultAssumeValid: null,
             maxMoney: long.MaxValue,
             coinbaseMaturity: 10,
             premineHeight: 2,
             premineReward: Money.Coins(SolarisSetup.PremineReward),
             proofOfWorkReward: Money.Coins(SolarisSetup.PoWBlockReward),
             targetTimespan: TimeSpan.FromSeconds(14 * 24 * 60 * 60), // two weeks
             targetSpacing: SolarisSetup.TargetSpacing,
             powAllowMinDifficultyBlocks: false,
             posNoRetargeting: false,
             powNoRetargeting: false,
             powLimit: new Target(new uint256("000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
             minimumChainWork: null,
             isProofOfStake: true,
             lastPowBlock: SolarisSetup.LastPowBlock,
             proofOfStakeLimit: new BigInteger(uint256.Parse("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeLimitV2: new BigInteger(uint256.Parse("000000000000ffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeReward: Money.Coins(SolarisSetup.PoSBlockReward),
             proofOfStakeTimestampMask: SolarisSetup.ProofOfStakeTimestampMask
         );

         Base58Prefixes[(int)Base58Type.PUBKEY_ADDRESS] = new byte[] { (SolarisSetup.RegTest.PubKeyAddress) };
         Base58Prefixes[(int)Base58Type.SCRIPT_ADDRESS] = new byte[] { (SolarisSetup.RegTest.ScriptAddress) };
         Base58Prefixes[(int)Base58Type.SECRET_KEY] = new byte[] { (239) };
         Base58Prefixes[(int)Base58Type.EXT_PUBLIC_KEY] = new byte[] { (0x04), (0x35), (0x87), (0xCF) };
         Base58Prefixes[(int)Base58Type.EXT_SECRET_KEY] = new byte[] { (0x04), (0x35), (0x83), (0x94) };
         // Base58Prefixes[(int)Base58Type.STEALTH_ADDRESS] = new byte[] { 0x2b };
         Base58Prefixes[(int)Base58Type.ASSET_ID] = new byte[] { 115 };

         Bech32Encoders = new Bech32Encoder[2];
         var encoder = new Bech32Encoder(SolarisSetup.RegTest.CoinTicker.ToLowerInvariant());
         Bech32Encoders[(int)Bech32Type.WITNESS_PUBKEY_ADDRESS] = encoder;
         Bech32Encoders[(int)Bech32Type.WITNESS_SCRIPT_ADDRESS] = encoder;

         Checkpoints = SolarisSetup.Test.Checkpoints;
         DNSSeeds = SolarisSetup.Test.DNS;
         SeedNodes = SolarisSetup.Test.Nodes;

         StandardScriptsRegistry = new SolarisStandardScriptsRegistry();

         // 64 below should be changed to TargetSpacingSeconds when we move that field.
         Assert(DefaultBanTimeSeconds <= Consensus.MaxReorgLength * 64 / 2);

         Assert(Consensus.HashGenesisBlock == uint256.Parse(SolarisSetup.Test.HashGenesisBlock));
         Assert(Genesis.Header.HashMerkleRoot == uint256.Parse(SolarisSetup.Test.HashMerkleRoot));

         RegisterRules(Consensus);
         RegisterMempoolRules(Consensus);
      }
   }
}
