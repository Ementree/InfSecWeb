using System.Numerics;

namespace InfSecWeb.RSA.Dtos
{
    public class RsaDecryptedDto
    {
        public ulong P { get; set; }
        public ulong Q { get; set; }
        public ulong D { get; set; }
        public string Message { get; set; }
    }
}