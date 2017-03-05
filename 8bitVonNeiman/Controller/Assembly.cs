using _8bitVonNeiman.Compiler;
using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Cpu;
using _8bitVonNeiman.Cpu.View;
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

        public static ICpuModelInput GetCpu(ICpuModelOutput output) {
            return new CpuModel(output, GetView);
        }

        private static ICpuFormInput GetView() {
            return new CpuForm();
        }
    }
}
