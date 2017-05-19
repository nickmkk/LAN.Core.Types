using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToDoubleBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<double>);
		public abstract T CreateObjectFromDouble(double serializedObj);

		public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonType = context.Reader.GetCurrentBsonType();
			double value;
			switch (bsonType)
			{
				case BsonType.Undefined:
					value = default(double);
					context.Reader.ReadUndefined();
					break;
				case BsonType.Null:
					value = default(double);
					context.Reader.ReadNull();
					break;
				case BsonType.Double:
					value = context.Reader.ReadDouble();
					break;
				case BsonType.String:
					double.TryParse(context.Reader.ReadString(), out value);
					break;
				case BsonType.Int32:
					value = context.Reader.ReadInt32();
					break;
				case BsonType.Int64:
					value = context.Reader.ReadInt64();
					break;
				default:
					throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the the bson type " + bsonType + ".");
			}
			return this.CreateObjectFromDouble(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
			if (value == null)
			{
				context.Writer.WriteDouble(0);
			}
			else
			{
				if (!_convertibleType.IsAssignableFrom(args.NominalType))
				{
					throw new NotSupportedException("The type " + args.NominalType.Name + " must implement the " + _convertibleType.Name + " interface.");
				}
				var typedObj = (IConvertible<double>)value;
				context.Writer.WriteDouble(typedObj.ToValueType());
			}
		}

	}
}