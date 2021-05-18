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
                if (isSimple(i))
                {
                    primeNumbers.Add(i);
                }
            }
            var rnd = new Random();
            var index = rnd.Next(0, primeNumbers.Count - 1);
            return primeNumbers[index];
        }
        
        private bool isSimple(BigInteger num)
        {
            for (int i = 2; i < (num / 2); i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }
    }
}