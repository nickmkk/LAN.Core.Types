using Newtonsoft.Json;
using System;

namespace LAN.Core.Types.JsonSerialization
{
    public abstract class ToTimeSpanJsonSerializer<T> : JsonConverter where T : class
    {
        private static readonly Type UType = typeof(T);

        public abstract T CreateObjectFromTimeSpan(TimeSpan serializedObj);
        public abstract TimeSpan CreateTimeSpanFromObject(T obj);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is T))
            {
                throw new NotSupportedException("Expected an object of type " + UType.Name + " but received an object of type " + value.GetType().Name + ".");
            }
            serializer.Serialize(writer, CreateTimeSpanFromObject((T)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value == null ? default(TimeSpan) : serializer.Deserialize<TimeSpan>(reader);
            return CreateObjectFromTimeSpan(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return UType.IsAssignableFrom(objectType);
        }
    }
}
