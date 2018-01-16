using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Common;
using System.IO;
using System.Windows.Forms;

namespace _8bitVonNeiman.Memory {
    class MemoryFileHandler {

        private string _lastFilePath;

        public Dictionary<int, ExtendedBitArray> LoadMemory() {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Memory files (*.mem8)|*.mem8|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                using (var sr = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))) {
                    string text;
                    byte value;
                    Dictionary<int, ExtendedBitArray> memory = new Dictionary<int, ExtendedBitArray>();
                    int i = 0;
                    while (true) {
                        text = sr.ReadLine();
                        if (text == null) {
                            break;
                        }
                        try {
                            value = Convert.ToByte(text);
                            if (value != 0) {
                                memory[i] = new ExtendedBitArray(value);
                            }
                        } catch {
                            MessageBox.Show("Неверный формат файла. Проверьте, что в нем находятся только числа от 0 до 255 по одному в строке.");
                            return null;
                        }
                        i++;
                    }
                    return memory;
                }
            }
            return null;
        }

        public void Save(Dictionary<int, ExtendedBitArray> memory) {
            if (_lastFilePath == null) {
                SaveAs(memory);
            } else {
                Save(memory, _lastFilePath);
            }
        }

        public void SaveAs(Dictionary<int, ExtendedBitArray> memory) {
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Memory files (*.mem8)|*.mem8";
            saveFileDialog.DefaultExt = "mem8";
            if (_lastFilePath == null) {
                saveFileDialog.FileName = "memory.mem8";
            } else {
                saveFileDialog.FileName = _lastFilePath;
            }
            saveFileDialog.Title = "Сохранить файл";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                _lastFilePath = saveFileDialog.FileName;
                Save(memory, saveFileDialog.FileName);
            }
        }

        private void Save(Dictionary<int, ExtendedBitArray> memory, string path) {
            var count = Convert.ToInt32(Math.Pow(2, Constants.FarAddressBitsCount));
            var memoryArray = new List<string>(count);
            for (int i = 0; i < count; i++) {
                memoryArray.Add("0");
            }
            foreach (var pair in memory) {
                memoryArray[pair.Key] = pair.Value.NumValue().ToString();
            }
            var text = string.Join(Environment.NewLine, memoryArray);
            try {
                using (var sw = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write))) {
                    sw.Write(text);
                }
            } catch {
                MessageBox.Show("Невозможно записать файл.");
            }
        }
    }
}
