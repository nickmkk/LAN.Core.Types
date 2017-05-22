using LAN.Core.Types.JsonSerialization;

namespace LAN.Core.Types.Serialization
{
    public class EmailAddressJsonSerializer : ToStringJsonSerializer<EmailAddress>
    {
        public override EmailAddress CreateObjectFromString(string serializedObj)
        {
            EmailAddress parsedObject;
            EmailAddress.TryParse(serializedObj, out parsedObject);
            return parsedObject;
        }

        public override string CreateStringFromObject(EmailAddress obj)
        {
            return obj;
        }
    }
}