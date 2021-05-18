using System.Numerics;

namespace InfSecWeb.RSA.Dtos
{
    public class RsaEncryptedDto
    {
        public ulong P { get; set; }
        public ulong Q { get; set; }
        public ulong E { get; set; }
        public string Message { get; set; }
    }
}