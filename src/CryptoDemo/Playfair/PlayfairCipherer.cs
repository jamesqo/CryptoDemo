using CryptoDemo.Utilities;
using CryptoDemo.Utilities.Pooling;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDemo.Playfair
{
    public class PlayfairCipherer : ICipherer<byte[]>
    {
        public byte[] Cipher(byte[] original, byte[] key)
        {
            if (original == null || key == null)
                throw new ArgumentNullException(original == null ? nameof(original) : nameof(key));

            // GOAL: Create a table with the sequence of
            // distinct bytes in key, followed by the rest
            // of the bytes from 0..255. For example, if
            // key was [3 5 5 7 244], then table would be
            // [3 5 7 244 0 1 2 4 6 .. 255]

            // TODO: Short-circuit for empty arrays

            // vector is used to keep track of bytes
            // found in the key
            var vector = new BitVector256();
            var pool = ArrayPool<byte>.Shared;

            using (var lease1 = pool.Lease(256))
            using (var lease2 = pool.Lease(256))
            {
                // lookup: maps bytes -> index in table
                // if table[i] == b, then lookup[b] == i
                byte[] lookup = lease1.Array;
                byte[] table = lease2.Array;

                // First add all of the distinct bytes
                // from the key
                int index = 0;
                foreach (byte b in key)
                {
                    if (!vector[b])
                    {
                        vector[b] = true;
                        table[index] = b;
                        lookup[b] = (byte)index++;
                    }
                }

                // Then add the rest of the bytes from 0..255
                for (int i = 0; i < 256; i++)
                {
                    if (!vector[i])
                    {
                        table[index] = (byte)i;
                        lookup[i] = (byte)index++;
                    }
                }

                Debug.Assert(index == 256, "We should have iterated through all the bytes");

                var copy = ArrayEx.Clone(original);

                // Go through the array, 2 bytes at a time
                for (int i = 0; i + 1 < copy.Length; i += 2)
                {
                    byte left = copy[i];
                    byte right = copy[i + 1];

                    byte index1 = lookup[left];
                    byte index2 = lookup[right];

                    var relationship = DetermineSpatialRelationship(index1, index2);

                    switch (relationship)
                    {
                        case SpatialRelationship.SameColumn:
                            // Take the byte to the bottom of each one (wrap if necessary)
                            copy[i] = table[(index1 + 16) % 256];
                            copy[i + 1] = table[(index2 + 16) % 256];
                            break;
                        case SpatialRelationship.SameRow:
                            // Take the byte to the right of each one (wrap if necessary)
                            int @base = index1 & ~15;
                            Debug.Assert(@base == (index2 & ~15));

                            int offset1 = (index1 + 1) % 16;
                            int offset2 = (index2 + 1) % 16;
                            copy[i] = table[@base + offset1];
                            copy[i + 1] = table[@base + offset2];
                            break;
                        case SpatialRelationship.Neither:
                            // Form a rectangle with the two bytes and take
                            // the other two corners (top-right then bottom-left)
                            int x1 = index1 / 16;
                            int y1 = index1 & ~15;

                            int x2 = index2 / 16;
                            int y2 = index2 & ~15;

                            int index3 = y1 + x2;
                            int index4 = x1 + y2;

                            copy[i] = table[index3];
                            copy[i + 1] = table[index4];
                            break;
                        default:
                            Debug.Fail($"Should be one of the {nameof(SpatialRelationship)} values");
                            break;
                    }
                }

                // TODO: For now, the last byte is ignored,
                // if the input array.Length is odd.
                // Find a way to refactor that ^ gigantic
                // code block into a new function, or wait
                // until C# 7 introduces local functions.
                return copy;
            }
        }

        private static SpatialRelationship DetermineSpatialRelationship(byte index1, byte index2)
        {
            // Suppose bytes were only 4 bits in size. Our table would look like this (index-wise):
            //  0  1  2  3
            //  4  5  6  7
            //  8  9 10 11
            // 12 13 14 15

            // If their % 4 is equal, they're in the same column.
            // If their floor of / 4 is equal, they're in the same row.
            // Otherwise, they're neigther.

            if (index1 % 16 == index2 % 16)
            {
                return SpatialRelationship.SameColumn;
            }
            if (index1 / 16 == index2 / 16)
            {
                return SpatialRelationship.SameRow;
            }
            return SpatialRelationship.Neither;
        }
    }
}
