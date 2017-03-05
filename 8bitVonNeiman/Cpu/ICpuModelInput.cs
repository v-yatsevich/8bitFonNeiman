using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Cpu {
    public interface ICpuModelInput {
        /// Функция делает "Шаг" процессора
        void Tick();

        void ChangeFormState();
    }
}
