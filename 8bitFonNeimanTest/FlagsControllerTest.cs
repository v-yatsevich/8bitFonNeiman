using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Cpu;

namespace _8bitFonNeimanTest {

    [TestClass]
    public class FlagsControllerTest {

        private void Check(List<string> inputs, List<ExtendedBitArray> args, List<ExtendedBitArray> prevStates,
            List<ExtendedBitArray> nextStates, List<bool> overrides, List<int> result) {
            var controller = new FlagsController();
            for (var i = 0; i < inputs.Count; i++) {
                if (args != null) {
                    controller.SetArgument(args[i]);
                }
                controller.SetPreviousState(prevStates[i]);
                controller.UpdateFlags(nextStates[i], inputs[i], overrides[i]);
                Assert.AreEqual(result[i], controller.Flags.NumValue());
            }
        }

        [TestMethod]
        public void FlagsTestFull() {
            var inputs = new List<string> {
                "add", "adc", "sub", "subb", "mul", "div", "cmp"
            };

            var args = new List<ExtendedBitArray> {
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("10000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
            };

            var prevStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00000001"),
            };

            var nextStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00010001"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("00000000"),
                new ExtendedBitArray("00000010"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("10000001"),
            };

            var overrides = new List<bool> {
                false,
                false,
                false,
                false,
                true,
                false,
                true
            };

            //Z   N   O   A   C
            //1 + 2 + 4 + 8 + 16
            var result = new List<int> {
                8,
                0,
                1,
                4,
                16,
                2 + 8,
                2 + 4 + 8 + 16
            };

            Check(inputs, args, prevStates, nextStates, overrides, result);
        }

        [TestMethod]
        public void FlagsTestLogic() {
            var inputs = new List<string> {
                "and", "or", "xor", "not"
            };

            var prevStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00000001"),
            };

            var nextStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00010001"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("00000000"),
                new ExtendedBitArray("00000010"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("10000001"),
            };

            var overrides = new List<bool> {
                false,
                false,
                false,
                false,
                true,
                false,
                true
            };

            //Z   N   O   A   C
            //1 + 2 + 4 + 8 + 16
            var result = new List<int> {
                0,
                0,
                1,
                0,
                0,
                2,
                2
            };

            Check(inputs, null, prevStates, nextStates, overrides, result);
        }

        [TestMethod]
        public void FlagsTestIncDec() {
            var inputs = new List<string> {
                "inc", "dec"
            };

            var args = new List<ExtendedBitArray> {
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("10000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
            };

            var prevStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00000001"),
            };

            var nextStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00010001"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("00000000"),
                new ExtendedBitArray("00000010"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("10000001"),
            };

            var overrides = new List<bool> {
                false,
                false,
                false,
                false,
                true,
                false,
                true
            };

            //Z   N   O   A   C
            //1 + 2 + 4 + 8 + 16
            var result = new List<int> {
                8,
                0,
                1,
                0,
                16,
                2 + 8,
                2 + 8 + 16
            };

            Check(inputs, null, prevStates, nextStates, overrides, result);
        }

        [TestMethod]
        public void FlagsTestWithoutInfluence() {
            var inputs = new List<string> {
                "rd", "wr", "xch", "push", "pop", "mov"
            };

            var args = new List<ExtendedBitArray> {
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("10000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
                new ExtendedBitArray("00000001"),
            };

            var prevStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00001001"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("00000001"),
            };

            var nextStates = new List<ExtendedBitArray> {
                new ExtendedBitArray("00010001"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("00000000"),
                new ExtendedBitArray("00000010"),
                new ExtendedBitArray("00001011"),
                new ExtendedBitArray("10001001"),
                new ExtendedBitArray("10000001"),
            };

            var overrides = new List<bool> {
                false,
                false,
                false,
                false,
                true,
                false,
                true
            };

            //Z   N   O   A   C
            //1 + 2 + 4 + 8 + 16
            var result = new List<int> {
                0,
                0,
                0,
                0,
                0,
                0,
                0
            };

            Check(inputs, null, prevStates, nextStates, overrides, result);
        }
    }
}
