using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ScintillaNET;

namespace _8bitVonNeiman.Compiler.View {
    public partial class CompilerForm : Form {

        private CompilerFormOutput _output;
        private AsmLexer _asmLexer;

        public CompilerForm(CompilerFormOutput output) {
            InitializeComponent();
            _output = output;
            _asmLexer = new AsmLexer(scintilla);
            ResizeLinesCount();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 11;
            ConfigStyles();
        }

        public void AddLineToOutput(string line) {
            outputRichTextBox.Text = outputRichTextBox.Text + line + Environment.NewLine;
        }

        private void CompilerForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
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
    }
}
