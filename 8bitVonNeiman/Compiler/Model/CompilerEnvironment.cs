using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Compiler.Model {

    /// Класс для представления состояния среды на текущем моменте компиляции.
    public class CompilerEnvironment {

        /// Текущая линия кода, на которой находится обработка.
        private int _currentLine;

        /// Множество меток.
        private Dictionary<string, short> _labels = new Dictionary<string, short>();

        public CompilerEnvironment() {
            
        }

        public int GitCurrentLine() {
            return _currentLine;
        }

        public void IncrementLine() {
            _currentLine++;
        }

        /// <summary>
        /// Возвращает адрес по переданной метке. -1, если адрес не был найден.
        /// </summary>
        /// <param name="label">Строка-метка.</param>
        /// <returns>Адресс переданной метки. -1, если метки с таким именем не существует.</returns>
        public short GetLabelAddress(string label) {
            if (_labels.ContainsKey(label)) {
                return _labels[label];
            } else {
                return -1;
            }
        }
    }
}
