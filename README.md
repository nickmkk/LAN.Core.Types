LAN.Core.Types
===

Encapsulated Json and Bson Serialization for Custom Value Objects:
* Json Serialization - [Nuget](https://www.nuget.org/packages/LAN.Core.Types.JsonSerialization/)
* Bson Serialization - [Nuget](https://www.nuget.org/packages/LAN.Core.Types.BsonSerialization/)

Create Serializers for your objects
---
Json Serializer
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
```

Bson Serializer
```c#
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
Create a serializer for each of your custom value types and inherit from the abstract serializer that matches your type, in this example we used decimal.  Then, implement the abstract methods for converting your objects to and from the value type.

Register Your Serializers
---
Json Registration
```c#
  JsonConvert.DefaultSettings = () => new JsonSerializerSettings
  {
    Converters = new JsonConverter[] { new CustomDecimalJsonSerializer() }
  };
```

Bson Registration
```c#
  BsonSerializer.RegisterSerializer(typeof(CustomDecimal), new CustomDecimalBsonSerializer());  
```
Finally, just register the serializers when your application starts up and you can start serializing custom value types, yay!
