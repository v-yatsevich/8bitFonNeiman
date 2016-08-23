using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Compiler {
    public class CompileErrorExcepton: Exception {

        public int LineNumber { get; private set; }
        public string ErrorMessage { get; private set; }

        public CompileErrorExcepton(string message, int lineNumber) : base(message) {
            ErrorMessage = message;
            LineNumber = lineNumber;
        }

        public CompileErrorExcepton(string message, int lineNumber, Exception inner) : base(message, inner) {
            ErrorMessage = message;
            LineNumber = lineNumber;
        }
    }
}
