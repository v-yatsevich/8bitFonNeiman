using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Memory.View;

namespace _8bitVonNeiman.Memory {
    public class MemoryController : IMemoryControllerInput, IMemoryFormOutput {

        private MemoryForm _form;
        private Dictionary<int, BitArray> _memory;

        public MemoryController() {
            _memory = new Dictionary<int, BitArray>();
        }

        public void SetMemory(Dictionary<int, BitArray> memory) {
            _memory = memory;
            if (_form != null) {
                ShowMemory();
            }
        }

        /// <summary>
        /// Функция, показывающая форму, если она закрыта, и закрывающая ее, если она открыта
        /// </summary>
        public void ChangeState() {
            if (_form == null) {
                _form = new MemoryForm(this);
                _form.Show();
                ShowMemory();
            } else {
                _form.Close();
            }
        }

        /// <summary>
        /// Функция, вызывающаяся при закрытии формы. Необходима для корректной работы функции ChangeState()
        /// </summary>
        public void FormClosed() {
            _form = null;
        }

        public void MemoryChanged(int row, int collumn, string s) {
            int num;
            try {
                num = Convert.ToInt32(s, 16);
                if (num > 255 || num < 0) {
                    _form.ShowMessage("Число должно быть от 0 до FF");
                    _form.SetMemory(row, collumn, MemoryHex(row, collumn));
                    return;
                }
            } catch {
                _form.ShowMessage("Введено некорректное число");
                _form.SetMemory(row, collumn, MemoryHex(row, collumn));
                return;
            }
            var bitArray = new BitArray(8);
            CompilerSupport.FillBitArray(null, bitArray, num, 8);
            _memory[row * MemoryForm.ColumnCount + collumn] = bitArray;
            if (s.Length == 1) {
                s = "0" + s;
            }
            s = s.ToUpper();
            _form.SetMemory(row, collumn, s);
        }

        public void ClearMemoryClicked() {
            _memory = new Dictionary<int, BitArray>();
            ShowMemory();
        }

        private void ShowMemory() {
            for (int i = 0; i < MemoryForm.RowCount; i++)
            for (int j = 0; j < MemoryForm.ColumnCount; j++) {
                _form.SetMemory(i, j, MemoryHex(i, j));
            }
        }

        private string MemoryHex(int row, int collumn) {
            int memoryIndex = row * MemoryForm.ColumnCount + collumn;
            if (_memory.ContainsKey(memoryIndex)) {
                return _memory[memoryIndex].ToHexString();
            } else {
                return "00";
            }
        }
    }
}
