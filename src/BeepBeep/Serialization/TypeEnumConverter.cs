using BeepBeep.Models;
using Newtonsoft.Json;
using System;

namespace BeepBeep.Serialization
{
    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(StepType) || t == typeof(StepType?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "beep":
                    return StepType.Beep;

                case "pause":
                    return StepType.Pause;
            }
            throw new Exception("Cannot unmarshal type StepType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (StepType)untypedValue;
            switch (value)
            {
                case StepType.Beep:
                    serializer.Serialize(writer, "beep");
                    return;

                case StepType.Pause:
                    serializer.Serialize(writer, "pause");
                    return;
            }
            throw new Exception("Cannot marshal type StepType");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}