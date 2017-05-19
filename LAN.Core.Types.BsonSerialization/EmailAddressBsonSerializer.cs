namespace LAN.Core.Types.BsonSerialization
{
	public class EmailAddressBsonSerializer : ToStringBsonSerializer<EmailAddress>
	{
		public override EmailAddress CreateObjectFromString(string serializedObj)
		{
			EmailAddress parsedObject;
			EmailAddress.TryParse(serializedObj, out parsedObject);
			return parsedObject;
		}
	}
}