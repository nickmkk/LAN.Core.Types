LAN.Core.Types
===

Encapsulated Json and Bson Serialization for Custom Value Objects:
* Json Serialization - [Nuget](https://www.nuget.org/packages/LAN.Core.Types.JsonSerialization/)
* Bson Serialization - [Nuget](https://www.nuget.org/packages/LAN.Core.Types.BsonSerialization/)

Implement IConvertible<T>
---
 ```c#
  public class DecimalValueObj : IConvertible<decimal>
  {
    private decimal _sourceValue;  
    
    ...
    
    public decimal ToValueType()
    {
      return _sourceValue;
    }
  }
```
This will allow the serializer to get your custom objects value for serialization.

Create Your Serializers
---
```c#
  public class DecimalValueObjJsonSerializer : ToDecimalJsonSerializer<DecimalValueObj>
  {
    public override DecimalValueObj CreateObjectFromDecimal(decimal serializedObj)
    {
      return new DecimalValueObj(serializedObj);
    }
  }

  public class DecimalValueObjBsonSerializer : ToDecimalBsonSerializer<DecimalValueObj>
  {
    public override DecimalValueObj CreateObjectFromDecimal(decimal serializedObj)
    {
      return new DecimalValueObj(serializedObj);
    }
  }
```
The abstract serializers allow you to control how your custom objects are created.

Register Your Serializers
---
```c#
  JsonConvert.DefaultSettings = () => new JsonSerializerSettings
  {
    Converters = new JsonConverter[] { new DecimalValueObjJsonSerializer() }
  };

  BsonSerializer.RegisterSerializer(typeof(DecimalValueObj), new DecimalValueObjBsonSerializer());  
```
Just register your serializers when your application starts and you can start serializing custom value types, yay!
