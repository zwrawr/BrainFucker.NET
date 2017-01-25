// <copyright file="CommandsTests.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>

namespace BrainFucker.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// This class tests <see cref="Commands"/>.
    /// </summary>
    [TestFixture]
    public class CommandsTests
    {
        /// <summary>
        /// Test to check that <see cref="Commands.IsCommand(char)"/> is working as expected.
        /// </summary>
        /// <param name="c">The character to test.</param>
        /// <param name="isCommand">Weather the character is a valid command.</param>
        [TestCase('a', false)]
        [TestCase('z', false)]
        [TestCase('/', false)]
        [TestCase('!', false)]
        [TestCase('*', false)]
        [TestCase('@', false)]
        [TestCase('#', false)]
        [TestCase('A', false)]
        [TestCase('H', false)]
        [TestCase('<', true)]
        [TestCase('>', true)]
        [TestCase('.', true)]
        [TestCase(',', true)]
        [TestCase('+', true)]
        [TestCase('-', true)]
        [TestCase('[', true)]
        [TestCase(']', true)]
        public void IsCommandTest(char c, bool isCommand)
        {
            Assert.IsTrue(isCommand == Commands.IsCommand(c));
        }

        /// <summary>
        /// Test to check that <see cref="Commands.IsCommand(char)"/> is working as expected.
        /// </summary>
        /// <param name="inputProgram">A test program.</param>
        /// <param name="expectedOutputProgram">A stripped version of the input used as reference.</param>
        [TestCase("++--+-+->><<<><>,.,.[[[]]][][]..,,<><<>>[][]", "++--+-+->><<<><>,.,.[[[]]][][]..,,<><<>>[][]")]
        [TestCase("hello++--", "++--")]
        [TestCase(">,.,.[[[[    ]]]][][]++--b++--", ">,.,.[[[[]]]][][]++--++--")]
        [TestCase(">,.,.[[[[]]]][][]++--++--", ">,.,.[[[[]]]][][]++--++--")]
        public void StripNonCommandsTest(string inputProgram, string expectedOutputProgram)
        {
            char[] result = Commands.StripNonCommands(inputProgram.ToCharArray());
            char[] expected = expectedOutputProgram.ToCharArray();

            Assert.IsTrue(result.Length == expected.Length);

            for (int i = 0; i < result.Length; i++)
            {
                Assert.IsTrue(result[i] == expected[i]);
            }
        }
    }
}