using BeepBeep.Serialization;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace BeepBeep.Models
{
    public class ConsoleMusicSheet
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("steps")]
        public Step[] Steps { get; }

        public ConsoleMusicSheet(string name, Step[] steps)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Steps = steps ?? throw new ArgumentException(nameof(steps));
        }

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