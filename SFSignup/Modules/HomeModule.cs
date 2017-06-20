using Nancy;
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
            

            this.Get["/"] = _ => new HomeModel();
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
