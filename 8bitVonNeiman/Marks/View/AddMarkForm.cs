using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Marks.View{
    public partial class AddMarkForm : Form {
        private readonly IMarkFormOutput _output;
        private readonly MarkEntity _mark;

        public AddMarkForm(IMarkFormOutput output, List<StudentEntity> students, List<TaskEntity> tasks, MarkEntity mark = null, bool isEditing = true) {
            InitializeComponent();

            _output = output;
            _mark = mark ?? new MarkEntity();

            dateTimePicker.Value = _mark.Date;
            descriptionTextBox.Text = _mark.Description;
            studentComboBox.DataSource = students;
            if (_mark.Student != null) {
                studentComboBox.SelectedIndex = students.FindIndex(x => x.Id == _mark.Student.Id);
            }

            taskComboBox.DataSource = tasks;
            if (_mark.Task != null) {
                taskComboBox.SelectedIndex = tasks.FindIndex(x => x.Id == _mark.Task.Id);
            }
        }

        private void AddMarkForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.AddingFormClosed();
        }

        private void saveButton_Click(object sender, System.EventArgs e) {
            _mark.Date = dateTimePicker.Value;
            _mark.Description = descriptionTextBox.Text;
            _mark.Student = (StudentEntity) studentComboBox.SelectedItem;
            _mark.Task = (TaskEntity) taskComboBox.SelectedItem;
            _output.SaveMark(_mark);
        }
    }
}
