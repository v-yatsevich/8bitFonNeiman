using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using _8bitVonNeiman.Controller;

namespace _8bitVonNeiman.Compiler.Model {
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class CommandProcessorFactory {

        public delegate void CommandProcessor(string[] args, CompilerEnvironment env);

        public class DataResponse {
            public BitArray lowBitArray { get; set; }
            public BitArray highBitArray { get; set; }
        }

        public static Dictionary<string, CommandProcessor> GetCommandProcessors() {
            return NoAddressCommandsFactory.GetCommands()
                .Concat(CycleCommandsFactory.GetCommands())
                .Concat(JumpCommandsFactory.GetCommands())
                .Concat(RamCommands.GetCommands())
                .Concat(BitRamCommands.GetCommands())
                .Concat(BitRegisterCommands.GetCommands())
                .Concat(InOutCommands.GetCommands())
                .Concat(RegisterCommands.GetCommands())
                .Concat(RamRegisterCommands.GetCommands())
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private static class NoAddressCommandsFactory {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["nop"] = NOP,
                    ["ret"] = RET,
                    ["iret"] = IRET,
                    ["ei"] = EI,
                    ["di"] = DI,
                    ["rr"] = RR,
                    ["rl"] = RL,
                    ["rrc"] = RRC,
                    ["rlc"] = RLC,
                    ["hlt"] = HLT,
                    ["inca"] = INCA,
                    ["deca"] = DECA,
                    ["swapa"] = SWAPA,
                    ["daa"] = DAA,
                    ["dsa"] = DSA,
                    ["es"] = ES,
                    ["movasr"] = MOVASR,
                    ["movsra"] = MOVSRA
                };
            }

            private static void NOP(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "NOP", env.GetCurrentLine());
                var array = new BitArray(8) { [0] = true };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void RET(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RET", env.GetCurrentLine());
                var array = new BitArray(8) { [1] = true };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void IRET(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "IRET", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [1] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void EI(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "EI", env.GetCurrentLine());
                var array = new BitArray(8) { [2] = true };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void DI(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DI", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [2] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void RR(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RR", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [1] = true,
                    [2] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void RL(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RL", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [1] = true,
                    [2] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void RRC(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RRC", env.GetCurrentLine());
                var array = new BitArray(8) { [3] = true };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void RLC(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RLC", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [3] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void HLT(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "HLT", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [1] = true,
                    [3] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void INCA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "INCA", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [1] = true,
                    [3] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void DECA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DECA", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [2] = true,
                    [3] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void SWAPA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "SWAPA", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [2] = true,
                    [3] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void DAA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DAA", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [1] = true,
                    [2] = true,
                    [3] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void DSA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DSA", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [1] = true,
                    [2] = true,
                    [3] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void ES(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "ES", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [1] = true,
                    [4] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void MOVASR(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "MOVASR", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [2] = true,
                    [4] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void MOVSRA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "MOVSRA", env.GetCurrentLine());
                var array = new BitArray(8) {
                    [0] = true,
                    [2] = true,
                    [4] = true
                };
                env.SetByte(array);
                env.SetByte(new BitArray(8));
            }

            private static void ValidateNoAddressCommand(string[] args, string op, int line) {
                if (args.Length != 0) {
                    throw new CompilationErrorExcepton($"Оператор {op} не должен принимать никаких аргументов", line);
                }
            }
        }

        private static class CycleCommandsFactory {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {["djrnz"] = DJRNZ};
            }

            private static void DJRNZ(string[] args, CompilerEnvironment env) {
                if (args.Length != 2) {
                    throw new CompilationErrorExcepton("Оператор DJRNZ должен принимать ровно 2 аргумента.", env.GetCurrentLine());
                }

                var R = CompilerSupport.ConvertToRegister(args[0]);
                if (!R.HasValue) {
                    throw new CompilationErrorExcepton("Первым аргументом должен быть регистр.", env.GetCurrentLine());
                }
                var register = R.Value;
                if (!register.IsDirect) {
                    throw new CompilationErrorExcepton("В этой команде нельзя использовать косвенную адерсацию.", env.GetCurrentLine());
                }
                if (register.Number > 3) {
                    throw new CompilationErrorExcepton("В этой команде можно использовать только первые 4 регистра.", env.GetCurrentLine());
                }
                string L = args[1];
                int address = CompilerSupport.ConvertLabelToFarAddress(L, env);
                
                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [5] = (register.Number & 1) == 1,
                    [6] = (register.Number & 2) == 1,
                    [7] = true
                };

                if (address == -1) {
                    var memoryForLabel = new CompilerEnvironment.MemoryForLabel {
                        HighBitArray = highBitArray,
                        LowBitArray = lowBitArray,
                        Address = env.CurrentAddress
                    };
                    env.SetCommandWithoutLabel(memoryForLabel, L);
                    return;
                }

                CompilerSupport.FillBitArray(highBitArray, lowBitArray, address, Constants.FarAddressBitsCount);
                env.SetByte(lowBitArray);
                env.SetByte(highBitArray);
            }
        }

        private static class JumpCommandsFactory {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["jnz"] = JNZ,
                    ["jnc"] = JNC,
                    ["jns"] = JNS,
                    ["jno"] = JNO,
                    ["jz"] = JZ,
                    ["jc"] = JC,
                    ["js"] = JS,
                    ["jo"] = JO,
                    ["jmp"] = JMP,
                    ["call"] = CALL,
                    ["int"] = INT
                };
            }

            private static void JNZ(string[] args, CompilerEnvironment env) {
                Validate(args, "JNZ", env.GetCurrentLine());
                
                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [5] = true
                };
                
                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JNC(string[] args, CompilerEnvironment env) {
                Validate(args, "JNC", env.GetCurrentLine());
                
                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [2] = true,
                    [5] = true
                };

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JNS(string[] args, CompilerEnvironment env) {
                Validate(args, "JNS", env.GetCurrentLine());
  
                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [3] = true,
                    [5] = true
                };

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JNO(string[] args, CompilerEnvironment env) {
                Validate(args, "JNO", env.GetCurrentLine());

                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [3] = true,
                    [4] = true,
                    [5] = true
                };

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JZ(string[] args, CompilerEnvironment env) {
                Validate(args, "JZ", env.GetCurrentLine());

                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [4] = true,
                    [5] = true
                };

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JC(string[] args, CompilerEnvironment env) {
                Validate(args, "JC", env.GetCurrentLine());
                
                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [3] = true,
                    [4] = true,
                    [5] = true
                };
                
                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JS(string[] args, CompilerEnvironment env) {
                Validate(args, "JS", env.GetCurrentLine());
                
                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [3] = true,
                    [4] = true,
                    [5] = true
                };

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JO(string[] args, CompilerEnvironment env) {
                Validate(args, "JO", env.GetCurrentLine());

                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [2] = true,
                    [3] = true,
                    [4] = true,
                    [5] = true
                };
                
                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void JMP(string[] args, CompilerEnvironment env) {
                Validate(args, "JMP", env.GetCurrentLine());

                var lowBitArray = new BitArray(8);
                var highBitArray = new BitArray(8) {
                    [6] = true
                };

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void CALL(string[] args, CompilerEnvironment env) {
                Validate(args, "CALL", env.GetCurrentLine());

                var highBitArray = new BitArray(8);
                var lowBitArray = new BitArray(8);
                
                highBitArray[2] = true;
                highBitArray[6] = true;

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void INT(string[] args, CompilerEnvironment env) {
                Validate(args, "INT", env.GetCurrentLine());

                var highBitArray = new BitArray(8);
                var lowBitArray = new BitArray(8);
                
                highBitArray[3] = true;
                highBitArray[6] = true;

                FillAddressAndSetCommand(highBitArray, lowBitArray, args[0], env);
            }

            private static void Validate(string[] args, string op, int line) {
                if (args.Length != 1) {
                    throw new CompilationErrorExcepton($"Оператор {op} должен принимать 1 аргумент.", line);
                }
            }

            private static void FillAddressAndSetCommand(BitArray highBitArray, BitArray lowBitArray, string label, CompilerEnvironment env) {
                int address = CompilerSupport.ConvertLabelToFarAddress(label, env);

                if (address == -1) {
                    var memoryForLabel = new CompilerEnvironment.MemoryForLabel {
                        HighBitArray = highBitArray,
                        LowBitArray = lowBitArray,
                        Address = env.CurrentAddress
                    };
                    env.SetCommandWithoutLabel(memoryForLabel, label);
                    return;
                }

                CompilerSupport.FillBitArray(highBitArray, lowBitArray, address, Constants.FarAddressBitsCount);

                env.SetByte(lowBitArray);
                env.SetByte(highBitArray);
            }
        }

        private static class RamCommands {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["ads"] = ADC,
                    ["subb"] = SUBB,
                    ["xch"] = XCH
                };
            }

            private static void ADC(string[] args, CompilerEnvironment env) {
                Validate(args, "ADC", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.highBitArray[0] = true;
                dataResponse.highBitArray[2] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SUBB(string[] args, CompilerEnvironment env) {
                Validate(args, "SUBB", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.highBitArray[1] = true;
                dataResponse.highBitArray[2] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void XCH(string[] args, CompilerEnvironment env) {
                Validate(args, "XCH", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.highBitArray[0] = true;
                dataResponse.highBitArray[1] = true;
                dataResponse.highBitArray[2] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static DataResponse GetBitArrays(string[] args, CompilerEnvironment env) {
                var dataResponse = new DataResponse {
                    lowBitArray = new BitArray(8),
                    highBitArray = new BitArray(8) {
                        [5] = true,
                        [6] = true
                    }
                };

                if (args[0][0] == '#') {
                    int num = CompilerSupport.ConvertToInt(args[0].Substring(1));
                    CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, num, Constants.ShortAddressBitsCount);
                    dataResponse.highBitArray[4] = true;
                    return dataResponse;
                }

                int address = CompilerSupport.ConvertVariableToAddress(args[0], env);
                if (address == -1) {
                    throw new CompilationErrorExcepton($"Переменной с именем {args[0]} не существует.", env.GetCurrentLine());
                }
                CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, address, Constants.ShortAddressBitsCount);
                return dataResponse;
            }

            private static void Validate(string[] args, string op, int line) {
                if (args.Length != 1) {
                    throw new CompilationErrorExcepton($"Оператор {op} должен принимать 1 аргумент.", line);
                }
            }
        }

        private static class RegisterCommands {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["push"] = PUSH,
                    ["pop"] = POP,
                    ["mov"] = MOV
                };
            }

            private static void PUSH(string[] args, CompilerEnvironment env) {
                if (args.Length != 1) {
                    throw new CompilationErrorExcepton("Оператор PUSH должен принимать 1 аргумент.", env.GetCurrentLine());
                }
                var r = CompilerSupport.ConvertToRegister(args[0]);
                if (!r.HasValue) {
                    throw new CompilationErrorExcepton("Аргументом должен быть регистр.", env.GetCurrentLine());
                }
                var registr = r.Value;
                if (!registr.IsDirect) {
                    throw new CompilationErrorExcepton("Адресация регистра должна быть прямой.", env.GetCurrentLine());
                }
                var highBitArray = new BitArray(8) {
                    [6] = true,
                    [4] = true,
                    [3] = true,
                    [2] = true,
                    [1] = true
                };
                var lowBitArray = new BitArray(8);
                CompilerSupport.FillBitArray(null, lowBitArray, registr.Number, 4);

                env.SetByte(lowBitArray);
                env.SetByte(highBitArray);
            }

            private static void POP(string[] args, CompilerEnvironment env) {
                if (args.Length != 1) {
                    throw new CompilationErrorExcepton("Оператор POP должен принимать 1 аргумент.", env.GetCurrentLine());
                }
                var r = CompilerSupport.ConvertToRegister(args[0]);
                if (!r.HasValue) {
                    throw new CompilationErrorExcepton("Аргументом должен быть регистр.", env.GetCurrentLine());
                }
                var registr = r.Value;
                if (!registr.IsDirect) {
                    throw new CompilationErrorExcepton("Адресация регистра должна быть прямой.", env.GetCurrentLine());
                }
                var highBitArray = new BitArray(8) {
                    [6] = true,
                    [4] = true,
                    [3] = true,
                    [2] = true,
                    [0] = true
                };
                var lowBitArray = new BitArray(8);
                CompilerSupport.FillBitArray(null, lowBitArray, registr.Number, 4);

                env.SetByte(lowBitArray);
                env.SetByte(highBitArray);
            }

            private static void MOV(string[] args, CompilerEnvironment env) {
                if (args.Length != 2) {
                    throw new CompilationErrorExcepton("Оператор MOV должен принимать 2 аргументa.", env.GetCurrentLine());
                }
                var r1 = CompilerSupport.ConvertToRegister(args[0]);
                var r2 = CompilerSupport.ConvertToRegister(args[1]);
                if (!r1.HasValue || !r2.HasValue) {
                    throw new CompilationErrorExcepton("Аргументом должен быть регистр.", env.GetCurrentLine());
                }
                var registr1 = r1.Value;
                var registr2 = r1.Value;
                if (!registr1.IsDirect || !registr2.IsDirect) {
                    throw new CompilationErrorExcepton("Адресация регистра должна быть прямой.", env.GetCurrentLine());
                }
                var highBitArray = new BitArray(8) {
                    [6] = true,
                    [4] = true,
                    [3] = true,
                    [2] = true,
                    [1] = true,
                    [0] = true
                };
                var lowBitArray = new BitArray(8);
                CompilerSupport.FillBitArray(null, lowBitArray, registr1.Number, 4);
                CompilerSupport.FillBitArray(null, lowBitArray, registr2.Number << 4, 4);

                env.SetByte(lowBitArray);
                env.SetByte(highBitArray);
            }
        }

        private static class RamRegisterCommands {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["add"] = ADD,
                    ["sub"] = SUB,
                    ["mul"] = MUL,
                    ["div"] = DIV,
                    ["and"] = AND,
                    ["or"] = OR,
                    ["xor"] = XOR,
                    ["cmp"] = CMP,
                    ["rd"] = RD,
                    ["wr"] = WR,
                    ["inc"] = INC,
                    ["dec"] = DEC,
                    ["not"] = NOT,
                };
            }

            private static void ADD(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "ADD", env);
                dataResponse.highBitArray[0] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SUB(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "SUB", env);
                dataResponse.highBitArray[1] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void MUL(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "MUL", env);
                dataResponse.highBitArray[0] = true;
                dataResponse.highBitArray[1] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void DIV(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "DIV", env);
                dataResponse.highBitArray[2] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void AND(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "AND", env);
                dataResponse.highBitArray[0] = true;
                dataResponse.highBitArray[2] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void OR(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "OR", env);
                dataResponse.highBitArray[1] = true;
                dataResponse.highBitArray[2] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void XOR(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "XOR", env);
                dataResponse.highBitArray[0] = true;
                dataResponse.highBitArray[1] = true;
                dataResponse.highBitArray[2] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void CMP(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "CMP", env);
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void RD(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "RD", env);
                dataResponse.highBitArray[0] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void WR(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "WR", env);
                dataResponse.highBitArray[1] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void INC(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "INC", env);
                dataResponse.highBitArray[0] = true;
                dataResponse.highBitArray[1] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void DEC(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "DEC", env);
                dataResponse.highBitArray[2] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void NOT(string[] args, CompilerEnvironment env) {
                var dataResponse = GetBitArrays(args, "NOT", env);

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static DataResponse GetBitArrays(string[] args, string op, CompilerEnvironment env) {
                if (args.Length < 0 || args.Length > 2) {
                    throw new CompilationErrorExcepton($"Команда {op} принимает 1 или 2 агрумента.", env.GetCurrentLine());
                }
                var dataResponse = new DataResponse {
                    lowBitArray = new BitArray(8),
                    highBitArray = new BitArray(8)
                };

                var r = CompilerSupport.ConvertToRegister(args[0]);
                if (r.HasValue) {
                    return DataResponseFromRegister(args, op, env, dataResponse, r.Value);
                }
                if (args.Length == 2) {
                    throw new CompilationErrorExcepton($"При работе с оперативной памятью команда {op} принимает только 1 аргумент", env.GetCurrentLine());
                }

                dataResponse.highBitArray[6] = true;
                dataResponse.highBitArray[5] = true;
                if (args[0][0] == '#') {
                    int num = CompilerSupport.ConvertToInt(args[0].Substring(1));
                    CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, num, Constants.ShortAddressBitsCount);
                    dataResponse.highBitArray[4] = true;
                } else {
                    var address = CompilerSupport.ConvertVariableToAddress(args[0], env);
                    CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, address, 8);
                }
                return dataResponse;
            }

            private static DataResponse DataResponseFromRegister(string[] args, string op, CompilerEnvironment env, DataResponse dataResponse, CompilerSupport.Register register) {
                dataResponse.highBitArray[6] = true;
                dataResponse.highBitArray[4] = true;
                CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, register.Number, 4);
                if (register.IsDirect) {
                    dataResponse.lowBitArray[4] = true;
                } else if (!register.IsChange) {
                    dataResponse.lowBitArray[5] = true;
                } else if (register.IsPostchange && register.IsIncrement) {
                    dataResponse.lowBitArray[4] = true;
                    dataResponse.lowBitArray[5] = true;
                } else if (register.IsPostchange) {
                    dataResponse.lowBitArray[6] = true;
                } else if (register.IsIncrement) {
                    dataResponse.lowBitArray[6] = true;
                    dataResponse.lowBitArray[4] = true;
                } else {
                    dataResponse.lowBitArray[6] = true;
                    dataResponse.lowBitArray[5] = true;
                }
                if (args.Length == 2) {
                    if (args[1] == "1") {
                        dataResponse.lowBitArray[7] = true;
                    } else if (args[1] == "0") {
                        return dataResponse;
                    }
                    throw new CompilationErrorExcepton(
                        $"Вторым аргументом у команды {op} может быть только 0 или 1.", env.GetCurrentLine());
                }
                return dataResponse;
            }
        }

