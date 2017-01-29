using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace _8bitVonNeiman.Common {
    public static class EntendedBitArray {
        public static string ToDigitString(this BitArray array) {
            var builder = new StringBuilder();
            foreach (var bit in array.Cast<bool>()) {
                builder.Insert(0, bit ? '1' : '0');
            }
            return builder.ToString();
        }

        public static string ToHexString(this BitArray array) {
            var s = array.ToDigitString();
            return Convert.ToInt32(s, 2).ToString("X2");
        }
    }
}
