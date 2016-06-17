using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Utilities
{
    internal struct WrappedArray<T>
    {
        private readonly T[] _array;

        public WrappedArray(T[] array)
        {
            _array = array;
        }

        public T this[int index]
        {
            get { return _array[index % _array.Length]; }
            set { _array[index % _array.Length] = value; }
        }
    }
}
