using System.Collections.Generic;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;

namespace LAN.Core.Types.Tests.Serialization
{
    public static class ToStringSerializerTests
    {
        #region Value Object Implementation

        public class StringValueObj : IConvertible<string>
        {

            #region Equality

            private sealed class SourceValueEqualityComparer : IEqualityComparer<StringValueObj>
            {
                public bool Equals(StringValueObj x, StringValueObj y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (ReferenceEquals(x, null)) return false;
                    if (ReferenceEquals(y, null)) return false;
                    if (x.GetType() != y.GetType()) return false;
                    return string.Equals(x._sourceValue, y._sourceValue);
                }

                public int GetHashCode(StringValueObj obj)
                {
                    return (obj._sourceValue != null ? obj._sourceValue.GetHashCode() : 0);
                }
            }

            private static readonly IEqualityComparer<StringValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

            public static IEqualityComparer<StringValueObj> SourceValueComparer
            {
                get { return SourceValueComparerInstance; }
            }

            protected bool Equals(StringValueObj other)
            {
                return string.Equals(_sourceValue, other._sourceValue);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((StringValueObj)obj);
            }

            public override int GetHashCode()
            {
                return (_sourceValue != null ? _sourceValue.GetHashCode() : 0);
            }

            #endregion

            private readonly string _sourceValue;

            public StringValueObj(string sourceValue)
            {
                _sourceValue = sourceValue;
            }

            public string ToValueType()
            {
                return _sourceValue;
            }
        }

        public class StringValueObjJsonSerializer : ToStringJsonSerializer<StringValueObj>
        {
            public override StringValueObj CreateObjectFromString(string serializedObj)
            {
                return new StringValueObj(serializedObj);
            }

            public override string CreateStringFromObject(StringValueObj obj)
            {
                return obj.ToValueType();
            }
        }

        public class StringValueObjBsonSerializer : ToStringBsonSerializer<StringValueObj>
        {
            public override StringValueObj CreateObjectFromString(string serializedObj)
            {
                return new StringValueObj(serializedObj);
            }

            public override string CreateStringFromObject(StringValueObj obj)
            {
                return obj.ToValueType();
            }
        }

        #endregion

        public class BsonDeserializeStringTests : BsonDeserializeContext<StringValueObj, string>
        {
            protected override string GetSerializedValue()
            {
                return "\"somestring\"";
            }

            protected override StringValueObj GetExpectedValue()
            {
                return new StringValueObj("somestring");
            }
        }

        public class BsonSerializeStringTests : BsonSerializeContext<StringValueObj, string>
        {
            protected override StringValueObj GetObjectToSerialize()
            {
                return new StringValueObj("somestring");
            }
        }

        public class JsonDeserializeStringTests : JsonDeserializeContext<StringValueObj, string>
        {
            protected override string GetSerializedValue()
            {
                return "\"somestring\"";
            }

            protected override StringValueObj GetExpectedValue()
            {
                return new StringValueObj("somestring");
            }
        }

        public class JsonSerializeStringTests : JsonSerializeContext<StringValueObj, string>
        {
            protected override StringValueObj GetObjectToSerialize()
            {
                return new StringValueObj("somestring");
            }
        }

    }
}
