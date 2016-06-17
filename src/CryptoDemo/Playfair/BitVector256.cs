using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Playfair
{
    // Compact storage of 256 bools
    internal unsafe struct BitVector256
    {
        private fixed int _data[8];

        public bool this[int index]
        {
            get
            {
                int offset = (byte)index >> 5;
                int mask = 1 << (index & 31);
                fixed (int* ip = _data)
                    return (ip[offset] & mask) == mask;
            }
            set
            {
                int offset = (byte)index >> 5;
                int mask = 1 << (index & 31);
                fixed (int* ip = _data)
                {
                    if (value)
                    {
                        ip[offset] |= mask;
                    }
                    else
                    {
                        ip[offset] &= ~mask;
                    }
                }
            }
        }

        // TODO: Refactor w/ ref returns once
        // C# 7 comes around
        public int GetInt(int index)
        {
            fixed (int* ip = _data)
                return ip[index];
        }

        public void SetInt(int index, int value)
        {
            fixed (int* ip = _data)
                ip[index] = value;
        }
    }
}
