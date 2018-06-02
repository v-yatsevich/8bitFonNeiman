using System.Collections.Generic;
using System.Drawing;
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
            dataGridView1.Rows.Clear();

            foreach (var command in commands) {
                int index = dataGridView1.Rows.Add(command.Selected ? ">" : "", command.Address.ToString("X"), command.Name, command.Command);
                dataGridView1.Rows[index].Cells[0].Style.BackColor = command.HasBreakpoint ? Color.Red : Color.White;
            }
        }

        public void ShowCommand(DebugCommand command, int address) {
            dataGridView1.Rows[address].Cells[0].Value = command.Selected ? ">" : "";
            dataGridView1.Rows[address].Cells[1].Value = command.Address.ToString("X");
            dataGridView1.Rows[address].Cells[2].Value = command.Name;
            dataGridView1.Rows[address].Cells[3].Value = command.Command;
            dataGridView1.Rows[address].Cells[0].Style.BackColor = command.HasBreakpoint ? Color.Red : Color.White;
        }

        private void DebugForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void deleteAllButton_Click(object sender, System.EventArgs e) {
            _output.DeleteAllBreakpoints();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) {
                return;
            }
            if (e.ColumnIndex == 0) {
                _output.ToggleBreakpoint(e.RowIndex);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e) {
            dataGridView1.ClearSelection();
        }
    }
}
