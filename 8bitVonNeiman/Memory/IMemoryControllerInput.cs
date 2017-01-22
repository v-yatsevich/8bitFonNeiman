using System.Collections;
using System.Collections.Generic;

namespace _8bitVonNeiman.Memory {
    public interface IMemoryControllerInput {
        /// <summary>
        /// Функция, показывающая форму, если она закрыта, и закрывающая ее, если она открыта
        /// </summary>
        void ChangeFormState();

        /// <summary>
        /// Устанавливает память.
        /// </summary>
        /// <param name="memory">Память, которую требуется установить в контроллер.</param>
        void SetMemory(Dictionary<int, BitArray> memory);

        /// <summary>
        /// Устанавливает значение конкретной ячейки памяти.
        /// </summary>
        /// <param name="memory">Значение ячейки памяти.</param>
        /// <param name="address">Адрес ячейки памяти.</param>
        void SetMemory(BitArray memory, int address);

        /// <summary>
        /// Возвращает значение конкретной ячейки памяти.
        /// </summary>
        /// <param name="address">Адрес ячейки памяти.</param>
        /// <returns>Значение ячейки памяти.</returns>
        BitArray GetMemory(int address);
    }
}
