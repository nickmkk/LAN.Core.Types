using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToTimeSpanBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<TimeSpan>);

		public abstract T CreateObjectFromTimeSpan(TimeSpan serializedObj);

		public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonType = context.Reader.GetCurrentBsonType();
			TimeSpan timeSpan;
			switch (bsonType)
			{
				case BsonType.Undefined:
					timeSpan = default(TimeSpan);
					context.Reader.ReadUndefined();
					break;
				case BsonType.Null:
					timeSpan = default(TimeSpan);
					context.Reader.ReadNull();
					break;
				case BsonType.Int64:
					timeSpan = new TimeSpan(context.Reader.ReadInt64());
					break;
				case BsonType.Int32:
					timeSpan = new TimeSpan(context.Reader.ReadInt32());
					break;
				default:
					throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the the bson type " + bsonType + ".");
			}
			return this.CreateObjectFromTimeSpan(timeSpan);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
			if (value == null)
			{
				context.Writer.WriteInt64(TimeSpan.MinValue.Ticks);
			}
			else
			{
				if (!_convertibleType.IsAssignableFrom(args.NominalType))
				{
					throw new NotSupportedException("The type " + args.NominalType.Name + " must implement the " + _convertibleType.Name + " interface.");
				}
				var typedObj = (IConvertible<TimeSpan>)value;
				context.Writer.WriteInt64(typedObj.ToValueType().Ticks);
			}
		}

	}
}
