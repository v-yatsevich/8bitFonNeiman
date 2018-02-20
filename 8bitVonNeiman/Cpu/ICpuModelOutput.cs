using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Cpu {
    public interface ICpuModelOutput {
        /// <summary>
        /// Возвращает память, содержащуюся по переданному адресу.
        /// </summary>
        /// <param name="address">Адрес, по которому запрашивается память.</param>
        /// <returns>Память, содержащаяся по переданному адресу.</returns>
        ExtendedBitArray GetMemory(int address);

        /// <summary>
        /// Устанавливает значение конкретной ячейки памяти.
        /// </summary>
        /// <param name="memory">Значение ячейки памяти.</param>
        /// <param name="address">Адрес ячейки памяти.</param>
        void SetMemory(ExtendedBitArray memory, int address);

        void commandHasRun();
    }
}
