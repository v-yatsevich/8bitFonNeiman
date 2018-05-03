using System;
using System.Windows.Forms;

namespace _8bitVonNeiman.Auth.View {
    public partial class AuthForm : Form {

        public IAuthFormOutput Output;

        public AuthForm(IAuthFormOutput output) {
            Output = output;

            InitializeComponent();
        }

        private void AuthForm_FormClosed(object sender, FormClosedEventArgs e) {
            Output.FormClosed();
        }

        private void loginButton_Click(object sender, EventArgs e) {
            Output.LoginButtonClicked(loginTextBox.Text, passwordTextBox.Text);
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                Output.LoginButtonClicked(loginTextBox.Text, passwordTextBox.Text);
            }
        }
    }
}
