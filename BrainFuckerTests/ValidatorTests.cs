// <copyright file="ValidatorTests.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>

namespace BrainFucker.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Responsible for testing the <see cref="Validator"/> class.
    /// </summary>
    public class ValidatorTests
    {
        /// <summary>
        /// The number of brain fuck programs under test
        /// </summary>
        private int tests = 9;

        /// <summary>
        /// The test brain fuck programs
        /// </summary>
        private string[] programs = 
            {
            "++++",
            "+++[>+<]++",
            "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.",
            "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.",
            "++++++++>++++>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.",
            "these are not commands",
            "+++[>+<] ++",
            "+++[>+<++",
            "+++>+<] ++"
        };

        /// <summary>
        /// Weather the test brain fuck programs only contain valid loops
        /// </summary>
        private bool[] validLoops =
            {
            true,
            true,
            true,
            false,
            false,
            true,
            true,
            false,
            false
        };

        /// <summary>
        /// Weather the test brain fuck programs only contain valid commands
        /// </summary>
        private bool[] validCommands =
           {
            true,
            true,
            true,
            true,
            true,
            false,
            false,
            false,
            false
        };

        /// <summary>
        /// Tests to make sure that the Validate method works correctly.
        /// </summary>
        [Test]
        public void ValidateTest()
        {
            bool isValid = true;
            for (int i = 0; i < this.tests; i++)
            {
                isValid = ((this.validLoops[i] && this.validCommands[i]) == Validator.Validate(this.programs[i])) ? true : false;
            }

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests to make sure that the CheckForUnclosedBrackets method works correctly.
        /// </summary>
        [Test]
        public void CheckForUnclosedBracketsTest()
        {
            bool isValid = true;
            for (int i = 0; i < this.tests; i++)
            {
                isValid = (this.validLoops[i] == Validator.Validate(this.programs[i])) ? true : false;
            }

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests to make sure that the CheckForInvalidCommands method works correctly.
        /// </summary>
        [Test]
        public void CheckForInvalidCommandsTest()
        {
            bool isValid = true;
            for (int i = 0; i < this.tests; i++)
            {
                isValid = (this.validCommands[i] == Validator.Validate(this.programs[i])) ? true : false;
            }

            Assert.IsTrue(isValid);
        }
    }
}