using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using _8bitVonNeiman.Core;

namespace _8bitVonNeiman.Compiler.Model {
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class CommandProcessorFactory {

        public delegate void CommandProcessor(string[] args, CompilerEnvironment env);

        public static Dictionary<string, CommandProcessor> GetCommandProcessors() {
            return NoAddressCommandsFactory.GetCommands()
                .Concat(CycleCommandsFactory.GetCommands())
                .Concat(ConditionalJumpCommandsFactory.GetCommands())
                .Concat(UnconditionalJumpCommandsFactory.GetCommands())
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
                    ["in"] = IN,
                    ["out"] = OUT,
                    ["es"] = ES,
                    ["movasr"] = MOVASR,
                    ["movsra"] = MOVSRA
                };
            }

            private static void NOP(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "NOP", env.GetCurrentLine());
                BitArray array = new BitArray(16) { [0] = true };
                env.SetCommand(array);
            }

            private static void RET(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RET", env.GetCurrentLine());
                BitArray array = new BitArray(16) { [1] = true };
                env.SetCommand(array);
            }

            private static void IRET(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "IRET", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [1] = true
                };
                env.SetCommand(array);
            }

            private static void EI(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "EI", env.GetCurrentLine());
                BitArray array = new BitArray(16) { [2] = true };
                env.SetCommand(array);
            }

            private static void DI(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DI", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [2] = true
                };
                env.SetCommand(array);
            }

            private static void RR(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RR", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [1] = true,
                    [2] = true
                };
                env.SetCommand(array);
            }

            private static void RL(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RL", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [2] = true
                };
                env.SetCommand(array);
            }

            private static void RRC(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RRC", env.GetCurrentLine());
                BitArray array = new BitArray(16) { [3] = true };
                env.SetCommand(array);
            }

            private static void RLC(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "RLC", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [3] = true
                };
                env.SetCommand(array);
            }

            private static void HLT(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "HLT", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [1] = true,
                    [3] = true
                };
                env.SetCommand(array);
            }

            private static void INCA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "INCA", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [3] = true
                };
                env.SetCommand(array);
            }

            private static void DECA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DECA", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [2] = true,
                    [3] = true
                };
                env.SetCommand(array);
            }

            private static void SWAPA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "SWAPA", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [2] = true,
                    [3] = true
                };
                env.SetCommand(array);
            }

            private static void DAA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DAA", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [1] = true,
                    [2] = true,
                    [3] = true
                };
                env.SetCommand(array);
            }

            private static void DSA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "DSA", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [2] = true,
                    [3] = true
                };
                env.SetCommand(array);
            }

            private static void IN(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "IN", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [4] = true
                };
                env.SetCommand(array);
            }

            private static void OUT(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "OUT", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [1] = true,
                    [4] = true
                };
                env.SetCommand(array);
            }

            private static void ES(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "ES", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [4] = true
                };
                env.SetCommand(array);
            }

            private static void MOVASR(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "MOVASR", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [2] = true,
                    [4] = true
                };
                env.SetCommand(array);
            }

            private static void MOVSRA(string[] args, CompilerEnvironment env) {
                ValidateNoAddressCommand(args, "MOVSRA", env.GetCurrentLine());
                BitArray array = new BitArray(16) {
                    [0] = true,
                    [2] = true,
                    [4] = true
                };
                env.SetCommand(array);
            }

            private static void ValidateNoAddressCommand(string[] args, string op, int line) {
                if (args.Length != 0) {
                    throw new CompileErrorExcepton($"Оператор {op} не должен принимать никаких аргументов", line);
                }
            }
        }

        private static class CycleCommandsFactory {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {["djrnz"] = DJRNZ};
            }

            private static void DJRNZ(string[] args, CompilerEnvironment env) {
                if (args.Length != 2) {
                    throw new CompileErrorExcepton("Оператор DJRNZ должен принимать ровно 2 аргумента", env.GetCurrentLine());
                }

                string R = args[0];
                if (R.Length != 2 || R[0] != 'R' || R[1] < '0' || R[1] > '4') {
                    throw new CompileErrorExcepton("У оператора DJRNZ первым аргументом должен выступать регистр R*", env.GetCurrentLine());
                }

                string L = args[1];
                int address = CompilerSupport.ConvertToFarAddress(L, env);

                BitArray bitArray = new BitArray(16);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);
                bitArray[10] = (R[1] - '0' & 1) == 1;
                bitArray[11] = (R[1] - '0' & 2) == 1;
                bitArray[12] = true;
                env.SetCommand(bitArray);
            }
        }

        private static class ConditionalJumpCommandsFactory {
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
                };
            }

            private static void JNZ(string[] args, CompilerEnvironment env) {
                Validate(args, "JNZ", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void JNC(string[] args, CompilerEnvironment env) {
                Validate(args, "JNC", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[10] = true;
                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void JNS(string[] args, CompilerEnvironment env) {
                Validate(args, "JNS", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[11] = true;
                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void JNO(string[] args, CompilerEnvironment env) {
                Validate(args, "JNO", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[10] = true;
                bitArray[11] = true;
                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void JZ(string[] args, CompilerEnvironment env) {
                Validate(args, "JZ", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[12] = true;
                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void JC(string[] args, CompilerEnvironment env) {
                Validate(args, "JC", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[10] = true;
                bitArray[12] = true;
                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void JS(string[] args, CompilerEnvironment env) {
                Validate(args, "JS", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[11] = true;
                bitArray[12] = true;
                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void JO(string[] args, CompilerEnvironment env) {
                Validate(args, "JO", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[10] = true;
                bitArray[11] = true;
                bitArray[12] = true;
                bitArray[13] = true;

                env.SetCommand(bitArray);
            }

            private static void Validate(string[] args, string op, int line) {
                if (args.Length != 1) {
                    throw new CompileErrorExcepton($"Оператор {op} должен принимать 1 аргумент.", line);
                }
            }
        }

        private static class UnconditionalJumpCommandsFactory {
            public static Dictionary<string, CommandProcessor> GetCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["jmp"] = JMP,
                    ["call"] = CALL,
                    ["int"] = INT
                };
            }

            private static void JMP(string[] args, CompilerEnvironment env) {
                Validate(args, "JMP", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[14] = true;

                env.SetCommand(bitArray);
            }

            private static void CALL(string[] args, CompilerEnvironment env) {
                Validate(args, "CALL", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[10] = true;
                bitArray[14] = true;

                env.SetCommand(bitArray);
            }

            private static void INT(string[] args, CompilerEnvironment env) {
                Validate(args, "INT", env.GetCurrentLine());

                var bitArray = new BitArray(16);

                int address = CompilerSupport.ConvertToFarAddress(args[0], env);
                CompilerSupport.FillBitArray(bitArray, address, Constants.FarAddressBitsCount);

                bitArray[11] = true;
                bitArray[14] = true;

                env.SetCommand(bitArray);
            }

            private static void Validate(string[] args, string op, int line) {
                if (args.Length != 1) {
                    throw new CompileErrorExcepton($"Оператор {op} должен принимать 1 аргумент.", line);
                }
            }
        }
    }
}
