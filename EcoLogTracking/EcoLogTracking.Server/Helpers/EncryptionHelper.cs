using System.Security.Cryptography;
using System.Text;

namespace todoAPI.Helpers
{
    public sealed class EncryptionHelper
    {
        private const string _secret = "fwr8734rf46ef84ser86f46se84fs598";

        public string Encrypt(string data)
        {
            var clearBytes = Encoding.Unicode.GetBytes(data);

            using (var encryptor = Aes.Create())
            {
                byte[] IV = new byte[15];

                new Random().NextBytes(IV);

                var pdb = new Rfc2898DeriveBytes(_secret, IV);

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);

                        cs.Close();
                    }

                    var result = Convert.ToBase64String(IV) + Convert.ToBase64String(ms.ToArray());

                    return result;
                }
            }
        }

        public string Decrypt(string data)
        {
            var IV = Convert.FromBase64String(data.Substring(0, 20));

            data = data.Substring(20).Replace(" ", "+");

            var cipherBytes = Convert.FromBase64String(data);

            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(_secret, IV);

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);

                        cs.Close();
                    }

                    var result = Encoding.Unicode.GetString(ms.ToArray());

                    return result;
                }
            }
        }
    }
}
