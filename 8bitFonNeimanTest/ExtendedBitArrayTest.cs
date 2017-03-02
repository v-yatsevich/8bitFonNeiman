using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _8bitVonNeiman.Common;

namespace _8bitFonNeimanTest {
    /// <summary>
    /// Сводное описание для ExtendedByteArrayTest
    /// </summary>
    [TestClass]
    public class ExtendedBitArrayTest {

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ConstructorTest() {
            var array = new List<string> { "00001100", "01110111", "00011100", "01010101", "01010001" };
            foreach (var code in array) {
                var ExtendedBitArray = new ExtendedBitArray(code);
                Assert.AreEqual(ExtendedBitArray.ToDigitString(), code);
            }
        }

        [TestMethod]
        public void EmptyConstructorTest() {
            var ExtendedBitArray = new ExtendedBitArray();
            Assert.AreEqual(ExtendedBitArray.ToDigitString(), "00000000");
        }

        [TestMethod]
        public void CopyConstructorTest() {
            var ExtendedBitArray = new ExtendedBitArray("00110000");
            var copy = new ExtendedBitArray(ExtendedBitArray);
            Assert.AreEqual(ExtendedBitArray.ToDigitString(), copy.ToDigitString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LongConstructorExceptionTest() {
            var ExtendedBitArray = new ExtendedBitArray("110011100");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongFormatConstructorExceptionTest() {
            var ExtendedBitArray = new ExtendedBitArray("11201100");
        }

        [TestMethod]
        public void AddMethodTest() {
            var array1 = new List<ExtendedBitArray> {
                new ExtendedBitArray("00001100"), //12
                new ExtendedBitArray("01110111"), //119
                new ExtendedBitArray("00011100"), //28
                new ExtendedBitArray("01010001"), //81
                new ExtendedBitArray("01010101"), //85
            };
            var array2 = new List<ExtendedBitArray> {
                new ExtendedBitArray("00000011"), //3
                new ExtendedBitArray("00100100"), //36
                new ExtendedBitArray("11000000"), //192
                new ExtendedBitArray("00001001"), //9
                new ExtendedBitArray("00001011"), //11
            };
            var results = new List<string> {
                "00001111", //15
                "10011011", //155
                "11011100", //220
                "01011010", //90
                "01100000", //96
            };
            for (int i = 0; i < array1.Count; i++) {
                array1[i].Add(array2[i]);
                Assert.AreEqual(array1[i].ToDigitString(), results[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void AddOverflowMethodTest() {
            var array1 = new ExtendedBitArray("11111111");
            var array2 = new ExtendedBitArray("00000001");
            array1.Add(array2);
        }

        [TestMethod]
        public void IncTest() {
            var array = new List<ExtendedBitArray> {
                new ExtendedBitArray("00000011"), //3
                new ExtendedBitArray("00100100"), //36
                new ExtendedBitArray("11000000"), //192
                new ExtendedBitArray("00001001"), //9
                new ExtendedBitArray("00001011"), //11
            };
            var results = new List<string> {
                "00000100", 
                "00100101", 
                "11000001", 
                "00001010",
                "00001100", 
            };
            for (int i = 0; i < array.Count; i++) {
                array[i].Inc();
                Assert.AreEqual(array[i].ToDigitString(), results[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void IncOverflowExceptionTest() {
            var ExtendedBitArray = new ExtendedBitArray("11111111");
            ExtendedBitArray.Inc();
        }

        [TestMethod]
        public void DecTest() {
            var array = new List<ExtendedBitArray> {
                new ExtendedBitArray("00000011"), //3
                new ExtendedBitArray("00100100"), //36
                new ExtendedBitArray("11000000"), //192
                new ExtendedBitArray("00001001"), //9
                new ExtendedBitArray("00001011"), //11
            };
            var results = new List<string> {
                "00000010",
                "00100011",
                "10111111",
                "00001000",
                "00001010"
            };
            for (int i = 0; i < array.Count; i++) {
                array[i].Dec();
                Assert.AreEqual(array[i].ToDigitString(), results[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void DecOverflowExceptionTest() {
            var ExtendedBitArray = new ExtendedBitArray("00000000");
            ExtendedBitArray.Dec();
        }

        [TestMethod]
        public void GetBitTest() {
            var code = "00100101";
            var array = new ExtendedBitArray(code);
            for (int i = 0; i < 8; i++) {
                Assert.AreEqual(array[i], code[7 - i] == '1');
            }
        }

        [TestMethod]
        public void SetBitTest() {
            var array = new ExtendedBitArray("00100101");

            array[1] = true;
            Assert.AreEqual(array.ToDigitString(), "00100111");

            array[1] = true;
            Assert.AreEqual(array.ToDigitString(), "00100111");

            array[1] = false;
            Assert.AreEqual(array.ToDigitString(), "00100101");

            array[7] = false;
            Assert.AreEqual(array.ToDigitString(), "00100101");

            array[7] = true;
            Assert.AreEqual(array.ToDigitString(), "10100101");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBitArgumentExceptionTest() {
            var array = new ExtendedBitArray();
            var temp = array[8];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetBitArgumentExceptionTest() {
            var array = new ExtendedBitArray();
            array[8] = true;
        }

        [TestMethod]
        public void InvertTest() {
            var array = new List<ExtendedBitArray> {
                new ExtendedBitArray("00000011"),
                new ExtendedBitArray("00100100"),
                new ExtendedBitArray("11000000"),
                new ExtendedBitArray("00000000"),
                new ExtendedBitArray("11111111"),      
            };
            var results = new List<string> {
                "11111100",
                "11011011",
                "00111111",
                "11111111",
                "00000000"
            };
            for (int i = 0; i < array.Count; i++) {
                array[i].Invert();
                Assert.AreEqual(array[i].ToDigitString(), results[i]);
            }
        }
    }
}
