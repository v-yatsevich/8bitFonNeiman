using System.Collections.Generic;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Debugger.View;

namespace _8bitVonNeiman.Debug {
    public class DebugController : IDebugModuleInput, IDebugFormOutput {

        private readonly IDebugModuleOutput _output;
        private DebugForm _form;

        private HashSet<int> _breakpoints = new HashSet<int>();
        private List<DebugCommand> _commands = new List<DebugCommand>();

        public DebugController(IDebugModuleOutput output) {
            _output = output;
        }

        /// Открывает форму, если она закрыта или закрывает, если открыта
        public void ChangeFormState() {
            if (_form == null) {
                _form = new DebugForm(this);
                _form.Show();
            } else {
                _form.Close();
            }
        }

        public void CommandHasRun(int pcl, List<ExtendedBitArray> memory, bool isAutomatic) {
            FormCommands(pcl, memory);
            if (!isAutomatic) {
                _form.ShowCommands(_commands);
            }
            if (_breakpoints.Contains(pcl)) {
                _output.StopExecution();
            }
        }

        public void FormClosed() {
            _form = null;
        }

        public void ToggleBreakpoint(int address) {
            if (_breakpoints.Remove(address * 2)) {
                _breakpoints.Add(address * 2);
            }

            _commands[address].HasBreakpoint = _breakpoints.Contains(address * 2);
            _form.ShowCommand(_commands[address], address);
        }

        public void DeleteAllBreakpoints() {
            _breakpoints.Clear();
            _commands.ForEach(x => x.HasBreakpoint = false);
            _form.ShowCommands(_commands);
        }

        private void FormCommands(int pcl, List<ExtendedBitArray> memory) {
            _commands = new List<DebugCommand>(128);
            for (int i = 0; i < memory.Count; i += 2) {
                var command = new DebugCommand(memory[i], memory[i + 1], i);
                command.Selected = pcl == i;
                command.HasBreakpoint = _breakpoints.Contains(i);
                _commands.Add(command);
            }
        }
    }
}
