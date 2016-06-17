using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CryptoDemo.Playfair
{
    internal static class AllBytesEnumerable
    {
        private static IEnumerable<byte> s_value;

        public static IEnumerable<byte> Value =>
            s_value ?? (s_value = InitializeValue());

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static IEnumerable<byte> InitializeValue()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                yield return (byte)i;
            }
        }
    }
}
