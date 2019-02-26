using BeepBeep.Models;
using Newtonsoft.Json;

namespace BeepBeep.Serialization
{
    public static class ConsoleMusicSheetExtensions
    {
        public static string ToJson(this ConsoleMusicSheet self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}