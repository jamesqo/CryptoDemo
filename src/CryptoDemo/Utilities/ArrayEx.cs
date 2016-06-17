using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CryptoDemo.Utilities
{
    internal static class ArrayEx
    {
        public static void Copy<T>(T[] src, int srcIndex, T[] dest, int destIndex, int length)
        {
            // Specialize primitive types for perf
            if (IsPrimitive<T>())
            {
                Buffer.BlockCopy(src, srcIndex, dest, destIndex, length * Unsafe.SizeOf<T>());
            }
            else
            {
                Array.Copy(src, srcIndex, dest, destIndex, length);
            }
        }

        public static T[] Clone<T>(T[] original)
        {
            var result = new T[original.Length];
            Copy(original, 0, result, 0, original.Length);
            return result;
        }

        // Will be JIT'd to a const for a given T
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsPrimitive<T>()
        {
            return default(T) != null &&
                (typeof(T) == typeof(byte) ||
                typeof(T) == typeof(sbyte) ||
                typeof(T) == typeof(short) ||
                typeof(T) == typeof(ushort) ||
                typeof(T) == typeof(int) ||
                typeof(T) == typeof(uint) ||
                typeof(T) == typeof(long) ||
                typeof(T) == typeof(ulong) ||
                typeof(T) == typeof(float) ||
                typeof(T) == typeof(double));
        }
    }
}
