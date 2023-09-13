using System;
using AMS.Networks.Policies;
using Blockcore.Base.Deployments;
using Blockcore.Consensus;
using Blockcore.Consensus.BlockInfo;
using Blockcore.Networks;
using Blockcore.NBitcoin;
using Blockcore.NBitcoin.BouncyCastle.math;
using Blockcore.NBitcoin.DataEncoders;

namespace AMS.Networks
{
   public class AMSRegTest : AMSMain
   {
      public AMSRegTest()
      {
         NetworkType = NetworkType.Regtest;

         Name = AMSSetup.RegTest.Name;
         CoinTicker = AMSSetup.RegTest.CoinTicker;
         Magic = ConversionTools.ConvertToUInt32(AMSSetup.Magic, true);
         RootFolderName = AMSSetup.RegTest.RootFolderName;
         DefaultPort = AMSSetup.RegTest.DefaultPort;
         DefaultRPCPort = AMSSetup.RegTest.DefaultRPCPort;
         DefaultAPIPort = AMSSetup.RegTest.DefaultAPIPort;

         var consensusFactory = new PosConsensusFactory();

         Block genesisBlock = CreateGenesisBlock(consensusFactory,
            AMSSetup.RegTest.GenesisTime,
            AMSSetup.RegTest.GenesisNonce,
            AMSSetup.RegTest.GenesisBits,
            AMSSetup.RegTest.GenesisVersion,
            AMSSetup.RegTest.GenesisReward,
            AMSSetup.GenesisText);

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
             coinType: AMSSetup.CoinType,
             hashGenesisBlock: genesisBlock.GetHash(),
             subsidyHalvingInterval: 250000,
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
             premineReward: Money.Coins(AMSSetup.PremineReward),
             proofOfWorkReward: Money.Coins(AMSSetup.PoWBlockReward),
             targetTimespan: TimeSpan.FromSeconds(14 * 24 * 60 * 60), // two weeks
             targetSpacing: AMSSetup.TargetSpacing,
             powAllowMinDifficultyBlocks: true,
             posNoRetargeting: true,
             powNoRetargeting: true,
             powLimit: new Target(new uint256("0000ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
             minimumChainWork: null,
             isProofOfStake: true,
             lastPowBlock: AMSSetup.LastPowBlock,
             proofOfStakeLimit: new BigInteger(uint256.Parse("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeLimitV2: new BigInteger(uint256.Parse("000000000000ffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeReward: Money.Coins(AMSSetup.PoSBlockReward),
             proofOfStakeTimestampMask: AMSSetup.ProofOfStakeTimestampMask
         );

         Base58Prefixes[(int)Base58Type.PUBKEY_ADDRESS] = new byte[] { (AMSSetup.RegTest.PubKeyAddress) };
         Base58Prefixes[(int)Base58Type.SCRIPT_ADDRESS] = new byte[] { (AMSSetup.RegTest.ScriptAddress) };
         Base58Prefixes[(int)Base58Type.SECRET_KEY] = new byte[] { (239) };
         Base58Prefixes[(int)Base58Type.EXT_PUBLIC_KEY] = new byte[] { (0x04), (0x35), (0x87), (0xCF) };
         Base58Prefixes[(int)Base58Type.EXT_SECRET_KEY] = new byte[] { (0x04), (0x35), (0x83), (0x94) };
         // Base58Prefixes[(int)Base58Type.STEALTH_ADDRESS] = new byte[] { 0x2b };
         Base58Prefixes[(int)Base58Type.ASSET_ID] = new byte[] { 115 };

         Bech32Encoders = new Bech32Encoder[2];
         var encoder = new Bech32Encoder(AMSSetup.RegTest.CoinTicker.ToLowerInvariant());
         Bech32Encoders[(int)Bech32Type.WITNESS_PUBKEY_ADDRESS] = encoder;
         Bech32Encoders[(int)Bech32Type.WITNESS_SCRIPT_ADDRESS] = encoder;

         Checkpoints = AMSSetup.RegTest.Checkpoints;
         DNSSeeds = AMSSetup.RegTest.DNS;
         SeedNodes = AMSSetup.RegTest.Nodes;

         StandardScriptsRegistry = new AMSStandardScriptsRegistry();

         // 64 below should be changed to TargetSpacingSeconds when we move that field.
         Assert(DefaultBanTimeSeconds <= Consensus.MaxReorgLength * 64 / 2);

         Assert(Consensus.HashGenesisBlock == uint256.Parse(AMSSetup.RegTest.HashGenesisBlock));
         Assert(Genesis.Header.HashMerkleRoot == uint256.Parse(AMSSetup.RegTest.HashMerkleRoot));

         RegisterRules(Consensus);
         RegisterMempoolRules(Consensus);
      }
   }
}
