using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToLongBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		public abstract T CreateObjectFromLong(long serializedObj);
		public abstract long CreateLongFromObject(T obj);

		public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonType = context.Reader.GetCurrentBsonType();
			long value;
			switch (bsonType)
			{
				case BsonType.Undefined:
					value = default(long);
					context.Reader.ReadUndefined();
					break;
				case BsonType.Null:
					value = default(long);
					context.Reader.ReadNull();
					break;
				case BsonType.Int64:
					value = context.Reader.ReadInt64();
					break;
				case BsonType.Int32:
					value = context.Reader.ReadInt32();
					break;
				case BsonType.String:
					Int64.TryParse(context.Reader.ReadString(), out value);
					break;
				default:
					throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the the bson type " + bsonType + ".");
			}
			return CreateObjectFromLong(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
		    context.Writer.WriteInt64(value == null ? 0 : CreateLongFromObject(value));
		}

	}
}
