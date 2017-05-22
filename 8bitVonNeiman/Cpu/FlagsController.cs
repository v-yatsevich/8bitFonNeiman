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
            var mask = 0;
            switch (command.ToLower()) {
            case "add":
            case "sub":
            case "mul":
            case "div":
            case "cmp":
            case "adc":
            case "subb":
                mask = 1 + 2 + 4 + 8 + 16;
                break;
            case "and":
            case "or":
            case "xor":
            case "not":
                Z = false;
                N = false;
                O = false;
                mask = 8 + 16;
                break;
            case "inc":
            case "dec":
                mask = 2 + 4 + 8 + 16;
                break;
            }
            FormFlags(newState, mask, overflow);
        }

        public void FormFlags(ExtendedBitArray newState, int mask, bool? overflow) {
            if ((mask & 1) != 0) {
                Z = newState.NumValue() == 0;
            }
            if ((mask & 2) != 0) {
                N = newState[Constants.WordSize - 1];
            }
            if ((mask & 4) != 0) {
                
            }
            if ((mask & 8) != 0) {
                
            }
            if ((mask & 16) != 0) {
                C = overflow.HasValue && overflow.Value;
            }
        }
    }
}
