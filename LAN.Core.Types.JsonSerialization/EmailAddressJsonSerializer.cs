namespace LAN.Core.Types.JsonSerialization
{
	public class EmailAddressJsonSerializer : ToStringJsonSerializer<EmailAddress>
	{
		public override EmailAddress CreateObjectFromString(string serializedObj)
		{
			EmailAddress parsedObject;
			EmailAddress.TryParse(serializedObj, out parsedObject);
			return parsedObject;
		}
	}
}