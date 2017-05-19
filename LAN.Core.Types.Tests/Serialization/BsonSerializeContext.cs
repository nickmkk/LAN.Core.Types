using System;
using MongoDB.Bson;
using NUnit.Framework;

namespace LAN.Core.Types.Tests.Serialization
{

    public abstract class BsonSerializeContext<TObject, TValue> : SerializerContextBase where TObject : IConvertible<TValue>
    {
        protected TObject ValueObj;
        protected byte[] TypedBson;
        protected byte[] UntypedBson;

        protected abstract TObject GetObjectToSerialize();

        protected override void Given()
        {
            base.Given();
            ValueObj = GetObjectToSerialize();
        }

        protected override void When()
        {
            var typedObj = new ValueObjWrapper<TObject>() { Value = ValueObj, Value2 = ValueObj, Value3 = ValueObj };
            TypedBson = typedObj.ToBson();
            
            var untypedObj = new { Value = (TValue)ValueObj.ToValueType(), Value2 = (TValue)ValueObj.ToValueType(), Value3 = (TValue)ValueObj.ToValueType() };
            UntypedBson = untypedObj.ToBson();
            
            base.When();
        }

        [Test]
        public void ObjectIsSerializedAsValue()
        {
            Assert.That(TypedBson, Is.EqualTo(UntypedBson));
        }
    }
}
