using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Cpu {
    ///Класс: имплементирующий работу с флагами процессора
    public class FlagsController {
        public ExtendedBitArray Flags { get; private set; } = new ExtendedBitArray();
        private ExtendedBitArray _state = null;

        /// Аллиас регистра нуля
        public bool Z {
            get { return Flags[0]; }
            set { Flags[0] = value; }
        }

        /// Аллиас регистра отрицательного числа
        public bool N {
            get { return Flags[1]; }
            set { Flags[1] = value; }
        }

        /// Аллиас регистра переполнения со знаком
        public bool O {
            get { return Flags[2]; }
            set { Flags[2] = value; }
        }

        /// Аллиас регистра переноса из третьего в четвертый
        public bool A {
            get { return Flags[3]; }
            set { Flags[3] = value; }
        }

        /// Аллиас регистра переноса
        public bool C {
            get { return Flags[4]; }
            set { Flags[4] = value; }
        }

        public void SetPreviousState(ExtendedBitArray array) {
            _state = new ExtendedBitArray(array);
        }

        public void UpdateFlags(ExtendedBitArray newState, string command, bool? overflow = null) {
            
        }
    }
}
