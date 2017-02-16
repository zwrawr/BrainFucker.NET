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

            bfe.Program = program.ToCharArray();
            string output = bfe.Run(input);

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
        /// Test checks the operation of the brain fuck Engine using Strings .
        /// </summary>
        /// <param name="program">the programs to be ran.</param>
        /// <param name="input">The inputs to the program.</param>
        /// <param name="expected">The expected output of the system.</param>
        [TestCase("+++++++++.", "", "\t")]
        [TestCase(",.", "1", "1")]
        [TestCase("++>+++++[<+>-]++++++++[<++++++>-]<.", "", "7")]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", "", "Hello World!\n")]
        [Timeout(100)]
        public void Test_Engine_Strings(string program, string input, string expected)
        {
            Engine bfe = new Engine();
            bfe.Program = program.ToCharArray();

            string output = bfe.Run(input);

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
        /// Test checks the operation of the brain fuck Engine using char[] .
        /// </summary>
        /// <param name="program">the programs to be ran.</param>
        /// <param name="input">The inputs to the program.</param>
        /// <param name="expected">The expected output of the system.</param>
        [TestCase("+++++++++.", new char[0], new char[] { '\t' })]
        [TestCase(",.", new char[] { '1' }, new char[] { '1' })]
        [TestCase(",.", new char[] { 'z' }, new char[] { 'z' })]
        [TestCase("++>+++++[<+>-]++++++++[<++++++>-]<.", new char[0], new char[] { '7' })]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", new char[0], new char[] { 'H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd', '!', '\n' })]
        [Timeout(100)]
        public void Test_Engine_Chars(string program, char[] input, char[] expected)
        {
            Engine bfe = new Engine();
            bfe.Program = program.ToCharArray();

            char[] output = bfe.Run( input);

            bool allMatch = true;
            for (int i = 0; i < output.Length; i++)
            {
                allMatch = (expected[i] == output[i]) ? allMatch : false;
            }
               
            Assert.IsTrue(
                allMatch,
                string.Format(
                    "Program ( {0} ), with inputs ( {1} ), expected results of ( {2} ), got results of ( {3} )",
                    program,
                    input,
                    expected,
                    output));
        }

        /// <summary>
        /// Test checks the operation of the brain fuck Engine using byte[] .
        /// </summary>
        /// <param name="program">the programs to be ran.</param>
        /// <param name="input">The inputs to the program.</param>
        /// <param name="expected">The expected output of the system.</param>
        [TestCase("+++++++++.", new byte[0], new byte[] { 9 })]
        [TestCase(",.", new byte[] { 1 }, new byte[] { 1 })]
        [TestCase(",.", new byte[] { 97 }, new byte[] { 97 })]
        [TestCase("++>+++++[<+>-]++++++++[<++++++>-]<.", new byte[0], new byte[] { 55 })]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", new byte[0], new byte[] { 72, 101, 108, 108, 111, 32, 87, 111, 114, 108, 100, 33, 10 })]
        [Timeout(100)]
        public void Test_Engine_Bytes(string program, byte[] input, byte[] expected)
        {
            Engine bfe = new Engine();
            bfe.Program = program.ToCharArray();

            byte[] output = bfe.Run(input);

            bool allMatch = true;
            for (int i = 0; i < output.Length; i++)
            {
                allMatch = (expected[i] == output[i]) ? allMatch : false;
            }

            Assert.IsTrue(
                allMatch,
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
            bfe.Program = program.ToCharArray();

            string output = bfe.Run( input_1);

            bool info = string.Compare(expected_1, output) == 0;

            Assert.IsTrue(info, string.Format("expected {0} =/= input {1}", expected_1, output));

            output = bfe.Run(input_2);

            info = string.Compare(expected_2, output) == 0;

            Assert.IsTrue(info, string.Format("expected {0} =/= input {1}", expected_1, output));
        }

        /// <summary>
        /// Tests the rerun method of <see cref="Engine"/>.
        /// </summary>
        /// <param name="program">The program to be ran.</param>
        /// <param name="input_1">The first input.</param>
        /// <param name="expected_1">The expected out put of the program with the first input.</param>
        /// <param name="input_2">THe second input.</param>
        /// <param name="expected_2">THe expected output of the program with the second input.</param>
        [TestCase(",.", new char[] { '1' }, new char[] { '1' }, new char[] { '4' }, new char[] { '4' })]
        [TestCase(",.", new char[] { 'a' }, new char[] { 'a' }, new char[] { 'Z' }, new char[] { 'Z' })]
        [TestCase(",>+++++++++[<----->-]<--->,>+++++++++[<----->-]<---<[->+<]>>+++++++++[<+++++>-]<+++.", new char[] { '1', '2' }, new char[] { '3' }, new char[] { '3', '4' }, new char[] { '7' })]
        [Timeout(1000)]
        public void Test_ReRun(string program, char[] input_1, char[] expected_1, char[] input_2, char[] expected_2)
        {
            Engine bfe = new Engine();
            bfe.Program = program.ToCharArray();

            char[] output = bfe.Run(input_1);

            bool match = true;
            match = output.Length == expected_1.Length ? match : false;
            for (int i = 0; i < output.Length; i++)
            {
                match = output[i] == expected_1[i] ? match : false;
            }

            Assert.IsTrue(match, string.Format("expected {0} =/= input {1}", expected_1, output));

            output = bfe.Run(input_2);

            match = output.Length == expected_2.Length ? match : false;
            for (int i = 0; i < output.Length; i++)
            {
                match = output[i] == expected_2[i] ? match : false;
            }

            Assert.IsTrue(match, string.Format("expected {0} =/= input {1}", expected_1, output));
        }

        /// <summary>
        /// Tests the rerun method of <see cref="Engine"/>.
        /// </summary>
        /// <param name="program">The program to be ran.</param>
        /// <param name="input_1">The first input.</param>
        /// <param name="expected_1">The expected out put of the program with the first input.</param>
        /// <param name="input_2">THe second input.</param>
        /// <param name="expected_2">THe expected output of the program with the second input.</param>
        [TestCase(",.", new byte[] { 1 }, new byte[] { 1 }, new byte[] { 4 }, new byte[] { 4 })]
        [TestCase(",.", new byte[] { 53 }, new byte[] { 53 }, new byte[] { 111 }, new byte[] { 111 })]
        [TestCase(",>+++++++++[<----->-]<--->,>+++++++++[<----->-]<---<[->+<]>>+++++++++[<+++++>-]<+++.", new byte[] { 49, 50 }, new byte[] { 51 }, new byte[] { 51, 52 }, new byte[] { 55 })]
        [Timeout(1000)]
        public void Test_ReRun(string program, byte[] input_1, byte[] expected_1, byte[] input_2, byte[] expected_2)
        {
            Engine bfe = new Engine();
            bfe.Program = program.ToCharArray();

            byte[] output = bfe.Run(input_1);

            bool match = true;
            match = output.Length == expected_1.Length ? match : false;
            for (int i = 0; i < output.Length; i++)
            {
                match = output[i] == expected_1[i] ? match : false;
            }

            Assert.IsTrue(match, string.Format("expected {0} =/= input {1}", expected_1, output));

            output = bfe.Run(input_2);

            match = output.Length == expected_2.Length ? match : false;
            for (int i = 0; i < output.Length; i++)
            {
                match = output[i] == expected_2[i] ? match : false;
            }

            Assert.IsTrue(match, string.Format("expected {0} =/= input {1}", expected_1, output));
        }

        /// <summary>
        /// Test the dump memory to file function.
        /// </summary>
        /// <param name="program">The program to run.</param>
        /// <param name="expected">The expected memory dump to file.</param>
        [TestCase("++++++++++[>++++++++++<-]", new byte[] { 0, 100 })]
        [TestCase(">>++++>+++>++>+>>>>->-->--->----", new byte[] { 0, 0, 4, 3, 2, 1, 0, 0, 0, 255, 254, 253, 252 })]
        [TestCase("+++++>++++>+++>++>+>>->-->--->---->-----", new byte[] { 5, 4, 3, 2, 1, 0, 255, 254, 253, 252, 251 })]
        public void Test_DumpMemoryToFile(string program, byte[] expected)
        {
            Engine bfe = new Engine();
            bfe.Program = program.ToCharArray();

            string output = bfe.Run(string.Empty);

            string filepath = bfe.DumpMemoryToFile(false);
            Assert.IsNotNull(filepath);

            // Check file exists.
            Assert.IsTrue(File.Exists(filepath));

            // Read file in and validate.
            using (BinaryReader br = new BinaryReader(File.Open(filepath, FileMode.Open)))
            {
                for (int i = 0; i < 30000; i++)
                {
                    byte read = br.ReadByte();

                    if (i < expected.Length)
                    {
                        Assert.IsTrue(expected[i] == read);
                    }
                    else
                    {
                        Assert.IsTrue(0 == read);
                    }
                }
            }
        }

        /// <summary>
        /// Test the memory dump function.
        /// </summary>
        /// <param name="program">The program to run.</param>
        /// <param name="expected">The expected memory dump info</param>
        [TestCase("++++++++++[>++++++++++<-]", new byte[] { 0, 100 })]
        [TestCase(">>++++>+++>++>+>>>>->-->--->----", new byte[] { 0, 0, 4, 3, 2, 1, 0, 0, 0, 255, 254, 253, 252 })]
        [TestCase("+++++>++++>+++>++>+>>->-->--->---->-----", new byte[] { 5, 4, 3, 2, 1, 0, 255, 254, 253, 252, 251 })]
        public void Test_DumpMemory(string program, byte[] expected)
        {
            Engine bfe = new Engine();
            bfe.Program = program.ToCharArray();

            string output = bfe.Run(string.Empty);

            byte[] dump = bfe.DumpMemory();

            for (int i = 0; i < dump.Length; i++)
            {
                if (i < expected.Length)
                {
                    Assert.IsTrue(dump[i] == expected[i]);
                }
                else
                {
                    Assert.IsTrue(dump[i] == 0);
                }
            }
        }

        /*
                // These tests are timing dependent so don't run them on travisCI.


                /// <summary>
                /// Test checks the operation of the brain fuck Engine using StringReaders and StringWriters .
                /// </summary>
                /// <param name="program">the programs to be ran.</param>
                /// <param name="input">The inputs to the program.</param>
                /// <param name="expected">The expected output of the system.</param>
                [TestCase("+++++++++.", 1, true)] 
                [TestCase(".+[.+]", 0, true)] // Prints ASCII with not timeout
                [TestCase("++++[>+++++<-]>[<+++++>-]+<+[>[>+>+<<-]++>>[<<+>>-]>>>[-]++>[-]+>>>+[[-]++++++>>>]<<<[[<++++++++<++>>-]+<.<[>----<-]<]<<[>>>>>[>>>[-]+++++++++<[>-<-]+++++++++>[-[<->-]+[<<<]]<[>+<-]>]<<-]<<-]", 10, false)] // test function takes along time
                [TestCase(".+[.+].+[.+].+[.+].+[.+].+[.+].+[.+].+[.+].+[.+].+[.+].+[.+].+[.+].+[.+]", 2, false)] // prints ASCII multiple times. Should time out.
                [TestCase(".+[.+].", 2, true)] // prints ASCII
                [TestCase("+[]", 100, false)] // infinite loop
                [Timeout(1000)]
                public void Test_Engine_TimeOut(string program, int timeOut, bool shouldFinish)
                {
                    Engine bfe = new Engine();
                    bfe.Program = program.ToCharArray();

                    string output = bfe.Run(string.Empty,timeOut);

                    bool didFinish = output!=null;

                    string message = didFinish ? "program finished":"program didn't finish";

                    Assert.IsTrue(didFinish == shouldFinish , message);

                }
        */
    }
}