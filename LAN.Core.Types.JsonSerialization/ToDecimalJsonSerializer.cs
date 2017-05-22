using System;
using Newtonsoft.Json;

namespace LAN.Core.Types.JsonSerialization
{
    public abstract class ToDecimalJsonSerializer<T> : JsonConverter where T : class
    {
        private static readonly Type UType = typeof(T);

        public abstract T CreateObjectFromDecimal(decimal serializedObj);
        public abstract decimal CreateDecimalFromObject(T obj);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is T))
            {
                throw new NotSupportedException("Expected an object of type " + UType.Name + " but received an object of type " + value.GetType().Name + ".");
            }
            serializer.Serialize(writer, CreateDecimalFromObject((T)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value == null ? default(decimal) : serializer.Deserialize<decimal>(reader);
            return CreateObjectFromDecimal(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return UType.IsAssignableFrom(objectType);
        }
    }
}
