using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _8bitVonNeiman.Compiler;
using _8bitVonNeiman.Compiler.View;
using _8bitVonNeiman.Core.View;

namespace _8bitVonNeiman.Core {
    public class CentralController: ApplicationContext, ComponentsFormOutput {

        private readonly ComponentsForm _componentsForm;
        private readonly CompilerController _compilerController;

        public CentralController() {
            _componentsForm = new ComponentsForm(this);
            _componentsForm.Show();
            _compilerController = Assembly.GetCompilerController();
        }

        public void FormClosed() {
            ExitThread();
        }

        public void EditorButtonClicked() {
            _compilerController.ChangeState();
        }
    }
}
