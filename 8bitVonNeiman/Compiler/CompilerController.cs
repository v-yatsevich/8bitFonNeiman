using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Compiler.View;

namespace _8bitVonNeiman.Compiler {
    public class CompilerController: CompilerFormOutput {

        private CompilerForm _form;
        private CompilerModel _compilerModel;
        private ICompilerControllerOutput _output;
        private string _code = "";

        public CompilerController(ICompilerControllerOutput output) {
            _output = output;
        }

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
                _form.SetCode(_code);
            } else {
                _form.Close();
            }
        }

        /// <summary>
        /// Функция, вызывающаяся при закрытии формы.
        /// Необходима для корректной работы функции ChangeState() и сохранения кода программы.
        /// </summary>
        public void FormClosed(string code) {
            _code = code;
            _form = null;
        }

        /// <summary>
        /// Запускает компиляцию программы.
        /// </summary>
        /// <param name="code">Код программы.</param>
        public void Compile(string code) {
            _form.AddLineToOutput("Начало компиляции...");
            _compilerModel.Compile(code);
        }

        /// <summary>
        /// Функция, вызывающаяся при заверщении компиляции.
        /// </summary>
        /// <param name="env">Окружение компилятора, сформировавшееся после компиляции.</param>
        public void CompilationComplete(CompilerEnvironment env) {
            _output.MemoryFormed(env.GetMemory());
            _form.AddLineToOutput("Компиляция завершена успешно!");
        }

        /// <summary>
        /// Функция: вызывающаяся при возникновении ошибки в процессе компиляции.
        /// </summary>
        /// <param name="e">Исключение, сгенерированное в процессе компиляции.</param>
        public void CompilationError(CompilationErrorExcepton e) {
            _form.AddLineToOutput($"Ошибка компиляции: {e.ErrorMessage} в строке {e.LineNumber + 1}");
        }
    }
}
