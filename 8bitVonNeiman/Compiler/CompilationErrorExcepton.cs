using System;

namespace _8bitVonNeiman.Compiler {
    public class CompilationErrorExcepton: Exception {

        public int LineNumber { get; private set; }
        public string ErrorMessage { get; private set; }

        public CompilationErrorExcepton(string message, int lineNumber) : base(message) {
            ErrorMessage = message;
            LineNumber = lineNumber;
        }

        public CompilationErrorExcepton(string message, int lineNumber, Exception inner) : base(message, inner) {
            ErrorMessage = message;
            LineNumber = lineNumber;
        }
    }
}
