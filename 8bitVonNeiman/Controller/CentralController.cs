using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Compiler;
using _8bitVonNeiman.Controller.View;
using _8bitVonNeiman.Cpu;
using _8bitVonNeiman.Memory;

namespace _8bitVonNeiman.Controller {
    public class CentralController: ApplicationContext, IComponentsFormOutput, ICompilerControllerOutput, ICpuModelOutput {

        private readonly IMemoryControllerInput _memoryController;
        private readonly ComponentsForm _componentsForm;
        private readonly CompilerController _compilerController;
        private readonly ICpuModelInput _cpu;

        public CentralController() {
            _componentsForm = new ComponentsForm(this);
            _componentsForm.Show();
            _compilerController = Assembly.GetCompilerController(this);
            _memoryController = Assembly.GetMemoryController();
            _cpu = Assembly.GetCpu(this);
        }

        public void FormClosed() {
            ExitThread();
        }

        public void MemoryFormed(Dictionary<int, ExtendedBitArray> memory) {
            _memoryController.SetMemory(memory);
        }

        public void EditorButtonClicked() {
            _compilerController.ChangeState();
        }

        public void MemoryButtonClicked() {
            _memoryController.ChangeFormState();
        }

        public void CpuButtonClicked() {
            _cpu.ChangeFormState();
        }

        public void TactButtonClicked() {
            _cpu.Tick();
        }

        public ExtendedBitArray GetMemory(int address) {
            return _memoryController.GetMemory(address);
        }

        public void SetMemory(ExtendedBitArray memory, int address) {
            _memoryController.SetMemory(memory, address);
        }

        public void commandHasRun() {
            
        }
    }
}
