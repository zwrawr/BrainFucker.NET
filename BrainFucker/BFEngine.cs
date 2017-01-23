using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BrainFucker
{
    public class BFEngine
    {
        private int dataSize = 30000;

        private struct COMMANDS {
            public const char NEXT = '>';
            public const char PREV = '<';
            public const char INC = '+';
            public const char DEC = '-';
            public const char OUT = '.';
            public const char IN = ',';
            public const char BL = '[';
            public const char BR = ']';
        }

        private char[] data;
        private int dataPointer = 0;


        private int programPointer = 0;

        private string program;

        private FileStream input;
        private StreamReader inputReader;

        private FileStream output;
        private StreamWriter outputWriter;


        public BFEngine(FileStream input, FileStream output)
        {
            this.data = new char[this.dataSize];

            this.input = input;
            this.inputReader = new StreamReader(input);

            this.output = output;
            this.outputWriter = new StreamWriter(output);

        }

        public void run(string program)
        {

            this.init();

            this.program = program;

            while (programPointer < program.Length)
            {
                this.interpret( this.program[programPointer],this.program.ToCharArray() );
            }
        }

        private void interpret(char command, char[] program )
        {
            int loopDepth;

            switch (command)
            {
                case COMMANDS.NEXT:

                    if ((dataPointer+1)< dataSize)
                    {
                        dataPointer++;
                    }
                    break;

                case COMMANDS.PREV:

                    if ((dataPointer -1 ) > 0)
                    {
                        dataPointer--;
                    }
                    break;

                case COMMANDS.INC:

                    this.data[dataPointer]++;
                    break;

                case COMMANDS.DEC:

                    this.data[dataPointer]--;
                    break;

                case COMMANDS.OUT:

                    this.outputWriter.Write(this.data[dataPointer]);
                    break;

                case COMMANDS.IN:

                    this.data[dataPointer] = (char)this.inputReader.Read();
                    break;

                case COMMANDS.BL:

                    if (data[dataPointer] == 0)
                    {
                        loopDepth = 1;
                        while (loopDepth > 0)
                        {
                            char futureCommand = program[++programPointer];

                            if (futureCommand == COMMANDS.BL)
                            {
                                loopDepth++;
                            }
                            else if (futureCommand == COMMANDS.BR)
                            {
                                loopDepth--;
                            }
                        }
                    }
                    break;

                case COMMANDS.BR:

                    loopDepth = 1;
                    while (loopDepth > 0)
                    {
                        char pastCommand = program[--programPointer];

                        if (pastCommand == COMMANDS.BL)
                        {
                            loopDepth--;
                        }
                        else if (pastCommand == COMMANDS.BR)
                        {
                            loopDepth++;
                        }
                    }
                    programPointer--;

                    break;

                default:
                    break;
            }
        }

        private void init()
        {
            // zero out memory
            for (int i = 0; i < this.dataSize; i++)
            {
                this.data[i] = (char)0;
            }

            // zero pointers
            this.dataPointer = 0;
            this.programPointer = 0;
        }
    }
}
