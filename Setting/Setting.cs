using System.IO;
using Newtonsoft.Json;

namespace tarkov_settings.Setting
{
    internal class Settings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "settings.json";

        public void Save(string fileName = DEFAULT_FILENAME) {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            if (File.Exists(fileName))
            {
                // ObjectCreationHandling.Replace ensures collection fields are replaced
                // rather than merged into their default-initialized values.
                var settings = new JsonSerializerSettings { ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace };
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName), settings);
            }
            return new T();
        }
    }
}
