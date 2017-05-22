using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
    public abstract class ToStringBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		public abstract T CreateObjectFromString(string serializedObj);
		public abstract string CreateStringFromObject(T obj);

		public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonType = context.Reader.GetCurrentBsonType();
			string value;
			switch (bsonType)
			{
				case BsonType.Undefined:
					value = string.Empty;
					context.Reader.ReadUndefined();
					break;
				case BsonType.Null:
					value = string.Empty;
					context.Reader.ReadNull();
					break;
				case BsonType.String:
					value = context.Reader.ReadString();
					break;
				default:
					throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the the bson type " + bsonType + ".");
			}
			return CreateObjectFromString(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
		    context.Writer.WriteString(value == null ? string.Empty : CreateStringFromObject(value));
		}
	}
}