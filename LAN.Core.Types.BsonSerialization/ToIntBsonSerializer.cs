using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
    public abstract class ToIntBsonSerializer<T> : AbstractClassSerializer<T> where T : class
    {
        public abstract T CreateObjectFromInteger(int serializedObj);
        public abstract int CreateIntegerFromObject(T obj);

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();
            int value;
            switch (bsonType)
            {
                case BsonType.Undefined:
                    value = default(int);
                    context.Reader.ReadUndefined();
                    break;
                case BsonType.Null:
                    value = default(int);
                    context.Reader.ReadNull();
                    break;
                case BsonType.Int32:
                    value = context.Reader.ReadInt32();
                    break;
                case BsonType.String:
                    Int32.TryParse(context.Reader.ReadString(), out value);
                    break;
                default:
                    throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the the bson type " + bsonType + ".");
            }
            return CreateObjectFromInteger(value);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            context.Writer.WriteInt32(value == null ? 0 : CreateIntegerFromObject(value));
        }

    }
}
