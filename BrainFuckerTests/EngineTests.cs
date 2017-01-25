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
    public class BFEngineTests
    {

        /// <summary>
        /// Test checks the operation of the brain fuck Engine using StringReaders and StringWriters .
        /// </summary>
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
        [TestCase("+++++++++.", "", "\t")]
        [TestCase(",.", "1", "1")]
        [TestCase("++>+++++[<+>-]++++++++[<++++++>-]<.", "", "7")]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", "", "Hello World!\n")]
        [Timeout(100)]
        public void Test_Engine_Streams(string program, string input, string expected)
        {

            Engine bfe = new Engine();

            string output = bfe.Run(program,input);

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

        [TestCase(",.", "1", "1", "5", "5")]
        [TestCase(",.", "6", "6", "9", "9")]
        [TestCase(",>+++++++++[<----->-]<--->,>+++++++++[<----->-]<---<[->+<]>>+++++++++[<+++++>-]<+++.", "12", "3","34","7")]
        [Timeout(1000)]
        public void Test_ReRun(string program, string input_1, string expected_1, string input_2, string expected_2)
        {

            Engine bfe = new Engine();

            string output = bfe.Run(program, input_1);

            bool info = string.Compare(expected_1, output) == 0;

            Assert.IsTrue(info, string.Format("expected {0} =/= input {1}", expected_1, output));

            output = bfe.Rerun(input_2);

            info = (string.Compare(expected_2, output) == 0);

            Assert.IsTrue(info, string.Format("expected {0} =/= input {1}", expected_1, output));

        }
    }
}