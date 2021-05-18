using System;
using System.Numerics;
using InfSecWeb.ElGamal.Dtos;
using InfSecWeb.Services;

namespace InfSecWeb.ElGamal.Services
{
    public class ElGamalParametersGenerator
    {
        private readonly PrimeNumberGenerator _primeNumberGenerator;

        public ElGamalParametersGenerator(PrimeNumberGenerator primeNumberGenerator)
        {
            _primeNumberGenerator = primeNumberGenerator;
        }

        public ElGamalParameters Generate()
        {
            var rnd = new Random();
            var p = (ulong) BigMath.BigInteger.ProbablePrime(12, rnd);
            return Set(p);
        }
        
        public ElGamalParameters Set(ulong p)
        {
            var rnd1 = new Random();
            var rnd2 = new Random();
            var symbolsCount = p.ToString().Length*2;
            var a = p + 1;
            while (a > p)
            {
                a = (ulong)BigMath.BigInteger.ProbablePrime(symbolsCount,rnd1);
            }
            
            var x = p + 1;
            while (x > p)
            {
                x = (ulong)BigMath.BigInteger.ProbablePrime(symbolsCount,rnd2);
            }

            var y = (ulong)BigInteger.ModPow(a, x, p);
            return new ElGamalParameters(p, a, x, y);
        } 
    }
}