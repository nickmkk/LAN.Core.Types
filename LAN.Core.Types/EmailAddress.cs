using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LAN.Core.Types
{
	[ImmutableObject(true)]
	public sealed class EmailAddress
	{

		private static readonly Regex ValidationRegex =
			new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		private readonly string _sourceString;

		private EmailAddress(string emailAddress)
		{
			this._sourceString = emailAddress.ToLower();
		}

		#region Equality

		private sealed class SourceStringEqualityComparer : IEqualityComparer<EmailAddress>
		{
			public bool Equals(EmailAddress x, EmailAddress y)
			{
				if (ReferenceEquals(x, y)) return true;
				if (ReferenceEquals(x, null)) return false;
				if (ReferenceEquals(y, null)) return false;
				if (x.GetType() != y.GetType()) return false;
				return string.Equals(x._sourceString, y._sourceString);
			}

			public int GetHashCode(EmailAddress obj)
			{
				return (obj._sourceString != null ? obj._sourceString.GetHashCode() : 0);
			}
		}

		private static readonly IEqualityComparer<EmailAddress> SourceStringComparerInstance = new SourceStringEqualityComparer();

		public static IEqualityComparer<EmailAddress> SourceStringComparer
		{
			get { return SourceStringComparerInstance; }
		}

		private bool Equals(EmailAddress other)
		{
			return string.Equals(_sourceString, other._sourceString);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is EmailAddress && Equals((EmailAddress)obj);
		}

		public override int GetHashCode()
		{
			return (_sourceString != null ? _sourceString.GetHashCode() : 0);
		}

		#endregion

		#region Convenience Properties

		public string NamePart
		{
			get
			{
				if (string.IsNullOrEmpty(_namePart))
				{
					_namePart = this._sourceString.Split('@')[0];
				}
				return _namePart;
			}
		}
		private string _namePart;

		public string DomainPart
		{
			get
			{
				if (string.IsNullOrEmpty(_domainPart))
				{
					this._domainPart = this._sourceString.Split('@')[1];
				}
				return this._domainPart;
			}
		}
		private string _domainPart;

		#endregion

		#region Operators

		public static implicit operator string(EmailAddress emailAddress)
		{
			return emailAddress == null ? null : emailAddress.ToString();
		}

		public static bool operator ==(EmailAddress left, EmailAddress right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(EmailAddress left, EmailAddress right)
		{
			return !Equals(left, right);
		}

		#endregion

		#region Parsing

		public static EmailAddress Parse(string emailAddress)
		{
			EmailAddress result;
			if (!TryParse(emailAddress, out result))
			{
				throw new ArgumentException("Invalid Email Address: " + emailAddress, "emailAddress");
			};
			return result;
		}

		public static bool TryParse(string emailAddress, out EmailAddress result)
		{
			if (string.IsNullOrWhiteSpace(emailAddress) || !ValidationRegex.IsMatch(emailAddress))
			{
				result = null;
				return false;
			}
			result = new EmailAddress(emailAddress);
			return true;
		}

		#endregion
        
		public override string ToString()
		{
			return this._sourceString;
		}

	}
}
