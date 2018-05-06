using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Tasks.View {
    public partial class TasksForm : Form {

        private ITaskFormOutput _output;

        public TasksForm(ITaskFormOutput output, bool showChange) {
            _output = output;

            InitializeComponent();

            if (!showChange) {
                dataGridView1.Columns.RemoveAt(4);
                dataGridView1.Columns.RemoveAt(3);
            }
        }

        public void ShowTasks(List<TaskEntity> tasks) {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < tasks.Count; i++) {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = tasks[i].Name;
                dataGridView1.Rows[i].Cells[1].Value = tasks[i].Description;
                dataGridView1.Rows[i].Tag = tasks[i];
            }
        }

        private void addButton_Click(object sender, System.EventArgs e) {
            _output.AddButtonClicked();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) {
                return;
            }
            if (e.ColumnIndex == 2) {
                _output.LookButtonClicked((TaskEntity)dataGridView1.Rows[e.RowIndex].Tag);
            }
            if (e.ColumnIndex == 3) {
                _output.DeleteButtonClicked((TaskEntity) dataGridView1.Rows[e.RowIndex].Tag);
                return;
            }
            if (e.ColumnIndex == 4) {
                _output.ChangeButtonClicked((TaskEntity) dataGridView1.Rows[e.RowIndex].Tag);
            }
        }

        private void TasksForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }
    }
}
