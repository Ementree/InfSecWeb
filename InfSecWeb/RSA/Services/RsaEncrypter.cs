using System;
using System.Numerics;
using System.Text;
using InfSecWeb.RSA.Dtos;

namespace InfSecWeb.Services
{
    public class RsaEncrypter
    {
        public string Encrypt(RsaEncryptedDto dto)
        {
            var stringBuilder = new StringBuilder();
            var length = dto.Message.Length;
            var n = dto.P * dto.Q;
            for (int i = 0; i < length - 1; i++)
            {
                var val = Convert.ToInt32(dto.Message[i]);
                var encrypted = BigInteger.ModPow(dto.Message[i], dto.E, n);
                var bytes = BitConverter.GetBytes((ulong)encrypted);
                
                stringBuilder.Append(BitConverter.ToString(BitConverter.GetBytes((ulong)encrypted)));
                stringBuilder.Append('-');
            }
            stringBuilder.Append(
                BitConverter.ToString(
                    BitConverter.GetBytes(
                        (ulong)BigInteger.ModPow(dto.Message[length - 1], dto.E, n)))); 
            return stringBuilder.ToString();
        }
        
        public string Decrypt(RsaDecryptedDto dto)
        {
            var array = dto.Message.Split('-');
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i += 8)
            {
                var bytes = new byte[8];
                for (int j = 0; j < 8; j++)
                {
                    bytes[j] = (byte)Convert.ToInt32(array[i + j], 16);
                }

                var value = BitConverter.ToUInt64(bytes, 0);
                var n = dto.P * dto.Q;
                var decrypted = BigInteger.ModPow(value, dto.D, n);
                
                var symbol = Convert.ToChar((ulong)decrypted);
                stringBuilder.Append(symbol);
            }
            return stringBuilder.ToString();
        }
    }
}