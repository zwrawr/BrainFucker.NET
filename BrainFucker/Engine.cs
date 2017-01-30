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
        private string program;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class. 
        /// </summary>
        public Engine()
        {
            this.data = new byte[Engine.DataSize];
        }

        /// <summary>
        /// Runs a brain fuck program
        /// </summary>
        /// <param name="program">The program to be run</param>
        /// <param name="input"> The input to the program. </param>
        /// <param name="timeLimit">The maximum time in milliseconds the program will be allowed to run for.
        /// Set to Zero for no time limit. Defaults to 1000 milliseconds</param>
        /// <returns>The outputs from the program</returns>
        public string Run(string program, string input, int timeLimit = 1000)
        {
            this.Init();

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
                    return null;
                }

                return output;
            }
            else if (isValid && timeLimit == 0)
            {
                return this.Interpret(program.ToCharArray(), input.ToCharArray());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Re runs the last program
        /// </summary>
        /// <param name="input"> The input to the program. </param>
        /// <param name="timeLimit">The maximum time in milliseconds the program will be allowed to run for.
        /// Set to Zero for no time limit. Defaults to 1000 milliseconds</param>
        /// <returns>The outputs from the program</returns>
        public string Rerun(string input, int timeLimit = 1000)
        {
            if (this.program != null)
            {
                this.Init();

                return this.Run(this.program, input, timeLimit);
            }
            else
            {
                return null;
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

            return builder.ToString();
        }

        /// <summary>
        /// Initializes the memory and pointers
        /// </summary>
        private void Init()
        {
            // zero out memory
            for (int i = 0; i < Engine.DataSize; i++)
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
