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
            

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("test");
            await this.Context.Message.Author.SendMessageAsync(string.Empty, false, builder.Build());
        }


        private async Task<string> GetAddressBalance(string address)
        {
            HttpResponseMessage response = await client.GetAsync($"http://explorer.ccb.cash/ext/getbalance/{address}");
            var result = response.Content.ReadAsStringAsync();

            return result.Result;
        }


        }
    }

