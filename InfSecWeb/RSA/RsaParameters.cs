using System.Numerics;
using InfSecWeb.Services;

namespace InfSecWeb.RSA
{
    public class RsaParameters
    {
        public RsaParameters()
        {
            
        }
        public RsaParameters(ulong p, ulong q, ulong e)
        {
            P = p;
            Q = q;
            E = e;

            var fi = (p - 1) * (q - 1);
            if (e > fi || fi % e == 0)
            {
                ErrorMessage = "Incorrect E";
                Error = true;
            }
            
            D = (ulong)CalculateD();
        }

        public ulong P { get; set; }
        public ulong Q { get; set; }
        public ulong E { get; set; }
        public ulong D { get; set; }
        
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        
        public BigInteger CalculateD()
        {
            BigInteger d = 0;
            while (d < 2)
            {
                d = ModInverse(E, (P - 1) * (Q - 1));
            }

            return d;
        }

        private BigInteger ModInverse(BigInteger e, BigInteger fi)
        {
            var i = fi;
            BigInteger v = 0;
            BigInteger d = 1;
            while (e > 0)
            {
                var t = i / e;
                var x = e;
                e = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }

            v %= fi;
            if (v < 0)
                v = (v + fi) % fi;
            return v;
        }
    }
}