using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;
using MongoDB.Bson;

namespace LAN.Core.Types.Tests.Serialization
{
    public static class ToDecimalSerializerTests
    {
        #region Value Object Implementation

        public class DecimalValueObj : IConvertible<Decimal128>, IConvertible<decimal>
        {

            #region Equality

            private sealed class SourceValueEqualityComparer : IEqualityComparer<DecimalValueObj>
            {
                public bool Equals(DecimalValueObj x, DecimalValueObj y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (ReferenceEquals(x, null)) return false;
                    if (ReferenceEquals(y, null)) return false;
                    if (x.GetType() != y.GetType()) return false;
                    return x._sourceValue == y._sourceValue;
                }

                public int GetHashCode(DecimalValueObj obj)
                {
                    return obj._sourceValue.GetHashCode();
                }
            }

            private static readonly IEqualityComparer<DecimalValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

            public static IEqualityComparer<DecimalValueObj> SourceValueComparer
            {
                get { return SourceValueComparerInstance; }
            }

            protected bool Equals(DecimalValueObj other)
            {
                return _sourceValue == other._sourceValue;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((DecimalValueObj)obj);
            }

            public override int GetHashCode()
            {
                return _sourceValue.GetHashCode();
            }

            #endregion

            private readonly decimal _sourceValue;

            public DecimalValueObj(decimal sourceValue)
            {
                _sourceValue = sourceValue;
            }

            Decimal128 IConvertible<Decimal128>.ToValueType()
            {
                return _sourceValue;
            }

            decimal IConvertible<decimal>.ToValueType()
            {
                return _sourceValue;
            }
        }

        public class DecimalValueObjJsonSerializer : ToDecimalJsonSerializer<DecimalValueObj>
        {
            public override DecimalValueObj CreateObjectFromDecimal(decimal serializedObj)
            {
                return new DecimalValueObj(serializedObj);
            }
        }


        public class DecimalValueObjBsonSerializer : ToDecimalBsonSerializer<DecimalValueObj>
        {
            public override DecimalValueObj CreateObjectFromDecimal(decimal serializedObj)
            {
                return new DecimalValueObj(serializedObj);
            }
        }


        #endregion

        public class BsonDeserializeDecimalTests : BsonDeserializeContext<DecimalValueObj, Decimal128>
        {
            protected override string GetSerializedValue()
            {
                return "1.1";
            }

            protected override DecimalValueObj GetExpectedValue()
            {
                return new DecimalValueObj(new decimal(1.1));
            }
        }

        //public class BsonSerializeDecimalTests : BsonSerializeContext<DecimalValueObj, Decimal128>
        //{
        //    protected override DecimalValueObj GetObjectToSerialize()
        //    {
        //        return new DecimalValueObj(new decimal(1.1));
        //    }
        //}

        public class JsonDeserializeDecimalTests : JsonDeserializeContext<DecimalValueObj, decimal>
        {
            protected override string GetSerializedValue()
            {
                return "\"0.1\"";
            }

            protected override DecimalValueObj GetExpectedValue()
            {
                return new DecimalValueObj(new decimal(0.1));
            }
        }

        public class JsonSerializeDecimalTests : JsonSerializeContext<DecimalValueObj, decimal>
        {
            protected override DecimalValueObj GetObjectToSerialize()
            {
                return new DecimalValueObj(new decimal(0.1));
            }
        }
    }
}
