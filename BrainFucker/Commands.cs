// <copyright file="Commands.cs" company="Zak West">
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

    /// <summary>
    /// Used to look up brain fuck commands by there name.
    /// </summary>
    public static class Commands
    {

        public const char NEXT = '>';
        public const char PREV = '<';
        public const char INC = '+';
        public const char DEC = '-';
        public const char OUT = '.';
        public const char IN = ',';
        public const char BL = '[';
        public const char BR = ']';

        /// <summary>
        /// Checks weather a char is a valid command.
        /// </summary>
        /// <param name="command">The possible command</param>
        /// <returns>A boolean indicating weather the input was a valid command.</returns>
        public static bool isCommand(char command)
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

        public static char[] StripNonCommands(char[] program)
        {
            List<char> commands = new List<char>();

            foreach (char c in program)
            {
                if (Commands.isCommand(c))
                {
                    commands.Add(c);
                }
            }

            return commands.ToArray();
        }
    }
}
