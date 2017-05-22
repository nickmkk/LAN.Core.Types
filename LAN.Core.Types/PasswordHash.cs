using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Replicon.Cryptography.SCrypt;

namespace LAN.Core.Types
{

    [ImmutableObject(true)]
    public class PasswordHash
    {

        private string Hash { get; set; }

        private PasswordHash(string passwordHash)
        {
            Contract.Requires(passwordHash != null);
            Hash = passwordHash;
        }

        private PasswordHash(Password password)
        {
            Contract.Requires(password != null);
            Hash = SCrypt.HashPassword(password.ToString());
        }

        #region Equality

        private sealed class HashEqualityComparer : IEqualityComparer<PasswordHash>
        {
            public bool Equals(PasswordHash x, PasswordHash y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Hash, y.Hash);
            }

            public int GetHashCode(PasswordHash obj)
            {
                return (obj.Hash != null ? obj.Hash.GetHashCode() : 0);
            }
        }

        private static readonly IEqualityComparer<PasswordHash> HashComparerInstance = new HashEqualityComparer();

        public static IEqualityComparer<PasswordHash> HashComparer
        {
            get { return HashComparerInstance; }
        }

        protected bool Equals(PasswordHash other)
        {
            return string.Equals(Hash, other.Hash);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PasswordHash)obj);
        }

        public override int GetHashCode()
        {
            return (Hash != null ? Hash.GetHashCode() : 0);
        }

        #endregion

        #region Parsing

        public static PasswordHash Parse(string passwordHash)
        {
            PasswordHash result;
            if (!TryParse(passwordHash, out result))
            {
                throw new ArgumentException("Invalid Password Hash: " + passwordHash, "passwordHash");
            };
            return result;
        }

        public static bool TryParse(string passwordHash, out PasswordHash result)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                result = null;
                return false;
            }
            result = new PasswordHash(passwordHash);
            return true;
        }

        public static PasswordHash Parse(Password password)
        {
            PasswordHash result;
            if (!TryParse(password, out result))
            {
                throw new ArgumentException("Invalid Password : " + password, "password");
            };
            return result;
        }

        public static bool TryParse(Password password, out PasswordHash result)
        {
            if (password == null)
            {
                result = null;
                return false;
            }
            result = new PasswordHash(password);
            return true;
        }

        #endregion

        public bool Verify(string password)
        {
            return SCrypt.Verify(password, this.Hash);
        }

        public static implicit operator string(PasswordHash passwordHash)
        {
            Contract.Requires(passwordHash != null);
            if (passwordHash == null) throw new ArgumentNullException("passwordHash");
            return passwordHash.Hash;
        }

        public static implicit operator PasswordHash(string passwordHash)
        {
            Contract.Requires(passwordHash != null);
            return new PasswordHash(passwordHash);
        }

        public override string ToString()
        {
            return this.Hash;
        }

    }
}