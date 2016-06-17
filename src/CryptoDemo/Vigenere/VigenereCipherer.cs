using CryptoDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Vigenere
{
    public class VigenereCipherer : ICipherer<byte[]>
    {
        public byte[] Cipher(byte[] original, byte[] key)
        {
            if (original == null || key == null)
                throw new ArgumentNullException(original == null ? nameof(original) : nameof(key));

            var wrapped = new WrappedArray<byte>(key);

            var copy = ArrayEx.Clone(original);

            for (int i = 0; i < original.Length; i++)
            {
                copy[i] += wrapped[i];
            }

            return copy;
        }
    }
}
