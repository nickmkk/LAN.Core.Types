using MongoDB.Bson.Serialization;
using NUnit.Framework;

namespace LAN.Core.Types.Tests.Serialization
{
	public abstract class BsonDeserializeContext<TObject, TValue> : SerializerContextBase where TObject : IConvertible<TValue>
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
			DeserializedObj = BsonSerializer.Deserialize<ValueObjWrapper<TObject>>(SerializedData);
			base.When();
		}

		[Test]
		public void ValueIsDeserializedAsObject()
		{
			Assert.That(DeserializedObj.Value.ToValueType().ToString(), Is.EqualTo(ExpectedValue.ToValueType().ToString()));
			if (DeserializedObj.Value.ToValueType() is string)
			{
				Assert.That(DeserializedObj.Value2.ToValueType(), Is.EqualTo(string.Empty));
			}
			else
			{
				Assert.That(DeserializedObj.Value2.ToValueType(), Is.EqualTo(default(TValue)));	
			}
			Assert.That(DeserializedObj.Value3, Is.EqualTo(null)); //when the document does not contain the property, the serializer will ignore deserializing it.
		}
	}

}
