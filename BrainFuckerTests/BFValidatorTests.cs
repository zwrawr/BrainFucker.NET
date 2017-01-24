using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrainFucker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFucker.Tests
{
    [TestClass()]
    public class BFValidatorTests
    {
        int tests = 9;

        string[] programs = 
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

        bool[] validLoops =
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

        bool[] validCommands =
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

        [TestMethod()]
        public void ValidateTest()
        {
            bool isValid = true;
            for (int i = 0; i < tests; i++)
            {
                isValid = ((validLoops[i] && validCommands[i]) == BFValidator.Validate(programs[i]))? true : false ;
            }

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void CheckForUnclosedBracketsTest()
        {
            bool isValid = true;
            for (int i = 0; i < tests; i++)
            {
                isValid = (validLoops[i] == BFValidator.Validate(programs[i])) ? true : false;
            }

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void CheckForInvalidCommandsTest()
        {
            bool isValid = true;
            for (int i = 0; i < tests; i++)
            {
                isValid = (validCommands[i] == BFValidator.Validate(programs[i])) ? true : false;
            }

            Assert.IsTrue(isValid);
        }
    }
}