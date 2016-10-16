using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8bitVonNeiman.Controller.View {
    public interface ComponentsFormOutput {
        void FormClosed();
        void EditorButtonClicked();
        void MemoryButtonClicked();
    }
}
