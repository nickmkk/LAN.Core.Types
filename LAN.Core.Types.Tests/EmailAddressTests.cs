using System;
using LAN.Core.Types.BsonSerialization;
using LAN.Core.Types.JsonSerialization;
using LAN.Core.Types.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LAN.Core.Types.Tests
{
	public class EmailAddressTests
	{
		[TestFixtureSetUp]
		public void FixtureSetup()
		{
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				Converters = JsonConverters.CoreTypesConverters
			};
			BsonConverters.RegisterConverters();
		}

		[TestCase("")]
		[TestCase(null)]
		[TestCase("  ")]
		public void Parse_NullEmail_Throws(string sampleInput)
		{
			Assert.Throws<ArgumentException>(() => { var k = EmailAddress.Parse(sampleInput); });
		}

		[TestCase("nickmkk@gmail.com")]
		[TestCase("shanedrye@jacobi.solutions")]
		public void Parse_ValidEmail_Creates(string sampleInput)
		{
			string address = EmailAddress.Parse(sampleInput);

			Assert.That(address, Is.EqualTo(sampleInput));
		}

		[TestCase("nickmkk gmail.com")]
		public void Parse_InvalidEmail_Throws(string sampleInput)
		{
			Assert.Throws<ArgumentException>(() => { var address = EmailAddress.Parse(sampleInput); });
		}

		[Test]
		public void DomainPart_GivenAValidEmailAddress_ReturnsDomainName()
		{
			const string email = "123@abc.com";
			var address = EmailAddress.Parse(email);
			Assert.AreEqual(address.DomainPart, "abc.com");
		}

		[Test]
		public void NamePart_GivenAValidEmailAddress_ReturnsDomainName()
		{
			const string email = "123@abc.com";
			var address = EmailAddress.Parse(email);
			Assert.AreEqual(address.NamePart, "123");
		}

		[Test]
		public void Equals_GivenEquivilentEmailObject_ReturnsTrue()
		{
			var email1 = EmailAddress.Parse("nickmkk@gmail.com");
			var email2 = EmailAddress.Parse("nickmkk@gmail.com");
			Assert.IsTrue(email1.Equals(email2));
		}

		[Test]
		public void Equals_GivenNonEquivilentEmailObject_ReturnsFalse()
		{
			var email1 = EmailAddress.Parse("nickmkk@gmail.corp");
			var email2 = EmailAddress.Parse("nickmkk@gmail.com");
			Assert.IsFalse(email1.Equals(email2));
		}

		[Test]
		public void Equals_GivenNonEmailObject_ReturnsFalse()
		{
			var email1 = EmailAddress.Parse("nickmkk@gmail.com");
			Assert.IsFalse(email1.Equals(new Object()));
		}

		[TestCase("abc@123.com", "\"abc@123.com\"")]
		public void Serialize_JsonNET_StringRepresentation(string sampleInput, string expectedOutput)
		{
			//arrange
			var username = EmailAddress.Parse(sampleInput);

			//act
			var fullEmail = JsonConvert.SerializeObject(username);

			//assert
			Assert.That(fullEmail, Is.EqualTo(expectedOutput));
		}

		[TestCase("\"abc@123.com\"", "abc@123.com")]
		public void Deserialize_JsonNET_StringRepresentation(string sampleInput, string expectedOutput)
		{
			var expected = EmailAddress.Parse(expectedOutput);

			//act
			var fullEmail = JsonConvert.DeserializeObject<EmailAddress>(sampleInput);
			
			//assert
			Assert.That(fullEmail, Is.EqualTo(expected));
		}

		[Test]
		public void Serialize_Bson_StringRepresentation()
		{
			//arrange
			var username = EmailAddress.Parse("abc@123.com");
			var fakeTyped = new ObjectWithEmail { Email = username };
			var fakeUntyped = new { Email = "abc@123.com" };

			//act
			var typedJson = fakeTyped.ToJson();
			var untypedJson = fakeUntyped.ToJson();

			//assert
			Assert.That(typedJson, Is.EqualTo(untypedJson));
		}

		[Test]
		public void Deserialize_Bson_StringRepresentation()
		{
			//arrange
			const string serializedData = "{\"Email\": \"abc@123.com\"}";

			//act
			var deserializedObj = BsonSerializer.Deserialize<ObjectWithEmail>(serializedData);

			//assert
			Assert.That(deserializedObj.Email, Is.EqualTo(EmailAddress.Parse("abc@123.com")));
		}

		[Test]
		public void GetHashCode_DifferentInstancesOfSameEmailAddress_ReturnsTrue()
		{
			//arrange
			var left = EmailAddress.Parse("abc@123.com");
			var right = EmailAddress.Parse("abc@123.com");

			//act
			var leftHash = left.GetHashCode();
			var rightHash = right.GetHashCode();

			//assert
			Assert.That(leftHash, Is.EqualTo(rightHash));
		}

		[Test]
		public void DoubleEquals_DifferentInstancesOfSameEmailAddress_ReturnsTrue()
		{
			//arrange
			var left = EmailAddress.Parse("abc@123.com");
			var right = EmailAddress.Parse("abc@123.com");

			//assert
			Assert.True(left == right);
		}

		[Test]
		public void NotEquals_DifferentInstancesOfDifferentEmailAddress_ReturnsTrue()
		{
			//arrange
			var left = EmailAddress.Parse("abc@123.com");
			var right = EmailAddress.Parse("def@123.com");

			//assert
			Assert.True(left != right);
		}

		private class ObjectWithEmail
		{
			public EmailAddress Email { get; set; }
		}
	}
}

