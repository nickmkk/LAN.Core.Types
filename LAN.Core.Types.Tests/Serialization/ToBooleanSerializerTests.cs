using System.Collections.Generic;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;

namespace LAN.Core.Types.Tests.Serialization
{
	public static class ToBooleanSerializerTests
	{
		#region Value Object Implementation

		public class BooleanValueObj : IConvertible<bool>
		{

			#region Equality

			private sealed class SourceValueEqualityComparer : IEqualityComparer<BooleanValueObj>
			{
				public bool Equals(BooleanValueObj x, BooleanValueObj y)
				{
					if (ReferenceEquals(x, y)) return true;
					if (ReferenceEquals(x, null)) return false;
					if (ReferenceEquals(y, null)) return false;
					if (x.GetType() != y.GetType()) return false;
					return x._sourceValue.Equals(y._sourceValue);
				}

				public int GetHashCode(BooleanValueObj obj)
				{
					return obj._sourceValue.GetHashCode();
				}
			}

			private static readonly IEqualityComparer<BooleanValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

			public static IEqualityComparer<BooleanValueObj> SourceValueComparer
			{
				get { return SourceValueComparerInstance; }
			}

			protected bool Equals(BooleanValueObj other)
			{
				return _sourceValue.Equals(other._sourceValue);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((BooleanValueObj)obj);
			}

			public override int GetHashCode()
			{
				return _sourceValue.GetHashCode();
			}

			#endregion

			private readonly bool _sourceValue;

			public BooleanValueObj(bool sourceValue)
			{
				_sourceValue = sourceValue;
			}

			public bool ToValueType()
			{
				return _sourceValue;
			}
		}

		public class BooleanValueObjJsonSerializer : ToBooleanJsonSerializer<BooleanValueObj>
		{
			public override BooleanValueObj CreateObjectFromBoolean(bool serializedObj)
			{
				return new BooleanValueObj(serializedObj);
			}
		}

		public class BooleanValueObjBsonSerializer : ToBooleanBsonSerializer<BooleanValueObj>
		{
			public override BooleanValueObj CreateObjectFromBoolean(bool serializedObj)
			{
				return new BooleanValueObj(serializedObj);
			}
		}

		#endregion

		public class BsonDeserializeBooleanTests : BsonDeserializeContext<BooleanValueObj, bool>
		{
			protected override string GetSerializedValue()
			{
				return "true";
			}

			protected override BooleanValueObj GetExpectedValue()
			{
				return new BooleanValueObj(true);
			}
		}

		public class BsonSerializeBooleanTests : BsonSerializeContext<BooleanValueObj, bool>
		{
			protected override BooleanValueObj GetObjectToSerialize()
			{
				return new BooleanValueObj(true);
			}
		}

		public class JsonDeserializeBooleanTests : JsonDeserializeContext<BooleanValueObj, bool>
		{
			protected override string GetSerializedValue()
			{
				return "true";
			}

			protected override BooleanValueObj GetExpectedValue()
			{
				return new BooleanValueObj(true);
			}
		}

		public class JsonSerializeBooleanTests : JsonSerializeContext<BooleanValueObj, bool>
		{
			protected override BooleanValueObj GetObjectToSerialize()
			{
				return new BooleanValueObj(true);
			}
		}
	}
}
