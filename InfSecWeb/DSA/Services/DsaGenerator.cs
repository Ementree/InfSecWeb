using System;
using InfSecWeb.DSA.Dtos;
using InfSecWeb.Services;
using BigInteger = System.Numerics.BigInteger;
using BigMathInteger = BigMath.BigInteger;

namespace InfSecWeb.DSA.Services
{
    public class DsaGenerator
    {
        private readonly PrimeNumberGenerator _primeNumberGenerator;
        private readonly StringFormatter _stringFormatter;

        public DsaGenerator(PrimeNumberGenerator primeNumberGenerator, StringFormatter stringFormatter)
        {
            _primeNumberGenerator = primeNumberGenerator;
            _stringFormatter = stringFormatter;
        }

        public DsaParameters GenerateParameters()
        {
            var rnd = new Random();
            var q = BigInteger.Parse(BigMathInteger.ProbablePrime(20, rnd).ToString());
            var p = new BigInteger(4);
            var index = 1;
            while (!_primeNumberGenerator.IsPrime(p))
            {
                p = index * q + 1;
                index++;
            }

            var g = BigInteger.One;
            for (BigInteger h = 2; h < p - 1; h++)
            {
                g = BigInteger.ModPow(h, (p - 1) / q, p);
                if (g > 1) break;
            }

            var x = new BigInteger(rnd.Next(1, int.Parse(q.ToString())));
            var y = BigInteger.ModPow(g, x, p);
            return new DsaParameters()
            {
                P = _stringFormatter.ToStringFormat(BitConverter.GetBytes(ulong.Parse(p.ToString()))),
                G = _stringFormatter.ToStringFormat(BitConverter.GetBytes(ulong.Parse(g.ToString()))),
                Q = _stringFormatter.ToStringFormat(BitConverter.GetBytes(ulong.Parse(q.ToString()))),
                Y = _stringFormatter.ToStringFormat(BitConverter.GetBytes(ulong.Parse(y.ToString()))),
                X = _stringFormatter.ToStringFormat(BitConverter.GetBytes(ulong.Parse(x.ToString())))
            };
        }
    }
}