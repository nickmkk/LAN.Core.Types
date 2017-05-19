using System;
using Newtonsoft.Json;

namespace LAN.Core.Types.JsonSerialization
{
	public abstract class ToBooleanJsonSerializer<T> : JsonConverter where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<bool>);
		private static readonly Type UType = typeof(T);

		public abstract T CreateObjectFromBoolean(bool serializedObj);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!_convertibleType.IsInstanceOfType(value))
			{
				throw new NotSupportedException("The type " + value.GetType().Name + " must implement the " + _convertibleType.Name + " interface.");
			}
			var typedObj = (IConvertible<bool>)value;
			serializer.Serialize(writer, typedObj.ToValueType());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var boolValue = reader.Value == null ? default(bool) : serializer.Deserialize<bool>(reader);
			return this.CreateObjectFromBoolean(boolValue);
		}

		public override bool CanConvert(Type objectType)
		{
			return UType.IsAssignableFrom(objectType);
		}
	}
}
