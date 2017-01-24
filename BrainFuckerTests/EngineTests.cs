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
        /// Test brain fuck programs.
        /// </summary>
        private string[] programs = 
            {
            "+++++.",
            ",.",
            "++>+++++[<+>-]++++++++[<++++++>-]<.",
            "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++."
        };

        /// <summary>
        /// Test inputs
        /// </summary>
        private string[] inputs = 
            {
            string.Empty,
            "1",
            string.Empty,
            string.Empty
        };

        /// <summary>
        /// The expected output of 
        /// </summary>
        private string[] expectedOutputs = 
            {
            new string((char)5, 1),
            "1",
            "7",
            "Hello World!\n"
        };

        /// <summary>
        /// Test checks the operation of the brain fuck output command "." .
        /// </summary>
        [Test]
        [Timeout(100)]
        public void Test_Output()
        {
            int testNum = 0;

            StringBuilder output = new StringBuilder();

            Engine bfe = new Engine(new StringReader(this.inputs[testNum]), new StringWriter(output));
            bfe.Run(this.programs[testNum]);

            string gotOutput = output.ToString();

            bool info = this.expectedOutputs[testNum] == gotOutput;

            Assert.IsTrue(
                info,
                string.Format(
                    "Program ( {0} ), with inputs ( {1} ), expected results of ( {2} ), got results of ( {3} )",
                    this.programs[testNum],
                    this.inputs[testNum],
                    this.expectedOutputs[testNum],
                    gotOutput));
        }

        /// <summary>
        /// Test checks the operation of the brain fuck input command "," .
        /// </summary>
        [Test]
        [Timeout(100)]
        public void Test_Input()
        {
            int testNum = 1;

            StringBuilder output = new StringBuilder();

            Engine bfe = new Engine(new StringReader(this.inputs[testNum]), new StringWriter(output));
            bfe.Run(this.programs[testNum]);

            string gotOutput = output.ToString();

            bool info = this.expectedOutputs[testNum] == gotOutput;

            Assert.IsTrue(
                info,
                string.Format(
                    "Program ( {0} ), with inputs ( {1} ), expected results of ( {2} ), got results of ( {3} )",
                    this.programs[testNum],
                    this.inputs[testNum],
                    this.expectedOutputs[testNum],
                    gotOutput));
        }

        /// <summary>
        /// Test checks the operation of the brain fuck looping commands "[" and "]" without any nesting .
        /// </summary>
        [Test]
        [Timeout(100)]
        public void Test_BasicLoop()
        {
            int testNum = 2;

            StringBuilder output = new StringBuilder();

            Engine bfe = new Engine(new StringReader(this.inputs[testNum]), new StringWriter(output));
            bfe.Run(this.programs[testNum]);

            string gotOutput = output.ToString();

            bool info = this.expectedOutputs[testNum] == gotOutput;

            Assert.IsTrue(
                info,
                string.Format(
                    "Program ( {0} ), with inputs ( {1} ), expected results of ( {2} ), got results of ( {3} )",
                    this.programs[testNum],
                    this.inputs[testNum],
                    this.expectedOutputs[testNum],
                    gotOutput));
        }

        /// <summary>
        /// Test checks the operation of the brain fuck looping commands "[" and "]" with nesting .
        /// </summary>
        [Test]
        [Timeout(1000)]
        public void Test_NestedLoops()
        {
            int testNum = 3;
            StringBuilder output = new StringBuilder();

            Engine bfe = new Engine(new StringReader(this.inputs[testNum]), new StringWriter(output));
            bfe.Run(this.programs[testNum]);

            string gotOutput = output.ToString();

            bool info = this.expectedOutputs[testNum] == gotOutput;

            Assert.IsTrue(
                info,
                string.Format(
                    "Inputs \"{1}\", expected results of \"{2}\", got results of \"{3}\"",
                    this.programs[testNum],
                    this.inputs[testNum],
                    this.expectedOutputs[testNum],
                    gotOutput));
        }

        /*[Test]
        [Timeout(1000)]
        public void Test_ReRun()
        {

            int testNum = 1;

            StringBuilder output = new StringBuilder();

            MemoryStream mStream = new MemoryStream();
            StreamWriter sWriter = new StreamWriter(mStream);
            sWriter.WriteLine(inputs[testNum]);


            BFEngine bfe = new BFEngine(new StreamReader(mStream), new StringWriter(output));
            bfe.run(programs[testNum]);

            string gotOutput = output.ToString();
            bool info = (expectedOutputs[testNum] == gotOutput);

            // clear output
            output.Clear();
            // =


            // re add the input
            sWriter.WriteLine("1");

            // Rerun with the same input
            bfe.rerun();
            gotOutput = output.ToString();

            info = info && ("1" == gotOutput);

            // clear output
            output.Clear();
            //=


            // add a different value
            sWriter.WriteLine("7");

            // Rerun with the same input
            bfe.rerun();
            gotOutput = output.ToString();

            info = info && ("7" == gotOutput);

            // clear output
            output.Clear();

            // add a different value
            sWriter.WriteLine("9");

            // Rerun with the same input
            bfe.rerun();
            gotOutput = output.ToString();

            info = info && ("9" == gotOutput);

            // clear output
            output.Clear();
            // =

            Assert.IsTrue(info);
        }*/
    }
}