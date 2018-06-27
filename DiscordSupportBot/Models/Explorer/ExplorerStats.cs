using DiscordSupportBot.Models.BaseModels;
using DiscordSupportBot.Modules;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.Explorer
{
    public class ExplorerStats
    {
        public const double blocksPerDay = 1440;

        public const int currentReward = 100;

        public const int firstHalveStart = 98801;
        public const double firstHalveReward = 72;

        public const int secondHalveStart = 113201;
        public const double secondHalveReward = 36;

        public const int thirdHalveStart = 127601;
        public const double thirdHalveReward = 18;

        public const double MasternodeReward = 0.70;
        public const double MasternodeRewardH1 = 0.72;
        public const double MasternodeRewardH2 = 0.74;
        public const double MasternodeRewardH3 = 0.75;

        public const double StakingReward = 0.30;
        public const double StakingRewardH1 = 0.23;
        public const double StakingRewardH2 = 0.21;
        public const double StakingRewardH3 = 0.20;

        public const double DevelopmentFee = 0.0;
        public const double DevelopmentFeeAfter = 0.05;


        public float Difficulty { get; set; }
        public int MasternodeCount { get; set; }
        public int BlockHeight { get; set; }
        public double Supply { get; set; }

    }
    public enum StatsDataType
    {
        Difficulty,
        MasternodeCount,
        BlockHeight,
        Supply
    }

}