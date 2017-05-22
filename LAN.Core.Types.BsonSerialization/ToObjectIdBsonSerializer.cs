using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToObjectIdBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		public abstract T CreateObjectFromObjectId(ObjectId serializedObj);
		public abstract ObjectId CreateObjectIdFromObject(T obj);

		public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonType = context.Reader.GetCurrentBsonType();
			ObjectId value;
			switch (bsonType)
			{
				case BsonType.Undefined:
					value = ObjectId.Empty;
					context.Reader.ReadUndefined();
					break;
				case BsonType.Null:
					value = ObjectId.Empty;
					context.Reader.ReadNull();
					break;
				case BsonType.ObjectId:
					value = context.Reader.ReadObjectId();
					break;
				case BsonType.String:
					value = new ObjectId(context.Reader.ReadString());
					break;
				default:
					throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the bson type " + bsonType + ".");
			}
			return CreateObjectFromObjectId(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
		    context.Writer.WriteObjectId(value == null ? ObjectId.Empty : CreateObjectIdFromObject(value));
		}

	}
}
