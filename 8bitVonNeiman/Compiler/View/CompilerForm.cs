using System;
using System.Drawing;
using System.Windows.Forms;

namespace _8bitVonNeiman.Compiler.View {
    public partial class CompilerForm : Form {

        private ICompilerFormOutput _output;

        public CompilerForm(ICompilerFormOutput output) {
            InitializeComponent();
            _output = output;
            //ResizeLinesCount();
            scintilla.Font = new Font("Consolas", 10);
        }

        public void SetCode(string code) {
            scintilla.Text = code;
        }

        public void ClearOutput() {
            outputRichTextBox.Text = "";
        }

        public void AddLineToOutput(string line) {
            outputRichTextBox.Text = outputRichTextBox.Text + line + Environment.NewLine;
            outputRichTextBox.ScrollToCaret();
        }

        public void ShowMessage(string message) {
            MessageBox.Show(message);
        }

        public void ShowSaveDialog() {
            var status = saveFileDialog.ShowDialog();
            if (status == DialogResult.OK || status == DialogResult.Yes) {
                string path = saveFileDialog.FileName;
                _output.SaveDialogEnded(path);
            }
            saveFileDialog.FileName = null;
        }

        public void SetFilename(string filename) {
            filenameLabel.Text = filename;
        }

        private void CompilerForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed(scintilla.Text);
        }

        private void compileButton_Click(object sender, EventArgs e) {
            _output.Compile(scintilla.Text);
        }

        private int _maxLineNumberCharLength;
        private void scintilla_TextChanged(object sender, EventArgs e) {
            //ResizeLinesCount();
        }

        //TODO: Переписать отступы
        /*private void ResizeLinesCount() {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == _maxLineNumberCharLength) {
                return;
            }

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            _maxLineNumberCharLength = maxLineNumberCharLength;
        }*/

        private void scintilla_KeyDown(object sender, KeyEventArgs e) {
            /*if (e.KeyCode == Keys.Enter) {
                int line = scintilla.LineFromPosition(scintilla.CurrentPosition);
                int count = 0;
                while (scintilla.Lines[line].Length != count && scintilla.Lines[line].Text[count] == ' ') {
                    count++;
                }
                if (count == 0) {
                    return;
                }
                string spaces = new string(' ', count);
                scintilla.InsertText(scintilla.CurrentPosition, Environment.NewLine + spaces);
                scintilla.GotoPosition(scintilla.CurrentPosition + count + Environment.NewLine.Length);
                e.SuppressKeyPress = true;
            }*/
        }

        private void saveButton_Click(object sender, EventArgs e) {
            _output.SaveButtonClicked(scintilla.Text);
        }

        private void loadButton_Click(object sender, EventArgs e) {
            var status = openFileDialog.ShowDialog();
            if (status == DialogResult.OK || status == DialogResult.Yes) {
                string path = openFileDialog.FileName;
                _output.Load(path);
            }
            openFileDialog.FileName = null;
        }

        private void saveAsButton_Click(object sender, EventArgs e) {
            _output.SaveAsButtonClicked(scintilla.Text);
        }
    }
}
