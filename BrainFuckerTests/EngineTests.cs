// <copyright file="EngineTests.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>

namespace BrainFucker.Tests
{
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    /// <summary>
    /// This class is responsible for unit testing <see cref="Engine"/>.
    /// </summary>
    public class EngineTests
    {
        /// <summary>
        /// Test checks the operation of the brain fuck Engine using StringReaders and StringWriters .
        /// </summary>
        /// <param name="program">The program to be ran.</param>
        /// <param name="input">The inputs to the program.</param>
        /// <param name="expected">The expected outputs of the program.</param>
        [TestCase("+++++++++.", "", "\t")]
        [TestCase(",.", "1", "1")]
        [TestCase("++>+++++[<+>-]++++++++[<++++++>-]<.", "", "7")]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", "", "Hello World!\n")]
        [Timeout(100)]
        public void Test_Engine_String_ReaderWriters(string program, string input, string expected)
        {
            Engine bfe = new Engine();

            string output = bfe.Run(program, input);

            bool info = string.Compare(expected, output) == 0;

            Assert.IsTrue(
                info,
                string.Format(
                    "Program ( {0} ), with inputs ( {1} ), expected results of ( {2} ), got results of ( {3} )",
                    program,
                    input,
                    expected,
                    output));
        }

        /// <summary>
        /// Test checks the operation of the brain fuck Engine using StringReaders and StringWriters .
        /// </summary>
        /// <param name="program">the programs to be ran.</param>
        /// <param name="input">The inputs to the program.</param>
        /// <param name="expected">The expected output of the system.</param>
        [TestCase("+++++++++.", "", "\t")]
        [TestCase(",.", "1", "1")]
        [TestCase("++>+++++[<+>-]++++++++[<++++++>-]<.", "", "7")]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", "", "Hello World!\n")]
        [Timeout(100)]
        public void Test_Engine_Streams(string program, string input, string expected)
        {
            Engine bfe = new Engine();

            string output = bfe.Run(program, input);

            bool info = string.Compare(expected, output) == 0;

            Assert.IsTrue(
                info,
                string.Format(
                    "Program ( {0} ), with inputs ( {1} ), expected results of ( {2} ), got results of ( {3} )",
                    program,
                    input,
                    expected,
                    output));
        }

        /// <summary>
        /// Tests the rerun method of <see cref="Engine"/>.
        /// </summary>
        /// <param name="program">The program to be ran.</param>
        /// <param name="input_1">The first input.</param>
        /// <param name="expected_1">The expected out put of the program with the first input.</param>
        /// <param name="input_2">THe second input.</param>
        /// <param name="expected_2">THe expected output of the program with the second input.</param>
        [TestCase(",.", "1", "1", "5", "5")]
        [TestCase(",.", "6", "6", "9", "9")]
        [TestCase(",>+++++++++[<----->-]<--->,>+++++++++[<----->-]<---<[->+<]>>+++++++++[<+++++>-]<+++.", "12", "3", "34", "7")]
        [Timeout(1000)]
        public void Test_ReRun(string program, string input_1, string expected_1, string input_2, string expected_2)
        {
            Engine bfe = new Engine();

            string output = bfe.Run(program, input_1);

            bool info = string.Compare(expected_1, output) == 0;

            Assert.IsTrue(info, string.Format("expected {0} =/= input {1}", expected_1, output));

            output = bfe.Rerun(input_2);

            info = string.Compare(expected_2, output) == 0;

            Assert.IsTrue(info, string.Format("expected {0} =/= input {1}", expected_1, output));
        }
    }
}