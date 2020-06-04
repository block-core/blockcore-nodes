using System;
using X42.Networks.Policies;
using NBitcoin;
using NBitcoin.BouncyCastle.Math;
using NBitcoin.DataEncoders;

namespace X42.Networks
{
   public class X42Test : X42Main
   {
      public X42Test()
      {
         NetworkType = NetworkType.Testnet;

         Name = X42Setup.Test.Name;
         CoinTicker = X42Setup.Test.CoinTicker;
         Magic = ConversionTools.ConvertToUInt32(X42Setup.Magic, true);
         RootFolderName = X42Setup.Test.RootFolderName;
         DefaultPort = X42Setup.Test.DefaultPort;
         DefaultRPCPort = X42Setup.Test.DefaultRPCPort;
         DefaultAPIPort = X42Setup.Test.DefaultAPIPort;

         var consensusFactory = new PosConsensusFactory();

         Block genesisBlock = CreateGenesisBlock(consensusFactory,
            X42Setup.Test.GenesisTime,
            X42Setup.Test.GenesisNonce,
            X42Setup.Test.GenesisBits,
            X42Setup.Test.GenesisVersion,
            X42Setup.Test.GenesisReward,
            X42Setup.GenesisText);

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

         Consensus = new NBitcoin.Consensus(
             consensusFactory: consensusFactory,
             consensusOptions: consensusOptions,
             coinType: X42Setup.CoinType,
             hashGenesisBlock: genesisBlock.GetHash(),
             subsidyHalvingInterval: 252342,
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
             premineReward: Money.Coins(X42Setup.PremineReward),
             proofOfWorkReward: Money.Coins(X42Setup.PoWBlockReward),
             targetTimespan: TimeSpan.FromSeconds(14 * 24 * 60 * 60), // two weeks
             targetSpacing: X42Setup.TargetSpacing,
             powAllowMinDifficultyBlocks: false,
             posNoRetargeting: false,
             powNoRetargeting: false,
             powLimit: new Target(new uint256("000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
             minimumChainWork: null,
             isProofOfStake: true,
             lastPowBlock: X42Setup.LastPowBlock,
             proofOfStakeLimit: new BigInteger(uint256.Parse("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeLimitV2: new BigInteger(uint256.Parse("000000000000ffffffffffffffffffffffffffffffffffffffffffffffffffff").ToBytes(false)),
             proofOfStakeReward: Money.Coins(X42Setup.PoSBlockReward),
             proofOfStakeTimestampMask: X42Setup.ProofOfStakeTimestampMask
         );

         Base58Prefixes[(int)Base58Type.PUBKEY_ADDRESS] = new byte[] { (X42Setup.RegTest.PubKeyAddress) };
         Base58Prefixes[(int)Base58Type.SCRIPT_ADDRESS] = new byte[] { (X42Setup.RegTest.ScriptAddress) };
         Base58Prefixes[(int)Base58Type.SECRET_KEY] = new byte[] { (239) };
         Base58Prefixes[(int)Base58Type.EXT_PUBLIC_KEY] = new byte[] { (0x04), (0x35), (0x87), (0xCF) };
         Base58Prefixes[(int)Base58Type.EXT_SECRET_KEY] = new byte[] { (0x04), (0x35), (0x83), (0x94) };
         Base58Prefixes[(int)Base58Type.STEALTH_ADDRESS] = new byte[] { 0x2b };
         Base58Prefixes[(int)Base58Type.ASSET_ID] = new byte[] { 115 };

         Bech32Encoders = new Bech32Encoder[2];
         var encoder = new Bech32Encoder(X42Setup.RegTest.CoinTicker.ToLowerInvariant());
         Bech32Encoders[(int)Bech32Type.WITNESS_PUBKEY_ADDRESS] = encoder;
         Bech32Encoders[(int)Bech32Type.WITNESS_SCRIPT_ADDRESS] = encoder;

         Checkpoints = X42Setup.Test.Checkpoints;
         DNSSeeds = X42Setup.Test.DNS;
         SeedNodes = X42Setup.Test.Nodes;

         StandardScriptsRegistry = new X42StandardScriptsRegistry();

         // 64 below should be changed to TargetSpacingSeconds when we move that field.
         Assert(DefaultBanTimeSeconds <= Consensus.MaxReorgLength * 64 / 2);

         Assert(Consensus.HashGenesisBlock == uint256.Parse(X42Setup.Test.HashGenesisBlock));
         Assert(Genesis.Header.HashMerkleRoot == uint256.Parse(X42Setup.Test.HashMerkleRoot));

         RegisterRules(Consensus);
         RegisterMempoolRules(Consensus);
      }
   }
}
