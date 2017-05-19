using Newtonsoft.Json;
using NUnit.Framework;

namespace LAN.Core.Types.Tests.Serialization
{

	public abstract class JsonSerializeContext<TObject, TValue> : SerializerContextBase where TObject : IConvertible<TValue>
	{
		protected TObject ValueObj;
		protected string TypedJson;
		protected string UntypedJson;

		protected abstract TObject GetObjectToSerialize();

		protected override void Given()
		{
			base.Given();
			ValueObj = GetObjectToSerialize();
		}

		protected override void When()
		{
			var typedObj = new ValueObjWrapper<TObject>() { Value = ValueObj, Value2 = ValueObj, Value3 = ValueObj };
			TypedJson = JsonConvert.SerializeObject(typedObj);

			var untypedObj = new { Value = ValueObj.ToValueType(), Value2 = ValueObj.ToValueType(), Value3 = ValueObj.ToValueType() };
			UntypedJson = JsonConvert.SerializeObject(untypedObj);

			base.When();
		}

		[Test]
		public void ObjectIsSerializedAsValue()
		{
			Assert.That(TypedJson, Is.EqualTo(UntypedJson));
		}
	}
}
