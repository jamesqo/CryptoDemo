using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Utilities
{
    // Allocation-free concat of 2 arrays
    internal struct JoinedArray<T>
    {
        private readonly T[] _array1;
        private readonly T[] _array2;

        public JoinedArray(T[] array1, T[] array2)
        {
            Debug.Assert(array1 != null);
            Debug.Assert(array2 != null);

            _array1 = array1;
            _array2 = array2;
        }

        public T this[int index]
        {
            get
            {
                if (index < _array1.Length)
                    return _array1[index];
                return _array2[index - _array1.Length];
            }
            set
            {
                if (index < _array1.Length)
                    _array1[index] = value;
                _array2[index - _array1.Length] = value;
            }
        }
    }
}
