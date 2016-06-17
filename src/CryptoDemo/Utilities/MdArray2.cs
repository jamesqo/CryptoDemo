using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Utilities
{
    // 2-dimensional wrapper around an SZ array
    internal struct MdArray2<T>
    {
        private readonly int _length1;
        private readonly T[] _inner;

        public MdArray2(T[] inner, int length1)
        {
            _inner = inner;
            _length1 = length1;
        }

        public T this[int index1, int index2]
        {
            get { return _inner[ComputeIndex(index1, index2)]; }
            set { _inner[ComputeIndex(index1, index2)] = value; }
        }

        private int ComputeIndex(int index1, int index2)
        {
            return (_length1 * index1) + index2;
        }
    }
}
