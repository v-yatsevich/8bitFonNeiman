using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Compiler.Model {
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class CommandProcessordFactory {

        public delegate BitArray CommandProcessor(string args, int line);

        public static Dictionary<string, CommandProcessor> GetCommandProcessors() {
            return NoAddressCommandsFactory.GetNoAddressCommands();
        }

        private static class NoAddressCommandsFactory {
            public static Dictionary<string, CommandProcessor> GetNoAddressCommands() {
                return new Dictionary<string, CommandProcessor> {
                    ["nop"] = NoAddressCommandsFactory.NOP,
                    ["ret"] = NoAddressCommandsFactory.RET,
                    ["iret"] = NoAddressCommandsFactory.IRET,
                    ["ei"] = NoAddressCommandsFactory.EI,
                    ["di"] = NoAddressCommandsFactory.DI,
                    ["rr"] = NoAddressCommandsFactory.RR,
                    ["rl"] = NoAddressCommandsFactory.RL,
                    ["rrc"] = NoAddressCommandsFactory.RRC,
                    ["rlc"] = NoAddressCommandsFactory.RLC,
                    ["hlt"] = NoAddressCommandsFactory.HLT,
                    ["inca"] = NoAddressCommandsFactory.INCA,
                    ["deca"] = NoAddressCommandsFactory.DECA,
                    ["swapa"] = NoAddressCommandsFactory.SWAPA,
                    ["daa"] = NoAddressCommandsFactory.DAA,
                    ["dsa"] = NoAddressCommandsFactory.DSA,
                    ["in"] = NoAddressCommandsFactory.IN,
                    ["out"] = NoAddressCommandsFactory.OUT,
                    ["es"] = NoAddressCommandsFactory.ES,
                    ["movasr"] = NoAddressCommandsFactory.MOVASR,
                    ["movsra"] = NoAddressCommandsFactory.MOVSRA
                };
            }

            private static BitArray NOP(string args, int line) {
                ValidateNoAddressCommand(args, "NOP", line);
                return new BitArray(16) { [0] = true };
            }

            private static BitArray RET(string args, int line) {
                ValidateNoAddressCommand(args, "RET", line);
                return new BitArray(16) { [1] = true };
            }

            private static BitArray IRET(string args, int line) {
                ValidateNoAddressCommand(args, "IRET", line);
                return new BitArray(16) {
                    [0] = true,
                    [1] = true
                };
            }

            private static BitArray EI(string args, int line) {
                ValidateNoAddressCommand(args, "EI", line);
                return new BitArray(16) { [2] = true };
            }

            private static BitArray DI(string args, int line) {
                ValidateNoAddressCommand(args, "DI", line);
                return new BitArray(16) {
                    [0] = true,
                    [2] = true
                };
            }

            private static BitArray RR(string args, int line) {
                ValidateNoAddressCommand(args, "RR", line);
                return new BitArray(16) {
                    [1] = true,
                    [2] = true
                };
            }

            private static BitArray RL(string args, int line) {
                ValidateNoAddressCommand(args, "RL", line);
                return new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [2] = true
                };
            }

            private static BitArray RRC(string args, int line) {
                ValidateNoAddressCommand(args, "RRC", line);
                return new BitArray(16) { [3] = true };
            }

            private static BitArray RLC(string args, int line) {
                ValidateNoAddressCommand(args, "RLC", line);
                return new BitArray(16) {
                    [0] = true,
                    [3] = true
                };
            }

            private static BitArray HLT(string args, int line) {
                ValidateNoAddressCommand(args, "HLT", line);
                return new BitArray(16) {
                    [1] = true,
                    [3] = true
                };
            }

            private static BitArray INCA(string args, int line) {
                ValidateNoAddressCommand(args, "INCA", line);
                return new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [3] = true
                };
            }

            private static BitArray DECA(string args, int line) {
                ValidateNoAddressCommand(args, "DECA", line);
                return new BitArray(16) {
                    [2] = true,
                    [3] = true
                };
            }

            private static BitArray SWAPA(string args, int line) {
                ValidateNoAddressCommand(args, "SWAPA", line);
                return new BitArray(16) {
                    [0] = true,
                    [2] = true,
                    [3] = true
                };
            }

            private static BitArray DAA(string args, int line) {
                ValidateNoAddressCommand(args, "DAA", line);
                return new BitArray(16) {
                    [1] = true,
                    [2] = true,
                    [3] = true
                };
            }

            private static BitArray DSA(string args, int line) {
                ValidateNoAddressCommand(args, "DSA", line);
                return new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [2] = true,
                    [3] = true
                };
            }

            private static BitArray IN(string args, int line) {
                ValidateNoAddressCommand(args, "IN", line);
                return new BitArray(16) {
                    [0] = true,
                    [4] = true
                };
            }

            private static BitArray OUT(string args, int line) {
                ValidateNoAddressCommand(args, "OUT", line);
                return new BitArray(16) {
                    [1] = true,
                    [4] = true
                };
            }

            private static BitArray ES(string args, int line) {
                ValidateNoAddressCommand(args, "ES", line);
                return new BitArray(16) {
                    [0] = true,
                    [1] = true,
                    [4] = true
                };
            }

            private static BitArray MOVASR(string args, int line) {
                ValidateNoAddressCommand(args, "MOVASR", line);
                return new BitArray(16) {
                    [2] = true,
                    [4] = true
                };
            }

            private static BitArray MOVSRA(string args, int line) {
                ValidateNoAddressCommand(args, "MOVSRA", line);
                return new BitArray(16) {
                    [0] = true,
                    [2] = true,
                    [4] = true
                };
            }

            private static void ValidateNoAddressCommand(string args, string op, int line) {
                if (args.Length != 0) {
                    throw new CompileErrorExcepton($"Оператор {op} не должен принимать никаких аргументов", line);
                }
            }
        }
    }
}
