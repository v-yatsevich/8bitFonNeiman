using System;
using System.Collections;
using System.Collections.Generic;

namespace _8bitVonNeiman.Compiler.Model {

    /// Класс для представления состояния среды на текущем моменте компиляции.
    public class CompilerEnvironment {

        private Dictionary<int, BitArray> _memory = new Dictionary<int, BitArray>();

        /// Текущая линия кода, на которой находится обработка.
        private int _currentLine;

        private int _currentAddress;
        /// Текущий адрес памяти.
        public int CurrentAddress {
            get { return _currentAddress; }
            set {
                if (value >= 256) {
                    throw new ArgumentOutOfRangeException("Адрес не может быть больше 255");
                }
                _currentAddress = value;
            }
        }

        public CompilerEnvironment() {
            _currentLine = 0;
            _currentAddress = 8;
        }

        private int _defaultCodeSegment;
        /// <summary>
        /// Номер сегмента кода, который будет установлен при сбросе и в который будет записываться код. 
        /// Меняется директивой /Dn, где n - число от 0 до 3
        /// </summary>
        public int DefaultCodeSegment {
            get { return _defaultCodeSegment; }
            set {
                if (value < 0 || value > 3) {
                    throw new CompilationErrorExcepton("Неверный номер сегмента кода.", _currentLine);
                }
                _defaultCodeSegment = value;
            }
        }

        private int _defaultDataSegment;
        /// Номер сегмента данных, который будет установлен при сбросе. Меняется директивой /Dn, где n - число от 0 до 3
        public int DefaultDataSegment {
            get { return _defaultDataSegment; }
            set {
                if (value < 0 || value > 3) {
                    throw new CompilationErrorExcepton("Неверный номер сегмента данных.", _currentLine);
                }
                _defaultDataSegment = value;
            }
        }

        private int _defaultStackSegment;
        /// Номер сегмента стека, который будет установлен при сбросе. Меняется директивой /Dn, где n - число от 0 до 3
        public int DefaultStackSegment {
            get { return _defaultStackSegment; }
            set {
                if (value < 0 || value > 3) {
                    throw new CompilationErrorExcepton("Неверный номер сегмента данных.", _currentLine);
                }
                _defaultStackSegment = value;
            }
        }

        /// Множество меток.
        private readonly Dictionary<string, int> _labels = new Dictionary<string, int>();

        public int GetCurrentLine() {
            return _currentLine;
        }

        public void IncrementLine() {
            _currentLine++;
        }

        public Dictionary<int, BitArray> GetMemory() {
            return _memory;
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
        public int GetLabelAddress(string label) {
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

        public int GetLabelsCount() {
            return _labels.Count;
        }
    }
}
