using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace _8bitVonNeiman.Compiler.Model {
    public class CompilerModel {

        public delegate void CompileCompleteDelegate(CompilerEnvironment env);
        private CompileCompleteDelegate _completeDelegate;

        public delegate void CompileErrorDelegate(CompilationErrorExcepton e);
        private CompileErrorDelegate _errorDelegate;

        private BackgroundWorker _backgroundWorker;
        private string _code;
        private Dictionary<string, CommandProcessorFactory.CommandProcessor> _commandProcessors;

        public CompilerModel(CompileCompleteDelegate completeDelegate, CompileErrorDelegate errorDelegate) {
            Setup(completeDelegate, errorDelegate, CommandProcessorFactory.GetCommandProcessors());
        }

        public CompilerModel(CompileCompleteDelegate completeDelegate, CompileErrorDelegate errorDelegate,
                             Dictionary<string, CommandProcessorFactory.CommandProcessor> commandProcessors) {
            Setup(completeDelegate, errorDelegate, commandProcessors);
        }

        private void Setup(CompileCompleteDelegate completeDelegate, CompileErrorDelegate errorDelegate,
                           Dictionary<string, CommandProcessorFactory.CommandProcessor> commandProcessors) {
            _completeDelegate = completeDelegate;
            _errorDelegate = errorDelegate;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += StartCompile;
            _backgroundWorker.RunWorkerCompleted += CompileCompleted;
            _backgroundWorker.WorkerReportsProgress = true;
            _commandProcessors = commandProcessors;
        }

        /// <summary>
        /// Запускает компиляцию переданного кода. Компиляция происходит ассинхронно, по завершению вызывается функция
        /// <see cref="CompileCompleteDelegate"/>
        /// </summary>
        /// <param name="code"></param>
        public void Compile(string code) {
            _code = code;
            _backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Функция, начинающая обработку кода и содержащая базовую логику прохождения по нему и вызова обработчиков.
        /// </summary>
        private void StartCompile(object sender, DoWorkEventArgs e) {
            List<string> lines = PrepareCode(_code);
            var env = new CompilerEnvironment();
            for (short i = 0; i < lines.Count; i++) {
                if (lines[i].Length == 0) {
                    env.IncrementLine();
                    continue;
                }
                if (lines[i][0] == '/') {
                    HandleDirective(lines[i], env);
                    env.IncrementLine();
                    continue;
                }
                string line = HandleLabelAndReturnLine(lines[i], env);
                bool isSuccess = TryToGetVariable(line, env);
                if (!isSuccess) {
                    HandleCommand(line, env);
                }
                env.IncrementLine();
            }
            var commandWithoutLabel = env.FirstCommandWithoutLabel();
            if (commandWithoutLabel.HasValue) {
                throw new CompilationErrorExcepton($"Метка {commandWithoutLabel.Value.Value} не определена", commandWithoutLabel.Value.Key);
            }
            e.Result = env;
        }

        /// <summary>
        /// Подготавливает исходный код к обработке: разбивает на массив строк, удаляет лишние пробелы, убирает комментарии.
        /// </summary>
        /// <param name="code">Исходный код программы.</param>
        /// <returns></returns>
        public List<string> PrepareCode(string code) {
            return code
                .Split('\n')
                .Select(x => {
                    int semicolon = x.IndexOf(";");
                    return semicolon == -1 ? x.Trim() : x.Remove(semicolon).Trim();
                })
                .ToList();
        } 

        /// <summary>
        /// Обрабатывает команду. В случае, если команда некорректна, генерируется <see cref="CompilationErrorExcepton"/>
        /// </summary>
        /// <param name="line">Строка кода, содержащая команду процессора.</param>
        /// <param name="env">Текущее окружение компилятора, в которое записывается команда.</param>
        public void HandleCommand(string line, CompilerEnvironment env) {
            string command;
            string[] args;

            line = line.Trim();

            int firstSpaceIndex = line.IndexOf(" ");
            if (firstSpaceIndex == -1) {
                command = line;
                args = new string[0];
            } else {
                command = line.Substring(0, firstSpaceIndex);
                args = line.Substring(firstSpaceIndex + 1).Split(',').Select(x => x.Trim()).ToArray();
            }

            command = command.ToLower();
            if (!_commandProcessors.ContainsKey(command)) {
                throw new CompilationErrorExcepton("Неверная команда", env.GetCurrentLine());
            }
            _commandProcessors[command](args, env);
        }

        /// <summary>
        /// Пытается достать из строки объявление переменной.
        /// Если в строке нет слова eql, то возвращает false,
        /// Если есть и строка имеет формат *идетификатор* eql *адрес*, то пытается сохранить адрес за идентификатором
        /// Если есть, но строка не подходит по формату, выбрасывает исключение <see cref="CompilationErrorExcepton"/>
        /// </summary>
        /// <param name="line">Строка кода для обработки.</param>
        /// <param name="env">Окружение компилятора.</param>
        /// <returns>True, если в строке присутствовала переменная, false, если нет.</returns>
        public bool TryToGetVariable(string line, CompilerEnvironment env) {
            if (!line.Contains(" eql ")) {
                return false;
            }
            var args = line.Split(' ').Select(x => x.ToLower().Trim()).ToArray();
            if (args.Length != 3 || !args[1].Equals("eql", StringComparison.OrdinalIgnoreCase)) {
                throw new CompilationErrorExcepton("Объявление переменной должно соответствовать формату *имя переменной* eql *адрес*.", env.GetCurrentLine());
            }

            bool isIdentifierCorrect = CompilerSupport.CheckIdentifierName(args[0]);
            if (!isIdentifierCorrect) {
                throw new CompilationErrorExcepton("Имя переменной должно содержать только латиские буквы, цифры знаки тире и подчеркивания и начинаться с буквы.", env.GetCurrentLine());
            }

            int address = CompilerSupport.ConvertToInt(args[2]);
            if (address > 255) {
                throw new CompilationErrorExcepton("Адрес не может превышать 255", env.GetCurrentLine());
            }
            if (env.IsIdentifierExist(args[0])) {
                throw new CompilationErrorExcepton($"Идентификатор с именем {args[0]} уже существует.", env.GetCurrentLine());
            }
            env.AddVariable(args[0], address);
            return true;
        }

        /// <summary>
        /// Проверяет, присутствует ли в строке метка, и если она есть, добавляет метку в окружение и удаляет ее из строки.
        /// Если имя метки некорректно или занято, генерируется <see cref="CompilationErrorExcepton"/>
        /// </summary>
        /// <param name="line">Строка кода, которая проверяется на наличие метки.</param>
        /// <param name="env">Текущее окружение компилятора, в которое добавляется метка.</param>
        /// <returns>Строка, из которой была удалена метка, если она существовала ранее.</returns>
        public string HandleLabelAndReturnLine(string line, CompilerEnvironment env) {
            int colon = line.IndexOf(':');
            if (colon == -1) {
                return line;
            }
            string label = line.Substring(0, colon);
            if (!CompilerSupport.CheckIdentifierName(label)) {
                throw new CompilationErrorExcepton($"Имя метки {label} некорректно", env.GetCurrentLine());
            }
            if (env.IsIdentifierExist(label)) {
                throw new CompilationErrorExcepton($"Идентификатор с именем {label} уже существует.", env.GetCurrentLine());
            }
            env.AddAddressLabelToNewCommand(label);
            return line.Substring(colon + 1);
        }

        /// <summary>
        /// Обрабатывает директиву процессора, и изменяет окружение в соответствии с ней.
        /// В случае, если директива некорректна, генерируется <see cref="CompilationErrorExcepton"/>
        /// </summary>
        /// <param name="line">Строка кода, из которой извлекается директива.</param>
        /// <param name="env">Текущее окружение компилятора, которое изменяется в соответствии с директивой.</param>
        public void HandleDirective(string line, CompilerEnvironment env) {
            if (line[1] == 'n' || line[1] == 'N') {
                string[] components = line.Split(' ');
                if (components.Length != 2) {
                    throw new CompilationErrorExcepton("После /n должно следовать одно число через пробел.", env.GetCurrentLine());
                }
                
                try {
                    int address = CompilerSupport.ConvertToInt(components[1]);
                    if (address > 255 || address < 0) {
                        throw new Exception();
                    }
                    env.CurrentAddress = address;
                } catch (Exception) {
                    throw new CompilationErrorExcepton("Некорректная директива процессора. Адрес должен быть числом в диапазоне от 0 до 255.", env.GetCurrentLine());
                }
            } else if (line[1] == 'C' || line[1] == 'c') {
                int segment = TryToGetSegmentFromDirecrive(line, env.GetCurrentLine());
                env.DefaultCodeSegment = segment;

            } else if (line[1] == 'S' || line[1] == 's') {
                int segment = TryToGetSegmentFromDirecrive(line, env.GetCurrentLine());
                env.DefaultStackSegment = segment;

            } else if (line[1] == 'D' || line[1] == 'd') {
                int segment = TryToGetSegmentFromDirecrive(line, env.GetCurrentLine());
                env.DefaultDataSegment = segment;

            } else {
                throw new CompilationErrorExcepton("Некорректная директива процессора.", env.GetCurrentLine());
            }
        }

        /// <summary>
        /// Извлекает сегмент кода из директивы и возвращает его. Сегмент должен быть числом в промежутке от 0 до 3.
        /// В случае, если сегмент не является числом или не попадает в промежуток от 0 до 3 генерируется
        /// <see cref="CompilationErrorExcepton"/>
        /// </summary>
        /// <param name="line">Строка кода, содержащая директиву.</param>
        /// <param name="lineNumber">Номер строки кода. Нужен для генерации исключения.</param>
        /// <returns></returns>
        public int TryToGetSegmentFromDirecrive(string line, int lineNumber) {
            int segment;
            try {
                segment = Convert.ToInt32(line.Substring(2));
            } catch (Exception e) {
                throw new CompilationErrorExcepton("Некорректная директива процессора. Номер сегмента должен быть числом.", lineNumber, e);
            }
            if (segment > 4 || segment < 0) {
                throw new CompilationErrorExcepton("Некорректная директива процессора. Номер сегмента должен быть в диапазоне от 0 до 4.", lineNumber);
            }
            return segment;
        }

        private void CompileCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error == null) {
                _completeDelegate(e.Result as CompilerEnvironment);
            } else {
                _errorDelegate(e.Error as CompilationErrorExcepton);
            }
        }
    }
}
