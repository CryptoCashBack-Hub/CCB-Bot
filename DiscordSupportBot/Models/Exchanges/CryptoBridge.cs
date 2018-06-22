using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DiscordSupportBot.Models.Exchanges
{
    
    public class CryptoBridge : ResponseBase
    {
        [JsonProperty("data")]
        public List<Coins>Coin { get; set; }
    }

        public class Coins
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("last")]
            public string LastPrice { get; set; }

            [JsonProperty("volume")]
            public string BTCVolume { get; set; }

            [JsonProperty("ask")]
            public string AskPrice { get; set; }

            [JsonProperty("bid")]
            public string BidPrice { get; set; }

        }
      
}