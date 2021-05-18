using System.Numerics;

namespace InfSecWeb.Services
{
    public static class CalculateDService
    {
        public static BigInteger CalculateD(BigInteger e, BigInteger fi)
        {
            for (int i = 2;; i++)
            {
                var value = (i * e) % fi;
                if (value == 1)
                    return i;
            }
        }
    }
}