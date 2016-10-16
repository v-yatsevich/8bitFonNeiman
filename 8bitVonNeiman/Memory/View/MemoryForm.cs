using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Memory.View {
    public partial class MemoryForm : Form {

        private const int RowCount = 128;
        private const int ColumnCount = 8;

        private IMemoryFormOutput _output;

        public MemoryForm(IMemoryFormOutput output) {
            InitializeComponent();
            _output = output;
            memoryDataGridView.RowCount = RowCount;
            memoryDataGridView.ColumnCount = ColumnCount;
            memoryDataGridView.RowHeadersWidth = 60;
            for (int i = 0; i < RowCount; i++) {
                memoryDataGridView.Rows[i].HeaderCell.Value = (i << 3).ToString();
            }
            for (int i = 0; i < ColumnCount; i++) {
                memoryDataGridView.Columns[i].HeaderCell.Value = i.ToString();
                memoryDataGridView.Columns[i].Width = 58;
            }
        }

        public void ShowMemory(Dictionary<int, BitArray> memory) {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++) {
                    int memoryIndex = i * ColumnCount + j;
                    if (memory.ContainsKey(memoryIndex)) {
                        memoryDataGridView.Rows[i].Cells[j].Value = memory[memoryIndex].ToDigitString();
                    } else {
                        memoryDataGridView.Rows[i].Cells[j].Value = "00000000";
                    }
                }
        }

        private void clearMemoryButton_Click(object sender, EventArgs e) {
            _output.ClearMemoryClicked();
        }

        private void MemoryForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }
    }
}
