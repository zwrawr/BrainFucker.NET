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
    static class Commands
    {

        public const char NEXT = '>';
        public const char PREV = '<';
        public const char INC = '+';
        public const char DEC = '-';
        public const char OUT = '.';
        public const char IN = ',';
        public const char BL = '[';
        public const char BR = ']';
    }
}
