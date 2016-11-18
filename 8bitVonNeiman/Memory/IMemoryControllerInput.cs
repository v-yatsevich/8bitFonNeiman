using System.Collections;
using System.Collections.Generic;

namespace _8bitVonNeiman.Memory {
    public interface IMemoryControllerInput {
        void ChangeState();
        void SetMemory(Dictionary<int, BitArray> memory);
    }
}
