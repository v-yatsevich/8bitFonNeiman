using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Compiler;
using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Compiler.View;
using _8bitVonNeiman.Memory;

namespace _8bitVonNeiman.Controller {
    public static class Assembly {
        public static CompilerController GetCompilerController(ICompilerControllerOutput output) {
            var compilerController = new CompilerController(output);

            var compilerModel = new CompilerModel(compilerController.CompilationComplete, compilerController.CompilationError);
            compilerController.SetCompiler(compilerModel);

            return compilerController;
        }

        public static MemoryController GetMemoryController() {
            var memoryController = new MemoryController();
            return memoryController;
        }
    }
}
