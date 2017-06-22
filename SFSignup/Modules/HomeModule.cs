using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFSignup.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            After.AddItemToEndOfPipeline((ctx) => ctx.Response
            .WithHeader("Access-Control-Allow-Origin", "*")
            .WithHeader("Access-Control-Allow-Methods", "POST,GET")
            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
           
            this.Get["/events"] = _ => JsonConvert.SerializeObject(MongoDatabase.GetEvents());
            this.Get["/raiders"] = _ => JsonConvert.SerializeObject(MongoDatabase.GetRaiders());
            
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
