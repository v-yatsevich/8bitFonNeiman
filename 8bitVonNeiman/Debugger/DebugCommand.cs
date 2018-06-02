using _8bitVonNeiman.Common;
using System.Linq;

namespace _8bitVonNeiman.Debug {
    public class DebugCommand {
        public readonly int Address;
        public readonly string Name;
        public readonly string Command;
        public bool HasBreakpoint = false;
        public bool Selected = false;

        private ExtendedBitArray _lowCommand;
        private ExtendedBitArray _highCommand;

        public DebugCommand(ExtendedBitArray lowCommand, ExtendedBitArray highCommand, int address) {
            Address = address;
            _lowCommand = lowCommand;
            _highCommand = highCommand;

            Command = new string(lowCommand.ToBinString().Reverse().ToArray()) + ' ' + new string(highCommand.ToBinString().Reverse().ToArray());
            Name = GetCommandName(lowCommand, highCommand);
        }

        private string GetCommandName(ExtendedBitArray lowCommand, ExtendedBitArray highCommand) {
            var highBin = highCommand.ToBinString();
            var highHex = highCommand.ToHexString();
            var lowBin = lowCommand.ToBinString();
            var lowHex = lowCommand.ToHexString();

            // Безадресные
            if (highHex == "00") {
                if (lowHex == "00") {
                    return "HLT";
                }
                if (lowHex == "01") {
                    return "NOP";
                }
                if (lowHex == "02") {
                    return "RET";
                }
                if (lowHex == "03") {
                    return "IRET";
                }
                if (lowHex == "04") {
                    return "EI";
                }
                if (lowHex == "05") {
                    return "DI";
                }
                if (lowHex == "06") {
                    return "RR";
                }
                if (lowHex == "07") {
                    return "RL";
                }
                if (lowHex == "08") {
                    return "RRC";
                }
                if (lowHex == "09") {
                    return "RLC";
                }
                if (lowHex == "0A") {
                    return "HLT";
                }
                if (lowHex == "0B") {
                    return "INCA";
                }
                if (lowHex == "0C") {
                    return "DECA";
                }
                if (lowHex == "0D") {
                    return "SWAPA";
                }
                if (lowHex == "0E") {
                    return "DAA";
                }
                if (lowHex == "0F") {
                    return "DSA";
                }
                if (lowHex == "10") {
                    return "IN";
                }
                if (lowHex == "11") {
                    return "OUT";
                }
                if (lowHex == "12") {
                    return "ES";
                }
                if (lowHex == "13") {
                    return "MOVASR";
                }
                if (lowHex == "14") {
                    return "MOVSRA";
                }
            }

            // DJRNZ
            if (highBin.StartsWith("0001")) {
                return "DJRNZ";
            }

            // операторы перехода
            if (highBin.StartsWith("001")) {
                if (highBin.StartsWith("001100")) {
                    return "JZ";
                }
                if (highBin.StartsWith("001000")) {
                    return "JNZ";
                }
                if (highBin.StartsWith("001101")) {
                    return "JC";
                }
                if (highBin.StartsWith("001001")) {
                    return "JNC";
                }
                if (highBin.StartsWith("001110")) {
                    return "JN";
                }
                if (highBin.StartsWith("001010")) {
                    return "JNN";
                }
                if (highBin.StartsWith("001111")) {
                    return "JO";
                }
                if (highBin.StartsWith("001011")) {
                    return "JNO";
                }
            }

            // Операторы передачи управления
            if (highBin.StartsWith("0100")) {
                if (highBin.StartsWith("010000")) {
                    return "JMP";
                }
                if (highBin.StartsWith("010010")) {
                    return "CALL";
                }
                if (highBin.StartsWith("010011")) {
                    return "INT";
                }
            }

            // Регистровые команды
            if (highBin.StartsWith("0101") || highBin.StartsWith("1111")) {
                if (highHex[1] == 'F') {
                    return "MOV";
                }
                if (highHex[1] == 'D') {
                    return "POP";
                }
                if (highHex[1] == 'A') {
                    return "WR";
                }
                if (highHex[1] == '0') {
                    return "NOT";
                }
                if (highHex[1] == '1') {
                    return "ADD";
                }
                if (highHex[1] == '2') {
                    return "SUB";
                }
                if (highHex[1] == '3') {
                    return "MUL";
                }
                if (highHex[1] == '4') {
                    return "DIV";
                }
                if (highHex[1] == '5') {
                    return "AND";
                }
                if (highHex[1] == '6') {
                    return "OR";
                }
                if (highHex[1] == '7') {
                    return "XOR";
                }
                if (highHex[1] == '8') {
                    return "CMP";
                }
                if (highHex[1] == '9') {
                    return "RD";
                }
                if (highHex[1] == 'B') {
                    return "INC";
                }
                if (highHex[1] == 'C') {
                    return "DEC";
                }
                if (highHex[1] == 'E') {
                    return "PUSH";
                }
                if (highHex == "F0") {
                    return "ADC";
                }
                if (highHex == "F1") {
                    return "SUBB";
                }
            }

            // ОЗУ
            if (highBin.StartsWith("011")) {
                if (highHex[1] == 'A') {
                    return "WR";
                }
                if (highHex[1] == '0') {
                    return "NOT";
                }
                if (highHex[1] == '1') {
                    return "ADD";
                }
                if (highHex[1] == '2') {
                    return "SUB";
                }
                if (highHex[1] == '3') {
                    return "MUL";
                }
                if (highHex[1] == '4') {
                    return "DIV";
                }
                if (highHex[1] == '5') {
                    return "ADD";
                }
                if (highHex[1] == '6') {
                    return "OR";
                }
                if (highHex[1] == '7') {
                    return "XOR";
                }
                if (highHex[1] == '8') {
                    return "CMP";
                }
                if (highHex[1] == '9') {
                    return "RD";
                }
                if (highHex[1] == 'B') {
                    return "INC";
                }
                if (highHex[1] == 'C') {
                    return "DEC";
                }
                if (highHex[1] == 'D') {
                    return "ADC";
                }
                if (highHex[1] == 'E') {
                    return "SUBB";
                }
                if (highHex[1] == 'F') {
                    return "XCH";
                }
            }

            // Битовые команды
            if (highBin.StartsWith("10000")) {
                return "CB";
            }
            if (highBin.StartsWith("10001")) {
                return "SB";
            }
            if (highBin.StartsWith("10010")) {
                return "SBC";
            }
            if (highBin.StartsWith("10011")) {
                return "SBS";
            }
            if (highBin.StartsWith("10100") && lowBin[0] == '1') {
                return "CBI";
            }
            if (highBin.StartsWith("10101") && lowBin[0] == '0') {
                return "SBI";
            }
            if (highBin.StartsWith("10101") && lowBin[0] == '1') {
                return "NBI";
            }
            if (highBin.StartsWith("10110") && lowBin[0] == '0') {
                return "SBIC";
            }
            if (highBin.StartsWith("10110") && lowBin[0] == '1') {
                return "SBIS";
            }
            if (highBin.StartsWith("10111") && lowBin[0] == '0') {
                return "SBISC";
            }

            // Команды ввода-вывода
            if (highBin == "11000000") {
                return "IN";
            }
            if (highBin == "11000001") {
                return "OUT";
            }

            return "ERROR";
        }
    }
}
