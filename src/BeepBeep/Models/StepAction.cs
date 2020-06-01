using Newtonsoft.Json;

namespace BeepBeep.Models
{
    public class StepAction
    {
        [JsonProperty("frequency", NullValueHandling = NullValueHandling.Ignore)]
        public int? Frequency { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }
    }
}