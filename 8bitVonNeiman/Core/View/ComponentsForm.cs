using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8bitVonNeiman.Core.View {
    public partial class ComponentsForm : Form {

        private ComponentsFormOutput _output;

        public ComponentsForm(ComponentsFormOutput output) {
            _output = output;
            InitializeComponent();
        }

        private void ComponentsForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void editorButton_Click(object sender, EventArgs e) {
            _output.EditorButtonClicked();
        }
    }
}
