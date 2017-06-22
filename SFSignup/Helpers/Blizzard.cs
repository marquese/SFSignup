using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SFSignup
{
    public sealed class Blizzard
    {
        public static async Task<List<RaidZone>> GetRaidZones()
        {
            var datastring = await RequestAsync("zone");
            var data = from x in JObject.Parse(datastring)["zones"]
                       where x["isRaid"].ToObject<bool>() == true
                       select x;

            // Get only the most recent raid zones
            var highestExpansionID = (from x in data
                                      group x by x["expansionId"].ToObject<int>() into g
                                      select g.Key).OrderBy(y => y).Last();

            var outputData = (from x in data
                              where x["expansionId"].ToObject<int>() == highestExpansionID
                              select x.ToObject<RaidZone>()).ToList();

            return outputData;
        }

        internal static JObject GetPlayer(string name)
        {
            return JObject.Parse(Request("character/argent%20dawn/" + name));
        }

        private static string Request(string url)
        {
            var u = "https://eu.api.battle.net/wow/" + url + "?locale=en_GB&apikey=" + Settings.Blizzard.Key;
            return new WebClient().DownloadString(u);
        }

        private static async Task<string> RequestAsync(string url)
        {
            var u = "https://eu.api.battle.net/wow/" + url + "/?locale=en_GB&apikey=" + Settings.Blizzard.Key;
            var result = await new WebClient().DownloadStringTaskAsync(new Uri(u));
            return result;
        }
    }
}