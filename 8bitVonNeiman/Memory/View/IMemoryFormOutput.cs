using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Memory.View {
    public interface IMemoryFormOutput {
        void FormClosed();
        void ClearMemoryClicked();
        void MemoryChanged(int row, int collumn, string s);
    }
}
