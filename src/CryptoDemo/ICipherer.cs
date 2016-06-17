using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo
{
    public interface ICipherer<T>
    {
        T Cipher(T original, T key);
    }
}
