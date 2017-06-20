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
            this.Get["/"] = _ => new HomeModel("Hello world");
        }

        private class HomeModel
        {
            public HomeModel(string text)
            {
                this.Text = text;
            }

            public string Text { get; }

            public string Title => "Home Page";
        }
    }
}
