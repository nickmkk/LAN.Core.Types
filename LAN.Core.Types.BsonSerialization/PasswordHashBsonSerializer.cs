namespace LAN.Core.Types.BsonSerialization
{
	public class PasswordHashBsonSerializer : ToStringBsonSerializer<PasswordHash>
	{
		public override PasswordHash CreateObjectFromString(string serializedObj)
		{
			PasswordHash parsedObject;
			PasswordHash.TryParse(serializedObj, out parsedObject);
			return parsedObject;
		}
	}
}