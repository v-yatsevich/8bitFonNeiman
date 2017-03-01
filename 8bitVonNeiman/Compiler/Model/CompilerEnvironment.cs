using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Controller;

namespace _8bitVonNeiman.Compiler.Model {

    /// Класс для представления состояния среды на текущем моменте компиляции.
    public class CompilerEnvironment {

        public struct MemoryForLabel {
            public ExtendedBitArray HighBitArray;
            public ExtendedBitArray LowBitArray;
            public int Address;
            public int Line;
        }

        private Dictionary<int, ExtendedBitArray> _memory = new Dictionary<int, ExtendedBitArray>();
        private Dictionary<string, List<MemoryForLabel>> _memoryForLabels = new Dictionary<string, List<MemoryForLabel>>(); 

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
            _currentAddress = Constants.StartAddress;
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

        private int _defaultDataSegment = 1;
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

        private int _defaultStackSegment = 3;
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
        private readonly Dictionary<string, int> _variables = new Dictionary<string, int>();

        public int GetCurrentLine() {
            return _currentLine;
        }

        public void IncrementLine() {
            _currentLine++;
        }

        public Dictionary<int, ExtendedBitArray> GetMemory() {
            return _memory;
        }

        /// <summary>
        /// Устанавливает байт по текущему адресу и переходит к следующему адресу.
        /// При переполнении адреса заползает на следующий сегмент.
        /// </summary>
        /// <param name="command">Бассив бит: определяющих байт. Должен содержать 8 бит.</param>
        public void SetByte(ExtendedBitArray command) {
            _memory[(_defaultCodeSegment << 8) + _currentAddress] = command;
            _currentAddress++;
        }

        public void SetCommandWithoutLabel(MemoryForLabel memoryForLabel, string label) {
            if (!_memoryForLabels.ContainsKey(label)) {
                _memoryForLabels.Add(label, new List<MemoryForLabel>());
            }
            memoryForLabel.Line = _currentLine;
            _memoryForLabels[label].Add(memoryForLabel);
            _currentAddress += 2;
        }

        /// <summary>
        /// Возвращает первую неопределенную, но использованную метку, или null, если таких нет.
        /// </summary>
        /// <returns>Первую неопределенная, но использованная метку, или null, если таких нет.</returns>
        public KeyValuePair<int, string>? FirstCommandWithoutLabel() {
            if (_memoryForLabels.Count == 0) {
                return null;
            }
            var first = _memoryForLabels.First();
            return new KeyValuePair<int, string>(first.Value.First().Line, first.Key);
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
        /// Добавляет метку, ссылающуюся на адрес новой команды. Если существуют команды, использующие эту метку, подставляет ее ад
        /// </summary>
        /// <param name="label">Имя метки.</param>
        public void AddAddressLabelToNewCommand(string label) {
            _labels.Add(label, _currentAddress + (_defaultCodeSegment << 8));
            if (!_memoryForLabels.ContainsKey(label)) {
                return;
            }
            foreach (var memoryForLabel in _memoryForLabels[label]) {
                CompilerSupport.FillBitArray(memoryForLabel.HighBitArray, memoryForLabel.LowBitArray,
                _currentAddress, Constants.FarAddressBitsCount);
                _memory[memoryForLabel.Address] = memoryForLabel.HighBitArray;
                _memory[memoryForLabel.Address + 1] = memoryForLabel.LowBitArray;
            }
            _memoryForLabels.Remove(label);
        }

        public int GetLabelsCount() {
            return _labels.Count;
        }
        
        public void AddVariable(string name, int address) {
            _variables.Add(name, address);
        }

        /// <summary>
        /// Возвращает адрес, ассоциированный с переданным идентификатором.
        /// </summary>
        /// <param name="name">Идентификатор переменной.</param>
        /// <returns>Адрес переменной, если переменная с переданным идентификатором существует, -1 в ином случае.</returns>
        public int GetVariableAddress(string name) {
            if (_variables.ContainsKey(name)) {
                return _variables[name];
            } else {
                return -1;
            }
        }

        /// <summary>
        /// Проверяет, существует ли переданный идентификатор (в качестве имени переменной или метки).
        /// </summary>
        /// <param name="name">Идентификатор.</param>
        /// <returns>True, если переданный идентификатор уже существует, false в ином случае.</returns>
        public bool IsIdentifierExist(string name) {
            return _labels.ContainsKey(name) || _variables.ContainsKey(name);
        }
    }
}
