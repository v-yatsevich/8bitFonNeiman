using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Compiler {
    public interface ICompilerControllerOutput {
        void MemoryFormed(Dictionary<int, BitArray> memory);
    }
}
