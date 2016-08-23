using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            InitCommandProcessors();
        }

        public void Compile(string code) {
            _code = code;
            _backgroundWorker.RunWorkerAsync();
        }

        private void StartCompile(object sender, DoWorkEventArgs e) {
            
        }

        private void CompileCompleted(object sender, RunWorkerCompletedEventArgs e) {
            
        }

        private void CompileProgressChanged(object sender, ProgressChangedEventArgs e) {
            
        }
    }
}
