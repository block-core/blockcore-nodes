using System;
using Stratis.Networks.Policies;
using NBitcoin;
using NBitcoin.BouncyCastle.Math;
using NBitcoin.DataEncoders;
using Stratis.Networks.Deployments;

namespace Stratis.Networks
{
   public class StratisRegTest : StratisMain
   {
      public StratisRegTest()
      {
         NetworkType = NetworkType.Regtest;

         Name = StratisSetup.RegTest.Name;
         CoinTicker = StratisSetup.RegTest.CoinTicker;
         Magic = ConversionTools.ConvertToUInt32(StratisSetup.Magic, true);
         RootFolderName = StratisSetup.RegTest.RootFolderName;
         DefaultPort = StratisSetup.RegTest.DefaultPort;
         DefaultRPCPort = StratisSetup.RegTest.DefaultRPCPort;
         DefaultAPIPort = StratisSetup.RegTest.DefaultAPIPort;
         DefaultSignalRPort = StratisSetup.RegTest.DefaultSignalRPort;

         var consensusFactory = new PosConsensusFactory();

         Block genesisBlock = CreateGenesisBlock(consensusFactory,
            StratisSetup.RegTest.GenesisTime,
            StratisSetup.RegTest.GenesisNonce,
            StratisSetup.RegTest.GenesisBits,
            StratisSetup.RegTest.GenesisVersion,
            StratisSetup.RegTest.GenesisReward,
            StratisSetup.GenesisText);

         Genesis = genesisBlock;

         // Taken from StratisX.
         var consensusOptions = new PosConsensusOptions(
             maxBlockBaseSize: 1_000_000,
             maxStandardVersion: 2,
             maxStandardTxWeight: 100_000,
             maxBlockSigopsCost: 20_000,
             maxStandardTxSigopsCost: 20_000 / 5,
             witnessScaleFactor: 4
         );

         var buriedDeployments = new BuriedDeploymentsArray
         {
            [BuriedDeployments.BIP34] = 0,
            [BuriedDeployments.BIP65] = 0,
            [BuriedDeployments.BIP66] = 0
         };

         var bip9Deployments = new StratisBIP9Deployments()
         {
            // Always active on StratisRegTest.
            [StratisBIP9Deployments.TestDummy] = new BIP9DeploymentsParameters("TestDummy", 28, BIP9DeploymentsParameters.AlwaysActive, 999999999, BIP9DeploymentsParameters.AlwaysActive),
            [StratisBIP9Deployments.CSV] = new BIP9DeploymentsParameters("CSV", 0, BIP9DeploymentsParameters.AlwaysActive, 999999999, BIP9DeploymentsParameters.AlwaysActive),
            [StratisBIP9Deployments.Segwit] = new BIP9DeploymentsParameters("Segwit", 1, BIP9DeploymentsParameters.AlwaysActive, 999999999, BIP9DeploymentsParameters.AlwaysActive),
            [StratisBIP9Deployments.ColdStaking] = new BIP9DeploymentsParameters("ColdStaking", 2, BIP9DeploymentsParameters.AlwaysActive, 999999999, BIP9DeploymentsParameters.AlwaysActive),
         };

         Consensus = new NBitcoin.Consensus(
             consensusFactory: consensusFactory,
             consensusOptions: consensusOptions,
             coinType: StratisSetup.CoinType,
             hashGenesisBlock: genesisBlock.GetHash(),
             subsidyHalvingInterval: 216178,
             majorityEnforceBlockUpgrade: 750,
             majorityRejectBlockOutdated: 950,
             majorityWindow: 1000,
             buriedDeployments: buriedDeployments,
             bip9Deployments: bip9Deployments,
             bip34Hash: new uint256("0x000000000000024b89b42a942fe0d9fea3bb44ab7bd1b19115dd6a759c0808b8"),
             minerConfirmationWindow: 2016, // nPowTargetTimespan / nPowTargetSpacing
             maxReorgLength: 500,
             defaultAssumeValid: null,
             maxMoney: long.MaxValue,
             coinbaseMaturity: 10,
             premineHeight: 2,
             premineReward: Money.Coins(StratisSetup.PremineReward),
             proofOfWorkReward: Money.Coins(StratisSetup.PoWBlockReward),
             targetTimespan: TimeSpan.FromSeconds(14 * 24 * 60 * 60), // two weeks
             targetSpacing: StratisSetup.TargetSpacing,
             powAllowMinDifficultyBlocks: true,
             posNoRetargeting: true,
             powNoRetargeting: true,
             powLimit: new Target(new uint256("0000ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
             minimumChainWork: null,
             isProofOfStake: true,
             lastPowBlock: StratisSetup.LastPowBlock,
             proofOfStakeLimit: new BigInteger(uint256.Parse("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeLimitV2: new BigInteger(uint256.Parse("000000000000ffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeReward: Money.Coins(StratisSetup.PoSBlockReward),
             proofOfStakeTimestampMask: StratisSetup.ProofOfStakeTimestampMask
         );

         Base58Prefixes[(int)Base58Type.PUBKEY_ADDRESS] = new byte[] { (StratisSetup.RegTest.PubKeyAddress) };
         Base58Prefixes[(int)Base58Type.SCRIPT_ADDRESS] = new byte[] { (StratisSetup.RegTest.ScriptAddress) };
         Base58Prefixes[(int)Base58Type.SECRET_KEY] = new byte[] { (239) };
         Base58Prefixes[(int)Base58Type.EXT_PUBLIC_KEY] = new byte[] { (0x04), (0x35), (0x87), (0xCF) };
         Base58Prefixes[(int)Base58Type.EXT_SECRET_KEY] = new byte[] { (0x04), (0x35), (0x83), (0x94) };
         Base58Prefixes[(int)Base58Type.STEALTH_ADDRESS] = new byte[] { 0x2b };
         Base58Prefixes[(int)Base58Type.ASSET_ID] = new byte[] { 115 };

         Bech32Encoders = new Bech32Encoder[2];
         var encoder = new Bech32Encoder(StratisSetup.RegTest.CoinTicker);
         Bech32Encoders[(int)Bech32Type.WITNESS_PUBKEY_ADDRESS] = encoder;
         Bech32Encoders[(int)Bech32Type.WITNESS_SCRIPT_ADDRESS] = encoder;

         Checkpoints = StratisSetup.RegTest.Checkpoints;
         DNSSeeds = StratisSetup.RegTest.DNS;
         SeedNodes = StratisSetup.RegTest.Nodes;

         StandardScriptsRegistry = new StratisStandardScriptsRegistry();

         // 64 below should be changed to TargetSpacingSeconds when we move that field.
         Assert(DefaultBanTimeSeconds <= Consensus.MaxReorgLength * 64 / 2);

         Assert(Consensus.HashGenesisBlock == uint256.Parse(StratisSetup.RegTest.HashGenesisBlock));
         Assert(Genesis.Header.HashMerkleRoot == uint256.Parse(StratisSetup.RegTest.HashMerkleRoot));

         RegisterRules(Consensus);
         RegisterMempoolRules(Consensus);
      }
   }
}
