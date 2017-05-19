using System;
using System.Diagnostics.Contracts;

namespace LAN.Core.Types
{
	[AttributeUsage(AttributeTargets.Field)]
	public class FriendlyNameAttribute : Attribute
	{
		private readonly string _friendlyName;

		public FriendlyNameAttribute(string friendlyName)
		{
			_friendlyName = friendlyName;
		}

		public static implicit operator string(FriendlyNameAttribute attribute)
		{
			Contract.Requires(attribute != null);

			return attribute._friendlyName;
		}
	}
}