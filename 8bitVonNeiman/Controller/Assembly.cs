using _8bitVonNeiman.Auth;
using _8bitVonNeiman.Compiler;
using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Cpu;
using _8bitVonNeiman.Memory;
using _8bitVonNeiman.Students;
using _8bitVonNeiman.Tasks;

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
            return new CpuModel(output);
        }

        public static IAuthModelInput GetAuthController(IAuthModelOutput output) {
            return new AuthController(output);
        }

        public static IStudentsModuleInput GetStudentsController() {
            return new StudentsController();
        }

        public static ITasksModuleInput GetTasksController() {
            return new TasksController();
        }
    }
}
