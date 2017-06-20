using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;

namespace SFSignup
{
    public class Event
    {
        public Event()
        {
        }

        [BsonId]
        public ObjectId ID { get; set; }

        public string Name { get; set; }

        [BsonIgnore]
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

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}