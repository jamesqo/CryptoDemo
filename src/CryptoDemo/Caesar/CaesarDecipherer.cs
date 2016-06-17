using CryptoDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Caesar
{
    public class CaesarDecipherer : ICipherer<byte[]>
    {
        public byte[] Cipher(byte[] original, byte[] key)
        {
            if (original == null || key == null)
                throw new ArgumentNullException(original == null ? nameof(original) : nameof(key));

            if (key.Length != 1)
                throw new ArgumentException("The key for a Caesar cipher should only be length 1 (for the byte amt. to shift).", nameof(key));

            byte minus = key[0];

            var copy = ArrayEx.Clone(original);

            for (int i = 0; i < copy.Length; i++)
            {
                copy[i] -= minus;
            }

            return copy;
        }
    }
}
