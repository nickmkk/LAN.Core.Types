using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToBooleanBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		public abstract T CreateObjectFromBoolean(bool serializedObj);
		public abstract bool CreateBooleanFromObject(T obj);

		public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonType = context.Reader.GetCurrentBsonType();
			bool value;
			switch (bsonType)
			{
				case BsonType.Undefined:
					value = default(bool);
					context.Reader.ReadUndefined();
					break;
				case BsonType.Null:
					value = default(bool);
					context.Reader.ReadNull();
					break;
				case BsonType.Boolean:
					value = context.Reader.ReadBoolean();
					break;
				default:
					throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the the bson type " + bsonType + ".");
			}
			return CreateObjectFromBoolean(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
		    context.Writer.WriteBoolean(value != null && CreateBooleanFromObject(value));
		}
	}
}
