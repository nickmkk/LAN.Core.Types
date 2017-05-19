using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LAN.Core.Types.BsonSerialization
{
	public abstract class ToDateBsonSerializer<T> : AbstractClassSerializer<T> where T : class
	{
		private static readonly Type _convertibleType = typeof(IConvertible<DateTime>);

		public abstract T CreateObjectFromDate(DateTime serializedObj);

		public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonType = context.Reader.GetCurrentBsonType();
			long msSinceEpoch;
			switch (bsonType)
			{
				case BsonType.Undefined:
					msSinceEpoch = BsonConstants.DateTimeMinValueMillisecondsSinceEpoch;
					context.Reader.ReadUndefined();
					break;
				case BsonType.Null:
					msSinceEpoch = BsonConstants.DateTimeMinValueMillisecondsSinceEpoch;
					context.Reader.ReadNull();
					break;
				case BsonType.Int64:
					msSinceEpoch = context.Reader.ReadInt64();
					break;
				case BsonType.Int32:
					msSinceEpoch = context.Reader.ReadInt32();
					break;
				case BsonType.DateTime:
					msSinceEpoch = context.Reader.ReadDateTime();
					break;
				case BsonType.Timestamp:
					msSinceEpoch = context.Reader.ReadTimestamp();
					break;
				default:
					throw new NotSupportedException("Unable to create the type " + args.NominalType.Name + " from the the bson type " + bsonType + ".");
			}
			var msSinceEpochOutOfRange = msSinceEpoch < BsonConstants.DateTimeMinValueMillisecondsSinceEpoch || 
				msSinceEpoch > BsonConstants.DateTimeMaxValueMillisecondsSinceEpoch;
			
			var date = msSinceEpochOutOfRange 
				? new DateTime(msSinceEpoch) 
				: BsonUtils.ToDateTimeFromMillisecondsSinceEpoch(msSinceEpoch);
			return this.CreateObjectFromDate(date);
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
		{
			if (value == null)
			{
				context.Writer.WriteDateTime(BsonConstants.DateTimeMinValueMillisecondsSinceEpoch);
			}
			else
			{
				if (!_convertibleType.IsAssignableFrom(args.NominalType))
				{
					throw new NotSupportedException("The type " + args.NominalType.Name + " must implement the " + _convertibleType.Name + " interface.");
				}
				var typedObj = (IConvertible<DateTime>)value;
				var msSinceEpoch = BsonUtils.ToMillisecondsSinceEpoch(typedObj.ToValueType());
				context.Writer.WriteDateTime(msSinceEpoch);
			}
		}
	}
}
