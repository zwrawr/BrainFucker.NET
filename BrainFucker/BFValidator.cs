// <copyright file="BFValidator.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>;

namespace BrainFucker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class BFValidator
    {

        public static bool Validate(string program)
        {
            char[] commands = program.ToCharArray();

            if (program.Length == 0)
            {
                return false;
            }

            bool isValid = CheckForInvalidCommands(commands);
            if (isValid == false)
            {
                return false;
            }

            isValid = CheckForUnclosedBrackets(commands);
            if (isValid == false)
            {
                return false;
            }

            return true;
        }

        public static bool CheckForUnclosedBrackets(char[] commands)
        {
            int loopDepth = 0;
            foreach (char c in commands)
            {
                if (c == Commands.BL)
                {
                    loopDepth++;
                }
                else if (c == Commands.BR)
                {
                    loopDepth--;
                }

                if (loopDepth < 0)
                {
                    break;
                }
            }

            return (loopDepth == 0);
        }

        public static bool CheckForInvalidCommands(char[] commands)
        {
            bool isValid = true;
            foreach (char c in commands)
            {
                if ((c != Commands.NEXT) &&
                    (c != Commands.PREV) &&
                    (c != Commands.INC) &&
                    (c != Commands.DEC) &&
                    (c != Commands.IN) &&
                    (c != Commands.OUT) &&
                    (c != Commands.BL) &&
                    (c != Commands.BR))
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }
    }
}
