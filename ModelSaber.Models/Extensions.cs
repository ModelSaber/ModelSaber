using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ModelSaber.Models
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var provider = new RNGCryptoServiceProvider();
            var list = enumerable.ToArray();
            var n = list.Length;
            while (n > 1)
            {
                var box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                var k = box[0] % n;
                n--;
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static bool ToBool(this bool? b) => b.HasValue && b.Value;
    }
}
