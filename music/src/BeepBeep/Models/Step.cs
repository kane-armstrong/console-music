using Newtonsoft.Json;

namespace BeepBeep.Models
{
    public class Step
    {
        [JsonProperty("type")]
        public StepType Type { get; set; }

        [JsonProperty("action")]
        public StepAction Action { get; set; }
    }
}