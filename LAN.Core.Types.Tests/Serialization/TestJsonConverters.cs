using Newtonsoft.Json;

namespace LAN.Core.Types.Tests.Serialization
{
	public static class TestJsonConverters
	{
		public static JsonConverter[] TypesConverters =
		{
			new ToIntSerializerTests.IntValueObjJsonSerializer(), 
			new ToStringSerializerTests.StringValueObjJsonSerializer(), 
			new ToBooleanSerializerTests.BooleanValueObjJsonSerializer(), 
			new ToDateSerializerTests.DateValueObjJsonSerializer(), 
			new ToDecimalSerializerTests.DecimalValueObjJsonSerializer(), 
			new ToDoubleSerializerTests.DoubleValueObjJsonSerializer(), 
			new ToLongSerializerTests.LongValueObjJsonSerializer(), 
			new ToObjectIdSerializerTests.ObjectIdValueObjJsonSerializer(), 
			new ToTimeSpanSerializerTests.TimeSpanValueObjJsonSerializer(), 
		};
	}
}
