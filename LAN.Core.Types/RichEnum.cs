using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace LAN.Core.Types
{
	public class RichEnum<T> : IEquatable<T>, IEquatable<RichEnum<T>> where T : struct, IConvertible
	{
		protected readonly T UnderlyingValue;
		// ReSharper disable once StaticFieldInGenericType
		protected static readonly Type TypeOfT;

		static RichEnum()
		{
			TypeOfT = typeof(T);
			if (!TypeOfT.IsEnum)
			{
				throw new NotSupportedException(string.Format("RichEnum only supports enum types, however you are using: {0}", TypeOfT));
			}
			FriendlyNameMapping = new Dictionary<string, T>();
			var values = Enum.GetNames(TypeOfT);
			foreach (var value in values)
			{
				var enumValue = (T)Enum.Parse(TypeOfT, value);
				var richValue = new RichEnum<T>(enumValue);
				FriendlyNameMapping.Add(richValue.GetFriendlyName(), enumValue);
			}
		}

		public RichEnum(T underlyingValue)
		{
			if (!TypeOfT.IsEnum)
			{
				throw new NotSupportedException(string.Format("RichEnum only supports enum types, however you are using: {0}",
					TypeOfT));
			}
			UnderlyingValue = underlyingValue;
		}

		public string GetFriendlyName()
		{
			var memInfo = TypeOfT.GetMember(this.UnderlyingValue.ToString(CultureInfo.InvariantCulture));
			var attributes = memInfo[0].GetCustomAttributes(typeof(FriendlyNameAttribute), false);
			if (attributes.Length > 0)
			{
				return (FriendlyNameAttribute)attributes[0];
			}
			return this.UnderlyingValue.ToString(CultureInfo.InvariantCulture).Replace("_", " ");
		}

		private static readonly Dictionary<string, T> FriendlyNameMapping;
		public static T FromFriendlyName(string friendlyName)
		{
			if (!FriendlyNameMapping.ContainsKey(friendlyName)) throw new ArgumentException(string.Format("The friendly name you provided ({0}) is not a known {1}", friendlyName, TypeOfT.Name), "friendlyName");
			return FriendlyNameMapping[friendlyName];
		}

		#region Implicit

		public static implicit operator T(RichEnum<T> sEnum)
		{
			Contract.Requires(sEnum != null);

			if (sEnum == null) throw new ArgumentNullException("sEnum");
			
			return sEnum.UnderlyingValue;
		}

		public static implicit operator RichEnum<T>(T sEnum)
		{
			return new RichEnum<T>(sEnum);
		}

		#endregion

		#region Equality

		public bool Equals(T other)
		{
			return this.UnderlyingValue.Equals(other);
		}

		public bool Equals(RichEnum<T> other)
		{
			return this.UnderlyingValue.Equals(other.UnderlyingValue);
		}

		public override bool Equals(object obj)
		{
			var objType = obj.GetType();
			if (objType != TypeOfT &&
			    objType != typeof(RichEnum<T>)) return false;
			var cast = (RichEnum<T>)obj;

			return this.Equals(cast);
		}

		public override int GetHashCode()
		{
			return this.UnderlyingValue.GetHashCode();
		}

		#endregion
	}
}