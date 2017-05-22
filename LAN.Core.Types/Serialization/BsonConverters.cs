using MongoDB.Bson.Serialization;

namespace LAN.Core.Types.Serialization
{
	public static class BsonConverters
	{
		public static void RegisterConverters()
		{
			BsonSerializer.RegisterSerializer(typeof(PasswordHash), new PasswordHashBsonSerializer());
			BsonSerializer.RegisterSerializer(typeof(EmailAddress), new EmailAddressBsonSerializer());
		}
	}
}
