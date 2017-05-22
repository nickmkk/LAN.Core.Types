using System;
using Newtonsoft.Json;

namespace LAN.Core.Types.JsonSerialization
{
    public abstract class ToDoubleJsonSerializer<T> : JsonConverter where T : class
    {
        private static readonly Type UType = typeof(T);

        public abstract T CreateObjectFromDouble(double serializedObj);
        public abstract double CreateDoubleFromObject(T obj);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is T))
            {
                throw new NotSupportedException("Expected an object of type " + UType.Name + " but received an object of type " + value.GetType().Name + ".");
            }
            serializer.Serialize(writer, CreateDoubleFromObject((T)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value == null ? default(double) : serializer.Deserialize<double>(reader);
            return CreateObjectFromDouble(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return UType.IsAssignableFrom(objectType);
        }
    }
}
