using System;
using System.Windows.Forms;

namespace _8bitVonNeiman.Controller.View {
    public partial class ComponentsForm : Form {

        private IComponentsFormOutput _output;

        public ComponentsForm(IComponentsFormOutput output) {
            _output = output;
            InitializeComponent();
        }

        public void SetAdminButtonsVisible(bool visible) {
            studentsButton.Visible = visible;
        }

        private void ComponentsForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void editorButton_Click(object sender, EventArgs e) {
            _output.EditorButtonClicked();
        }

        private void memoryButton_Click(object sender, EventArgs e) {
            _output.MemoryButtonClicked();
        }

        private void cpuButton_Click(object sender, EventArgs e) {
            _output.CpuButtonClicked();
        }

        private void studentsButton_Click(object sender, EventArgs e) {
            _output.StudentsButtonClicked();
        }

        private void tasksButton_Click(object sender, EventArgs e) {
            _output.TasksButtonClicked();
        }

        private void marksButton_Click(object sender, EventArgs e) {
            _output.MarksButtonClicked();
        }
    }
}
