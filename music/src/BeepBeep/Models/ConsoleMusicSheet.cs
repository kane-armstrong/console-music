using BeepBeep.Serialization;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace BeepBeep.Models
{
    public class ConsoleMusicSheet
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("steps")]
        public Step[] Steps { get; set; }

        public void Play()
        {
            foreach (var step in Steps)
            {
                switch (step.Type)
                {
                    case StepType.Beep:
                        var frequency = step.Action.Frequency ?? throw new ArgumentException(nameof(Steps));
                        Console.Beep(frequency, step.Action.Duration);
                        break;

                    case StepType.Pause:
                        Thread.Sleep(step.Action.Duration);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static ConsoleMusicSheet FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ConsoleMusicSheet>(json, Converter.Settings);
        }
    }
}