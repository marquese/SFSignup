using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SFSignup.Modules
{
    public class HomeModule : NancyModule
    {
        private static string APIKey = "12345"; // Temporary

        public HomeModule()
        {
            After.AddItemToEndOfPipeline((ctx) => ctx.Response
            .WithHeader("Access-Control-Allow-Origin", "*")
            .WithHeader("Access-Control-Allow-Methods", "POST,GET")
            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
           
            this.Get["/events"] = _ => JsonConvert.SerializeObject(MongoDatabase.GetEvents());
            this.Get["/raiders"] = _ => JsonConvert.SerializeObject(MongoDatabase.GetRaiders());
            this.Get["/raiderKey/{raiderName}/{apiKey}"] = args => GetRaiderLoginKey(args);
            this.Get["/register/{raiderName}/{apiKey}",true] = (args,ct) => RegisterNewUser(args);
            this.Get["/logout"] = _ => {
                return ((Response)"Logged Out").WithCookie("sfSignup", "", DateTime.Now);
            };
            this.Get["/login/{apiKey}"] = (args) =>
            {
                if (args.apiKey == null)
                    return "Server Error";
                var key = ConvertHexToString(args.apiKey.Value);
                var raiders = MongoDatabase.GetRaiders();
                var user = raiders.FirstOrDefault(x => x.Key == key);
                if (user == null)
                    return ((Response)"Invalid Key").WithStatusCode(HttpStatusCode.Forbidden);
               
                
                return ((Response)("Logged In! Hello " + user.Name)).WithCookie("sfSignup",user.Key, new DateTime(9999, 1, 1));
            };
            this.Get["/"] = _ => {
                if (!Request.Cookies.ContainsKey("sfSignup"))
                {
                    return ((Response)"You must login to use this site.").WithStatusCode(HttpStatusCode.Forbidden);
                }
                var cookie = Request.Cookies["sfSignup"];
                var raiders = MongoDatabase.GetRaiders();
                var user = raiders.FirstOrDefault(x => x.Key == cookie);
                if (user == null)
                    return ((Response)"User does not exist").WithStatusCode(HttpStatusCode.Forbidden);
                var events = MongoDatabase.GetEvents();
                return $"<h1>Hello {user.Name}</h1>" + JsonConvert.SerializeObject(events);
            };

        }

        private dynamic GetRaiderLoginKey(dynamic args)
        {
            if (args.apiKey == null)
                return "Missing API Key";
            if (args.raiderName == null)
                return "Missing name";
            if (args.apiKey != APIKey)
                return "Invalid API Key";
            var raiders = MongoDatabase.GetRaiders();
            var user = raiders.FirstOrDefault(x => string.Equals(x.Name,args.raiderName,StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
                return "User not found";
            return ConvertStringToHex(user.Key);
        }

        public static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }


        private async Task<object> RegisterNewUser(dynamic args)
        {
            
            if (args.apiKey != APIKey)
                return "API Key not correct";
            var newRaider = new Raider() { Name = args.raiderName };
            await MongoDatabase.AddRaiderAsync(newRaider);
            var resp = (Response)ConvertStringToHex(newRaider.Key);
            
            
            return resp.WithCookie("sfSignup",newRaider.Key,new DateTime(9999,1,1));
        }


        public class HomeModel
        {
            public HomeModel()
            {
                this.Raiders = (List<Raider>)MongoDatabase.GetRaiders();
            }

            public List<Raider> Raiders { get; }
        }
    }
}
