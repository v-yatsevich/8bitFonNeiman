using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Compiler.Model;

namespace _8bitVonNeiman.Memory.View {
    public partial class MemoryForm : Form {

        private const int RowCount = 64;
        private const int ColumnCount = 16;

        private IMemoryFormOutput _output;
        private Dictionary<int, BitArray> _memory;

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

        public void ShowMemory(Dictionary<int, BitArray> memory) {
            _memory = memory;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++) {
                    int memoryIndex = i * ColumnCount + j;
                    if (memory.ContainsKey(memoryIndex)) {
                        memoryDataGridView.Rows[i].Cells[j].Value = memory[memoryIndex].ToHexString();
                    } else {
                        memoryDataGridView.Rows[i].Cells[j].Value = "00";
                    }
                }
        }

        private void clearMemoryButton_Click(object sender, EventArgs e) {
            _output.ClearMemoryClicked();
        }

        private void MemoryForm_FormClosed(object sender, FormClosedEventArgs e) {
            _output.FormClosed();
        }

        private void memoryDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            int num;
            try {
                num = Convert.ToInt32(memoryDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), 16);
                if (num > 255 || num < 0) {
                    MessageBox.Show("Число должно быть от 0 до FF");
                    if (_memory.ContainsKey(e.RowIndex * ColumnCount + e.ColumnIndex)) {
                        memoryDataGridView[e.ColumnIndex, e.RowIndex].Value = _memory[e.RowIndex * ColumnCount + e.ColumnIndex].ToHexString();
                    } else {
                        memoryDataGridView[e.ColumnIndex, e.RowIndex].Value = "00";
                    }
                    return;
                }
            } catch {
                MessageBox.Show("Введено некорректное число");
                if (_memory.ContainsKey(e.RowIndex * ColumnCount + e.ColumnIndex)) {
                    memoryDataGridView[e.ColumnIndex, e.RowIndex].Value = _memory[e.RowIndex * ColumnCount + e.ColumnIndex].ToHexString();
                } else {
                    memoryDataGridView[e.ColumnIndex, e.RowIndex].Value = "00";
                }
                return;
            }
            var bitArray = new BitArray(8);
            CompilerSupport.FillBitArray(null, bitArray, num, 8);
            _memory[e.RowIndex * ColumnCount + e.ColumnIndex] = bitArray;
            if (memoryDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Length == 1) {
                memoryDataGridView[e.ColumnIndex, e.RowIndex].Value = "0" +
                    memoryDataGridView[e.ColumnIndex, e.RowIndex].Value;
            }
            memoryDataGridView[e.ColumnIndex, e.RowIndex].Value = memoryDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
        }
    }
}
