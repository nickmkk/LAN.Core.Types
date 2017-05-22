using LAN.Core.Types.BsonSerialization;

namespace LAN.Core.Types.Serialization
{
    public class PasswordHashBsonSerializer : ToStringBsonSerializer<PasswordHash>
    {
        public override PasswordHash CreateObjectFromString(string serializedObj)
        {
            PasswordHash parsedObject;
            PasswordHash.TryParse(serializedObj, out parsedObject);
            return parsedObject;
        }

        public override string CreateStringFromObject(PasswordHash obj)
        {
            return obj;
        }
    }
}