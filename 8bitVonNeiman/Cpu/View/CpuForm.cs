using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8bitVonNeiman.Cpu.View {
    public partial class CpuForm : Form, ICpuFormInput {
        public CpuForm() {
            InitializeComponent();

            SetupPswGridView();
            SetupSrGridView();
        }

        public void ShowState(CpuState state) {
            accHexTextBox.Text = state.Acc.ToHexString();
            accBinTextBox.Text = state.Acc.ToBinString();

            drHexTextBox.Text = state.Dr.ToHexString();
            drBinTextBox.Text = state.Dr.ToBinString();

            for (int i = 0; i < 8; i++) {
                pswDataGridView[i, 0].Value = state.Psw[7-i] ? "1" : "0";
            }

            srDataGridView[0, 0].Value = state.Ss;
            srDataGridView[0, 0].Value = state.Ds;
            srDataGridView[0, 0].Value = state.Cs;

            pclTextBox.Text = state.Pcl.ToString("X2");
            splTextBox.Text = state.Spl.ToString("X2");
            crBinTextBox.Text = state.Cr[0].ToBinString() + state.Cr[1].ToBinString();
            crHexTextBox.Text = state.Cr[0].ToHexString() + state.Cr[1].ToHexString();

            r0TextBox.Text = state.Registers[0].ToHexString();
            r1TextBox.Text = state.Registers[0].ToHexString();
            r2TextBox.Text = state.Registers[0].ToHexString();
            r3TextBox.Text = state.Registers[0].ToHexString();
            r4TextBox.Text = state.Registers[0].ToHexString();
            r5TextBox.Text = state.Registers[0].ToHexString();
            r6TextBox.Text = state.Registers[0].ToHexString();
            r7TextBox.Text = state.Registers[0].ToHexString();
        }

        private void SetupPswGridView() {
            pswDataGridView.Columns.Add("-", "-");
            pswDataGridView.Columns.Add("U/S", "S");
            pswDataGridView.Columns.Add("I", "I");
            pswDataGridView.Columns.Add("C", "C");
            pswDataGridView.Columns.Add("A", "A");
            pswDataGridView.Columns.Add("O", "O");
            pswDataGridView.Columns.Add("N", "N");
            pswDataGridView.Columns.Add("Z", "Z");
            for (int i = 0; i < pswDataGridView.Columns.Count; i++) {
                pswDataGridView.Columns[i].Width = 30;
            }
            //pswDataGridView.Rows.Add();
        }

        private void SetupSrGridView() {
            srDataGridView.Columns.Add("SS", "SS");
            srDataGridView.Columns.Add("DS", "DS");
            srDataGridView.Columns.Add("CS", "CS");
            for (int i = 0; i < srDataGridView.Columns.Count; i++) {
                srDataGridView.Columns[i].Width = 30;
            }
            //srDataGridView.Rows.Add();
        }
    }
}
