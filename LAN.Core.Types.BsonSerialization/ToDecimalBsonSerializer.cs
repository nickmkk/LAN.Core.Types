using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
    public abstract class ToDecimalBsonSerializer<T> : AbstractClassSerializer<T> where T : class
    {
        private static readonly Type _convertibleType = typeof(IConvertible<decimal>);
        public abstract T CreateObjectFromDecimal(decimal serializedObj);

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();
            decimal value;
            switch (bsonType)
            {
                case BsonType.Undefined:
                    value = default(decimal);
                    context.Reader.ReadUndefined();
                    break;
                case BsonType.Null:
                    value = default(decimal);
                    context.Reader.ReadNull();
                    break;
                case BsonType.Decimal128:
                    value = (decimal)context.Reader.ReadDecimal128();
                    break;
                case BsonType.Double:
                    value = Convert.ToDecimal(context.Reader.ReadDouble());
                    break;
                case BsonType.String:
                    decimal.TryParse(context.Reader.ReadString(), out value);
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
            return this.CreateObjectFromDecimal(value);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            if (value == null)
            {
                context.Writer.WriteDecimal128(0);
            }
            else
            {
                if (!_convertibleType.IsAssignableFrom(args.NominalType))
                {
                    throw new NotSupportedException("The type " + args.NominalType.Name + " must implement the " + _convertibleType.Name + " interface.");
                }
                var typedObj = (IConvertible<decimal>)value;
                context.Writer.WriteDecimal128(typedObj.ToValueType());
            }
        }

    }
}
