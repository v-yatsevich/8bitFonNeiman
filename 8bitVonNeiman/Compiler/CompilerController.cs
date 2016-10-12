using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Compiler.View;

namespace _8bitVonNeiman.Compiler {
    public class CompilerController: CompilerFormOutput {

        private CompilerForm _form;
        private CompilerModel _compilerModel;

        public void SetCompiler(CompilerModel compilerModel) {
            _compilerModel = compilerModel;
        }

        /// <summary>
        /// Функция, показывающая форму, если она закрыта, и закрывающая ее, если она открыта
        /// </summary>
        public void ChangeState() {
            if (_form == null) {
                _form = new CompilerForm(this);
                _form.Show();
            } else {
                _form.Close();
            }
        }

        /// <summary>
        /// Функция, вызывающаяся при закрытии формы. Необходима для корректной работы функции ChangeState()
        /// </summary>
        public void FormClosed() {
            _form = null;
        }

        /// <summary>
        /// Запускает компиляцию программы.
        /// </summary>
        /// <param name="code">Код программы.</param>
        public void Compile(string code) {
            _compilerModel.Compile(code);
        }

        /// <summary>
        /// Функция, вызывающаяся при заверщении компиляции.
        /// </summary>
        /// <param name="env">Окружение компилятора, сформировавшееся после компиляции.</param>
        public void CompilationComplete(CompilerEnvironment env) {
            
        }

        /// <summary>
        /// Функция: вызывающаяся при возникновении ошибки в процессе компиляции.
        /// </summary>
        /// <param name="e">Исключение, сгенерированное в процессе компиляции.</param>
        public void CompilationError(CompilationErrorExcepton e) {
            
        }
    }
}
