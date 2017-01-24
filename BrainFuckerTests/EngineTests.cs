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
        /// Test checks the operation of the brain fuck output command "." .
        /// </summary>
        [TestCase("+++++++++.", "", "\t")]
        [TestCase(",.", "1", "1")]
        [TestCase("++>+++++[<+>-]++++++++[<++++++>-]<.", "", "7")]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", "", "Hello World!\n")]
        [Timeout(100)]
        public void Test_Output(string programString, string inputString, string outputString)
        {

            StringBuilder output = new StringBuilder();

            Engine bfe = new Engine(new StringReader(inputString), new StringWriter(output));
            bfe.Run(programString);

            string gotOutput = output.ToString();

            bool info = outputString == gotOutput;

            Assert.IsTrue(
                info,
                string.Format(
                    "Program ( {0} ), with inputs ( {1} ), expected results of ( {2} ), got results of ( {3} )",
                    programString,
                    inputString,
                    outputString,
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