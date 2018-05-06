using System.Windows.Forms;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Tasks.View {
    public partial class AddTaskForm : Form {

        private ITaskFormOutput _output;
        private TaskEntity _task;

        public AddTaskForm(ITaskFormOutput output, TaskEntity task = null, bool isEditing = true) {
            InitializeComponent();

            _output = output;

            _task = task ?? new TaskEntity();
            nameTextBox.Text = _task.Name;
            descriptionTextBox.Text = _task.Description;

            if (!isEditing) {
                button1.Hide();
                Height -= 27;
            }
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button1_Click(object sender, System.EventArgs e) {
            _task.Name = nameTextBox.Text;
            _task.Description = descriptionTextBox.Text;

            _output.TaskSaveButtonClicked(_task);
        }

        private void AddTaskForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.AddFormClosed();
        }
    }
}
