using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToBooleanBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<bool>);
		public abstract T CreateObjectFromBoolean(bool serializedObj);

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
			return this.CreateObjectFromBoolean(value);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
			if (value == null)
			{
				context.Writer.WriteBoolean(false);
			}
			else
			{
				if (!_convertibleType.IsAssignableFrom(args.NominalType))
				{
					throw new NotSupportedException("The type " + args.NominalType.Name + " must implement the " + _convertibleType.Name + " interface.");
				}
				var typedObj = (IConvertible<bool>)value;
				context.Writer.WriteBoolean(typedObj.ToValueType());
			}
		}
	}
}
