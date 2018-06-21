namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
 //   using DiscordSupportBot.Models.General;
    using DiscordSupportBot.Common;
    using DiscordSupportBot.Models.Exchanges;
    using DiscordSupportBot.Models.Github;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        private static HttpClient client = new HttpClient();

        [Command("help")]
        public async Task Help()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("CCB Bot Help")
                .WithColor(Discord.Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/CCB.png")
                .WithFooter("https://github.com/CryptoCashBack-Hub")

                .AddField("//help", "shows available commands")
                .AddField("//guides or //guide", "replies with current installation guides")
                .AddField("//price <ticker> or //checkprice <ticker>", "replies with cmc price")
                .AddField("//build or //version", "replies with current wallet realse link");


            var isBotChannel = this.Context.Channel.Id.Equals(DiscordData.BotChannel);
            await this.ReplyAsync(string.Empty, false, builder.Build());
        }

        [Command("guide")]
        [Alias("guides")]
        public async Task guide()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("Master List of Guides").WithColor(Discord.Color.Blue)
                .WithDescription("\u200b")
                .WithUrl("https://github.com/CryptoCashBack-Hub/CCB_Guides")
                .WithThumbnailUrl("https://masternodes.online/coin_image/CCB.png")

                .AddField("The current wallet download", "https://github.com/CryptoCashBack-Hub/CCB/releases")
                .AddField("Complete install script for vps", "https://github.com/CryptoCashBack-Hub/CCB_Sripts")
                .AddField("Configuration Seed List", "https://github.com/CryptoCashBack-Hub/CCB_Guides/tree/master/Seed_List");

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordSupportBot.Common.DiscordData.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
        }

        /*
        [Command("donate")]
        [Alias("donations")]
        public async Task Donation()
        {
            var builder = new EmbedBuilder();

            var dataBtc = this.GetBtcDonationAddressBalance("1592K4xS5QkXDStELPk9nHBEqZ5vLNAyrm");
            var dataIps = this.GetIpsDonationAddressBalance("iSv6vXhSbb7WH8D3dVHuWecZ7pGj4AJMmt");

            builder.WithTitle("")
                .WithColor(Discord.Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/CCB.png")
                .AddField("Donations will be used for:", "Exchange Listings, Development, and Infrastructure")
                .AddField("IPS Donation Address:", "iSv6vXhSbb7WH8D3dVHuWecZ7pGj4AJMmt")
                .AddField("BTC Donation Address:", "1592K4xS5QkXDStELPk9nHBEqZ5vLNAyrm")
                .AddField("\u200b", "\u200b")
                .AddField("Current BTC donation balance:", $"{dataBtc.Result} BTC")
                .AddField("Current IPS donation balance:", $"{dataIps.Result} IPS");

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordSupportBot.Common.DiscordData.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
        }
        */
        [Command("build")]
        [Alias("version")]
        public async Task CurrentBuild()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("The current build is on v1.0.0.1").WithColor(Discord.Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/CCB.png")
                .WithDescription("\u200b")

                .AddField("Current wallet build!", "https://github.com/CryptoCashBack/CryptoCashBack/releases/");

            await this.ReplyAsync(string.Empty, false, builder.Build());
        }
        
<<<<<<< HEAD
        

=======
>>>>>>> 03256413cd84315d0fa06faeff6513bf90c8779d
        private async Task<GithubRelease> GetGithubReleaseData()
        {
            client.DefaultRequestHeaders.Add("User-Agent", "request");

            var response = await client.GetStringAsync($"https://github.com/CryptoCashBack/CryptoCashBack/releases/");
            var result = JsonConvert.DeserializeObject<GithubRelease>(response.ToString());

            return result;
        }

        private async Task<decimal> GetBtcDonationAddressBalance(string address)
        {
            var response = await client.GetStringAsync($"https://blockchain.info/q/addressbalance/{address}");

            return decimal.Parse(response, System.Globalization.NumberStyles.AllowDecimalPoint) / 100000000;
        }

        private async Task<decimal> GetIpsDonationAddressBalance(string address)
        {
            var response = await client.GetStringAsync($"https://explorer.ipsum.network/ext/getbalance/{address}");

            return decimal.Parse(response, System.Globalization.NumberStyles.AllowDecimalPoint);
        }
    }
}
