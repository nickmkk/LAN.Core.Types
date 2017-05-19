using Newtonsoft.Json;

namespace LAN.Core.Types.JsonSerialization
{
	public static class JsonConverters
	{
		public static JsonConverter[] CoreTypesConverters = { new EmailAddressJsonSerializer(), };
	}
}
