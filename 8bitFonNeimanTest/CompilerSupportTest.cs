using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _8bitVonNeiman.Compiler.Model;

namespace _8bitFonNeimanTest {

    [TestClass]
    public class CompilerSupportTest {

        [TestMethod]
        public void RegisterTest() {
            var inputs = new List<string> {
                "R1",
                "r2+",
                "R3-",
                "+r4",
                "-R5",
                "@r6",
                "@R7+",
                "@r8-",
                "+@R9",
                "-@ra",

                "RS",
                "sf",
                "+r2+",
                "+r4-",
                "-R2-",
                "-r4+",
                "R2+-",
                "@R@2"
            };

            var outputs = new List<CompilerSupport.Register?> {
                new CompilerSupport.Register() {
                    IsChange = false,
                    Number = 1,
                    IsDirect = true
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = true,
                    IsPostchange = true,
                    Number = 2,
                    IsDirect = true
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = false,
                    IsPostchange = true,
                    Number = 3,
                    IsDirect = true
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = true,
                    IsPostchange = false,
                    Number = 4,
                    IsDirect = true
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = false,
                    IsPostchange = false,
                    Number = 5,
                    IsDirect = true
                },
                new CompilerSupport.Register() {
                    IsChange = false,
                    Number = 6,
                    IsDirect = false
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = true,
                    IsPostchange = true,
                    Number = 7,
                    IsDirect = false
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = false,
                    IsPostchange = true,
                    Number = 8,
                    IsDirect = false
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = true,
                    IsPostchange = false,
                    Number = 9,
                    IsDirect = false
                },
                new CompilerSupport.Register() {
                    IsChange = true,
                    IsIncrement = false,
                    IsPostchange = false,
                    Number = 10,
                    IsDirect = false
                },
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            };

            for (int i = 0; i < inputs.Count; i++) {
                var register = CompilerSupport.ConvertToRegister(inputs[i]);
                if (!outputs[i].HasValue) {
                    Assert.IsFalse(register.HasValue);
                    continue;
                }
                Assert.IsTrue(register.HasValue, $"Строка: {inputs[i]}, регистр null");
                var r = register.Value;
                var expected = outputs[i].Value;
                Assert.AreEqual(r.Number, expected.Number, $"Строка: {inputs[i]}, регистр {r}, ожидалось {expected}");
                Assert.AreEqual(r.IsDirect, expected.IsDirect, $"Строка: {inputs[i]}, регистр {r}, ожидалось {expected}");
                Assert.AreEqual(r.IsChange, expected.IsChange, $"Строка: {inputs[i]}, регистр {r}, ожидалось {expected}");
                Assert.AreEqual(r.IsIncrement, expected.IsIncrement, $"Строка: {inputs[i]}, регистр {r}, ожидалось {expected}");
                Assert.AreEqual(r.IsPostchange, expected.IsPostchange, $"Строка: {inputs[i]}, регистр {r}, ожидалось {expected}");
            }
        }
    }
}
