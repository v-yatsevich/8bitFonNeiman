using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Compiler.Model;

namespace _8bitVonNeiman.Memory.View {
    public partial class MemoryForm : Form {

        public const int RowCount = 64;
        public const int ColumnCount = 16;

        private IMemoryFormOutput _output;

        public MemoryForm(IMemoryFormOutput output) {
            InitializeComponent();
            _output = output;
            memoryDataGridView.RowCount = RowCount;
            memoryDataGridView.ColumnCount = ColumnCount;
            memoryDataGridView.RowHeadersWidth = 60;
            for (int i = 0; i < RowCount; i++) {
                memoryDataGridView.Rows[i].HeaderCell.Value = (i << 4).ToString();
            }
            for (int i = 0; i < ColumnCount; i++) {
                memoryDataGridView.Columns[i].HeaderCell.Value = i.ToString();
                memoryDataGridView.Columns[i].Width = 25;
            }
        }

        public void SetMemory(int row, int collumn, string memory) {
            memoryDataGridView[collumn, row].Value = memory;
        }

        public void ShowMessage(string text) {
            MessageBox.Show(text);
        }

        private void clearMemoryButton_Click(object sender, EventArgs e) {
            _output.ClearMemoryClicked();
        }

        private void MemoryForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void memoryDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            _output.MemoryChanged(e.RowIndex, e.ColumnIndex, memoryDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString());
        }
    }
}
