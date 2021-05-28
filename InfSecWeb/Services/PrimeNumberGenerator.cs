using System;
using System.Collections.Generic;
using System.Numerics;

namespace InfSecWeb.Services
{
    public class PrimeNumberGenerator
    {
        public ulong GeneratePrimeNumber(BigInteger max)
        {
            var primeNumbers = new List<ulong>();
            for (ulong i = 2; i <= max; i++)
            {
                if (IsPrime(i))
                {
                    primeNumbers.Add(i);
                }
            }
            var rnd = new Random();
            var index = rnd.Next(0, primeNumbers.Count - 1);
            return primeNumbers[index];
        }

        public bool IsPrime(BigInteger bigInteger)
        {
            var upper = BigInteger.Divide(bigInteger, 2) + 1;
            for (BigInteger i = 2; i < upper; i++)
            {
                BigInteger.DivRem(bigInteger, i, out var r);
                if (r == BigInteger.Zero)
                    return false;
            }

            return true;
        }
    }
}