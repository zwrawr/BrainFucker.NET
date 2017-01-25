// <copyright file="Engine.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>

namespace BrainFucker
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// A brain fuck engine, for interpreting brain fuck code.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// The amount of memory that this brain fuck engine has.
        /// </summary>
        private const int dataSize = 30000;

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
        /// The input pointer for this brain fuck engine.
        /// This points to the current input to the program.        
        /// </summary>
        private int inputPointer = 0;

        /// <summary>
        /// The brain fuck program that is being ran.
        /// </summary>
        private string program;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class. 
        /// </summary>
        public Engine()
        {
            this.data = new byte[Engine.dataSize];

            this.Init();
        }

        /// <summary>
        /// Runs a brain fuck program
        /// </summary>
        /// <param name="program">The program to be run</param>
        /// <param name="input"> The input to the program. </param>
        /// <param name="timeLimit">The maximum time in milliseconds the program will be allowed to run for.
        /// Set to Zero for no time limit. Defaults to 1000ms</param>
        /// <returns>The outputs from the program</returns>
        public string Run(string program, string input, int timeLimit = 1000)
        {
            this.program = program;

            bool isValid = Validator.Validate(program);

            if (isValid && timeLimit > 0)
            {
                string output = string.Empty;

                Thread thread = new Thread(() => output = this.Interpret(program.ToCharArray(), input.ToCharArray()));
                thread.Start();

                bool completed = thread.Join(timeLimit);
                if (!completed)
                {
                    return string.Empty;
                }

                return output;
            }
            else if (isValid && timeLimit == 0)
            {
                return this.Interpret(program.ToCharArray(), input.ToCharArray());
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Re runs the last program
        /// </summary>
        /// <param name="input"> The input to the program. </param>
        /// <param name="timeLimit">The maximum time in milliseconds the program will be allowed to run for.
        /// Set to Zero for no time limit. Defaults to 1000ms</param>
        /// <returns>The outputs from the program</returns>
        public string Rerun(string input, int timeLimit = 1000)
        {
            if (this.program != null)
            {
                return this.Run(this.program,input,timeLimit);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Interpreter for brain fuck. This function is what actually runs brain fuck. 
        /// </summary>
        /// <param name="commands"> The program to be ran. </param>
        /// <param name="inputs"> The inputs to the program. </param>
        /// <returns>The outputs from the program</returns>
        private string Interpret(char[] commands, char[] inputs)
        {
            StringBuilder builder = new StringBuilder();
            int loopDepth = 0;
            for (this.programPointer = 0; this.programPointer < this.program.Length; this.programPointer++)
            {
                switch (commands[this.programPointer])
                {
                    case Commands.NEXT:

                        this.dataPointer = (this.dataPointer == Engine.dataSize - 1) ? 0 : this.dataPointer + 1;
                        break;

                    case Commands.PREV:

                        this.dataPointer = (this.dataPointer == 0) ? Engine.dataSize - 1 : this.dataPointer - 1;
                        break;

                    case Commands.INC:

                        this.data[this.dataPointer]++;
                        break;

                    case Commands.DEC:

                        this.data[this.dataPointer]--;
                        break;

                    case Commands.OUT:

                        builder.Append((char)this.data[this.dataPointer]);
                        break;

                    case Commands.IN:

                        this.data[this.dataPointer] = (byte)inputs[this.inputPointer];
                        this.inputPointer++;
                        break;

                    case Commands.BL:

                        if (this.data[this.dataPointer] == 0)
                        {
                            this.programPointer++;

                            while (loopDepth > 0 || commands[this.programPointer] != Commands.BR)
                            {
                                if (this.program[this.programPointer] == Commands.BL)
                                {
                                    loopDepth++;
                                }

                                if (this.program[this.programPointer] == Commands.BR)
                                {
                                    loopDepth--;
                                }

                                this.programPointer++;
                            }
                        }

                        break;

                    case Commands.BR:

                        if (this.data[this.dataPointer] != 0)
                        {
                            this.programPointer--;

                            while (loopDepth > 0 || commands[this.programPointer] != Commands.BL)
                            {
                                if (this.program[this.programPointer] == Commands.BR)
                                {
                                    loopDepth++;
                                }

                                if (this.program[this.programPointer] == Commands.BL)
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
            return builder.ToString();
        }

        /// <summary>
        /// Initializes the memory and pointers
        /// </summary>
        private void Init()
        {
            // zero out memory
            for (int i = 0; i < Engine.dataSize; i++)
            {
                this.data[i] = 0;
            }

            // zero pointers
            this.dataPointer = 0;
            this.programPointer = 0;
            this.inputPointer = 0;
        }
    }
}
