using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Utilities.Pooling
{
    internal struct ArrayLease<T> : IDisposable
    {
        private T[] _rented;
        private ArrayPool<T> _owner;

        public ArrayLease(T[] rented, ArrayPool<T> owner)
        {
            _rented = rented;
            _owner = owner;
        }

        public T[] Array => _rented;

        public void Dispose()
        {
            try
            {
                // TODO: Add support for passing in clearArray
                _owner.Return(_rented);
            }
            finally
            {
                _owner = null;
                _rented = null;
            }
        }
    }
}
