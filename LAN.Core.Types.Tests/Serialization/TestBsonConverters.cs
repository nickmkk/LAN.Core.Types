using MongoDB.Bson.Serialization;

namespace LAN.Core.Types.Tests.Serialization
{
	public static class TestBsonConverters
	{
		public static void RegisterConverters()
		{
			BsonSerializer.RegisterSerializer(typeof(ToIntSerializerTests.IntValueObj), new ToIntSerializerTests.IntValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToStringSerializerTests.StringValueObj), new ToStringSerializerTests.StringValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToBooleanSerializerTests.BooleanValueObj), new ToBooleanSerializerTests.BooleanValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToDateSerializerTests.DateValueObj), new ToDateSerializerTests.DateValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToDecimalSerializerTests.DecimalValueObj), new ToDecimalSerializerTests.DecimalValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToDoubleSerializerTests.DoubleValueObj), new ToDoubleSerializerTests.DoubleValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToLongSerializerTests.LongValueObj), new ToLongSerializerTests.LongValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToObjectIdSerializerTests.ObjectIdValueObj), new ToObjectIdSerializerTests.ObjectIdValueObjBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(ToTimeSpanSerializerTests.TimeSpanValueObj), new ToTimeSpanSerializerTests.TimeSpanValueObjBsonSerializer());
		}
	}
}
