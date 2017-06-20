using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFSignup
{
    public sealed class MongoDatabase
    {
        private static MongoClient Client;
        private static IMongoDatabase Database;

        public static async Task AddRaiderAsync(Raider raider)
        {
            var db = Database.GetCollection<Raider>(Settings.Mongo.Collections.Raiders);
            var existing = await db.FindAsync(new FilterDefinitionBuilder<Raider>().Eq("Name", raider.Name));
            if (existing.Any())
            {
                Console.WriteLine($"Can't add {raider.Name}. A raider with that name already exists");
                return;
            }

            await db.InsertOneAsync(raider);
        }

        internal static async Task AddEventAsync(Event newEvent)
        {
            var db = Database.GetCollection<Event>(Settings.Mongo.Collections.Events);

            await db.InsertOneAsync(newEvent);
        }

        public static async Task<IEnumerable<Event>> GetEventsAsync()
        {
            var result = await Database.GetCollection<Event>(Settings.Mongo.Collections.Events).FindAsync(FilterDefinition<Event>.Empty);
            return result.ToList();
        }

        public static void Connect()
        {
            Client = new MongoClient(Settings.Mongo.URL);
            Database = Client.GetDatabase(Settings.Mongo.DatabaseName);
            Console.WriteLine("Connected to database.");

            return;

            var desiredCollections = typeof(Settings.Mongo.Collections).GetProperties().Select(x => (string)x.GetValue(null));

            
            foreach (var collection in desiredCollections)
            {
                CollectionExistsAsync(collection).ContinueWith(x =>
                {
                    if (!x.Result)
                    {
                        Database.CreateCollectionAsync(collection);
                        Console.WriteLine($"Created collection: {collection}");
                    }
                }).Wait();
            }
        }

        private static async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = await Database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            return await collections.AnyAsync();
        }

        public static async Task<IEnumerable<Raider>> GetRaidersAsync()
        {
            var result = await Database.GetCollection<Raider>(Settings.Mongo.Collections.Raiders).FindAsync(FilterDefinition<Raider>.Empty);
            return result.ToEnumerable();
        }

        public static IEnumerable<Raider> GetRaiders()
        {
            return Database.GetCollection<Raider>(Settings.Mongo.Collections.Raiders).Find(FilterDefinition<Raider>.Empty).ToList();
        }
    }
}