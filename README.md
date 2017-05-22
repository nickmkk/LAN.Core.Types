LAN.Core.Types
===

Encapsulated Json and Bson Serialization for Custom Value Objects:
* Json Serialization - [Nuget](https://www.nuget.org/packages/LAN.Core.Types.JsonSerialization/)
* Bson Serialization - [Nuget](https://www.nuget.org/packages/LAN.Core.Types.BsonSerialization/)

Create Serializers for your objects
---
```c#
  public class CustomDecimalJsonSerializer : ToDecimalJsonSerializer<CustomDecimal>
  {
    public override CustomDecimal CreateObjectFromDecimal(decimal serializedObj)
    {
      return new CustomDecimal(serializedObj);
    }
    
    public override decimal CreateDecimalFromObject(CustomDecimal obj)
    {
      return obj.ToValueType();
    }
  }

  public class CustomDecimalBsonSerializer : ToDecimalBsonSerializer<CustomDecimal>
  {
    public override CustomDecimal CreateObjectFromDecimal(decimal serializedObj)
    {
      return new CustomDecimal(serializedObj);
    }
    
    public override decimal CreateDecimalFromObject(CustomDecimal obj)
    {
      return obj.ToValueType();
    }
  }
```
Create a serializer for each of your custom value types and inherit from the abstract serializer that matches your type, in this example we used decimal.  Implement the abstract methods for converting your objects to and from the value type.

Register Your Serializers
---
```c#
  JsonConvert.DefaultSettings = () => new JsonSerializerSettings
  {
    Converters = new JsonConverter[] { new CustomDecimalJsonSerializer() }
  };

  BsonSerializer.RegisterSerializer(typeof(CustomDecimal), new CustomDecimalBsonSerializer());  
```
Then, just register the serializers when your application starts up and you can start serializing custom value types, yay!
