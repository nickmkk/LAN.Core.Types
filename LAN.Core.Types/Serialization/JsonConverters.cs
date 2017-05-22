using Newtonsoft.Json;

namespace LAN.Core.Types.Serialization
{
	public static class JsonConverters
	{
		public static JsonConverter[] CoreTypesConverters = { new EmailAddressJsonSerializer(), };
	}
}
