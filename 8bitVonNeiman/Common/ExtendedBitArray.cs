using System;

namespace _8bitVonNeiman.Common {

    /// Класс, предоставляющий удобную работу с числом как с массивом байт.
    public class ExtendedBitArray {

        /// Информация байта. При изменении размера слова нужно будет изменить тип.
        private byte _data;

        public ExtendedBitArray() {
            //Do nothing
        }

        /// <summary>
        /// Конструктор, принимающий на вход двоичную интерпретацию байта вида 00100111. 
        /// 
        /// При некорректном формате аргумента будет сгенерировано исключение.
        /// </summary>
        /// <param name="code">Байт в двоичном формате вида 00110110</param>
        public ExtendedBitArray(string code) {
            if (code.Length > Constants.WordSize) {
                throw new ArgumentException("Код байта не может быть больше " + new string('1', Constants.WordSize));
            }
            try {
                _data = (byte)Convert.ToInt32(code, 2);
            } catch (Exception e) {
                throw new ArgumentException($"Некорректный код байта ({code})", e);
            }
        }

        /// <summary>
        /// Создает копию передаваемого слова
        /// </summary>
        public ExtendedBitArray(ExtendedBitArray array) {
            _data = array._data;
        }

        /// <summary>
        /// Возвращает информацию байта в виде двоичной строки.
        /// </summary>
        /// <returns>Информация, хранимая в байте в виде двоичной строки.</returns>
        public string ToBinString() {
            var s = Convert.ToString(_data, 2);
            var diff = Constants.WordSize - s.Length;
            if (diff == 0) {
                return s;
            } else {
                return new string('0', diff) + s;
            }
        }

        /// <summary>
        /// Возвращает информацию байта в виде шестнадцатиричной строки.
        /// </summary>
        /// <returns>Инфомация, хранимая в байте в виде шестнадцатиричной строки.</returns>
        public string ToHexString() {
            return _data.ToString("X2");
        }

        /// <summary>
        /// Прибавляет значение передаваемого слова к текущему слову. Генерирует <see cref="OverflowException"/> при переполнении.
        /// </summary>
        /// <param name="array">Байт, значение которого прибавляется к текущему.</param>
        public void Add(ExtendedBitArray array) {
            checked {
                _data += array._data;
            }
        }

        /// <summary>
        /// Прибавляет значение передаваемого числа к текущему слову. Генерирует <see cref="OverflowException"/> при переполнении.
        /// </summary>
        /// <param name="num">Число, значение которого прибавляется к текущему байту.</param>
        public void Add(int num) {
            checked {
                _data += (byte)num;
            }
        }

        /// <summary>
        /// Возвращает или задает значения бита с переданным индексом.
        /// </summary>
        /// <param name="key">Индекс бита.</param>
        /// <returns>Значение бита.</returns>
        public bool this[int key] {
            get {
                if (key >= Constants.WordSize || key < 0) {
                    throw new ArgumentException("Индекс находится вне границ диапазлна");
                }
                return ((_data >> key) & 1) == 1;
            }
            set {
                if (key >= Constants.WordSize || key < 0) {
                    throw new ArgumentException("Индекс находится вне границ диапазлна");
                }
                int mask = 1 << key;
                if (value) {
                    _data |= (byte)mask;
                } else {
                    _data &= (byte)~mask;
                }
            }
        }
        
        /// Увеличивает значение слова на 1. При переполнении вызывает <see cref="OverflowException"/>
        public void Inc() {
            checked {
                _data++;
            }
        }

        /// Уменьшает значение слова на 1. При переполнении вызывает <see cref="OverflowException"/>
        public void Dec() {
            checked {
                _data--;
            }
        }

        /// Инвертирует все биты слова.
        public void Invert() {
            _data = (byte)~_data;
        }

        /// Возвращает числовую интерпретацию слова
        public int NumValue() {
            return _data;
        }
    }
}
