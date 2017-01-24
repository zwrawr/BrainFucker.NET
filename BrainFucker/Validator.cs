// <copyright file="Validator.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>;

namespace BrainFucker
{
    /// <summary>
    /// This class provides methods for validating brain fuck programs
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validates a brain fuck program.
        /// </summary>
        /// <param name="program">The program to validate</param>
        /// <returns>Returns true if program is valid, if not false</returns>
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

        /// <summary>
        /// Checks to see if the program has any incomplete brackets.
        /// </summary>
        /// <param name="commands">The brain fuck program to validate.</param>
        /// <returns>Returns true if program is valid, if not false</returns>
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

                // if loop depth is ever negative then there were too many closing brackets.
                if (loopDepth < 0)
                {
                    break;
                }
            }

            return loopDepth == 0;
        }

        /// <summary>
        /// Checks to see if the program has any non command chars.
        /// </summary>
        /// <param name="commands">The brain fuck program to validate.</param>
        /// <returns>Returns true if program is valid, if not false</returns>
        public static bool CheckForInvalidCommands(char[] commands)
        {
            bool isValid = true;
            foreach (char c in commands)
            {
                isValid = Commands.isCommand(c);

                if (!isValid)
                {
                    break;
                }
            }

            return isValid;
        }
    }
}
