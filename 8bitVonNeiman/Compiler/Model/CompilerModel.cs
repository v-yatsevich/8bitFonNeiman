using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Compiler.Model;

namespace _8bitVonNeiman.Compiler {
    public class CompilerModel {

        private delegate BitArray CommandProcessor(string args);

        private readonly CompilerModelOutput _output;
        private readonly BackgroundWorker _backgroundWorker;
        private string _code;
        private List<BitArray> memory;
        private Dictionary<string, CommandProcessor> _commandProcessors;

        public CompilerModel(CompilerModelOutput output) {
            _output = output;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += StartCompile;
            _backgroundWorker.RunWorkerCompleted += CompileCompleted;
            _backgroundWorker.ProgressChanged += CompileProgressChanged;
            _backgroundWorker.WorkerReportsProgress = true;
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
                    return semicolon != -1 ? x.Remove(semicolon).Trim() : x.Trim();
                })
                .ToList();
            var env = new CompilerEnvironment();
            for (short i = 0; i < lines.Count; i++) {
                int colon = lines[i].IndexOf(':');
                if (colon == -1) {
                    continue;
                }
                string label = lines[i].Substring(0, colon);
                if (!CompilerSupport.CheckWord(label)) {
                    throw new CompileErrorExcepton($"Имя метки {label} некорректно", i);
                }
                if (env.GetLabelAddress(label) != -1) {
                    throw new CompileErrorExcepton($"Метка {label} уже существует", i);
                }
                //TODO: Подумать, как организовать корректное вычисление адреса метки.
                env.AddAddressLabel(label, i);
            }
        }

        private void CompileCompleted(object sender, RunWorkerCompletedEventArgs e) {
            
        }

        private void CompileProgressChanged(object sender, ProgressChangedEventArgs e) {
            
        }
    }
}
