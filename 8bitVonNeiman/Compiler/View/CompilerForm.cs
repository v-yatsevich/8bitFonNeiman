using System;
using System.Windows.Forms;

namespace _8bitVonNeiman.Compiler.View {
    public partial class CompilerForm : Form {

        private CompilerFormOutput _output;

        public CompilerForm(CompilerFormOutput output) {
            _output = output;
            InitializeComponent();
        }

        public void AddLineToOutput(string line) {
            outputRichTextBox.Text = outputRichTextBox.Text + line + "\n";
        }

        private void CompilerForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void compileButton_Click(object sender, EventArgs e) {
            _output.Compile(codeRichTextBox.Text);
        }
    }
}
