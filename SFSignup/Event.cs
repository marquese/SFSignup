using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SFSignup
{
    public class Event
    {
        public Event()
        {
        }

        [BsonId]
        [JsonIgnore]
        public ObjectId ID { get; set; }

        public string Name { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public RaidZone Location { get; set; }

        public int LocationID
        {
            get
            {
                return Location.ID;
            }
            set
            {
                Location = Program.RaidZones.First(x => x.ID == value);
            }
        }

        [BsonSerializer(typeof(RaiderListSerializer))]
        public List<Raider> DPS = new List<Raider>();
        [BsonSerializer(typeof(RaiderListSerializer))]
        public List<Raider> Healers = new List<Raider>();
        [BsonSerializer(typeof(RaiderListSerializer))]
        public List<Raider> Tanks = new List<Raider>();
        [BsonSerializer(typeof(RaiderListSerializer))]
        public List<Raider> Unavailable = new List<Raider>();

        [BsonIgnore]
        [JsonIgnore]
        public DateTime StartTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(Start).DateTime;
            }
            set
            {
                Start = new DateTimeOffset(value.ToUniversalTime()).ToUnixTimeMilliseconds();
            }
        }
        [BsonIgnore]
        [JsonIgnore]
        public DateTime EndTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(End).DateTime;
            }
            set
            {
                End = new DateTimeOffset(value.ToUniversalTime()).ToUnixTimeMilliseconds();
            }
        }

        private string logUrl;

        public string ReportID
        {
            get
            {
                if (logUrl == null)
                {
                    var url = @"https://www.warcraftlogs.com:443/v1/reports/guild/Seventh%20Flight/Argent-Dawn/EU?start=" + Start.ToString() + "&end=" + End.ToString() + "&api_key=" + Settings.WarcraftLogs.Key;
                    var dataString = new WebClient().DownloadString(url);
                    var data = JArray.Parse(dataString);
                    if (data.Count == 0)
                        logUrl = null;
                    else
                    {
                        logUrl = data.Last["id"].ToString();
                        SaveToDatabase();
                    }
                }
                return logUrl;
            }
            set
            {
                logUrl = value;
            }
        }

        public void SaveToDatabase()
        {
            MongoDatabase.UpdateEvent(this);
        }

        public long Start { get; set; }
        public long End { get; set; }
    }
}