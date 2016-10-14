using System;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using _8bitVonNeiman.Core;

namespace _8bitVonNeiman.Compiler.Model {
    public class CompilerSupport {

        /// <summary>
        /// Структура, для более удобной работы с регистрами при компиляции.
        /// </summary>
        public struct Register {
            /// <summary>
            /// Номер регистра
            /// </summary>
            public int Number;
            /// <summary>
            /// Тип адресации. True, если прямая, False, если косвенная.
            /// </summary>
            public bool IsDirect;
            /// <summary>
            /// Показывает, должен ли быть изменен регистр при выполении команды.
            /// </summary>
            public bool IsChange;
            /// <summary>
            /// True, если регистр должен быть увеличен, False, если уменьшен.
            /// </summary>
            public bool IsIncrement;
            /// <summary>
            /// True, если регист должен быть изменен после обращения, False, если перед.
            /// </summary>
            public bool IsPostchange;
        }

        public const int MaxFarAddress = 2 ^ Constants.FarAddressBitsCount - 1;

        /// <summary>
        /// Функция, которая приводит переданную строку к полному, 10-битовому адресу.
        /// Если введен слишком большой адрес или строка не является меткой, 
        /// будет сгенерированно исключение <see cref="CompilerEnvironment"/>.
        /// </summary>
        /// <param name="label">Строка-метка или адрес в памяти.</param>
        /// <param name="env">Текущее окружение компилятора.</param>
        /// <returns>Адресс, на который ссылается метка или который был записан как число.</returns>
        public static int ConvertToFarAddress(string label, CompilerEnvironment env) {
            return ConvertToAddress(label, env, MaxFarAddress);
        }

        /// <summary>
        /// Функция, которая приводит переданную строку к полному, 10-битовому адресу.
        /// Если введен слишком большой адрес или строка не является меткой, 
        /// будет сгенерированно исключение <see cref="CompilerEnvironment"/>.
        /// </summary>
        /// <param name="label">Строка-метка или адрес в памяти.</param>
        /// <param name="env">Текущее окружение компилятора.</param>
        /// <param name="maxAddress">Максимальный адрес, который может быть использован.</param>
        /// <returns>Адресс, на который ссылается метка или который был записан как число.</returns>
        private static int ConvertToAddress(string label, CompilerEnvironment env, int maxAddress) {
            try {
                if (label[0] >= '0' && label[0] <= '9') {
                    int address = ConvertToInt(label);
                    if (address > maxAddress) {
                        throw new OverflowException();
                    }
                    return address;
                } else {
                    int address = env.GetLabelAddress(label);
                    if (address == -1) {
                        throw new CompilationErrorExcepton($"Метка с именем {label} не найдена.", env.GetCurrentLine());
                    }
                    return address;
                }
            } catch (OverflowException) {
                throw new CompilationErrorExcepton($"Адрес не должен превышать {maxAddress}", env.GetCurrentLine());
            } catch (FormatException) {
                throw new CompilationErrorExcepton("Некорректный адрес метки", env.GetCurrentLine());
            } catch (Exception e) {
                throw new CompilationErrorExcepton("Непредвиденная ошибка при обработке метки", env.GetCurrentLine(), e);
            }
        }

        public static int ConvertToInt(string s) {    
            if (s.StartsWith("0x")) {
                return Convert.ToInt32(s.Substring(2), 16);
            } else if (s.StartsWith("0b")) {
                return Convert.ToInt32(s.Substring(2), 2);
            } else {
                return Convert.ToInt32(s);
            }
        }

        /// <summary>
        /// Заполняет младшие биты массива битов битами из переданного числа.
        /// </summary>
        /// <param name="bitArray">Массив бит, в который будут записываться биты.</param>
        /// <param name="number">Число, из которого будут браться биты.</param>
        /// <param name="bitsCount">Количество бит, которое будет записано.</param>
        public static void FillBitArray(BitArray bitArray, int number, int bitsCount) {
            for (int i = 0; i < bitsCount; i++) {
                bitArray[i] = (number & 2 ^ i) == 1;
            }
        }

        /// <summary>
        /// Проверяет слово на корректность для использования в качестве метки, переменной или аргумента.
        /// </summary>
        /// <param name="word">Слово для проверки на корректность.</param>
        public static bool CheckWord(string word) {
            return word.Length != 0 && 
                Regex.IsMatch(word, @"^[a-zA-Z0-9_-]+$") && 
                !(word[0] <= '9' && word[0] >= '0');
        }

        /// <summary>
        /// Получает регистр из строки. Возвращает null если не удается выполнить преобразование.
        /// </summary>
        /// <param name="s">Строка, из которой необходимо получить регистр.</param>
        /// <returns>Регистр, полученный из строки. Null, если не удалось совершить преобразование.</returns>
        public static Register? ConvertToRegister(string s) {
            if (s.Length < 2 || s.Length > 4) {
                return null;
            }
            Register register = new Register();
            register.IsChange = false;
            register.IsDirect = true;
            try {
                int counter = 0;
                if (s[counter] == '+') {
                    register.IsChange = true;
                    register.IsIncrement = true;
                    register.IsPostchange = false;
                    counter++;
                } else if (s[counter] == '-') {
                    register.IsChange = true;
                    register.IsIncrement = false;
                    register.IsPostchange = false;
                    counter++;
                }
                if (s[counter] == '@') {
                    register.IsDirect = false;
                    counter++;
                }
                if (s[counter] != 'R' && s[counter] != 'r') {
                    return null;
                }
                counter++;
                register.Number = Convert.ToInt32(s[counter].ToString(), 16);
                counter++;
                if (counter == s.Length) {
                    return register;
                }
                if (s[counter] == '+') {
                    if (register.IsChange) {
                        return null;
                    }
                    register.IsChange = true;
                    register.IsIncrement = true;
                    register.IsPostchange = true;
                    counter++;
                } else if (s[counter] == '-') {
                    if (register.IsChange) {
                        return null;
                    }
                    register.IsChange = true;
                    register.IsIncrement = false;
                    register.IsPostchange = true;
                    counter++;
                }
                if (counter == s.Length) {
                    return register;
                } else {
                    return null;
                }
            } catch {
                return null;
            }
        }
    }
}
