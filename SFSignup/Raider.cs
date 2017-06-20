using System;
using SFSignup.WoW;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;

namespace SFSignup
{
    public class Raider
    {
        public string Name;
        public PlayerClass Class;

        private string key;

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


        public ObjectId _id { get; internal set; }


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