using _8bitVonNeiman.Common;
using System.Collections.Generic;

namespace _8bitVonNeiman.Memory {
    public interface IMemoryControllerInput {
        /// Функция, показывающая форму, если она закрыта
        void ChangeFormState();

        /// <summary>
        /// Устанавливает память.
        /// </summary>
        /// <param name="memory">Память, которую требуется установить в контроллер.</param>
        void SetMemory(Dictionary<int, ExtendedBitArray> memory);

        /// <summary>
        /// Устанавливает значение конкретной ячейки памяти.
        /// </summary>
        /// <param name="memory">Значение ячейки памяти.</param>
        /// <param name="address">Адрес ячейки памяти.</param>
        void SetMemory(ExtendedBitArray memory, int address);

        /// <summary>
        /// Возвращает значение конкретной ячейки памяти.
        /// </summary>
        /// <param name="address">Адрес ячейки памяти.</param>
        /// <returns>Значение ячейки памяти.</returns>
        ExtendedBitArray GetMemory(int address);
        
        /// Возвращает массив памяти переданного сегмента. Если номер сегмента неходится вне диапазона 0..3 будет возвращен пустой массив.
        List<ExtendedBitArray> GetMemoryFromSegment(int segment);
    }
}
