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
    [TestFixture]
    public class ValidatorTests
    {

        /// <summary>
        /// Tests to make sure that the Validate method works correctly.
        /// </summary>
        [TestCase("++++", true)]
        [TestCase("+++[>+<]++", true)]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", true)]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", false)]
        [TestCase("++++++++>++++>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", false)]
        [TestCase("these are not commands", false)]
        [TestCase("+++[>+<] ++", false)]
        [TestCase("+++[>+<++", false)]
        [TestCase("+++>+<] ++", false)]
        public void ValidateTest(string program, bool isValid)
        {
            Assert.IsTrue(isValid == Validator.Validate(program));
        }

        /// <summary>
        /// Tests to make sure that the CheckForUnclosedBrackets method works correctly.
        /// </summary>
        [TestCase("++++", true)]
        [TestCase("+++[>+<]++", true)]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", true)]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", false)]
        [TestCase("++++++++>++++>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", false)]
        [TestCase("these are not commands", true)]
        [TestCase("+++[>+<] ++", true)]
        [TestCase("+++[>+<++", false)]
        [TestCase("+++>+<] ++", false)]
        public void CheckForUnclosedBracketsTest(string program, bool isValid)
        {
            Assert.IsTrue(isValid == Validator.CheckForUnclosedBrackets(program.ToCharArray()));
        }

        /// <summary>
        /// Tests to make sure that the CheckForInvalidCommands method works correctly.
        /// </summary>
        [TestCase("++++", true)]
        [TestCase("+++[>+<]++", true)]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", true)]
        [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", true)]
        [TestCase("++++++++>++++>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", true)]
        [TestCase("these are not commands", false)]
        [TestCase("+++[>+<] ++", false)]
        [TestCase("+++[>+<++", true)]
        [TestCase("+++>+<] ++", false)]
        public void CheckForInvalidCommandsTest(string program, bool isValid)
        {
            Assert.IsTrue(isValid == Validator.CheckForInvalidCommands(program.ToCharArray()));
        }
    }
}