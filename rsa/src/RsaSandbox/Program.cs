using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsaSandbox
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var keySource = RSA.Create();
            var privateKey = keySource.ExportPrivateKey();
            var publicKey = keySource.ExportPublicKey();
            var privateKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(privateKey));
            var publicKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(publicKey));
            Console.WriteLine($"Private key: {privateKeyBase64}");
            Console.WriteLine($"Public key: {publicKeyBase64}");

            const string text = "Hello World!";

            var encryptor = RSA.Create();
            var encryptedBytes = encryptor.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA512);
            var encryptedText = Convert.ToBase64String(encryptedBytes);

            // won't work. see https://cs.stackexchange.com/a/59695/49789
            var decryptor = RSA.Create();
            decryptor.ImportPublicKeyFromPem(publicKey);
            var decryptedText = Encoding.UTF8.GetString(decryptor.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.OaepSHA512));
            Console.WriteLine(decryptedText);

            // will work
            var decryptorThatWorks = RSA.Create();
            decryptor.ImportPrivateKeyFromPem(privateKey);
            var decryptedText2 = Encoding.UTF8.GetString(decryptor.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.OaepSHA512));
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
