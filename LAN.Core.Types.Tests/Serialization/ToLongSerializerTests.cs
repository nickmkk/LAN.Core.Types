using System.Collections.Generic;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;

namespace LAN.Core.Types.Tests.Serialization
{
    public abstract class ToLongSerializerTests
    {
        #region Value Object Implementation

        public class LongValueObj : IConvertible<long>
        {

            #region Equality

            private sealed class SourceValueEqualityComparer : IEqualityComparer<LongValueObj>
            {
                public bool Equals(LongValueObj x, LongValueObj y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (ReferenceEquals(x, null)) return false;
                    if (ReferenceEquals(y, null)) return false;
                    if (x.GetType() != y.GetType()) return false;
                    return x._sourceValue == y._sourceValue;
                }

                public int GetHashCode(LongValueObj obj)
                {
                    return obj._sourceValue.GetHashCode();
                }
            }

            private static readonly IEqualityComparer<LongValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

            public static IEqualityComparer<LongValueObj> SourceValueComparer
            {
                get { return SourceValueComparerInstance; }
            }

            protected bool Equals(LongValueObj other)
            {
                return _sourceValue == other._sourceValue;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((LongValueObj)obj);
            }

            public override int GetHashCode()
            {
                return _sourceValue.GetHashCode();
            }

            #endregion

            private readonly long _sourceValue;

            public LongValueObj(long sourceValue)
            {
                _sourceValue = sourceValue;
            }

            public long ToValueType()
            {
                return _sourceValue;
            }
        }

        public class LongValueObjJsonSerializer : ToLongJsonSerializer<LongValueObj>
        {
            public override LongValueObj CreateObjectFromLong(long serializedObj)
            {
                return new LongValueObj(serializedObj);
            }

            public override long CreateLongFromObject(LongValueObj obj)
            {
                return obj.ToValueType();
            }
        }

        public class LongValueObjBsonSerializer : ToLongBsonSerializer<LongValueObj>
        {
            public override LongValueObj CreateObjectFromLong(long serializedObj)
            {
                return new LongValueObj(serializedObj);
            }

            public override long CreateLongFromObject(LongValueObj obj)
            {
                return obj.ToValueType();
            }
        }

        #endregion

        public class BsonDeserializeLongTests : BsonDeserializeContext<LongValueObj, long>
        {
            protected override string GetSerializedValue()
            {
                return "1";
            }

            protected override LongValueObj GetExpectedValue()
            {
                return new LongValueObj(1);
            }
        }

        public class BsonSerializeLongTests : BsonSerializeContext<LongValueObj, long>
        {
            protected override LongValueObj GetObjectToSerialize()
            {
                return new LongValueObj(1);
            }
        }

        public class JsonDeserializeIntTests : JsonDeserializeContext<LongValueObj, long>
        {
            protected override string GetSerializedValue()
            {
                return "1";
            }

            protected override LongValueObj GetExpectedValue()
            {
                return new LongValueObj(1);
            }
        }

        public class JsonSerializeIntTests : JsonSerializeContext<LongValueObj, long>
        {
            protected override LongValueObj GetObjectToSerialize()
            {
                return new LongValueObj(1);
            }
        }
    }
}
