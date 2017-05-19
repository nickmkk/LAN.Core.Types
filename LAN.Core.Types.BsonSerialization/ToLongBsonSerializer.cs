using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToLongBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<long>);
		public abstract T CreateObjectFromLong(long serializedObj);

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
			return this.CreateObjectFromLong(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
			if (value == null)
			{
				context.Writer.WriteInt64(0);
			}
			else
			{
				if (!_convertibleType.IsAssignableFrom(args.NominalType))
				{
					throw new NotSupportedException("The type " + args.NominalType.Name + " must implement the " + _convertibleType.Name + " interface.");
				}
				var typedObj = (IConvertible<long>)value;
				context.Writer.WriteInt64(typedObj.ToValueType());
			}
		}

	}
}
