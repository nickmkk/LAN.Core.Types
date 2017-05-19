using Newtonsoft.Json;
using System;

namespace LAN.Core.Types.JsonSerialization
{
	public abstract class ToTimeSpanJsonSerializer<T> : JsonConverter where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<TimeSpan>);
		private static readonly Type UType = typeof(T);

		public abstract T CreateObjectFromTimeSpan(TimeSpan serializedObj);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!_convertibleType.IsInstanceOfType(value))
			{
				throw new NotSupportedException("The type " + value.GetType().Name + " must implement the " + _convertibleType.Name + " interface.");
			}
			var typedObj = (IConvertible<TimeSpan>)value;
			serializer.Serialize(writer, typedObj.ToValueType());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = reader.Value == null ? default(TimeSpan) : serializer.Deserialize<TimeSpan>(reader);
			return this.CreateObjectFromTimeSpan(value);
		}

		public override bool CanConvert(Type objectType)
		{
			return UType.IsAssignableFrom(objectType);
		}
	}
}
