using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;

namespace LAN.Core.Types.Tests.Serialization
{
	public static class ToDoubleSerializerTests
	{
		#region Value Object Implementation

		public class DoubleValueObj : IConvertible<double>
		{

			#region Equality

			private sealed class SourceValueEqualityComparer : IEqualityComparer<DoubleValueObj>
			{
				public bool Equals(DoubleValueObj x, DoubleValueObj y)
				{
					if (ReferenceEquals(x, y)) return true;
					if (ReferenceEquals(x, null)) return false;
					if (ReferenceEquals(y, null)) return false;
					if (x.GetType() != y.GetType()) return false;
					return x._sourceValue.Equals(y._sourceValue);
				}

				public int GetHashCode(DoubleValueObj obj)
				{
					return obj._sourceValue.GetHashCode();
				}
			}

			private static readonly IEqualityComparer<DoubleValueObj> SourceValueComparerInstance = new SourceValueEqualityComparer();

			public static IEqualityComparer<DoubleValueObj> SourceValueComparer
			{
				get { return SourceValueComparerInstance; }
			}

			protected bool Equals(DoubleValueObj other)
			{
				return _sourceValue.Equals(other._sourceValue);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((DoubleValueObj)obj);
			}

			public override int GetHashCode()
			{
				return _sourceValue.GetHashCode();
			}

			#endregion

			private readonly double _sourceValue;

			public DoubleValueObj(double sourceValue)
			{
				_sourceValue = sourceValue;
			}

			public double ToValueType()
			{
				return _sourceValue;
			}
		}

		public class DoubleValueObjJsonSerializer : ToDoubleJsonSerializer<DoubleValueObj>
		{
			public override DoubleValueObj CreateObjectFromDouble(double serializedObj)
			{
				return new DoubleValueObj(serializedObj);
			}

		    public override double CreateDoubleFromObject(DoubleValueObj obj)
		    {
		        return obj.ToValueType();
		    }
		}

		public class DoubleValueObjBsonSerializer : ToDoubleBsonSerializer<DoubleValueObj>
		{
			public override DoubleValueObj CreateObjectFromDouble(double serializedObj)
			{
				return new DoubleValueObj(serializedObj);
			}

		    public override double CreateDoubleFromObject(DoubleValueObj obj)
		    {
		        return obj.ToValueType();
		    }
		}

		#endregion

		public class BsonDeserializeDoubleTests : BsonDeserializeContext<DoubleValueObj, double>
		{
			protected override string GetSerializedValue()
			{
				return "1.1";
			}

			protected override DoubleValueObj GetExpectedValue()
			{
				return new DoubleValueObj(1.1);
			}
		}

		public class BsonSerializeDoubleTests : BsonSerializeContext<DoubleValueObj, double>
		{
			protected override DoubleValueObj GetObjectToSerialize()
			{
				return new DoubleValueObj(1.1);
			}
		}

		public class JsonDeserializeDoubleTests : JsonDeserializeContext<DoubleValueObj, double>
		{
			protected override string GetSerializedValue()
			{
				return "1.1";
			}

			protected override DoubleValueObj GetExpectedValue()
			{
				return new DoubleValueObj(1.1);
			}
		}

		public class JsonSerializeDoubleTests : JsonSerializeContext<DoubleValueObj, double>
		{
			protected override DoubleValueObj GetObjectToSerialize()
			{
				return new DoubleValueObj(1.1);
			}
		}
	}
}
