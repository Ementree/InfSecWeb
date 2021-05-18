namespace InfSecWeb.ElGamal.Dtos
{
    public class ElGamalParameters
    {
        public ulong P { get; set; }
        public ulong A { get; set; }
        public ulong X { get; set; }
        public ulong Y { get; set; }
                
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }

        public ElGamalParameters(ulong p, ulong a, ulong x, ulong y)
        {
            P = p;
            A = a;
            X = x;
            Y = y;
        }
    }
}