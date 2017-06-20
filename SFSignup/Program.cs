using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;

namespace SFSignup
{
    internal class Program
    {

        public static List<RaidZone> RaidZones;
        private static void Main(string[] args)
        {
            //Blizzard.GetRaidZones().ContinueWith((t) => { RaidZones = t.Result; });

            Settings.Load();
            var url = "http://127.0.0.1:9000";
            MongoDatabase.Connect();

            //MongoDatabase.AddRaiderAsync(new Raider() { Name = "Iluin" }).Wait();
            //MongoDatabase.AddEventAsync(new Event() { Name = "Iluin",Location=RaidZones[0],StartTime=DateTime.Now,EndTime=DateTime.Now + new TimeSpan(1,0,0) }).Wait();

            //var eventsTask = MongoDatabase.GetEventsAsync();
            //eventsTask.Wait();

            //var events = eventsTask.Result;
            MongoDatabase.GetRaidersAsync().ContinueWith((t) =>
            {
                foreach (var result in t.Result)
                    Console.WriteLine(result.Name);
            });
            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();

                Console.WriteLine("Nancy Server listening on {0}", url);
                Console.ReadLine();
            }
        }
    }
}