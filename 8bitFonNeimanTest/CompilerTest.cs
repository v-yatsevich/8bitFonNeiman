using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _8bitVonNeiman.Compiler;
using _8bitVonNeiman.Compiler.Model;

namespace _8bitFonNeimanTest {
    [TestClass]
    public class CompilerTest {

        [TestMethod]
        public void TestPrepareCode() {
            var inputs = new List<string>() {
                "123",
                "123\n456",
                "123\n\n456",
                " 123\n456 \n  789  ",
                "123;test\n456",
                "123  ;test\n456",
                "123\n;test\n456"
            };
            var outputs = new List<List<string>>() {
                new List<string> {"123"},
                new List<string> {"123", "456"},
                new List<string> {"123", "", "456"},
                new List<string> {"123", "456", "789"},
                new List<string> {"123", "456"},
                new List<string> {"123", "456"},
                new List<string> {"123", "", "456"}
            };
            var compilerModel = new CompilerModel(null, null, null);

            for (int i = 0; i < inputs.Count; i++) {
                var result = compilerModel.PrepareCode(inputs[i]);
                Assert.AreEqual(result.Count, outputs[i].Count);
                for (int j = 0; j < result.Count; j++) {
                    Assert.AreEqual(result[j], outputs[i][j]);
                }
            }
        }
        
        [TestMethod]
        public void TestHandleLabelAndReturnLine_whenThereIsNoLabel_thenReturnTheSameLine() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "test";
            var env = new CompilerEnvironment();

            var result = compilerModel.HandleLabelAndReturnLine(line, env);
            Assert.AreSame(result, line);
            Assert.AreEqual(env.GetLabelsCount(), 0);
        }

        [TestMethod]
        public void TestHandleLabelAndReturnLine_whenThereIsLabel_thenReturnLineWithoutLabelAndAddLabelToEnv() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "la2_-s:test";
            var env = new CompilerEnvironment();
            int address = 67;
            env.CurrentAddress = 67;

            var result = compilerModel.HandleLabelAndReturnLine(line, env);
            Assert.IsTrue(result.Equals("test"));
            Assert.AreEqual(env.GetLabelsCount(), 1);
            Assert.AreEqual(env.GetLabelAddress("la2_-s"), address);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleLabelAndReturnLine_whenThereAreTwoSameLabels_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "l:test";
            var line2 = "l: test2";
            var env = new CompilerEnvironment();

            compilerModel.HandleLabelAndReturnLine(line, env);
            compilerModel.HandleLabelAndReturnLine(line2, env);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleLabelAndReturnLine_whenLabelNameStartsWithNumber_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "2:test";
            var env = new CompilerEnvironment();

            compilerModel.HandleLabelAndReturnLine(line, env);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleLabelAndReturnLine_whenLabelNameIncorrect_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "l2!:test";
            var env = new CompilerEnvironment();

            compilerModel.HandleLabelAndReturnLine(line, env);
        }
        
        [TestMethod]
        public void TestHandleDirective_whenGoToAddress_thenAddressChanged() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/n 100";
            var env = new CompilerEnvironment();
            env.CurrentAddress = 10;

            compilerModel.HandleDirective(line, env);

            Assert.AreEqual(env.CurrentAddress, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenGoToTooBigAddress_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/n 1000";
            var env = new CompilerEnvironment();
            env.CurrentAddress = 10;

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenGoToIncorrectAddress_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/n 10x00";
            var env = new CompilerEnvironment();
            env.CurrentAddress = 10;

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        public void TestHandleDirective_whenSetCodeSegment_thenSegmentChanged() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/c2";
            var env = new CompilerEnvironment();
            env.DefaultCodeSegment = 1;

            compilerModel.HandleDirective(line, env);

            Assert.AreEqual(env.DefaultCodeSegment, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenSetTooBigCodeSegment_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/c4";
            var env = new CompilerEnvironment();

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenSetIncorrectCodeSegment_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/cx2";
            var env = new CompilerEnvironment();

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        public void TestHandleDirective_whenSetDataSegment_thenSegmentChanged() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/d2";
            var env = new CompilerEnvironment();
            env.DefaultDataSegment = 1;

            compilerModel.HandleDirective(line, env);

            Assert.AreEqual(env.DefaultDataSegment, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenSetTooBigDataSegment_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/d4";
            var env = new CompilerEnvironment();

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenSetIncorrectDataSegment_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/dx2";
            var env = new CompilerEnvironment();

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        public void TestHandleDirective_whenSetStackSegment_thenSegmentChanged() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/S2";
            var env = new CompilerEnvironment();
            env.DefaultStackSegment = 1;

            compilerModel.HandleDirective(line, env);

            Assert.AreEqual(env.DefaultStackSegment, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenSetTooBigStackSegment_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/S4";
            var env = new CompilerEnvironment();

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleDirective_whenSetIncorrectStackSegment_thenThrowException() {
            var compilerModel = new CompilerModel(null, null, null);
            var line = "/sx2";
            var env = new CompilerEnvironment();

            compilerModel.HandleDirective(line, env);
        }

        [TestMethod]
        public void TestHandleCommand_whenCommandCorrect_thenArgumentsPassed() {
            var env = new CompilerEnvironment();
            var line = "test arg1  , arg2 ";
            var commands = new Dictionary<string, CommandProcessorFactory.CommandProcessor> {
                {
                    "TEST",
                    (args, innerEnv) => {
                        Assert.AreSame(innerEnv, env);
                        Assert.AreEqual(args.Length, 2);
                        Assert.IsTrue(args[0].Equals("arg1"));
                        Assert.IsTrue(args[1].Equals("arg2"));
                    }
                }
            };
            var compilerModel = new CompilerModel(null, null, commands);
            
            compilerModel.HandleCommand(line, env);
        }

        [TestMethod]
        public void TestHandleCommand_whenCommandWithoutArgs_thenArgumentsEmpty() {
            var env = new CompilerEnvironment();
            var line = "test";
            var commands = new Dictionary<string, CommandProcessorFactory.CommandProcessor> {
                {
                    "TEST",
                    (args, innerEnv) => {
                        Assert.AreSame(innerEnv, env);
                        Assert.AreEqual(args.Length, 0);
                    }
                }
            };
            var compilerModel = new CompilerModel(null, null, commands);

            compilerModel.HandleCommand(line, env);
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationErrorExcepton))]
        public void TestHandleCommand_whenCommandNotFound_thenThrowException() {
            var env = new CompilerEnvironment();
            var line = "test arg1";
            var commands = new Dictionary<string, CommandProcessorFactory.CommandProcessor>();
            var compilerModel = new CompilerModel(null, null, commands);

            compilerModel.HandleCommand(line, env);
        }
    }
}
