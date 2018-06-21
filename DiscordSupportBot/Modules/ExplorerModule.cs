namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DiscordSupportBot.Models.Explorer;
    using System.Net.Http;

    public class ExplorerModule : ModuleBase<SocketCommandContext>
    {
        private static HttpClient client = new HttpClient();

        [Command("balance")]
        public async Task Balance(string address)
        {
            var result = await this.GetAddressBalance(address);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("CCB Bot Balance").WithColor(Discord.Color.Blue);
            builder.WithDescription(result.ToString() + " CCB");

            await this.Context.Message.Author.SendMessageAsync(string.Empty, false, builder.Build());
        }


        [Command("stats")]
        public async Task Stats()
        {
            var result = await this.GetStats();

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Current Statistics for CCB").WithColor(Discord.Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/CCB.png")
                .AddInlineField("Difficulty", result.Difficulty)
                .AddInlineField("Masternodes Count", result.MasternodeCount)
                .AddInlineField("Block Count", result.Block)
                .AddInlineField("Supply Count",result.Supply + "  CCB")
                .WithFooter("All block rewards are split: 70% Masternode, 30% Staking and 0% Development fee");
            await this.ReplyAsync(string.Empty, false, builder.Build());
        }





        private async Task<ExplorerStats> GetStats()
        {
            var difficultyResponse = await client.GetAsync($"http://explorer.ccbcoin.club/api/getdifficulty");
            var masternodeResponse = await client.GetAsync($"http://explorer.ccbcoin.club/api/getmasternodecount");
            var supplyResponse = await client.GetAsync($"http://explorer.ccbcoin.club/ext/getmoneysupply");
            var blockResponse = await client.GetAsync($"http://explorer.ccbcoin.club/api/getblockcount");


            var result = new ExplorerStats
            {
                Difficulty = float.Parse(difficultyResponse.Content.ReadAsStringAsync().Result),
                MasternodeCount = int.Parse(JsonConvert.DeserializeObject<dynamic>(masternodeResponse.Content.ReadAsStringAsync().Result).ToString()),
                Block = int.Parse(JsonConvert.DeserializeObject<dynamic>(blockResponse.Content.ReadAsStringAsync().Result).ToString()),
                Supply = double.Parse(JsonConvert.DeserializeObject<dynamic>(supplyResponse.Content.ReadAsStringAsync().Result).ToString()),
            };

            return result;
        }
       

        private async Task<string> GetAddressBalance(string address)
        {
            HttpResponseMessage response = await client.GetAsync($"http://explorer.ccbcoin.club/ext/getbalance/{address}");
            var result = response.Content.ReadAsStringAsync();

            return result.Result;
        }
    }
}

