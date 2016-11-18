using System.Collections;
using System.Collections.Generic;

namespace _8bitVonNeiman.Compiler {
    public interface ICompilerControllerOutput {
        void MemoryFormed(Dictionary<int, BitArray> memory);
    }
}
