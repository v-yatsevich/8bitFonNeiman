using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Marks.View {
    public partial class MarksForm : Form {
        private readonly IMarkFormOutput _output;

        public MarksForm(IMarkFormOutput output, bool showChange) {
            InitializeComponent();

            _output = output;

            if (!showChange) {
                Height = 370;
            }
        }

        public void ShowMarks(List<MarkEntity> marks) {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < marks.Count; i++) {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = marks[i].Date.ToString("dd.MM.yyy");
                dataGridView1.Rows[i].Cells[1].Value = marks[i].Description;
                dataGridView1.Rows[i].Cells[2].Value = marks[i].Student.Group;
                dataGridView1.Rows[i].Cells[3].Value = marks[i].Student.Name;
                dataGridView1.Rows[i].Cells[4].Value = marks[i].Task.Name;
                dataGridView1.Rows[i].Tag = marks[i];
            }
        }

        public void SetComboBoxes(List<StudentEntity> students, List<string> groups) {
            studentComboBox.DataSource = students;
            studentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            groupComboBox.DataSource = groups;
            groupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void MarksForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void addButton_Click(object sender, System.EventArgs e) {
            _output.AddButtonClicked();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) {
                return;
            }

            if (e.ColumnIndex == 5) {
                _output.ChangeButtonClicked((MarkEntity) dataGridView1.Rows[e.RowIndex].Tag);
                return;
            }
            if (e.ColumnIndex == 6) {
                _output.DeleteButtonClicked((MarkEntity)dataGridView1.Rows[e.RowIndex].Tag);
            }
        }

        private void groupComboBox_SelectedIndexChanged(object sender, System.EventArgs e) {
            _output.GroupValueChanged(groupComboBox.SelectedIndex);
        }

        private void studentComboBox_SelectedIndexChanged(object sender, System.EventArgs e) {
            _output.StudentValueChanged(studentComboBox.SelectedIndex);
        }
    }
}
