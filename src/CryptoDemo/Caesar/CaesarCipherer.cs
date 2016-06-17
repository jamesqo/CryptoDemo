using CryptoDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Caesar
{
    public class CaesarCipherer : ICipherer<byte[]>
    {
        public byte[] Cipher(byte[] original, byte[] key)
        {
            if (original == null || key == null)
                throw new ArgumentNullException(original == null ? nameof(original) : nameof(key));

            if (key.Length != 1)
                throw new ArgumentException("The key for a Caesar cipher should only be length 1 (for the byte amt. to shift).", nameof(key));

            byte addend = key[0];

            var copy = ArrayEx.Clone(original);
            
            for (int i = 0; i < copy.Length; i++)
            {
                copy[i] += addend;
            }

            return copy;
        }
    }
}
