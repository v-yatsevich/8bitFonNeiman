using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Cpu {
    public interface ICpuModelOutput {
        /// <summary>
        /// Возвращает память, содержащуюся по переданному адресу.
        /// </summary>
        /// <param name="address">Адрес, по которому запрашивается память.</param>
        /// <returns>Память, содержащаяся по переданному адресу.</returns>
        BitArray GetMemory(int address);

        /// <summary>
        /// Устанавливает значение конкретной ячейки памяти.
        /// </summary>
        /// <param name="memory">Значение ячейки памяти.</param>
        /// <param name="address">Адрес ячейки памяти.</param>
        void SetMemory(BitArray memory, int address);
    }
}
