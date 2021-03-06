﻿// <copyright file="Validator.cs" company="Zak West">
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
        /// Represents the different validation modes.
        /// </summary>
        public enum Mode
        {
            /// <summary> Only allows bf commands. </summary>
            STRICT,

            /// <summary> Only allows bf commands and whitespace. </summary>
            WHITESPACE,

            /// <summary> Only allows bf commands, whitespace and comments. </summary>
            COMMENTS,

            /// <summary> Allows all. </summary>
            ALL
        }

        /// <summary>
        /// Validates a brain fuck program.
        /// </summary>
        /// <param name="commands">The program to validate</param>
        /// <param name="validatorMode">Which validation mode to use. Defaults to strict.</param>
        /// <returns>Returns true if program is valid, if not false</returns>
        public static bool Validate(char[] commands, Mode validatorMode = Mode.STRICT)
        {
            if (commands.Length == 0)
            {
                return false;
            }

            bool isValid = CheckForInvalidCommands(commands, validatorMode);
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
        /// Validates a brain fuck program.
        /// </summary>
        /// <param name="program">The program to validate</param>
        /// <param name="validatorMode">Which validation mode to use. Defaults to strict.</param>
        /// <returns>Returns true if program is valid, if not false</returns>
        public static bool Validate(string program, Mode validatorMode = Mode.STRICT)
        {
            char[] commands = program.ToCharArray();
            return Validate(commands, validatorMode);
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
        /// <param name="validatorMode">Which validation mode to use. Defaults to strict.</param>
        /// <returns>Returns true if program is valid, if not false</returns>
        public static bool CheckForInvalidCommands(char[] commands, Mode validatorMode = Mode.STRICT)
        {
            if (validatorMode == Mode.ALL)
            {
                return true;
            }

            bool isValid = true;

            for (int i = 0; i < commands.Length; i++)
            {
                char c = commands[i];

                bool isCommand = Commands.IsCommand(c);

                if (isCommand)
                {
                    isValid = true;
                }
                else if (validatorMode == Mode.WHITESPACE)
                {
                    if (!(c == ' ' || c == '\n' || c == '\t' || c == '\r'))
                    {
                        return false;
                    }
                }
                else if (validatorMode == Mode.COMMENTS || validatorMode == Mode.ALL)
                {
                    if (!(c == ' ' || c == '\n' || c == '\t' || c == '\r'))
                    {
                        if (commands[i] == '/' && commands[i + 1] == '/') // in line comment start.
                        {
                            do { i++; } while ((i < commands.Length-1) && commands[i] != '\n');

                            // make sure we didn't hit the end of the input before finding the matching comment delimiter.
                            if ((i == commands.Length - 1) && commands[i] != '\n')
                            {
                                return false;
                            }
                        }
                        else if (commands[i] == '/' && commands[i + 1] == '*') // multi line comment start.
                        {
                            do { i++; } while ((i < commands.Length - 2) && !(commands[i] == '*' && commands[i + 1] == '/'));

                            // make sure we didn't hit the end of the input before finding the matching comment delimiter.
                            if ((i == commands.Length - 2) && !(commands[i] == '*' && commands[i + 1] == '/'))
                            {
                                return false;
                            }

                            i++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return isValid;
        }
    }
}
