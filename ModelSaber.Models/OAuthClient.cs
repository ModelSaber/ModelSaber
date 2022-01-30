using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ModelSaber.Models
{
    public class OAuthClient : BaseId
    {
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public byte[] SecKey { get; set; }
        public string? RedirectUri { get; set; }
        public string? Scope { get; set; }
        public virtual List<OAuthToken> Tokens { get; set; }

        public void InitSecKey()
        {
            SecKey = getKey();
        }

        public void RotateSecret()
        {
            ClientSecret = Convert.ToBase64String(getKey());
            InitSecKey();
            Tokens.Clear();
        }

        private byte[] getKey()
        {
            using var aes = Aes.Create("AesCryptoServiceProvider");
            aes.GenerateKey();
            return aes.Key;
        }

        public OAuthToken GetToken()
        {
            using var sha = SHA512.Create();
            var utcNow = DateTime.UtcNow;
            var bytes = new[] { BitConverter.GetBytes(utcNow.ToBinary()), ClientId.ToByteArray(), SecKey }.SelectMany(t => t).ToArray();
            bytes.Shuffle();
            return new OAuthToken
            {
                ClientId = Id,
                Token = sha.ComputeHash(bytes)
            };
        }
    }

    public class OAuthToken : BaseId
    {
        public uint ClientId { get; set; }
        public byte[] Token { get; set; }
        public DateTime InsertedAt { get; set; }
        public uint ExpiresIn { get; set; }
        public virtual OAuthClient Client { get; set; }
        public byte[] Refresh { get; set; }
        public string? Scope { get; set; }

        public bool IsExpired()
        {
            return DateTime.UtcNow > InsertedAt.AddSeconds(ExpiresIn);
        }

        public IEnumerable<string> GetScopes() => new[] { "public" }.Concat(Scope?.Split(' ') ?? Array.Empty<string>());
    }
}
