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
        /// <param name="program">The program to test.</param>
        /// <param name="isValid">Weather the input is a valid program.</param>
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
        /// <param name="program">The test program.</param>
        /// <param name="isValid">Weather the program has valid brackets.</param>
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
        /// <param name="program">The test program.</param>
        /// <param name="isValid">Weather the program contains no invalid commands.</param>
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

        /// <summary>
        /// Test the CheckForInvalidCommands with a variety of validation modes/
        /// </summary>
        /// <param name="program">the program to check.</param>
        /// <param name="mode">The validation mode.</param>
        /// <param name="isValid">Whether the test program is valid in the current mode.</param>
        [TestCase("++++", Validator.Mode.STRICT, true)]
        [TestCase("+++ +", Validator.Mode.STRICT, false)]
        [TestCase("+++a+", Validator.Mode.STRICT, false)]
        [TestCase("++++", Validator.Mode.WHITESPACE, true)]
        [TestCase("+++ +", Validator.Mode.WHITESPACE, true)]
        [TestCase("+++a+", Validator.Mode.WHITESPACE, false)]
        [TestCase("+++ //hello\n+", Validator.Mode.COMMENTS, true)]
        [TestCase("++/*hello*/++", Validator.Mode.COMMENTS, true)]
        [TestCase("+//abcde\n++/*words*/+", Validator.Mode.COMMENTS, true)]
        [TestCase("+a+//abcde\n++/*words*/+", Validator.Mode.COMMENTS, false)]
        [TestCase("+a+//abcde\n++/*words*/+", Validator.Mode.ALL, true)]
        public void InvalidCommands_ValidationModes_Test(string program, Validator.Mode mode, bool isValid)
        {
            Assert.IsTrue(isValid == Validator.CheckForInvalidCommands(program.ToCharArray(), mode));
        }

        [TestCase("/* Multi Line Comment */", true)]
        [TestCase("/* Multi Line Comment * /", false)]
        [TestCase("/* Multi Line Comment *", false)]
        [TestCase("/* Multi Line Comment ", false)]
        [TestCase("/* Multi Line Comment /", false)]
        [TestCase("// Multi Line Comment \n", true)]
        [TestCase("// Multi Line Comment ", false)]
        public void UnclosedComments_Test(string program, bool isValid)
        {
            Assert.IsTrue(isValid == Validator.CheckForInvalidCommands(program.ToCharArray(), Validator.Mode.COMMENTS));
        }
    }
}