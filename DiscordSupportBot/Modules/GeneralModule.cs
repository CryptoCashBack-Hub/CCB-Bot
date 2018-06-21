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
        private string[] VoteOptions = { "1⃣", "2⃣", "3⃣", "4⃣", "5⃣", "6⃣", "7⃣", "8⃣", "9⃣", "🔟" };

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

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordData.BotChannel);
            await this.ReplyAsync(string.Empty, false, builder.Build());
        }

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

        [Command("createpoll")]
        [Alias("poll")]
        public async Task Poll(string question, params string[] options)
        {
            var user = this.Context.Message.Author as SocketGuildUser;
            var permissiveRole = user.Roles.FirstOrDefault(r => r.Name.Equals("DEV Team") || r.Name.Equals("Admin") || r.Name.Equals("Moderator"));

            if (permissiveRole != null)
            {
                var builder = new EmbedBuilder();
                var optionsList = this.GetVoteOptions(options);

                builder.WithTitle($"{question.Trim('?')}?")
                    .WithDescription(optionsList)
                    .WithColor(Discord.Color.Blue);

                var message = await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotPollChannel)
                    .SendMessageAsync(string.Empty, false, builder.Build());

                for (int i = 0; i < options.Length; i++)
                {
                    await message.AddReactionAsync(new Emoji(this.VoteOptions[i]));
                }
            }
            else
            {
                await this.Context.Message.Author.SendMessageAsync($"not enough permissions for the usage of poll command!");
            }
        }


        private string GetVoteOptions(string[] options)
        {
            var result = string.Empty;

            for (int i = 0; i < options.Length; i++)
            {
                result += $"\n{this.VoteOptions[i]} - {options[i]}";
            }

            return result;
        }

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

        private async Task<decimal> GetCCBDonationAddressBalance(string address)
        {
            var response = await client.GetStringAsync($"https://explorer.ccbcoin.club/ext/getbalance/{address}");

            return decimal.Parse(response, System.Globalization.NumberStyles.AllowDecimalPoint);
        }
    }
}
