using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8bitVonNeiman.Compiler.View {
    public partial class CompilerForm : Form {

        private CompilerFormOutput _output;

        public CompilerForm(CompilerFormOutput output) {
            _output = output;
            InitializeComponent();
        }

        private void CompilerForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }
    }
}
