using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace SFSignup
{
    internal class RaiderListSerializer : SerializerBase<List<Raider>>
    {
        public override void Serialize(MongoDB.Bson.Serialization.BsonSerializationContext context, MongoDB.Bson.Serialization.BsonSerializationArgs args, List<Raider> value)
        {
            context.Writer.WriteStartArray();
            foreach (Raider mvnt in value)
            {
                new MongoDBRefSerializer().Serialize(context, args, (new MongoDBRef(Settings.Mongo.Collections.Raiders, mvnt._id)));
            }
            context.Writer.WriteEndArray();

        }

        public override List<Raider> Deserialize(MongoDB.Bson.Serialization.BsonDeserializationContext context, MongoDB.Bson.Serialization.BsonDeserializationArgs args)
        {
            context.Reader.ReadStartArray();

            List<Raider> result = new List<Raider>();
    
            while (true)
            {
                try
                {
                    var referance = new MongoDBRefSerializer().Deserialize(context, args);
                    result.Add(MongoDatabase.FindUserByID(referance.Id.AsObjectId));
                }
                catch
                {
                    break;
                }
              
            }
            context.Reader.ReadEndArray();
            return result;
        }
    }
}