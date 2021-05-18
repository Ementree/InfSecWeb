using System.Numerics;
using InfSecWeb.RSA;

namespace InfSecWeb.Services
{
    public class RsaParametersGenerator
    {
        private readonly PrimeNumberGenerator _primeNumberGenerator;

        public RsaParametersGenerator(PrimeNumberGenerator primeNumberGenerator)
        {
            _primeNumberGenerator = primeNumberGenerator;
        }

        public RsaParameters GenerateParameters()
        {
            var parameters = new RsaParameters();
            parameters.P = _primeNumberGenerator.GeneratePrimeNumber(10000);
            parameters.Q = _primeNumberGenerator.GeneratePrimeNumber(10000);
            var n = parameters.P * parameters.Q;
            if (n < 0)
            {
                parameters.ErrorMessage = "P or Q is too big";
                parameters.Error = true;
                return parameters;
            }
            parameters.E = GenerateE(n);
            parameters.D = (ulong)parameters.CalculateD();

            return parameters;
        }
        
        private ulong GenerateE(ulong n)
        {
            ulong e;
            while (true)
            {
                e = _primeNumberGenerator.GeneratePrimeNumber(100);
                if (n % e != 0)
                    break;
            }

            return e;
        }
    }
}