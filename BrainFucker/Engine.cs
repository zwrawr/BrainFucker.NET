﻿// <copyright file="Engine.cs" company="Zak West">
//     This code is licensed under GNU LGPL v3.0.
// </copyright>
// <author>Zak West, @zwrawr, zwrawr@gmail.com</author>

namespace BrainFucker
{
    using System;
    using System.Collections.Generic;
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
        private const int DataSize = 30000;

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
        private char[] program;

        /// <summary>
        /// Whether the program has started execution.
        /// </summary>
        private bool programStarted = false;

        /// <summary>
        /// Whether the program has finished execution.
        /// </summary>
        private bool programFinished = false;

        /// <summary>
        /// How many loops are we in at the current location of the programPointer.
        /// </summary>
        private int loopDepth = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class. 
        /// </summary>
        public Engine()
        {
            this.programStarted = false;
            this.programFinished = false;
            this.data = new byte[Engine.DataSize];
        }

        /// <summary>
        /// Gets or sets the program that this engine will run.
        /// </summary>
        public char[] Program
        {
            get
            {
                return this.program;
            }

            set
            {
                this.program = value;
                this.Init();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the program has stared running.
        /// </summary>
        public bool Started
        {
            get { return this.programFinished; }
        }

        /// <summary>
        /// Gets a value indicating whether the program is currently running.
        /// </summary>
        public bool Running
        {
            get { return this.programStarted && !this.programFinished; }
        }

        /// <summary>
        /// Gets a value indicating whether the program has finished running.
        /// </summary>
        public bool Finished
        {
            get { return this.programFinished; }
        }

        /// <summary>
        /// Runs a brain fuck program
        /// </summary>
        /// <param name="input"> The input to the program, in the form of a string.</param>
        /// <param name="timeLimit">The maximum time in milliseconds the program will be allowed to run for.
        /// Set to Zero for no time limit. Defaults to 1000 milliseconds</param>
        /// <returns>The outputs from the program, in the form of a string.</returns>
        public string Run(string input, int timeLimit = 1000)
        {
            char[] result = this.Run(input.ToCharArray(), timeLimit);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToChar(result[i]);
            }

            return new string(result);
        }

        /// <summary>
        /// Runs a brain fuck program.
        /// </summary>
        /// <param name="input"> The input to the program, in the form of a char array.</param>
        /// <param name="timeLimit">The maximum time in milliseconds the program will be allowed to run for.
        /// Set to Zero for no time limit. Defaults to 1000 milliseconds</param>
        /// <returns>The outputs from the program, in the form of a char array.</returns>
        public char[] Run(char[] input, int timeLimit = 1000)
        {
            byte[] inputBytes = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                inputBytes[i] = Convert.ToByte(input[i]);
            }

            byte[] rawResult = this.Run(inputBytes, timeLimit);

            char[] result = Encoding.ASCII.GetString(rawResult).ToCharArray();
            return result;
        }

