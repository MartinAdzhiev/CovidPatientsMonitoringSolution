using Newtonsoft.Json;

namespace Utilities
{
    public static class JsonDeserializer
    {
        public static T? Deserialize<T>(string jsonString)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                return JsonConvert.DeserializeObject<T>(jsonString, settings);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.Message}");
                return default;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.Message}");
                return default;
            }
        }

    }
}
