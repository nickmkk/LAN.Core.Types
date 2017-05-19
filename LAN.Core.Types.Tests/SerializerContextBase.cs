using LAN.Core.Types.Tests.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LAN.Core.Types.Tests
{
	public abstract class SerializerContextBase
	{
		private static bool _hasRegistered = false;

		[TestFixtureSetUp]
		public void Setup()
		{
			if (!_hasRegistered)
			{
				_hasRegistered = true;
				JsonConvert.DefaultSettings = () => new JsonSerializerSettings
				{
					Converters = TestJsonConverters.TypesConverters
				};
				TestBsonConverters.RegisterConverters();
			}
			
			this.Given();
			this.When();
		}

		protected virtual void Given()
		{
		}

		protected virtual void When()
		{
		}
	}
}
