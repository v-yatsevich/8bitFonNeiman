using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Compiler.View;
using _8bitVonNeiman.Core;

namespace _8bitVonNeiman.Compiler {
    public class CompilerController: CompilerFormOutput, CompilerModelOutput {

        private CompilerForm _form;
        private CompilerModel _compilerModel;

        public void SetCompiler(CompilerModel compilerModel) {
            _compilerModel = compilerModel;
        }

        public void ChangeState() {
            if (_form == null) {
                _form = new CompilerForm(this);
                _form.Show();
            } else {
                _form.Close();
            }
        }

        public void FormClosed() {
            _form = null;
        }

        public void Compile(string code) {
            _compilerModel.Compile(code);
        }
    }
}
