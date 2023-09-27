using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SuperCroods.Skeleton.Security.Cipher
{
    public class RijndaelSymmetricCipher : ISymmetricCipher
    {
        private ICipherParameter parameter;
        private ICipherProvider provider;

        public RijndaelSymmetricCipher(ICipherParameter parameter, ICipherProvider provider)
        {
            this.parameter = parameter;
            this.provider = provider;
        }

        public string Decrypt(string arg)
        {
            return InternalDecrypt(arg);
        }

        public string Encrypt(string arg)
        {
            return InternalEncrypt(arg);
        }

        private string InternalEncrypt(string arg)
        {
            string result = String.Empty;

            if (parameter.Plaintext.Length > 0)
            {
                byte[] saltBytes =
                    provider.GetCryptoBytes(new byte[16]);

                byte[] initVectorBytes =
                    provider.GetCryptoBytes(new byte[16]);

                byte[] passPhraseBytes =
                    Encoding.UTF8.GetBytes(arg);

                var deriveBytes =
                    new Rfc2898DeriveBytes(
                      parameter.Plaintext,
                      saltBytes,
                      derivationIteration
                    );

                using (var symmetric = new RijndaelManaged()
                {
                    BlockSize = parameter.CipherSize.Value,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                })
                {
                    using (var encryptor = symmetric.CreateEncryptor(
                        deriveBytes.GetBytes(parameter.CipherSize.Value / 4),
                        initVectorBytes))
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        CryptoStream cryptoStream = new CryptoStream(
                            memoryStream, encryptor, CryptoStreamMode.Write);
                        cryptoStream.Write(passPhraseBytes, 0, passPhraseBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        result = Convert.ToBase64String(
                            saltBytes.Concat(initVectorBytes).ToArray()
                            .Concat(memoryStream.ToArray()).ToArray());
                        memoryStream.Close();
                        cryptoStream.Close();
                    }
                }
                deriveBytes.Dispose();
            }

            return result;
        }

        private string InternalDecrypt(string arg)
        {
            string result = String.Empty;

            if (parameter.Plaintext.Length > 0)
            {
                byte[] cipherTextBytesWithSaltAndIv =
                    Convert.FromBase64String(arg);

                byte[] saltBytes =
                    cipherTextBytesWithSaltAndIv
                        .Take(parameter.CipherSize.Value / 8).ToArray();

                byte[] initVectorBytes =
                    cipherTextBytesWithSaltAndIv
                        .Skip(parameter.CipherSize.Value / 8)
                        .Take(parameter.CipherSize.Value / 8).ToArray();

                byte[] cipherTextBytes =
                    cipherTextBytesWithSaltAndIv
                    .Skip((parameter.CipherSize.Value / 8) * 2)
                    .Take(cipherTextBytesWithSaltAndIv.Length -
                            ((parameter.CipherSize.Value / 8) * 2)).ToArray();

                var deriveBytes = new Rfc2898DeriveBytes(
                    parameter.Plaintext,
                    saltBytes,
                    derivationIteration);

                using (var symmetric = new RijndaelManaged()
                {
                    BlockSize = parameter.CipherSize.Value,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                })
                {
                    using (var decryptor = symmetric.CreateDecryptor(
                        deriveBytes.GetBytes(parameter.CipherSize.Value / 4),
                        initVectorBytes))
                    {
                        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                        CryptoStream cryptoStream = new CryptoStream(
                                memoryStream,
                                decryptor,
                                CryptoStreamMode.Read);
                        var plainTextBytes = new byte[cipherTextBytes.Length];

                        result = Encoding.UTF8.GetString(
                                    plainTextBytes,
                                    0,
                                    cryptoStream.Read(
                                        plainTextBytes,
                                        0,
                                        plainTextBytes.Length));

                        cryptoStream.Close();
                        memoryStream.Close();
                    }
                }
                deriveBytes.Dispose();
            }

            return result;
        }

        private readonly int derivationIteration = 1000;

    }
}
