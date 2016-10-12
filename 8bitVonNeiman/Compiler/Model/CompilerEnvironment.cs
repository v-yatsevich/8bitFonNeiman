using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Compiler.Model {

    /// Класс для представления состояния среды на текущем моменте компиляции.
    public class CompilerEnvironment {

        private List<BitArray> _memory = new List<BitArray>(1024);

        /// Текущая линия кода, на которой находится обработка.
        private int _currentLine;

        /// Текущий адрес памяти.
        private short _currentAddress;

        /// <summary>
        /// Номер сегмента кода, который будет установлен при сбросе и в который будет записываться код. 
        /// Меняется директивой /Dn, где n - число от 0 до 3
        /// </summary>
        private int _defaultCodeSegment;

        /// Номер сегмента данных, который будет установлен при сбросе. Меняется директивой /Dn, где n - число от 0 до 3
        private int _defaultDataSegment;

        /// Номер сегмента стека, который будет установлен при сбросе. Меняется директивой /Dn, где n - число от 0 до 3
        private int _defaultStackSegment;

        /// Множество меток.
        private readonly Dictionary<string, short> _labels = new Dictionary<string, short>();

        public CompilerEnvironment() {
            _currentLine = 0;
            _currentAddress = 8;
        }

        public void SetDefaultDataSegment(int segment) {
            if (segment < 0 || segment > 3) {
                throw new CompilationErrorExcepton("Неверный номер сегмента данных.", _currentLine);
            }
            _defaultDataSegment = segment;
        }

        public void SetDefaultCodeSegment(int segment) {
            if (segment < 0 || segment > 3) {
                throw new CompilationErrorExcepton("Неверный номер сегмента кода.", _currentLine);
            }
            _defaultCodeSegment = segment;
        }

        public void SetDefaultStackSegment(int segment) {
            if (segment < 0 || segment > 3) {
                throw new CompilationErrorExcepton("Неверный номер сегмента данных.", _currentLine);
            }
            _defaultStackSegment = segment;
        }

        public int GetCurrentLine() {
            return _currentLine;
        }

        public void IncrementLine() {
            _currentLine++;
        }

        public void SetCurrentAddress(int address) {
            _currentAddress = (short)address;
        }

        public void SetCommand(BitArray command) {
            _memory[_defaultCodeSegment << 8 + _currentAddress] = command;
            _currentAddress++;
        }

        public void SetCommandWithoutLabel(BitArray command, string label) {
            //do nothing
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

        /// <summary>
        /// Добавляет метку, ссылающуюся на текущий адрес.
        /// </summary>
        /// <param name="label">Имя метки.</param>
        public void AddAddressLabelToCurrentAddress(string label) {
            _labels.Add(label, _currentAddress);
        }
    }
}
