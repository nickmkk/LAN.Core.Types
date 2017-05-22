using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToDoubleBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		public abstract T CreateObjectFromDouble(double serializedObj);
		public abstract double CreateDoubleFromObject(T obj);

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
			return CreateObjectFromDouble(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
		    context.Writer.WriteDouble(value == null ? 0 : CreateDoubleFromObject(value));
		}

	}
}