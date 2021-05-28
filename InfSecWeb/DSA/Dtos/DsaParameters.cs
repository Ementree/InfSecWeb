namespace InfSecWeb.DSA.Dtos
{
    public class DsaParameters
    {
        public string P { get; set; }
        public string Q { get; set; }
        public string G { get; set; }
        public string Y { get; set; }
        public string X { get; set; }
        public string Message { get; set; }
        public string MessageHash { get; set; }
        public string ChangedMessage { get; set; }
        public string R { get; set; }
        public string S { get; set; }
    }
}