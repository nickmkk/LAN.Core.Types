using System.Collections.Generic;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;
using MongoDB.Bson;

namespace LAN.Core.Types.Tests.Serialization
{
    public static class ToObjectIdSerializerTests
    {
        #region Value Object Implementation

        public class ObjectIdValueObj : IConvertible<ObjectId>, IConvertible<string>
        {

            #region Equality

            private sealed class SourceValueEqualityComparer : IEqualityComparer<ObjectIdValueObj>
            {
                public bool Equals(ObjectIdValueObj x, ObjectIdValueObj y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (ReferenceEquals(x, null)) return false;
                    if (ReferenceEquals(y, null)) return false;
                    if (x.GetType() != y.GetType()) return false;
                    return x._sourceValue.Equals(y._sourceValue);
                }

                public int GetHashCode(ObjectIdValueObj obj)
                {
                    return obj._sourceValue.GetHashCode();
                }
            }

            private static readonly IEqualityComparer<ObjectIdValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

            public static IEqualityComparer<ObjectIdValueObj> SourceValueComparer
            {
                get { return SourceValueComparerInstance; }
            }

            protected bool Equals(ObjectIdValueObj other)
            {
                return _sourceValue.Equals(other._sourceValue);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((ObjectIdValueObj)obj);
            }

            public override int GetHashCode()
            {
                return _sourceValue.GetHashCode();
            }

            #endregion

            private readonly ObjectId _sourceValue;

            public ObjectIdValueObj(string sourceValue)
            {
                _sourceValue = new ObjectId(sourceValue);
            }

            public ObjectIdValueObj(ObjectId sourceValue)
            {
                _sourceValue = sourceValue;
            }

            string IConvertible<string>.ToValueType()
            {
                return _sourceValue.ToString();
            }

            ObjectId IConvertible<ObjectId>.ToValueType()
            {
                return _sourceValue;
            }
        }

        public class ObjectIdValueObjJsonSerializer : ToStringJsonSerializer<ObjectIdValueObj>
        {
            public override ObjectIdValueObj CreateObjectFromString(string serializedObj)
            {
                return new ObjectIdValueObj(serializedObj == string.Empty ? ObjectId.Empty.ToString() : serializedObj);
            }

            public override string CreateStringFromObject(ObjectIdValueObj obj)
            {
                return ((IConvertible<string>)obj).ToValueType();
            }
        }

        public class ObjectIdValueObjBsonSerializer : ToObjectIdBsonSerializer<ObjectIdValueObj>
        {
            public override ObjectIdValueObj CreateObjectFromObjectId(ObjectId serializedObj)
            {
                return new ObjectIdValueObj(serializedObj);
            }

            public override ObjectId CreateObjectIdFromObject(ObjectIdValueObj obj)
            {
                return ((IConvertible<ObjectId>)obj).ToValueType();
            }
        }

        #endregion

        public class BsonDeserializeObjectIdTests : BsonDeserializeContext<ObjectIdValueObj, ObjectId>
        {
            protected override string GetSerializedValue()
            {
                return "\"554e52bcc1f3c71e94ed3285\"";
            }

            protected override ObjectIdValueObj GetExpectedValue()
            {
                return new ObjectIdValueObj("554e52bcc1f3c71e94ed3285");
            }
        }

        public class BsonSerializeObjectIdTests : BsonSerializeContext<ObjectIdValueObj, ObjectId>
        {
            protected override ObjectIdValueObj GetObjectToSerialize()
            {
                return new ObjectIdValueObj(ObjectId.GenerateNewId());
            }
        }

        public class JsonDeserializeObjectIdTests : JsonDeserializeContext<ObjectIdValueObj, ObjectId>
        {
            protected override string GetSerializedValue()
            {
                return "\"554e52bcc1f3c71e94ed3285\"";
            }

            protected override ObjectIdValueObj GetExpectedValue()
            {
                return new ObjectIdValueObj("554e52bcc1f3c71e94ed3285");
            }
        }

        public class JsonSerializeObjectIdTests : JsonSerializeContext<ObjectIdValueObj, ObjectId>
        {
            protected override ObjectIdValueObj GetObjectToSerialize()
            {
                return new ObjectIdValueObj(ObjectId.GenerateNewId());
            }
        }

    }
}
