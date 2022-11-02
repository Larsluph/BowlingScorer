namespace BowlingScorer
{
    [TestClass]
    public class TestFrame
    {
        [TestMethod]
        public void TestClassic_NotComplete()
        {
            Frame testFrame = new(1) { Shot1 = 4 };

            Assert.AreEqual(4, testFrame.Shot1);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot2);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot3);

            Assert.IsFalse(testFrame.IsComplete, "IsComplete");
            Assert.IsFalse(testFrame.Is10th, "Is10th");

            Assert.IsFalse(testFrame.IsSpecial, "IsSpecial");
            Assert.IsFalse(testFrame.IsStrike, "IsStrike");
            Assert.IsFalse(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void TestClassic_Complete()
        {
            Frame testFrame = new(1) { Shot1 = 4, Shot2 = 5 };

            Assert.AreEqual(4, testFrame.Shot1);
            Assert.AreEqual(5, testFrame.Shot2);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot3);

            Assert.IsTrue(testFrame.IsComplete, "IsComplete");
            Assert.IsFalse(testFrame.Is10th, "Is10th");

            Assert.IsFalse(testFrame.IsSpecial, "IsSpecial");
            Assert.IsFalse(testFrame.IsStrike, "IsStrike");
            Assert.IsFalse(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void TestClassic_Strike()
        {
            Frame testFrame = new(1) { Shot1 = 10 };

            Assert.AreEqual(10, testFrame.Shot1);
            Assert.AreEqual(0, testFrame.Shot2);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot3);

            Assert.IsTrue(testFrame.IsComplete, "IsComplete");
            Assert.IsFalse(testFrame.Is10th, "Is10th");

            Assert.IsTrue(testFrame.IsSpecial, "IsSpecial");
            Assert.IsTrue(testFrame.IsStrike, "IsStrike");
            Assert.IsFalse(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void TestClassic_Spare()
        {
            Frame testFrame = new(1) { Shot1 = 9, Shot2 = 1 };

            Assert.AreEqual(9, testFrame.Shot1);
            Assert.AreEqual(1, testFrame.Shot2);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot3);

            Assert.IsTrue(testFrame.IsComplete, "IsComplete");
            Assert.IsFalse(testFrame.Is10th, "Is10th");

            Assert.IsTrue(testFrame.IsSpecial, "IsSpecial");
            Assert.IsFalse(testFrame.IsStrike, "IsStrike");
            Assert.IsTrue(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void Test10th_NotComplete()
        {
            Frame testFrame = new(10) { Shot1 = 4 };

            Assert.AreEqual(4, testFrame.Shot1);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot2);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot3);

            Assert.IsFalse(testFrame.IsComplete, "IsComplete");
            Assert.IsTrue(testFrame.Is10th, "Is10th");

            Assert.IsFalse(testFrame.IsSpecial, "IsSpecial");
            Assert.IsFalse(testFrame.IsStrike, "IsStrike");
            Assert.IsFalse(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void Test10th_NotComplete_Strike()
        {
            Frame testFrame = new(10) { Shot1 = 10 };

            Assert.AreEqual(10, testFrame.Shot1);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot2);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot3);

            Assert.IsFalse(testFrame.IsComplete, "IsComplete");
            Assert.IsTrue(testFrame.Is10th, "Is10th");

            Assert.IsTrue(testFrame.IsSpecial, "IsSpecial");
            Assert.IsTrue(testFrame.IsStrike, "IsStrike");
            Assert.IsFalse(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void Test10th_NotComplete_Spare()
        {
            Frame testFrame = new(10) { Shot1 = 9, Shot2 = 1 };

            Assert.AreEqual(9, testFrame.Shot1);
            Assert.AreEqual(1, testFrame.Shot2);
            Assert.ThrowsException<InvalidOperationException>(() => testFrame.Shot3);

            Assert.IsFalse(testFrame.IsComplete, "IsComplete");
            Assert.IsTrue(testFrame.Is10th, "Is10th");

            Assert.IsTrue(testFrame.IsSpecial, "IsSpecial");
            Assert.IsFalse(testFrame.IsStrike, "IsStrike");
            Assert.IsTrue(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void Test10th_Complete_NotSpecial()
        {
            Frame testFrame = new(10) { Shot1 = 4, Shot2 = 5 };

            Assert.AreEqual(4, testFrame.Shot1);
            Assert.AreEqual(5, testFrame.Shot2);
            Assert.AreEqual(0, testFrame.Shot3);

            Assert.IsTrue(testFrame.IsComplete, "IsComplete");
            Assert.IsTrue(testFrame.Is10th, "Is10th");

            Assert.IsFalse(testFrame.IsSpecial, "IsSpecial");
            Assert.IsFalse(testFrame.IsStrike, "IsStrike");
            Assert.IsFalse(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void Test10th_Complete_Strike()
        {
            Frame testFrame = new(10) { Shot1 = 10, Shot2 = 5, Shot3 = 4 };

            Assert.AreEqual(10, testFrame.Shot1);
            Assert.AreEqual(5, testFrame.Shot2);
            Assert.AreEqual(4, testFrame.Shot3);

            Assert.IsTrue(testFrame.IsComplete, "IsComplete");
            Assert.IsTrue(testFrame.Is10th, "Is10th");

            Assert.IsTrue(testFrame.IsSpecial, "IsSpecial");
            Assert.IsTrue(testFrame.IsStrike, "IsStrike");
            Assert.IsFalse(testFrame.IsSpare, "IsSpare");
        }

        [TestMethod]
        public void Test10th_Complete_Spare()
        {
            Frame testFrame = new(10) { Shot1 = 9, Shot2 = 1, Shot3 = 4 };

            Assert.AreEqual(9, testFrame.Shot1);
            Assert.AreEqual(1, testFrame.Shot2);
            Assert.AreEqual(4, testFrame.Shot3);

            Assert.IsTrue(testFrame.IsComplete, "IsComplete");
            Assert.IsTrue(testFrame.Is10th, "Is10th");

            Assert.IsTrue(testFrame.IsSpecial, "IsSpecial");
            Assert.IsFalse(testFrame.IsStrike, "IsStrike");
            Assert.IsTrue(testFrame.IsSpare, "IsSpare");
        }
    }
}