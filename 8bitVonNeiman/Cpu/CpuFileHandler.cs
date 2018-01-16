using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Cpu.View;

namespace _8bitVonNeiman.Cpu {
    class CpuFileHandler {
        private string _lastFilePath;

        public CpuState LoadState() {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Cpu files (*.cpu8)|*.cpu8|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                using (var sr = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))) {
                    string text, key;
                    byte value;
                    var stateDict = new Dictionary<string, ExtendedBitArray>();
                    while (true) {
                        text = sr.ReadLine();
                        if (text == null) {
                            break;
                        }
                        var array = text.Split(':');
                        if (array.Length != 2) {
                            MessageBox.Show("Неверный формат файла. Проверьте, что в каждой строке находятся название параметра и число от 0 до 255, разделенные двоеточием.");
                            return null;
                        }
                        try {
                            stateDict[array[0]] = new ExtendedBitArray(Convert.ToByte(array[1]));
                        } catch {
                            MessageBox.Show("Неверный формат файла. Проверьте, что в каждой строке находятся название параметра и число от 0 до 255, разделенные двоеточием.");
                            return null;
                        }
                    }
                    return StateFromDict(stateDict);
                }
            }
            return null;
        }

        public void Save(CpuState state) {
            if (_lastFilePath == null) {
                SaveAs(state);
            } else {
                Save(state, _lastFilePath);
            }
        }

        public void SaveAs(CpuState state) {
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Cpu files (*.cpu8)|*.cpu8";
            saveFileDialog.DefaultExt = "cpu8";
            if (_lastFilePath == null) {
                saveFileDialog.FileName = "cpu.cpu8";
            } else {
                saveFileDialog.FileName = _lastFilePath;
            }
            saveFileDialog.Title = "Сохранить файл";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                _lastFilePath = saveFileDialog.FileName;
                Save(state, saveFileDialog.FileName);
            }
        }

        private void Save(CpuState state, string path) {
            var text = StringFromState(state);
            try {
                using (var sw = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write))) {
                    sw.Write(text);
                }
            } catch {
                MessageBox.Show("Невозможно записать файл.");
            }
        }

        private CpuState StateFromDict(Dictionary<string, ExtendedBitArray> dict) {
            var acc = dict["acc"];
            var dr = dict["dr"];
            var psw = dict["psw"];
            var ss = dict["ss"].NumValue();
            var ds = dict["ds"].NumValue();
            var cs = dict["cs"].NumValue();
            var pcl = dict["pcl"].NumValue();
            var spl = dict["spl"].NumValue();
            ExtendedBitArray[] cr = { dict["cr1"], dict["cr2"] };
            var registers = new List<ExtendedBitArray>(8);
            for (int i = 0; i < 8; i++) {
                registers.Add(dict[$"r{i}"]);
            }

            return new CpuState(acc, dr, psw, ss, ds, cs, pcl, spl, cr, registers);
        }

        private string StringFromState(CpuState state) {
            var text = "";
            text += "acc:" + state.Acc.NumValue() + Environment.NewLine;
            text += "dr:" + state.Dr.NumValue() + Environment.NewLine;
            text += "psw:" + state.Psw.NumValue() + Environment.NewLine;
            text += "ss:" + state.Ss + Environment.NewLine;
            text += "ds:" + state.Ds + Environment.NewLine;
            text += "cs:" + state.Cs + Environment.NewLine;
            text += "pcl:" + state.Pcl + Environment.NewLine;
            text += "spl:" + state.Spl + Environment.NewLine;
            text += "cr1:" + state.Cr[0].NumValue() + Environment.NewLine;
            text += "cr2:" + state.Cr[1].NumValue() + Environment.NewLine;
            for (int i = 0; i < 8; i++) {
                text += $"r{i}:" + state.Registers[i].NumValue() + Environment.NewLine;
            }
            return text;
        }
    }
}
