using System;
using System.Numerics;
using System.Text;
using InfSecWeb.ElGamal.Dtos;
using InfSecWeb.Services;

namespace InfSecWeb.ElGamal.Services
{
    public class ElGamalEncrypter
    {
        private readonly PrimeNumberGenerator _primeNumberGenerator;

        public ElGamalEncrypter(PrimeNumberGenerator primeNumberGenerator)
        {
            _primeNumberGenerator = primeNumberGenerator;
        }

        public string Encrypt(ElGamalEncryptedDto dto)
        {
            var sb = new StringBuilder();
            var k = _primeNumberGenerator.GeneratePrimeNumber(dto.P - 1);
            while ((dto.P - 1) % k == 0 || k >= int.MaxValue)
            {
                k = _primeNumberGenerator.GeneratePrimeNumber(dto.P - 1);
            }
            foreach (var symbol in dto.Message)
            {
                var a = BigInteger.ModPow(dto.A, k, dto.P);
                var b = BigInteger.Pow(dto.Y, (int)k)*symbol % dto.P;
                sb.Append($"-{a}-{b}");
            }

            return sb.ToString().Substring(1);
        }
        
        public string Decrypt(ElGamalDecryptedDto dto)
        {
            var sb = new StringBuilder();
            var arr = dto.Message.Split('-');
            for (int i = 0; i < arr.Length; i += 2)
            {
                var a = Convert.ToUInt64(arr[i]);
                var b = Convert.ToUInt64(arr[i+1]);
                var decrypted = b * BigInteger.Pow(a, (int) (dto.P - 1 - dto.X)) % dto.P;
                sb.Append(Convert.ToChar((ulong)decrypted));
            }
            
            return sb.ToString();
        }
    }
}