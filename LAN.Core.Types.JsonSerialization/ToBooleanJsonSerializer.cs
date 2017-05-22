using System;
using Newtonsoft.Json;

namespace LAN.Core.Types.JsonSerialization
{
    public abstract class ToBooleanJsonSerializer<T> : JsonConverter where T : class
    {
        private static readonly Type UType = typeof(T);

        public abstract T CreateObjectFromBoolean(bool serializedObj);
        public abstract bool CreateBooleanFromObject(T obj);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is T))
            {
                throw new NotSupportedException("Expected an object of type " + UType.Name + " but received an object of type " + value.GetType().Name + ".");
            }
            serializer.Serialize(writer, CreateBooleanFromObject((T)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var boolValue = reader.Value != null && serializer.Deserialize<bool>(reader);
            return CreateObjectFromBoolean(boolValue);
        }

        public override bool CanConvert(Type objectType)
        {
            return UType.IsAssignableFrom(objectType);
        }
    }
}
