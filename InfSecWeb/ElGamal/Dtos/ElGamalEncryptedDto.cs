namespace InfSecWeb.ElGamal.Dtos
{
    public class ElGamalEncryptedDto
    {
        public ulong P { get; set; }
        public ulong A { get; set; }
        public ulong Y { get; set; }
        public string Message { get; set; }
    }
}