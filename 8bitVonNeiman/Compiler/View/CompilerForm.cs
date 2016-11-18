using System;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;

namespace _8bitVonNeiman.Compiler.View {
    public partial class CompilerForm : Form {

        private ICompilerFormOutput _output;

        public CompilerForm(ICompilerFormOutput output) {
            InitializeComponent();
            _output = output;
            ResizeLinesCount();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            ConfigStyles();
        }

        public void SetCode(string code) {
            scintilla.Text = code;
        }

        public void AddLineToOutput(string line) {
            outputRichTextBox.Text = outputRichTextBox.Text + line + Environment.NewLine;
        }

        public void ShowMessage(string message) {
            MessageBox.Show(message);
        }

        private void CompilerForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed(scintilla.Text);
        }

        private void compileButton_Click(object sender, EventArgs e) {
            _output.Compile(scintilla.Text);
        }

        private int _maxLineNumberCharLength;
        private void scintilla_TextChanged(object sender, EventArgs e) {
            ResizeLinesCount();
        }

        private void ResizeLinesCount() {
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
        }

        private void scintilla_StyleNeeded(object sender, StyleNeededEventArgs e) {
            //var endPos = e.Position;

            //_asmLexer.Style(0, endPos);
        }

        private void ConfigStyles() {
            scintilla.Styles[AsmLexer.StyleComment].ForeColor = Color.DarkGray;
            scintilla.Styles[AsmLexer.StyleError].ForeColor = Color.Red;
            scintilla.Styles[AsmLexer.StyleIdentifier].ForeColor = Color.Green;
            scintilla.Styles[AsmLexer.StyleKeyword].ForeColor = Color.DodgerBlue;
            scintilla.Styles[AsmLexer.StyleNumber].ForeColor = Color.DarkMagenta;
        }

        private void scintilla_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
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
            }
        }

        private void saveButton_Click(object sender, EventArgs e) {
            var status = saveFileDialog.ShowDialog();
            if (status == DialogResult.OK || status == DialogResult.Yes) {
                string path = saveFileDialog.FileName;
                _output.Save(scintilla.Text, path);
            }
            saveFileDialog.FileName = null;
        }

        private void loadButton_Click(object sender, EventArgs e) {
            var status = openFileDialog.ShowDialog();
            if (status == DialogResult.OK || status == DialogResult.Yes) {
                string path = openFileDialog.FileName;
                _output.Load(path);
            }
            openFileDialog.FileName = null;
        }
    }
}
