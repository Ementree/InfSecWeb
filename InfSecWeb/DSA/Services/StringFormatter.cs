using System;
using System.Linq;
using BigInteger = System.Numerics.BigInteger;

namespace InfSecWeb.DSA.Services
{
    public class StringFormatter
    {
        public string ToStringFormat(byte[] bytes)
        {
            var hexString = BitConverter.ToString(bytes.ToArray());
            hexString = hexString.Replace("-", " ");
            return hexString;
        }
        
        public byte[] FromStringFormat(string input)
        {
            var strings = input
                .Split(" ");
            return strings
                .Select(x => Convert.ToByte(x, 16))
                .ToArray();
        }
        
        public BigInteger BytesToBigint(byte[] bytes)
        {
            if ((bytes[bytes.Length - 1] & 0x80) > 0)
            {
                var temp = new byte[bytes.Length];
                Array.Copy(bytes, temp, bytes.Length);
                bytes = new byte[temp.Length + 1];
                Array.Copy(temp, bytes, temp.Length);
            }

            return new BigInteger(bytes);
        }
    }
}