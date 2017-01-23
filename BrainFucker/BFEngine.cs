﻿// <copyright file="BFEngine.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>

namespace BrainFucker
{
    using System;
    using System.IO;

    /// <summary>
    /// A brain fuck engine, for interpreting brain fuck code.
    /// </summary>
    public class BFEngine
    {
        /// <summary>
        /// The amount of memory that this brain fuck engine has.
        /// </summary>
        private int dataSize = 30000;

        /// <summary>
        /// The memory for this brain fuck engine.
        /// </summary>
        private byte[] data;

        /// <summary>
        /// The data pointer for this brain fuck engine. 
        /// This points to the current memory cell.
        /// </summary>
        private int dataPointer = 0;

        /// <summary>
        /// The program pointer for this brain fuck engine.
        /// This points to the current command in the program.        
        /// </summary>
        private int programPointer = 0;

        /// <summary>
        /// The brain fuck program that is being ran.
        /// </summary>
        private string program;

        /// <summary>
        /// The input reader for this brain fuck engine. 
        /// Inputs to the brain fuck program are obtained from this reader.
        /// </summary>
        private StringReader inputReader;

        /// <summary>
        /// The output writer for this brain fuck engine. 
        /// Outputs from the brain fuck program are sent over this writer.
        /// </summary>
        private StringWriter outputWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BFEngine"/> class. 
        /// </summary>
        /// <param name="inputTextReader">The input reader to use.</param>
        /// <param name="outputTextWriter">The output writer to use.</param>
        public BFEngine(TextReader inputTextReader, TextWriter outputTextWriter)
        {
            this.inputReader = (StringReader)inputTextReader;
            this.outputWriter = (StringWriter)outputTextWriter;

            this.data = new byte[this.dataSize];

            this.Init();
        }

        /// <summary>
        /// Runs a brain fuck program
        /// </summary>
        /// <param name="program">The program to be run</param>
        public void Run(string program)
        {
            this.program = program;

            this.Interpret(program.ToCharArray());
        }

        /// <summary>
        /// Re runs the last program
        /// </summary>
        public void Rerun()
        {
            if (this.program != null)
            {
                this.Interpret(this.program.ToCharArray());
            }
        }

        /// <summary>
        /// Interpreter for brain fuck. This function is what actually runs brain fuck. 
        /// </summary>
        /// <param name="program"> The program to be ran. </param>
        private void Interpret(char[] program)
        {
            int loopDepth = 0;
            for (this.programPointer = 0; this.programPointer < this.program.Length; this.programPointer++)
            {
                switch (program[this.programPointer])
                {
                    case COMMANDS.NEXT:

                        this.dataPointer = (this.dataPointer == this.dataSize - 1) ? 0 : this.dataPointer + 1;
                        break;

                    case COMMANDS.PREV:

                        this.dataPointer = (this.dataPointer == 0) ? this.dataSize - 1 : this.dataPointer - 1;
                        break;

                    case COMMANDS.INC:

                        this.data[this.dataPointer]++;
                        break;

                    case COMMANDS.DEC:

                        this.data[this.dataPointer]--;
                        break;

                    case COMMANDS.OUT:

                        this.outputWriter.Write((char)this.data[this.dataPointer]);
                        break;

                    case COMMANDS.IN:

                        this.data[this.dataPointer] = (byte)this.inputReader.Read();
                        break;

                    case COMMANDS.BL:

                        if (this.data[this.dataPointer] == 0)
                        {
                            this.programPointer++;

                            while (loopDepth > 0 || program[this.programPointer] != COMMANDS.BR)
                            {
                                if (this.program[this.programPointer] == COMMANDS.BL)
                                {
                                    loopDepth++;
                                }

                                if (this.program[this.programPointer] == COMMANDS.BR)
                                {
                                    loopDepth--;
                                }

                                this.programPointer++;
                            }
                        }

                        break;

                    case COMMANDS.BR:

                        if (this.data[this.dataPointer] != 0)
                        {
                            this.programPointer--;

                            while (loopDepth > 0 || program[this.programPointer] != COMMANDS.BL)
                            {
                                if (this.program[this.programPointer] == COMMANDS.BR)
                                {
                                    loopDepth++;
                                }

                                if (this.program[this.programPointer] == COMMANDS.BL)
                                {
                                    loopDepth--;
                                }

                                this.programPointer--;
                            }

                            this.programPointer--;
                        }

                        break;

                    default:
                        throw new Exception("Undefined command");
                }
            }

            this.Init();
        }

        /// <summary>
        /// Initializes the memory and pointers
        /// </summary>
        private void Init()
        {
            // zero out memory
            for (int i = 0; i < this.dataSize; i++)
            {
                this.data[i] = 0;
            }

            // zero pointers
            this.dataPointer = 0;
            this.programPointer = 0;
        }

        /// <summary>
        /// Used to look up brain fuck commands by there name.
        /// </summary>
        private struct COMMANDS
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
}
