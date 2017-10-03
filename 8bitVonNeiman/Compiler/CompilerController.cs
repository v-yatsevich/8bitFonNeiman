using System.IO;
using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Compiler.View;

namespace _8bitVonNeiman.Compiler {
    public class CompilerController: ICompilerFormOutput {

        private CompilerForm _form;
        private CompilerModel _compilerModel;
        private ICompilerControllerOutput _output;
        private string _code = "";
        private string _filePath = null;

        private string filePath {
            get => _filePath;
            set {
                _filePath = value;
                var index = _filePath.LastIndexOf('\\') + 1;
                _form.SetFilename("Имя файла: " + _filePath.Substring(index));
            }
        }

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
        /// Необходима для корректной работы функции ChangeFormState() и сохранения кода программы.
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
            _form.ClearOutput();
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

        public void SaveButtonClicked(string code) {
            _code = code;
            if (filePath == null) {
                _form.ShowSaveDialog();
            } else {
                SaveCode();
            }
        }

        public void SaveAsButtonClicked(string code) {
            _code = code;
            _form.ShowSaveDialog();
        }

        public void SaveDialogEnded(string path) {
            filePath = path;
            SaveCode();
        }

        public void Load(string path) {
            try {
                using (var sr = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read))) {
                    var text = sr.ReadToEnd();
                    _form.SetCode(text);
                    filePath = path;
                }
            } catch {
                _form.ShowMessage("В процессе открытия файла возникла ошибка.");
            }
        }

        private void SaveCode() {
            try {
                using (var sw = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write))) {
                    sw.Write(_code);
                }
            } catch {
                _form.ShowMessage("В процессе записи файла возникла ошибка.");
            }
        }
    }
}
