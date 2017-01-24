using NUnit.Framework;
using BrainFucker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFucker.Tests
{
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