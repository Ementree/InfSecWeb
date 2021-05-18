namespace InfSecWeb.ElGamal.Dtos
{
    public class ElGamalDecryptedDto
    {
        public ulong P { get; set; }
        public ulong X { get; set; }
        public string Message { get; set; }
    }
}