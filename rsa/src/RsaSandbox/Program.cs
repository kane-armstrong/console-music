using System;
using System.Security.Cryptography;
using System.Text;

namespace RsaSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var rsa1 = RSA.Create();
            var privateKey = rsa1.ExportPrivateKey();
            var publicKey = rsa1.ExportPublicKey();

            var privateKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(privateKey));
            var publicKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(publicKey));
            Console.WriteLine($"Private key: {privateKeyBase64}");
            Console.WriteLine($"Public key: {publicKeyBase64}");
            
            const string text = "Hello World!";
            var encryptedBytes = rsa1.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA512);
            var encryptedText = Convert.ToBase64String(encryptedBytes);

            var rsa2 = RSA.Create();
            rsa2.ImportPrivateKeyFromPem(privateKey);
            var decryptedText1 = Encoding.UTF8.GetString(rsa2.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.OaepSHA512));
            Console.WriteLine(decryptedText1);

            var rsa3 = RSA.Create();
            rsa3.ImportPublicKeyFromPem(publicKey);
            var decryptedText2 = Encoding.UTF8.GetString(rsa2.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.OaepSHA512));
            Console.WriteLine(decryptedText2);
        }
    }

    public static class RSAExtensions
    {
        private const string RsaPrivateKeyHeader = "-----BEGIN RSA PRIVATE KEY-----";
        private const string RsaPrivateKeyFooter = "-----END RSA PRIVATE KEY-----";

        private const string RsaPublicKeyHeader = "-----BEGIN RSA PUBLIC KEY-----";
        private const string RsaPublicKeyFooter = "-----END RSA PUBLIC KEY-----";

        public static RSA ImportPrivateKeyFromPem(this RSA rsa, string pem)
        {
            if (!pem.StartsWith(RsaPrivateKeyHeader)) throw new InvalidOperationException();
            var endIndex = pem.IndexOf(RsaPrivateKeyFooter, RsaPrivateKeyHeader.Length, StringComparison.Ordinal);
            var base64 = pem.Substring(RsaPrivateKeyHeader.Length, endIndex - RsaPrivateKeyHeader.Length);
            var source = Convert.FromBase64String(base64);
            rsa.ImportRSAPrivateKey(source, out _);
            return rsa;
        }

        public static RSA ImportPublicKeyFromPem(this RSA rsa, string pem)
        {
            if (!pem.StartsWith(RsaPublicKeyHeader)) throw new InvalidOperationException();
            var endIndex = pem.IndexOf(RsaPublicKeyFooter, RsaPublicKeyHeader.Length, StringComparison.Ordinal);
            var base64 = pem.Substring(RsaPublicKeyHeader.Length, endIndex - RsaPublicKeyHeader.Length);
            var source = Convert.FromBase64String(base64);
            rsa.ImportRSAPublicKey(source, out _);
            return rsa;
        }

        public static string ExportPrivateKey(this RSA rsa)
        {
            var bytes = rsa.ExportRSAPrivateKey();
            var builder = new StringBuilder(RsaPrivateKeyHeader);
            GeneratePemContent(bytes, builder);
            builder.AppendLine(RsaPrivateKeyFooter);
            return builder.ToString();
        }

        public static string ExportPublicKey(this RSA rsa)
        {
            var bytes = rsa.ExportRSAPublicKey();
            var builder = new StringBuilder(RsaPublicKeyHeader);
            GeneratePemContent(bytes, builder);
            builder.AppendLine(RsaPublicKeyFooter);
            return builder.ToString();
        }

        private static void GeneratePemContent(byte[] bytes, StringBuilder builder)
        {
            builder.AppendLine();

            var base64 = Convert.ToBase64String(bytes);
            var offset = 0;
            const int lineLength = 64;

            while (offset < base64.Length)
            {
                var lineEnd = Math.Min(offset + lineLength, base64.Length);
                builder.AppendLine(base64[offset..lineEnd]);
                offset = lineEnd;
            }
        }
    }
}
