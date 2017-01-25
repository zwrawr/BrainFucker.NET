// <copyright file="Commands.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>;

namespace BrainFucker
{
    using System.Collections.Generic;

    /// <summary>
    /// Used to look up brain fuck commands by there name.
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Increments the dataPointer.
        /// </summary>
        public const char NEXT = '>';

        /// <summary>
        /// Decrements the dataPointer.
        /// </summary>
        public const char PREV = '<';

        /// <summary>
        /// Increments the value in memory at the current dataPointer.
        /// </summary>
        public const char INC = '+';

        /// <summary>
        /// Decrements the value in memory at the current dataPointer.
        /// </summary>
        public const char DEC = '-';

        /// <summary>
        /// Outputs the value in memory at the current dataPointer.
        /// </summary>
        public const char OUT = '.';

        /// <summary>
        /// Inputs a value and places its value in memory at the current dataPointer.
        /// </summary>
        public const char IN = ',';

        /// <summary>
        /// Begins a loop.
        /// </summary>
        public const char BL = '[';

        /// <summary>
        /// Ends a loop.
        /// </summary>
        public const char BR = ']';

        /// <summary>
        /// Checks weather a char is a valid command.
        /// </summary>
        /// <param name="command">The possible command</param>
        /// <returns>A boolean indicating weather the input was a valid command.</returns>
        public static bool IsCommand(char command)
        {
            switch (command)
            {
                case Commands.NEXT:
                    return true;
                case Commands.PREV:
                    return true;
                case Commands.INC:
                    return true;
                case Commands.DEC:
                    return true;
                case Commands.IN:
                    return true;
                case Commands.OUT:
                    return true;
                case Commands.BL:
                    return true;
                case Commands.BR:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Strips all non command characters from a program.
        /// </summary>
        /// <param name="program">THe program to be sanitized.</param>
        /// <returns>The sanitized version of the program.</returns>
        public static char[] StripNonCommands(char[] program)
        {
            List<char> commands = new List<char>();

            foreach (char c in program)
            {
                if (Commands.IsCommand(c))
                {
                    commands.Add(c);
                }
            }

            return commands.ToArray();
        }
    }
}
