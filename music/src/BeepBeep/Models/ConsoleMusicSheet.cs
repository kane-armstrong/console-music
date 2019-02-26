using BeepBeep.Serialization;
using Newtonsoft.Json;

namespace BeepBeep.Models
{
    public class ConsoleMusicSheet
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("steps")]
        public Step[] Steps { get; set; }

        public static ConsoleMusicSheet FromJson(string json) => JsonConvert.DeserializeObject<ConsoleMusicSheet>(json, Converter.Settings);
    }
}