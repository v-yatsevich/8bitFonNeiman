using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Compiler;
using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Compiler.View;

namespace _8bitVonNeiman.Core {
    public static class Assembly {
        public static CompilerController GetCompilerController() {
            CompilerController compilerController = new CompilerController();

            CompilerModel compilerModel = new CompilerModel(compilerController);
            compilerController.SetCompiler(compilerModel);

            return compilerController;
        }
    }
}
