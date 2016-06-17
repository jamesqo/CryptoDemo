using CryptoDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Playfair
{
    public class PlayfairCipherer : ICipherer<byte[]>
    {
        public byte[] Cipher(byte[] original, byte[] key)
        {
            if (original == null || key == null)
                throw new ArgumentNullException(original == null ? nameof(original) : nameof(key));

            // TODO: Better performance
            var distinct = key
                .Concat(AllBytesEnumerable.Value)
                .Distinct()
                .ToArray();

            Debug.Assert(distinct.Length == byte.MaxValue + 1);

            var table = new MdArray2<byte>(distinct, 16);

            var copy = ArrayEx.Clone(original);

            for (int i = 0; i + 1 < copy.Length; i += 2)
            {
                byte l = copy[i];
                byte r = copy[i + 1];


            }
        }
    }
}
