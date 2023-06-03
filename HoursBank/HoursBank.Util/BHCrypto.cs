using System.Security.Cryptography;
using System.Text;

namespace HoursBank.Util
{
    public static class BHCrypto
    {
        private static string _key = "NiJOMyevhbLr52.kQ7fVcxMG";

        public static string Encode(string str)
        {
            return Encrypt(str);
        }

        public static string Decode(string str)
        {
            return Decrypt(str);
        }

        private static string Encrypt(string str)
        {
            try
            {
                byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(str);

#pragma warning disable SYSLIB0021 // O tipo ou membro é obsoleto
                using (MD5CryptoServiceProvider MD5CryptoService = new MD5CryptoServiceProvider())
                {
                    byte[] SecurityKeyArray = MD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));

                    MD5CryptoService.Clear();

                    var TripleDESCryptoService = Aes.Create();
                    TripleDESCryptoService.Key = SecurityKeyArray;
                    TripleDESCryptoService.Mode = CipherMode.ECB;
                    TripleDESCryptoService.Padding = PaddingMode.PKCS7;

                    var CrytpoTransform = TripleDESCryptoService.CreateEncryptor();

                    byte[] resultArray = CrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);

                    TripleDESCryptoService.Clear();

                    return System.Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
#pragma warning restore SYSLIB0021 // O tipo ou membro é obsoleto
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static string Decrypt(string str)
        {
            try
            {
                byte[] toEncryptArray = System.Convert.FromBase64String(str);

#pragma warning disable SYSLIB0021 // O tipo ou membro é obsoleto
                using (MD5CryptoServiceProvider MD5CryptoService = new MD5CryptoServiceProvider())
                {
                    byte[] SecurityKeyArray = MD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));

                    MD5CryptoService.Clear();

                    var TripleDESCryptoService = Aes.Create();
                    TripleDESCryptoService.Key = SecurityKeyArray;
                    TripleDESCryptoService.Mode = CipherMode.ECB;
                    TripleDESCryptoService.Padding = PaddingMode.PKCS7;

                    var CrytpoTransform = TripleDESCryptoService.CreateDecryptor();

                    byte[] resultArray = CrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    TripleDESCryptoService.Clear();

                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
#pragma warning restore SYSLIB0021 // O tipo ou membro é obsoleto
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}


