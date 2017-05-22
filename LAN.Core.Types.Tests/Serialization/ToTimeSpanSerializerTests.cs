using System;
using System.Collections.Generic;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;
using MongoDB.Bson;
using NUnit.Framework;

namespace LAN.Core.Types.Tests.Serialization
{
    public static class ToTimeSpanSerializerTests
    {
        #region Value Object Implementation

        public class TimeSpanValueObj : IConvertible<TimeSpan>
        {
            #region Equality

            private sealed class SourceValueEqualityComparer : IEqualityComparer<TimeSpanValueObj>
            {
                public bool Equals(TimeSpanValueObj x, TimeSpanValueObj y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (ReferenceEquals(x, null)) return false;
                    if (ReferenceEquals(y, null)) return false;
                    if (x.GetType() != y.GetType()) return false;
                    return x._sourceValue.Equals(y._sourceValue);
                }

                public int GetHashCode(TimeSpanValueObj obj)
                {
                    return obj._sourceValue.GetHashCode();
                }
            }

            private static readonly IEqualityComparer<TimeSpanValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

            public static IEqualityComparer<TimeSpanValueObj> SourceValueComparer
            {
                get { return SourceValueComparerInstance; }
            }

            protected bool Equals(TimeSpanValueObj other)
            {
                return _sourceValue.Equals(other._sourceValue);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TimeSpanValueObj)obj);
            }

            public override int GetHashCode()
            {
                return _sourceValue.GetHashCode();
            }

            #endregion

            private readonly TimeSpan _sourceValue;

            public TimeSpanValueObj(TimeSpan sourceValue)
            {
                _sourceValue = sourceValue;
            }

            public TimeSpan ToValueType()
            {
                return _sourceValue;
            }
        }

        public class TimeSpanValueObjJsonSerializer : ToTimeSpanJsonSerializer<TimeSpanValueObj>
        {
            public override TimeSpanValueObj CreateObjectFromTimeSpan(TimeSpan serializedObj)
            {
                return new TimeSpanValueObj(serializedObj);
            }

            public override TimeSpan CreateTimeSpanFromObject(TimeSpanValueObj obj)
            {
                return obj.ToValueType();
            }
        }

        public class TimeSpanValueObjBsonSerializer : ToTimeSpanBsonSerializer<TimeSpanValueObj>
        {
            public override TimeSpanValueObj CreateObjectFromTimeSpan(TimeSpan serializedObj)
            {
                return new TimeSpanValueObj(serializedObj);
            }

            public override TimeSpan CreateTimeSpanFromObject(TimeSpanValueObj obj)
            {
                return obj.ToValueType();
            }
        }

        #endregion

        public class BsonDeserializeTimeSpanTests : BsonDeserializeContext<TimeSpanValueObj, TimeSpan>
        {
            private TimeSpan expectedValue = TimeSpan.FromMinutes(20);

            protected override string GetSerializedValue()
            {
                return "NumberLong(" + expectedValue.Ticks + ")";
            }

            protected override TimeSpanValueObj GetExpectedValue()
            {
                return new TimeSpanValueObj(expectedValue);
            }
        }

        public class BsonSerializeTimeSpanTests : SerializerContextBase
        {
            protected TimeSpanValueObj ValueObj;
            protected byte[] TypedBson;
            protected byte[] UntypedBson;

            protected override void Given()
            {
                base.Given();

                ValueObj = new TimeSpanValueObj(TimeSpan.FromMinutes(20));
            }

            protected override void When()
            {
                var typedObj = new ValueObjWrapper<TimeSpanValueObj>() { Value = ValueObj, Value2 = ValueObj, Value3 = ValueObj };
                TypedBson = typedObj.ToBson();

                var untypedObj = new { Value = ValueObj.ToValueType().Ticks, Value2 = ValueObj.ToValueType().Ticks, Value3 = ValueObj.ToValueType().Ticks };
                UntypedBson = untypedObj.ToBson();

                base.When();
            }

            [Test]
            public void ObjectIsSerializedAsValue()
            {
                Assert.That(TypedBson, Is.EqualTo(UntypedBson));
            }
        }

        public class JsonDeserializeTimeSpanTests : JsonDeserializeContext<TimeSpanValueObj, TimeSpan>
        {
            private TimeSpan expectedValue = TimeSpan.FromMinutes(20);

            protected override string GetSerializedValue()
            {
                return "\"" + expectedValue + "\"";
            }

            protected override TimeSpanValueObj GetExpectedValue()
            {
                return new TimeSpanValueObj(expectedValue);
            }
        }

        public class JsonSerializeTimeSpanTests : JsonSerializeContext<TimeSpanValueObj, TimeSpan>
        {
            protected override TimeSpanValueObj GetObjectToSerialize()
            {
                return new TimeSpanValueObj(TimeSpan.FromMinutes(20));
            }
        }
    }
}
