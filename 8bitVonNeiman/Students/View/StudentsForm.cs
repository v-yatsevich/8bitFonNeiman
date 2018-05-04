using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Students.View {
    public partial class StudentsForm : Form {

        private readonly IStudentsFormOutput _output;

        public StudentsForm(IStudentsFormOutput output) {
            _output = output;

            InitializeComponent();
        }

        public void ShowStudents(List<StudentEntity> students) {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < students.Count; i++) {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Tag = students[i];
                dataGridView1.Rows[i].Cells[0].Value = students[i].Name;
                dataGridView1.Rows[i].Cells[1].Value = students[i].Group;
                dataGridView1.Rows[i].Cells[0].Value = students[i].Name;
            }
        }

        private void StudentsForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void addButton_Click(object sender, System.EventArgs e) {
            _output.AddingFormButtonDidTap();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) {
                return;
            }
            if (e.ColumnIndex == 3) {
                _output.ChangeDidTap((StudentEntity) dataGridView1.Rows[e.RowIndex].Tag);
                return;
            }
            if (e.ColumnIndex == 2) {
                _output.DeleteDidTap((StudentEntity)dataGridView1.Rows[e.RowIndex].Tag);
                return;
            }
        }
    }
}
