using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Utilities.Pooling
{
    internal static class ArrayPoolExtensions
    {
        public static ArrayLease<T> Lease<T>(this ArrayPool<T> pool, int minimumLength)
        {
            return new ArrayLease<T>(pool.Rent(minimumLength), pool);
        }
    }
}
