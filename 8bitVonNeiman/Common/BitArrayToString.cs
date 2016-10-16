using System.Collections;
using System.Linq;
using System.Text;

namespace _8bitVonNeiman.Common {
    public static class BitArrayToString {
        public static string ToDigitString(this BitArray array) {
            var builder = new StringBuilder();
            foreach (var bit in array.Cast<bool>())
                builder.Append(bit ? "1" : "0");
            return builder.ToString();
        }
    }
}
