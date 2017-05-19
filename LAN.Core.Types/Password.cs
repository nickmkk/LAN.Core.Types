using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace LAN.Core.Types
{
	public class Password
	{
		private byte[] _bytes;
		private string _value;

		private Password(string password)
		{
			Contract.Requires(password != null);
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("The provided password is null or empty.");
			this.Value = password;
		}

		private Password(byte[] password)
		{
			Contract.Requires(password != null);
			Contract.Ensures(this.Bytes != null);

			if (password == null || password.Length == 0) throw new ArgumentException("The provided password is null or empty");
			this.Bytes = password;
		}
		
		#region Parsing

		public static Password Parse(string password)
		{
			Password result;
			if (!TryParse(password, out result))
			{
				throw new ArgumentException("Invalid Password: " + password, "password");
			};
			return result;
		}

		public static bool TryParse(string password, out Password result)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
				result = null;
				return false;
			}
			result = new Password(password);
			return true;
		}

		#endregion
		
		public string Value
		{
			get
			{
				if (this._value == null)
				{
					this._value = Encoding.UTF8.GetString(this._bytes);
				}
				return _value;
			}
			private set { _value = value; }
		}

		public byte[] Bytes
		{
			get
			{
				if (_bytes == null)
				{
					this._bytes = Encoding.UTF8.GetBytes(this.Value);
				}
				return _bytes;
			}
			private set { _bytes = value; }
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}