using System;
using System.Collections;
using System.Collections.Generic;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Compiler.Model;
using _8bitVonNeiman.Memory.View;

namespace _8bitVonNeiman.Memory {
    public class MemoryController : IMemoryControllerInput, IMemoryFormOutput {

        private MemoryForm _form;
        private Dictionary<int, ExtendedBitArray> _memory;

        public MemoryController() {
            _memory = new Dictionary<int, ExtendedBitArray>();
        }

        public void SetMemory(Dictionary<int, ExtendedBitArray> memory) {
            _memory = memory;
            if (_form != null) {
                ShowMemory();
            }
        }
        
        public void ChangeFormState() {
            if (_form == null) {
                _form = new MemoryForm(this);
                _form.Show();
                ShowMemory();
            } else {
                _form.Close();
            }
        }

        public void SetMemory(ExtendedBitArray memory, int address) {
            _memory[address] = memory;
            int i = address / MemoryForm.ColumnCount;
            int j = address % MemoryForm.ColumnCount;
            _form.SetMemory(i, j, MemoryHex(i, j));
        }

        public ExtendedBitArray GetMemory(int address) {
            if (_memory.ContainsKey(address)) {
                return _memory[address];
            } else {
                return new ExtendedBitArray();
            }
        }

        /// <summary>
        /// Функция, вызывающаяся при закрытии формы. Необходима для корректной работы функции ChangeFormState()
        /// </summary>
        public void FormClosed() {
            _form = null;
        }

        /// <summary>
        /// Функция, вызываемая при изменении пользователем текста в ячейке памяти. 
        /// Помещает новое значение ячейки в память или выводит сообщение об ощибке, если введено некорректное число.
        /// </summary>
        /// <param name="row">Строка измененной ячейки.</param>
        /// <param name="collumn">Столбец измененной ячейки.</param>
        /// <param name="s">Строка, введенная в ячейку.</param>
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
            var ExtendedBitArray = new ExtendedBitArray();
            CompilerSupport.FillBitArray(null, ExtendedBitArray, num, 8);
            _memory[row * MemoryForm.ColumnCount + collumn] = ExtendedBitArray;
            if (s.Length == 1) {
                s = "0" + s;
            }
            s = s.ToUpper();
            _form.SetMemory(row, collumn, s);
        }

        public void ClearMemoryClicked() {
            _memory = new Dictionary<int, ExtendedBitArray>();
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
