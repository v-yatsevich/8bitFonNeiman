using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Memory {
    public interface IMemoryControllerInput {
        void ChangeState();
        void SetMemory(Dictionary<int, BitArray> memory);
    }
}
