using MongoDB.Driver;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFSignup
{
    internal class Program
    {
        public static List<RaidZone> RaidZones;
        public static List<Event> Events = new List<Event>();
        public static List<Raider> Raiders = new List<Raider>();

        private static async Task LoadData()
        {
            RaidZones = await Blizzard.GetRaidZones();
          
           
            Events = await MongoDatabase.GetEventsAsync();
        }

        private static void Main(string[] args)
        {
            Settings.Load();
            var url = "http://127.0.0.1:9000";
            MongoDatabase.Connect();

            Task[] tasks = {
              LoadData(),
              /*
            MongoDatabase.AddRaiderAsync(new Raider() { Name = "Iluin" }),
            MongoDatabase.AddRaiderAsync(new Raider() { Name = "Tiasana" }),
            MongoDatabase.AddRaiderAsync(new Raider() { Name = "Trebon" }),
            MongoDatabase.AddRaiderAsync(new Raider() { Name = "Melissea" }),
            MongoDatabase.AddRaiderAsync(new Raider() { Name = "Aelinna" }),
            MongoDatabase.AddRaiderAsync(new Raider() { Name = "Mournhardt" }),
            */
            Task.Run(async()=>{Raiders = await MongoDatabase.GetRaidersAsync(); })
        };
            Task.WaitAll(tasks);
            var @event = new Event() { Name = "My Test Raid", Location = RaidZones[0], StartTime = DateTime.Now - new TimeSpan(30, 0, 0, 0, 0), EndTime = DateTime.Now + new TimeSpan(1, 0, 0) };
            foreach(var raider in Raiders)
                @event.DPS.Add(raider);
            MongoDatabase.AddEventAsync(@event).Wait();
            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();

                Console.WriteLine("Nancy Server listening on {0}", url);
                Console.ReadLine();
            }
        }
    }
}