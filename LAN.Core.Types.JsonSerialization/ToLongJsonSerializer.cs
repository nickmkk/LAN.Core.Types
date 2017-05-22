using System;
using Newtonsoft.Json;

namespace LAN.Core.Types.JsonSerialization
{
    public abstract class ToLongJsonSerializer<T> : JsonConverter where T : class
    {
        private static readonly Type UType = typeof(T);

        public abstract T CreateObjectFromLong(long serializedObj);
        public abstract long CreateLongFromObject(T obj);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is T))
            {
                throw new NotSupportedException("Expected an object of type " + UType.Name + " but received an object of type " + value.GetType().Name + ".");
            }
            serializer.Serialize(writer, CreateLongFromObject((T)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value == null ? default(long) : serializer.Deserialize<long>(reader);
            return CreateObjectFromLong(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return UType.IsAssignableFrom(objectType);
        }
    }
}
