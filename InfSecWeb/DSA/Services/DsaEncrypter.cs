using System;
using System.Linq;
using System.Security.Cryptography;
using InfSecWeb.DSA.Dtos;
using BigInteger = System.Numerics.BigInteger;

namespace InfSecWeb.DSA.Services
{
    public class DsaEncrypter
    {
        private readonly StringFormatter _stringFormatter;

        public DsaEncrypter(StringFormatter stringFormatter)
        {
            _stringFormatter = stringFormatter;
        }

        public DsaParameters Sign(DsaParameters parameters)
        {
            var rnd = new Random();
            var q = BigInteger.Parse(BitConverter.ToUInt64(
                _stringFormatter.FromStringFormat(parameters.Q)).ToString());
            var g = BigInteger.Parse(BitConverter.ToUInt64(
                _stringFormatter.FromStringFormat(parameters.G)).ToString());
            var p = BigInteger.Parse(BitConverter.ToUInt64(
                _stringFormatter.FromStringFormat(parameters.P)).ToString());
            var x = BigInteger.Parse(BitConverter.ToUInt64(
                _stringFormatter.FromStringFormat(parameters.X)).ToString());

            var k = q > int.MaxValue
                ? rnd.Next(2, int.MaxValue)
                : rnd.Next(2, int.Parse(q.ToString()));

            var message = parameters.Message;
            var messageBytes = message.Select(BitConverter.GetBytes).SelectMany(x => x).ToArray();
            var messageHash = SHA1.HashData(messageBytes);
            var messageHashBigint = _stringFormatter.BytesToBigint(messageHash);

            var part1 = BigInteger.ModPow(k, q - 2, q);
            var r = BigInteger.ModPow(BigInteger.ModPow(g, k, p), 1, q);
            var s = BigInteger.ModPow(part1 * (messageHashBigint + x * r), 1, q);
            parameters.R = _stringFormatter.ToStringFormat(BitConverter.GetBytes(Convert.ToInt64(r.ToString())));
            parameters.S = _stringFormatter.ToStringFormat(BitConverter.GetBytes(Convert.ToInt64(s.ToString())));
            parameters.MessageHash = _stringFormatter.ToStringFormat(messageHash);
            return parameters;
        }
        
        public bool CheckSign(DsaParameters parameters)
        {
            var q = BigInteger.Parse(
                BitConverter.ToUInt64(
                    _stringFormatter.FromStringFormat(parameters.Q)).ToString());
            var g = BigInteger.Parse(
                BitConverter.ToUInt64(
                    _stringFormatter.FromStringFormat(parameters.G)).ToString());
            var p = BigInteger.Parse(
                BitConverter.ToUInt64(
                    _stringFormatter.FromStringFormat(parameters.P)).ToString());
            var s = BigInteger.Parse(
                BitConverter.ToUInt64(
                    _stringFormatter.FromStringFormat(parameters.S)).ToString());
            var r = BigInteger.Parse(
                BitConverter.ToUInt64(
                    _stringFormatter.FromStringFormat(parameters.R)).ToString());
            var y = BigInteger.Parse(
                BitConverter.ToUInt64(
                    _stringFormatter.FromStringFormat(parameters.Y)).ToString());
            
            var messageBytes = parameters.ChangedMessage.Select(BitConverter.GetBytes).SelectMany(x => x).ToArray();
            var messageHash = SHA1.HashData(messageBytes);

            var afterShaBigint = _stringFormatter.BytesToBigint(messageHash);

            var w = BigInteger.ModPow(s, q - 2, q);
            var u1 = BigInteger.ModPow(afterShaBigint * w, 1, q);
            var u2 = BigInteger.ModPow(r * w, 1, q);

            var v = BigInteger.ModPow(
                BigInteger.ModPow(BigInteger.ModPow(g, u1, p) * BigInteger.ModPow(y, u2, p), 1, p), 1, q);

            var isCheckTrue = v == r;
            return isCheckTrue;
        }
    }
}