using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using _8bitVonNeiman.Core;

namespace _8bitVonNeiman.Compiler.Model {
    public class CompilerSupport {

        private const int MaxFarAddress = 2 ^ Constants.FarAddressBitsCount;

        /// <summary>
        /// Функция, которая приводит переданную строку к полному, 10-битовому адресу.
        /// Если введен слишком большой адрес или строка не является меткой, 
        /// будет сгенерированно исключение <see cref="CompilerEnvironment"/>.
        /// </summary>
        /// <param name="L">Строка-метка.</param>
        /// <param name="env">Текущее окружение компилятора.</param>
        /// <returns>Адресс, на который ссылается метка или который был записан как число.</returns>
        public static short ConvertToFarAddress(string L, CompilerEnvironment env) {
            return ConvertToAddress(L, env, MaxFarAddress);
        }


        private static short ConvertToAddress(string L, CompilerEnvironment env, int maxAddress) {
            try {
                if (L.All(c => c >= '0' && c <= '9')) {
                    short address = Convert.ToInt16(L, L[0] == 0 ? 8 : 10);
                    if (address > maxAddress) {
                        throw new OverflowException();
                    }
                    return address;
                } else if (L.StartsWith("0x") && L.Skip(2).All(c => c >= '0' && c <= '9')) {
                    return Convert.ToInt16(L.Substring(2), 16);
                } else {
                    short address = env.GetLabelAddress(L);
                    if (address == -1) {
                        throw new CompileErrorExcepton($"Метка с именем {L} не найдена.", env.GetCurrentLine());
                    }
                    return address;
                }
            } catch (OverflowException) {
                throw new CompileErrorExcepton("Адрес не должен превышать 255", env.GetCurrentLine());
            }
        }

        /// <summary>
        /// Заполняет младшие биты массива битов битами из переданного числа.
        /// </summary>
        /// <param name="bitArray">Массив бит, в который будут записываться биты.</param>
        /// <param name="number">Число, из которого будут браться биты.</param>
        /// <param name="bitsCount">Количество бит, которое будет записано.</param>
        public static void FillBitArray(BitArray bitArray, short number, int bitsCount) {
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
                !(word[0] <= '9' && word[1] >= '0');
        }
    }
}
