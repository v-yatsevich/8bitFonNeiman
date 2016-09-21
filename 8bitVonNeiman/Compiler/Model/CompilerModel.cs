using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace _8bitVonNeiman.Compiler.Model {
    public class CompilerModel {

        private readonly ICompilerModelOutput _output;
        private readonly BackgroundWorker _backgroundWorker;
        private string _code;
        private readonly Dictionary<string, CommandProcessorFactory.CommandProcessor> _commandProcessors;

        public CompilerModel(ICompilerModelOutput output) {
            _output = output;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += StartCompile;
            _backgroundWorker.RunWorkerCompleted += CompileCompleted;
            _backgroundWorker.WorkerReportsProgress = true;
            _commandProcessors = CommandProcessorFactory.GetCommandProcessors();
        }

        public void Compile(string code) {
            _code = code;
            _backgroundWorker.RunWorkerAsync();
        }
        
        private void StartCompile(object sender, DoWorkEventArgs e) {
            List<string> lines = _code
                .Split('\n')
                .Select(x => {
                    int semicolon = x.IndexOf(";");
                    return semicolon == -1 ? x.Trim() : x.Remove(semicolon).Trim();
                })
                .ToList();
            var env = new CompilerEnvironment();
            for (short i = 0; i < lines.Count; i++) {
                if (lines[i][0] == '/') {
                    HandleDirective(lines[i], env);
                    env.IncrementLine();
                    continue;
                }
                string line = HandleLabelAndReturnLine(lines[i], env);
                HandleCommand(line, env);
                env.IncrementLine();
            }
            e.Result = env;
        }

        private void HandleCommand(string line, CompilerEnvironment env) {
            string command;
            string[] args;

            int firstSpaceIndex = line.IndexOf(" ");
            if (firstSpaceIndex == -1) {
                command = line;
                args = new string[0];
            } else {
                command = line.Substring(0, firstSpaceIndex - 1);
                args = line.Substring(firstSpaceIndex + 1).Split(',').Select(x => x.Trim()).ToArray();
            }

            command = command.ToUpper();
            if (!_commandProcessors.ContainsKey(command)) {
                throw new CompileErrorExcepton("Неверная команда", env.GetCurrentLine());
            }
            _commandProcessors[command](args, env);
        }

        private string HandleLabelAndReturnLine(string line, CompilerEnvironment env) {
            int colon = line.IndexOf(':');
            if (colon == -1) {
                return line;
            }
            string label = line.Substring(0, colon - 1);
            if (!CompilerSupport.CheckWord(label)) {
                throw new CompileErrorExcepton($"Имя метки {label} некорректно", env.GetCurrentLine());
            }
            if (env.GetLabelAddress(label) != -1) {
                throw new CompileErrorExcepton($"Метка {label} уже существует", env.GetCurrentLine());
            }
            env.AddAddressLabelToCurrentAddress(label);
            return line.Substring(colon + 1);
        }

        private void HandleDirective(string line, CompilerEnvironment env) {
            if (line[1] == 'n' || line[1] == 'N') {
                int address;
                try {
                    address = Convert.ToInt32(line.Substring(2));
                } catch (Exception e) {
                    throw new CompileErrorExcepton("Некорректная директива процессора. Адрес должен быть числом.", env.GetCurrentLine(), e);
                }
                if (address < 0 || address > 255) {
                    throw new CompileErrorExcepton("Некорректная директива процессора. Адрес должен быть в диапазоне от 0 до 255.", env.GetCurrentLine());
                }
                env.SetCurrentAddress(address);

            } else if (line[1] == 'C' || line[1] == 'c') {
                int segment = TryToGetSegmentFromDirecrive(line, env);
                env.SetDefaultCodeSegment(segment);

            } else if (line[1] == 'S' || line[1] == 's') {
                int segment = TryToGetSegmentFromDirecrive(line, env);
                env.SetDefaultStackSegment(segment);

            } else if (line[1] == 'D' || line[1] == 'd') {
                int segment = TryToGetSegmentFromDirecrive(line, env);
                env.SetDefaultDataSegment(segment);

            } else {
                throw new CompileErrorExcepton("Некорректная директива процессора.", env.GetCurrentLine());
            }
        }

        private int TryToGetSegmentFromDirecrive(string line, CompilerEnvironment env) {
            int segment;
            try {
                segment = Convert.ToInt32(line.Substring(2));
            } catch (Exception e) {
                throw new CompileErrorExcepton("Некорректная директива процессора. Номер сегмента должен быть числом.", env.GetCurrentLine(), e);
            }
            if (segment > 4 || segment < 0) {
                throw new CompileErrorExcepton("Некорректная директива процессора. Номер сегмента должен быть в диапазоне от 0 до 4.", env.GetCurrentLine());
            }
            return segment;
        }

        private void CompileCompleted(object sender, RunWorkerCompletedEventArgs e) {
            
        }
    }
}
