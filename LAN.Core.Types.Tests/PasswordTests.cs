using NUnit.Framework;

namespace LAN.Core.Types.Tests
{
    class PasswordTests
    {
        [Test()]
        public void HashConstructor_GivenPassword_CreatesAHash()
        {
            var password = Password.Parse("Pa55word");
            var hash = PasswordHash.Parse(password);
            Assert.IsNotNull((string)hash);
            Assert.IsNotEmpty((string)hash);
        }

        [Test()]
        public void VerifyHash_ReturnsTrueWhenHashMatches()
        {
            var password = Password.Parse("Pa55word");
            var createdHash = PasswordHash.Parse(password);
            var verifiedHash = PasswordHash.Parse(createdHash.ToString());
            Assert.IsTrue(verifiedHash.Verify(password.ToString()));
        }

        [Test()]
        public void VerifyHash_ReturnsFalseWhenHashDoesNotMatch()
        {
            var password = Password.Parse("Pa55word");
            var createdHash = PasswordHash.Parse(password);
            var verifiedHash = PasswordHash.Parse(createdHash.ToString());
            Assert.IsFalse(verifiedHash.Verify("badpassword"));
        }
    }
}
