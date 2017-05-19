using System;
using System.Collections.Generic;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;
using MongoDB.Bson;

namespace LAN.Core.Types.Tests.Serialization
{
	public static class ToDateSerializerTests
	{
		#region Value Object Implementation

		public class DateValueObj : IConvertible<DateTime>
		{

			#region Equality

			private sealed class SourceValueEqualityComparer : IEqualityComparer<DateValueObj>
			{
				public bool Equals(DateValueObj x, DateValueObj y)
				{
					if (ReferenceEquals(x, y)) return true;
					if (ReferenceEquals(x, null)) return false;
					if (ReferenceEquals(y, null)) return false;
					if (x.GetType() != y.GetType()) return false;
					return x._sourceValue.Equals(y._sourceValue);
				}

				public int GetHashCode(DateValueObj obj)
				{
					return obj._sourceValue.GetHashCode();
				}
			}

			private static readonly IEqualityComparer<DateValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

			public static IEqualityComparer<DateValueObj> SourceValueComparer
			{
				get { return SourceValueComparerInstance; }
			}

			protected bool Equals(DateValueObj other)
			{
				return this._sourceValue.Equals(other._sourceValue);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((DateValueObj)obj);
			}

			public override int GetHashCode()
			{
				return _sourceValue.GetHashCode();
			}

			#endregion

			private readonly DateTime _sourceValue;
			
			public DateValueObj(DateTime sourceValue)
			{
				_sourceValue = sourceValue;
			}

			DateTime IConvertible<DateTime>.ToValueType()
			{
				return _sourceValue;
			}
		}

		public class DateValueObjJsonSerializer : ToDateJsonSerializer<DateValueObj>
		{
			public override DateValueObj CreateObjectFromDate(DateTime serializedObj)
			{
				return new DateValueObj(serializedObj);
			}
		}

		public class DateValueObjBsonSerializer : ToDateBsonSerializer<DateValueObj>
		{
			public override DateValueObj CreateObjectFromDate(DateTime serializedObj)
			{
				return new DateValueObj(serializedObj);
			}
		}

		#endregion

		public class BsonDeserializeDateTests : BsonDeserializeContext<DateValueObj, DateTime>
		{
			private DateTime expectedValue = DateTime.UtcNow;

			protected override string GetSerializedValue()
			{
				return "ISODate(\"" + expectedValue.ToString("O") + "\")";
			}

			protected override DateValueObj GetExpectedValue()
			{
				return new DateValueObj(expectedValue);
			}
		}

		public class BsonSerializeDateTests : BsonSerializeContext<DateValueObj, DateTime>
		{
			protected override DateValueObj GetObjectToSerialize()
			{
				return new DateValueObj(DateTime.Now);
			}
		}

		public class JsonDeserializeDateTests : JsonDeserializeContext<DateValueObj, DateTime>
		{
			private DateTime expectedValue = DateTime.Now;

			protected override string GetSerializedValue()
			{
				return "\"" + expectedValue.ToString("yyyy-MM-ddTHH:mm:ssZ") + "\"";
			}

			protected override DateValueObj GetExpectedValue()
			{
				return new DateValueObj(expectedValue);
			}
		}

		public class JsonSerializeDateTests : JsonSerializeContext<DateValueObj, DateTime>
		{
			protected override DateValueObj GetObjectToSerialize()
			{
				return new DateValueObj(DateTime.Now);
			}
		}
	}
}
