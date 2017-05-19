using System;
using Newtonsoft.Json;

namespace LAN.Core.Types.JsonSerialization
{
	public abstract class ToDoubleJsonSerializer<T> : JsonConverter where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<double>);
		private static readonly Type UType = typeof(T);

		public abstract T CreateObjectFromDouble(double serializedObj);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!_convertibleType.IsInstanceOfType(value))
			{
				throw new NotSupportedException("The type " + value.GetType().Name + " must implement the " + _convertibleType.Name + " interface.");
			}
			var typedObj = (IConvertible<double>)value;
			serializer.Serialize(writer, typedObj.ToValueType());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = reader.Value == null ? default(double) : serializer.Deserialize<double>(reader);
			return this.CreateObjectFromDouble(value);
		}

		public override bool CanConvert(Type objectType)
		{
			return UType.IsAssignableFrom(objectType);
		}
	}
}
