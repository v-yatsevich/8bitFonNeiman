using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Cpu.View {
    public interface ICpuFormOutput {
        void ResetButtonTapped();
        void FormClosed();
        void Tick();

        void CheckButtonClicked();
        void SaveButtonClicked();
        void SaveAsButtonClicked();
        void RunButtonClicked();
    }
}
