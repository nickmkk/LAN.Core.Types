using System;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LAN.Core.Types.Tests.Serialization
{

	public abstract class JsonDeserializeContext<TObject, TValue> : SerializerContextBase where TObject : IConvertible<TValue>
	{
		protected TObject ExpectedValue;
		protected string SerializedData;
		protected ValueObjWrapper<TObject> DeserializedObj;

		protected abstract string GetSerializedValue();
		protected abstract TObject GetExpectedValue();

		protected override void Given()
		{
			base.Given();
			ExpectedValue = GetExpectedValue();
			SerializedData = "{\"Value\": " + GetSerializedValue() + ", \"Value2\": null}";
		}

		protected override void When()
		{
			DeserializedObj = JsonConvert.DeserializeObject<ValueObjWrapper<TObject>>(SerializedData);
			base.When();
		}

		[Test]
		public void TheValueIsDeserializedAsObject()
		{
			if (DeserializedObj.Value.ToValueType() is DateTime)
			{
				Assert.That(DeserializedObj.Value.ToValueType(), Is.EqualTo(ExpectedValue.ToValueType()).Within(1).Seconds);
			}
			else
			{
				Assert.That(DeserializedObj.Value, Is.EqualTo(ExpectedValue));
			}
			if (DeserializedObj.Value2.ToValueType() is DateTime)
			{
				Assert.That(DeserializedObj.Value2.ToValueType(), Is.EqualTo(DateTime.MinValue).Within(1).Seconds);
			}
			else if (DeserializedObj.Value2.ToValueType() is string)
			{
				Assert.That(DeserializedObj.Value2.ToValueType(), Is.EqualTo(string.Empty));
			}
			else
			{
				Assert.That(DeserializedObj.Value2.ToValueType(), Is.EqualTo(default(TValue)));
			}
			Assert.That(DeserializedObj.Value3, Is.EqualTo(null));
		}
	}
}