        /// <summary>
        /// Runs a brain fuck program.
        /// </summary>
        /// <param name="input"> The input to the program, in the form of a byte array.</param>
        /// <param name="timeLimit">The maximum time in milliseconds the program will be allowed to run for.
        /// Set to Zero for no time limit. Defaults to 1000 milliseconds</param>
        /// <returns>The outputs from the program, in the form of a byte array.</returns>
        public byte[] Run(byte[] input, int timeLimit = 1000)
        {
            this.Init();

            if (this.program == null)
            {
                return null;
            }

            bool isValid = Validator.Validate(this.program);

            if (isValid && timeLimit > 0)
            {
                this.programStarted = true;

                byte[] output = null;

                Thread thread = new Thread(() => output = this.Interpret(this.program, input));
                thread.Start();

                bool completed = thread.Join(timeLimit);
                if (!completed)
                {
                    return null;
                }

                this.programFinished = true;
                return output;
            }
            else if (isValid && timeLimit == 0)
            {
                this.programStarted = true;

                byte[] output = this.Interpret(this.program, input);

                this.programFinished = true;

                return output;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Runs a brain fuck program.
        /// </summary>
        /// <param name="inputCallBack">A call back function to get the next input value.</param>
        /// <param name="outputCallback">A call back function to pass out any outputs.</param>
        /// <param name="timeLimit">
        /// The maximum time to try and execute the brain fuck program. If zero then no time limit.
        /// </param>
        /// <returns>A boolean indicating whether or not the execution completed.</returns>
        public bool Run(Func<byte> inputCallBack, Action<byte> outputCallback, int timeLimit = 1000)
        {
            this.Init();

            if (this.program == null)
            {
                return false;
            }

            bool isValid = Validator.Validate(this.program);

            if (isValid)
            {
                this.programStarted = true;

                Thread thread = new Thread(
                    () =>
                        {
                            for (this.programPointer = 0; this.programPointer < this.program.Length; this.programPointer++)
                            {
                                byte input = (this.program[this.programPointer] == Commands.IN) ? inputCallBack() : (byte)0;
                                byte output = (byte)0;

                                this.InterpretCommand(this.program, input, out output);

                                if (this.program[this.programPointer] == Commands.OUT)
                                {
                                    outputCallback(output);
                                }
                            }
                        });

                thread.Start();

                if (timeLimit != 0)
                {
                    bool completed = thread.Join(timeLimit);
                    if (!completed)
                    {
                        this.programFinished = true;
                        return false; // did finish in time limit
                    }
                    else
                    {
                        return false; // didn't finish executing in time limit
                    }
                }
                else
                {
                    thread.Join();
                    this.programFinished = true;
                    return true; // did finish
                }
            }
            else
            {
                return false; // didn't finish running, didn't start.
            }
        }

        /// <summary>
        /// Gets a dump of the memory.
        /// </summary>
        /// <returns>Returns the contents of memory. Contains 30,000 bytes.</returns>
        public byte[] DumpMemory()
        {
            return this.data;
        }

        /// <summary>
        /// Writes the contents of memory to file.
        /// </summary>
        /// <param name="formatted">If true writes a formatted version in Hex, if not writes a raw version of the file.</param>
        /// <param name="path">The folder in which to create the memory dump file. Default to "./BFDumps" .</param>
        /// <returns>The path to the created file.</returns>
        public string DumpMemoryToFile(bool formatted, string path = "")
        {
            if (path == string.Empty)
            {
                path = string.Format("{0}/BF", Path.GetTempPath());
            }

            Directory.CreateDirectory(path);

            if (formatted)
            {
                string file = string.Format("{0}/{1}.mem", path, DateTime.Now.ToFileTime().ToString());

                FileStream fs = new FileStream(file, FileMode.Create);

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < DataSize; i++)
                    {
                        if (i % 32 == 0)
                        {
                            sw.Write(sw.NewLine);
                        }

                        sw.Write(this.data[i].ToString("X2") + " ");
                    }
                }

                fs.Close();
                return file;
            }
            else
            {
                string file = string.Format("{0}/{1}.bd", path, DateTime.Now.ToFileTime().ToString());
                FileStream fs = new FileStream(file, FileMode.Create);

                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(this.data);
                }

                fs.Close();
                return file;
            }
        }

        /// <summary>
        /// Preforms one step of the program.
        /// </summary>
        /// <param name="inputCallBack">A call back function to get the next input value.</param>
        /// <param name="outputCallback">A call back function to pass out any outputs.</param>
        public void Step(Func<byte> inputCallBack, Action<byte> outputCallback)
        {
            if (this.programPointer <= 0 || this.programFinished)
            {
                this.Init();
                this.programStarted = true;
            }

            byte input = (this.program[this.programPointer] == Commands.IN) ? inputCallBack() : (byte)0;
            byte output;

            this.InterpretCommand(this.program, input, out output);

            if (this.program[this.programPointer] == Commands.OUT)
            {
                outputCallback(output);
            }

            this.programPointer++;

            if (this.programPointer == this.program.Length)
            {
                this.programFinished = true;
            }
        }

        /// <summary>
        /// Interpreter for brain fuck. This function is what actually runs brain fuck. 
        /// </summary>
        /// <param name="commands"> The program to be ran. </param>
        /// <param name="inputs"> The inputs to the program. </param>
        /// <returns>The outputs from the program</returns>
        private byte[] Interpret(char[] commands, byte[] inputs)
        {
            List<byte> builder = new List<byte>();

            for (this.programPointer = 0; this.programPointer < this.program.Length; this.programPointer++)
            {
                byte input = (inputs.Length > 0 && this.inputPointer < inputs.Length) ? inputs[this.inputPointer] : (byte)0;

                byte output = (byte)0;
                this.InterpretCommand(commands, input, out output);

                if (this.program[this.programPointer] == Commands.OUT)
                {
                    builder.Add(output);
                }
            }

            return builder.ToArray();
        }

        /// <summary>
        /// Interpreters a single command.
        /// </summary>
        /// <param name="commands">The commands in the program.</param>
        /// <param name="input">The input to the program.</param>
        /// <param name="output">The output of the program.</param>
        private void InterpretCommand(char[] commands, byte input, out byte output)
        {
            output = (byte)0;

            switch (commands[this.programPointer])
            {
                case Commands.NEXT:

                    this.dataPointer = (this.dataPointer == Engine.DataSize - 1) ? 0 : this.dataPointer + 1;
                    break;

                case Commands.PREV:

                    this.dataPointer = (this.dataPointer == 0) ? Engine.DataSize - 1 : this.dataPointer - 1;
                    break;

                case Commands.INC:

                    this.data[this.dataPointer]++;
                    break;

                case Commands.DEC:

                    this.data[this.dataPointer]--;
                    break;

                case Commands.OUT:

                    output = this.data[this.dataPointer];
                    break;

                case Commands.IN:

                    this.data[this.dataPointer] = input;
                    this.inputPointer++;
                    break;

                case Commands.BL:

                    if (this.data[this.dataPointer] == 0)
                    {
                        this.programPointer++;

                        while (this.loopDepth > 0 || commands[this.programPointer] != Commands.BR)
                        {
                            if (this.program[this.programPointer] == Commands.BL)
                            {
                                this.loopDepth++;
                            }

                            if (this.program[this.programPointer] == Commands.BR)
                            {
                                this.loopDepth--;
                            }

                            this.programPointer++;
                        }
                    }

                    break;

                case Commands.BR:

                    if (this.data[this.dataPointer] != 0)
                    {
                        this.programPointer--;

                        while (this.loopDepth > 0 || commands[this.programPointer] != Commands.BL)
                        {
                            if (this.program[this.programPointer] == Commands.BR)
                            {
                                this.loopDepth++;
                            }

                            if (this.program[this.programPointer] == Commands.BL)
                            {
                                this.loopDepth--;
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

        /// <summary>
        /// Initializes the memory and pointers
        /// </summary>
        private void Init()
        {
            if (this.programFinished == false && this.programStarted == false)
            {
                return;
            }

            // zero out memory
            for (int i = 0; i < Engine.DataSize; i++)
            {
                this.data[i] = 0;
            }

            // zero pointers
            this.dataPointer = 0;
            this.programPointer = 0;
            this.inputPointer = 0;

            this.loopDepth = 0;

            // reset program running info
            this.programStarted = false;
            this.programFinished = false;
        }
    }
}
