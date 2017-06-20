using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace SFSignup
{
    public sealed class Settings
    {
        private static string DefaultSettingsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SFKeys.yaml");

        public static void Load()
        {
            if (!File.Exists(DefaultSettingsPath))
                throw new FileNotFoundException("The Keys file cannot be found in " + DefaultSettingsPath);

            var deserializer = new Deserializer();
            var yamlObject = deserializer.Deserialize<Dictionary<string,string>>(File.ReadAllText(DefaultSettingsPath));

            Mongo.URL = yamlObject["mongodb_url"];
            Blizzard.APIKey = yamlObject["blizzard_api_key"];
        }


        public sealed class Mongo
        {
            public static string URL { get; internal set; } 
            public static string DatabaseName => "sf";
            public sealed class Collections
            {
                public static string Raiders => "raiders";
                public static string Events => "events";
                public static string Raids => "raids";
            }
        }

        public sealed class Blizzard
        {
            public static string APIKey { get; internal set; }
        }
        

    }
}
