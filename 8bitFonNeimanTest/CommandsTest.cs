using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Compiler.Model;

namespace _8bitFonNeimanTest {
    [TestClass]
    public class CommandsTest {

        private void Test(List<string> inputs, List<string[]> args, List<string> outputs, CompilerEnvironment env = null) {
            var commands = CommandProcessorFactory.GetCommandProcessors();
            if (env == null) {
                env = new CompilerEnvironment();
            }
            for (int i = 0; i < inputs.Count; i++) {
                env.CurrentAddress = 0;
                commands[inputs[i].ToLower()](args[i], env);
                var memory = env.GetMemory();
                var str = memory[0].ToDigitString() + memory[1].ToDigitString();
                Assert.AreEqual(outputs[i], str, $"Command: {inputs[i]}");
            }
        }

        [TestMethod]
        public void NoAddressCommandsTest() {
            var inputs = new List<string> {
                "NOP",
                "RET",
                "IRET",
                "EI",
                "DI",
                "RR",
                "RL",
                "RRC",
                "RLC",
                "HLT",
                "INCA",
                "DECA",
                "SWAPA",
                "DAA",
                "DSA",
                "IN",
                "OUT",
                "ES",
                "MOVASR",
                "MOVSRA"
            };

            var args = new List<string[]> {
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0],
                new string[0]
            };

            var outputs = new List<string> {
                "1000000000000000",
                "0100000000000000",
                "1100000000000000",
                "0010000000000000",
                "1010000000000000",
                "0110000000000000",
                "1110000000000000",
                "0001000000000000",
                "1001000000000000",
                "0101000000000000",
                "1101000000000000",
                "0011000000000000",
                "1011000000000000",
                "0111000000000000",
                "1111000000000000",
                "0000100000000000",
                "1000100000000000",
                "0100100000000000",
                "1100100000000000",
                "0010100000000000",
            };
            
            Test(inputs, args, outputs);
        }

        [TestMethod]
        public void CycleCommandsTest() {
            var inputs = new List<string> {
                "DJRNZ"
            };

            var args = new List<string[]> {
                new []{"R2", "3"}
            };

            var outputs = new List<string> {
                "1100000000011000"
            };

            Test(inputs, args, outputs);
        }

        [TestMethod]
        public void JumpCommandsTest() {
            var inputs = new List<string> {
                "JNZ", "JNC", "JNS", "JNO", "JZ", "JC", "JS", "JO", "jmp", "call", "int"
            };

            var args = new List<string[]> {
                new []{"5"},
                new []{"label1"},
                new []{"label2"},
                new []{"6"},
                new []{"7"},
                new []{"0"},
                new []{"12"},
                new []{"18"},
                new []{"0xFA"},
                new []{"12"},
                new []{"18"}
            };

            var outputs = new List<string> {
                "1010000000000100",
                "0011000000100100",
                "0110010101010100",
                "0110000000110100",
                "1110000000001100",
                "0000000000101100",
                "0011000000011100",
                "0100100000111100",

                "0101111100000010",
                "0011000000010010",
                "0100100000110010"
            };

            var env = new CompilerEnvironment();
            env.CurrentAddress = 12;
            env.AddAddressLabelToNewCommand("label1");
            env.DefaultCodeSegment = 2;
            env.CurrentAddress = 166;
            env.AddAddressLabelToNewCommand("label2");
            env.DefaultCodeSegment = 0;

            Test(inputs, args, outputs, env);
        }

        [TestMethod]
        public void RegisterCommandsTest() {
            var inputs = new List<string> {
                "NOT", "ADD", "SUB", "MUL", "DIV", "AND", "OR", "XOR", "CMP", "RD", "WR", "INC", "DEC", "POP", "PUSH", "MOV", "ADC", "SUBB"
            };

            var args = new List<string[]> {
                new []{"R0"},
                new []{"@R1"},
                new []{"@R2+"},
                new []{"@R3-"},
                new []{"+@R4"},
                new []{"-@r5"},
                new []{"r6", "0"},
                new []{"@R7", "0"},
                new []{"@R8+", "0"},
                new []{"@R9-", "1"},
                new []{"+@ra", "1"},
                new []{"-@rb", "1"},
                new []{"rc", "1"},
                new []{"Rd"},
                new []{"Re"},
                new []{"ra", "rc"},
                new []{"+@ra", "1"},
                new []{ "-@rb", "1" }
            };
            //Прямая - 000
            //@R     - 100
            //@R+    - 001
            //+@R    - 101
            //@R-    - 011
            //-@R    - 111
            var outputs = new List<string> {
                "0000000000001010",
                "1000100010001010",
                "0100001001001010",
                "1100011011001010",
                "0010101000101010",
                "1010111010101010",
                "0110000001101010",
                "1110100011101010",
                "0001001000011010",
                "1001011110011010",
                "0101101101011010",
                "1101111111011010",
                "0011000100111010",
                "1011000010111010",
                "0111000001111010",
                         
                "0101001111111010",

                "0101101100001111",
                "1101111110001111",
            };

            Test(inputs, args, outputs);
        }

        [TestMethod]
        public void RamCommandsTest() {
            var inputs = new List<string> {
                "NOT", "ADD", "SUB", "MUL", "DIV", "AND", "OR", "XOR", "CMP", "RD", "WR", "INC", "DEC", "ADC", "SUBB", "XCH"
            };

            var args = new List<string[]> {
                new []{"q"},
                new []{"1"},
                new []{"2"},
                new []{"3"},
                new []{"4"},
                new []{"5"},
                new []{"6"},
                new []{"7"},
                new []{"#8"},
                new []{"#9"},
                new []{"#10"},
                new []{"#11"},
                new []{"#0xc"},
                new []{"#13"},
                new []{"#14"},
                new []{"#0b1111"}
            };
            
            var outputs = new List<string> {
                "0101111000000110",
                "1000000010000110",
                "0100000001000110",
                "1100000011000110",
                "0010000000100110",
                "1010000010100110",
                "0110000001100110",
                "1110000011100110",
                "0001000000011110",
                "1001000010011110",
                "0101000001011110",
                "1101000011011110",
                "0011000000111110",
                "1011000010111110",
                "0111000001111110",
                "1111000011111110"
            };

            var env = new CompilerEnvironment();
            env.AddVariable("q", 122);

            Test(inputs, args, outputs, env);
        }
    }
}
