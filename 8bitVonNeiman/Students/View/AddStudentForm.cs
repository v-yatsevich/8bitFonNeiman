using System.Windows.Forms;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Students.View {
    public partial class AddStudentForm : Form {

        private readonly StudentEntity _student;
        private readonly IStudentsFormOutput _output;

        public AddStudentForm(IStudentsFormOutput output, StudentEntity student = null) {
            InitializeComponent();

            _output = output;
            _student = student ?? new StudentEntity();

            loginTextBox.Text = _student.User.Login;
            nameTextBox.Text = _student.Name;
            groupTextBox.Text = _student.Group;
        }

        private void AddStudentForm_Load(object sender, System.EventArgs e) {
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button1_Click(object sender, System.EventArgs e) {
            _student.User.Login = loginTextBox.Text;
            _student.User.Password = passwordTextBox.Text == "" ? null : passwordTextBox.Text;
            _student.Name = nameTextBox.Text;
            _student.Group = groupTextBox.Text;

            _output.AddingFromSaveDidTap(_student);
        }
    }
}
