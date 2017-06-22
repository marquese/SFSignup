using System;
using SFSignup.WoW;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace SFSignup
{
    public class Raider
    {
        [BsonId]
        public ObjectId _id;

        public string Name { get; set; }

        private PlayerClass _Class = PlayerClass.Unknown;

        public PlayerClass Class
            {
            get
            {
                if (_Class == PlayerClass.Unknown)
                    _Class = Blizzard.GetPlayer(Name)["class"].ToObject<PlayerClass>();
                return _Class;
            }
            set
            {
                _Class = value;
            }
            }

        private string key;

        [JsonIgnore]
        
        public string Key
        {
            get
            {
                if (key == null)
                    GenerateAPIKey();
                return key;
            }
            internal set
            {
                key = value;
            }
        }

        public void GenerateAPIKey()
        {
            var guid = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("B"));
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
               
                cryptoProvider.GetBytes(guid);
                key = Convert.ToBase64String(guid);
            }

        }
    }
}