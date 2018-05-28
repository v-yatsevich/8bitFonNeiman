using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Debug;

namespace _8bitVonNeiman.Debugger.View {
    public partial class DebugForm : Form {

        private readonly IDebugFormOutput _output;

        public DebugForm(IDebugFormOutput output) {
            _output = output;

            InitializeComponent();
        }

        public void ShowCommands(List<DebugCommand> commands) {

        }

        public void ShowCommand(DebugCommand command, int address) {

        }

        private void DebugForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void deleteAllButton_Click(object sender, System.EventArgs e) {
            _output.DeleteAllBreakpoints();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) {
                return;
            }
            if (e.ColumnIndex == 0) {
                _output.ToggleBreakpoint(e.RowIndex);
            }
        }
    }
}
