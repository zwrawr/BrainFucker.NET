// <copyright file="CommandsTests.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>

namespace BrainFucker.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class CommandsTests
    {

        [TestCase('a',false)]
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
        public void isCommandTest(char c, bool isCommand)
        {
            Assert.IsTrue(isCommand == Commands.isCommand(c));
        }

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