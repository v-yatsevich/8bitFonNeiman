using System.Collections;
using System.Collections.Generic;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Compiler {
    public interface ICompilerControllerOutput {
        void MemoryFormed(Dictionary<int, ExtendedBitArray> memory);
    }
}