        private static class BitRamCommands {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["cb"] = CB,
                    ["sb"] = SB,
                    ["sbc"] = SBC,
                    ["sbs"] = SBS
                };
            }

            private static void CB(string[] args, CompilerEnvironment env) {
                Validate(args, "CB", env.GetCurrentLine());

                var dataResponse = GetBitArrays(args, env);

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SB(string[] args, CompilerEnvironment env) {
                Validate(args, "SB", env.GetCurrentLine());

                var dataResponse = GetBitArrays(args, env);
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SBC(string[] args, CompilerEnvironment env) {
                Validate(args, "SBC", env.GetCurrentLine());

                var dataResponse = GetBitArrays(args, env);
                dataResponse.highBitArray[4] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SBS(string[] args, CompilerEnvironment env) {
                Validate(args, "SBS", env.GetCurrentLine());

                var dataResponse = GetBitArrays(args, env);
                dataResponse.highBitArray[3] = true;
                dataResponse.highBitArray[4] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static DataResponse GetBitArrays(string[] args, CompilerEnvironment env) {
                var dataResponse = new DataResponse {
                    lowBitArray = new BitArray(8),
                    highBitArray = new BitArray(8) {
                        [7] = true
                    }
                };

                int address = CompilerSupport.ConvertVariableToAddress(args[0], env);
                if (address == -1) {
                    throw new CompilationErrorExcepton($"Переменной с именем {args[0]} не существует.", env.GetCurrentLine());
                }
                CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, address, Constants.ShortAddressBitsCount);

                int bit = CompilerSupport.ConvertToInt(args[1]);
                if (bit >= 1 << 3 || bit < 0) {
                    throw new CompilationErrorExcepton("Номер бита не должен превышать 7", env.GetCurrentLine());
                }
                CompilerSupport.FillBitArray(null, dataResponse.highBitArray, bit, 3);

                return dataResponse;
            }

            private static void Validate(string[] args, string op, int line) {
                if (args.Length != 2) {
                    throw new CompilationErrorExcepton($"Оператор {op} должен принимать 2 аргумента.", line);
                }
            }
        }

        private static class BitRegisterCommands {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["cbi"] = CBI,
                    ["sbi"] = SBI,
                    ["nbi"] = NBI,
                    ["sbic"] = SBIC,
                    ["sbis"] = SBIS,
                    ["sbisc"] = SBISC
                };
            }

            private static void CBI(string[] args, CompilerEnvironment env) {
                Validate(args, "CBI", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.lowBitArray[7] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SBI(string[] args, CompilerEnvironment env) {
                Validate(args, "SBI", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void NBI(string[] args, CompilerEnvironment env) {
                Validate(args, "NBI", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.lowBitArray[7] = true;
                dataResponse.highBitArray[3] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SBIC(string[] args, CompilerEnvironment env) {
                Validate(args, "SBIC", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.highBitArray[4] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SBIS(string[] args, CompilerEnvironment env) {
                Validate(args, "SBIS", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.lowBitArray[7] = true;
                dataResponse.highBitArray[4] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void SBISC(string[] args, CompilerEnvironment env) {
                Validate(args, "SBISC", env.GetCurrentLine());
                var dataResponse = GetBitArrays(args, env);

                dataResponse.highBitArray[3] = true;
                dataResponse.highBitArray[4] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static DataResponse GetBitArrays(string[] args, CompilerEnvironment env) {
                int register = CompilerSupport.ConvertToInt(args[0]);
                if (register < 0 || register > 127) {
                    throw new CompilationErrorExcepton("Номер регистра должен быть числом от 0 до 127", env.GetCurrentLine());
                }
                int bit = CompilerSupport.ConvertToInt(args[1]);
                if (bit < 0 || bit > 7) {
                    throw new CompilationErrorExcepton("Номер бита должен быть числом от 0 до 7", env.GetCurrentLine());
                }
                var dataResponse = new DataResponse {
                    lowBitArray = new BitArray(8),
                    highBitArray = new BitArray(8) {
                        [7] = true,
                        [5] = true
                    }
                };
                CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, register, 7);
                CompilerSupport.FillBitArray(null, dataResponse.highBitArray, bit, 3);
                return dataResponse;
            }

            private static void Validate(string[] args, string op, int line) {
                if (args.Length != 2) {
                    throw new CompilationErrorExcepton($"Оператор {op} должен принимать 2 аргумента.", line);
                }
            }
        }

        private static class InOutCommands {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["in"] = IN,
                    ["out"] = OUT,
                };
            }

            private static void IN(string[] args, CompilerEnvironment env) {
                if (args.Length == 0) {
                    var array = new BitArray(8) {
                        [0] = true,
                        [4] = true
                    };
                    env.SetByte(array);
                    env.SetByte(new BitArray(8));
                    return;
                }
                if (args.Length > 0) {
                    throw new CompilationErrorExcepton("Команда IN не может принимать более 1 аргумента.", env.GetCurrentLine());
                }

                var dataResponse = GetBitArrays(args, env);

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static void OUT(string[] args, CompilerEnvironment env) {
                if (args.Length == 0) {
                    var array = new BitArray(8) {
                        [1] = true,
                        [4] = true
                    };
                    env.SetByte(array);
                    env.SetByte(new BitArray(8));
                    return;
                }
                if (args.Length > 0) {
                    throw new CompilationErrorExcepton("Команда OUT не может принимать более 1 аргумента.", env.GetCurrentLine());
                }
                var dataResponse = GetBitArrays(args, env);

                dataResponse.highBitArray[0] = true;

                env.SetByte(dataResponse.lowBitArray);
                env.SetByte(dataResponse.highBitArray);
            }

            private static DataResponse GetBitArrays(string[] args, CompilerEnvironment env) {
                int register = CompilerSupport.ConvertToInt(args[0]);
                if (register < 0 || register > 127) {
                    throw new CompilationErrorExcepton("Номер регистра должен быть числом от 0 до 127", env.GetCurrentLine());
                }
                var dataResponse = new DataResponse {
                    lowBitArray = new BitArray(8),
                    highBitArray = new BitArray(8) {
                        [7] = true,
                        [6] = true
                    }
                };
                CompilerSupport.FillBitArray(null, dataResponse.lowBitArray, register, 7);
                return dataResponse;
            }
        }
    }
}
