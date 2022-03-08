using HashProperty.Config;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace HashProperty.Provider
{
    public class HashProvider : IHashProvider
    {
        private readonly static string SaltKey = HashConfig.SaltKey;
        private readonly static bool IsHash = HashConfig.IsHash;

        public string Hash(string value)
        {
            if (!IsHash) return value;

            if (string.IsNullOrEmpty(SaltKey))
                throw new ArgumentNullException("SaltKey", "Please initialize your salt key.");

            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value.IndexOf("æ") > -1) return value;

            var valueBytes = KeyDerivation.Pbkdf2(
                              password: value,
                              salt: Encoding.UTF8.GetBytes(SaltKey),
                              prf: KeyDerivationPrf.HMACSHA512,
                              iterationCount: 10000,
                              numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes) + "æ" + SaltKey;

        }

        public bool ValidateHash(string value, string hash) => Hash(value) == hash;

    }
}
