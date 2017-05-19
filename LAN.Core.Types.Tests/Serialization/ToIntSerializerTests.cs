using System.Collections.Generic;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;

namespace LAN.Core.Types.Tests.Serialization
{
	public static class ToIntSerializerTests
	{
		#region Value Object Implementation

		public class IntValueObj : IConvertible<int>
		{
			#region Equality

			private sealed class SourceValueEqualityComparer : IEqualityComparer<IntValueObj>
			{
				public bool Equals(IntValueObj x, IntValueObj y)
				{
					if (ReferenceEquals(x, y)) return true;
					if (ReferenceEquals(x, null)) return false;
					if (ReferenceEquals(y, null)) return false;
					if (x.GetType() != y.GetType()) return false;
					return x._sourceValue == y._sourceValue;
				}

				public int GetHashCode(IntValueObj obj)
				{
					return obj._sourceValue;
				}
			}

			private static readonly IEqualityComparer<IntValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

			public static IEqualityComparer<IntValueObj> SourceValueComparer
			{
				get { return SourceValueComparerInstance; }
			}

			protected bool Equals(IntValueObj other)
			{
				return _sourceValue == other._sourceValue;
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((IntValueObj)obj);
			}

			public override int GetHashCode()
			{
				return _sourceValue;
			}

			#endregion

			private readonly int _sourceValue;

			public IntValueObj(int sourceValue)
			{
				_sourceValue = sourceValue;
			}

			public int ToValueType()
			{
				return _sourceValue;
			}
		}

		public class IntValueObjJsonSerializer : ToIntJsonSerializer<IntValueObj>
		{
			public override IntValueObj CreateObjectFromInt(int serializedObj)
			{
				return new IntValueObj(serializedObj);
			}
		}

		public class IntValueObjBsonSerializer : ToIntBsonSerializer<IntValueObj>
		{
			public override IntValueObj CreateObjectFromInt(int serializedObj)
			{
				return new IntValueObj(serializedObj);
			}
		}

		#endregion

		public class BsonDeserializeIntTests : BsonDeserializeContext<IntValueObj, int>
		{
			protected override string GetSerializedValue()
			{
				return "1";
			}

			protected override IntValueObj GetExpectedValue()
			{
				return new IntValueObj(1);
			}
		}

		public class BsonSerializeIntTests : BsonSerializeContext<IntValueObj, int>
		{
			protected override IntValueObj GetObjectToSerialize()
			{
				return new IntValueObj(1);
			}
		}

		public class JsonDeserializeIntTests : JsonDeserializeContext<IntValueObj, int>
		{
			protected override string GetSerializedValue()
			{
				return "1";
			}

			protected override IntValueObj GetExpectedValue()
			{
				return new IntValueObj(1);
			}
		}

		public class JsonSerializeIntTests : JsonSerializeContext<IntValueObj, int>
		{
			protected override IntValueObj GetObjectToSerialize()
			{
				return new IntValueObj(1);
			}
		}
	}
}
