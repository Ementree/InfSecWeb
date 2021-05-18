using System.Numerics;

namespace InfSecWeb.RSA.Dtos
{
    public class RsaDto
    {
        public ulong P { get; set; }
        public ulong Q { get; set; }
        public ulong E { get; set; }
    }
}