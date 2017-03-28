# BrainFucker #

## Type Engine

 A brain fuck engine, for interpreting brain fuck code. 



---
#### Field Engine.DataSize

 The amount of memory that this brain fuck engine has. 



---
#### Field Engine.data

 The memory for this brain fuck engine. 



---
#### Field Engine.dataPointer

 The data pointer for this brain fuck engine. This points to the current memory cell. 



---
#### Field Engine.programPointer

 The program pointer for this brain fuck engine. This points to the current command in the program. 



---
#### Field Engine.inputPointer

 The input pointer for this brain fuck engine. This points to the current input to the program. 



---
#### Field Engine.program

 The brain fuck program that is being ran. 



---
#### Field Engine.programStarted

 Whether the program has started execution. 



---
#### Field Engine.programFinished

 Whether the program has finished execution. 



---
#### Field Engine.loopDepth

 How many loops are we in at the current location of the programPointer. 



---
#### Method Engine.#ctor

 Initializes a new instance of the [[|T:BrainFucker.Engine]] class. 



---
#### Property Engine.Program

 Gets or sets the program that this engine will run. 



---
#### Property Engine.Started

 Gets a value indicating whether the program has stared running. 



---
#### Property Engine.Running

 Gets a value indicating whether the program is currently running. 



---
#### Property Engine.Finished

 Gets a value indicating whether the program has finished running. 



---
#### Method Engine.Run(System.String,System.Int32)

 Runs a brain fuck program 

|Name | Description |
|-----|------|
|input: | The input to the program, in the form of a string.|
|timeLimit: |The maximum time in milliseconds the program will be allowed to run for. Set to Zero for no time limit. Defaults to 1000 milliseconds|
**Returns**: The outputs from the program, in the form of a string.



---
#### Method Engine.Run(System.Char[],System.Int32)

 Runs a brain fuck program. 

|Name | Description |
|-----|------|
|input: | The input to the program, in the form of a char array.|
|timeLimit: |The maximum time in milliseconds the program will be allowed to run for. Set to Zero for no time limit. Defaults to 1000 milliseconds|
**Returns**: The outputs from the program, in the form of a char array.



---
#### Method Engine.Run(System.Byte[],System.Int32)

 Runs a brain fuck program. 

|Name | Description |
|-----|------|
|input: | The input to the program, in the form of a byte array.|
|timeLimit: |The maximum time in milliseconds the program will be allowed to run for. Set to Zero for no time limit. Defaults to 1000 milliseconds|
**Returns**: The outputs from the program, in the form of a byte array.



---
#### Method Engine.Run(System.Func{System.Byte},System.Action{System.Byte},System.Int32)

 Runs a brain fuck program. 

|Name | Description |
|-----|------|
|inputCallBack: |A call back function to get the next input value.|
|outputCallback: |A call back function to pass out any outputs.|
|timeLimit: | The maximum time to try and execute the brain fuck program. If zero then no time limit. |
**Returns**: A boolean indicating whether or not the execution completed.



---
#### Method Engine.DumpMemory

 Gets a dump of the memory. 

**Returns**: Returns the contents of memory. Contains 30,000 bytes.



---
#### Method Engine.DumpMemoryToFile(System.Boolean,System.String)

 Writes the contents of memory to file. 

|Name | Description |
|-----|------|
|formatted: |If true writes a formatted version in Hex, if not writes a raw version of the file.|
|path: |The folder in which to create the memory dump file. Default to "./BFDumps" .|
**Returns**: The path to the created file.



---
#### Method Engine.Step(System.Func{System.Byte},System.Action{System.Byte})

 Preforms one step of the program. 

|Name | Description |
|-----|------|
|inputCallBack: |A call back function to get the next input value.|
|outputCallback: |A call back function to pass out any outputs.|


---
#### Method Engine.Interpret(System.Char[],System.Byte[])

 Interpreter for brain fuck. This function is what actually runs brain fuck. 

|Name | Description |
|-----|------|
|commands: | The program to be ran. |
|inputs: | The inputs to the program. |
**Returns**: The outputs from the program



---
#### Method Engine.InterpretCommand(System.Char[],System.Byte,System.Byte@)

 Interpreters a single command. 

|Name | Description |
|-----|------|
|commands: |The commands in the program.|
|input: |The input to the program.|
|output: |The output of the program.|


---
#### Method Engine.Init

 Initializes the memory and pointers 



---
## Type Validator

 This class provides methods for validating brain fuck programs 



---
## Type Validator.Mode

 Represents the different validation modes. 



---
#### Field Validator.Mode.STRICT

 Only allows bf commands. 



---
#### Field Validator.Mode.WHITESPACE

 Only allows bf commands and whitespace. 



---
#### Field Validator.Mode.COMMENTS

 Only allows bf commands, whitespace and comments. 



---
#### Field Validator.Mode.ALL

 Allows all. 



---
#### Method Validator.Validate(System.Char[],BrainFucker.Validator.Mode)

 Validates a brain fuck program. 

|Name | Description |
|-----|------|
|commands: |The program to validate|
|validatorMode: |Which validation mode to use. Defaults to strict.|
**Returns**: Returns true if program is valid, if not false



---
#### Method Validator.Validate(System.String,BrainFucker.Validator.Mode)

 Validates a brain fuck program. 

|Name | Description |
|-----|------|
|program: |The program to validate|
|validatorMode: |Which validation mode to use. Defaults to strict.|
**Returns**: Returns true if program is valid, if not false



---
#### Method Validator.CheckForUnclosedBrackets(System.Char[])

 Checks to see if the program has any incomplete brackets. 

|Name | Description |
|-----|------|
|commands: |The brain fuck program to validate.|
**Returns**: Returns true if program is valid, if not false



---
#### Method Validator.CheckForInvalidCommands(System.Char[],BrainFucker.Validator.Mode)

 Checks to see if the program has any non command chars. 

|Name | Description |
|-----|------|
|commands: |The brain fuck program to validate.|
|validatorMode: |Which validation mode to use. Defaults to strict.|
**Returns**: Returns true if program is valid, if not false



---
## Type Commands

 Used to look up brain fuck commands by there name. 



---
#### Field Commands.NEXT

 Increments the dataPointer. 



---
#### Field Commands.PREV

 Decrements the dataPointer. 



---
#### Field Commands.INC

 Increments the value in memory at the current dataPointer. 



---
#### Field Commands.DEC

 Decrements the value in memory at the current dataPointer. 



---
#### Field Commands.OUT

 Outputs the value in memory at the current dataPointer. 



---
#### Field Commands.IN

 Inputs a value and places its value in memory at the current dataPointer. 



---
#### Field Commands.BL

 Begins a loop. 



---
#### Field Commands.BR

 Ends a loop. 



---
#### Method Commands.IsCommand(System.Char)

 Checks weather a char is a valid command. 

|Name | Description |
|-----|------|
|command: |The possible command|
**Returns**: A boolean indicating weather the input was a valid command.



---
#### Method Commands.StripNonCommands(System.Char[])

 Strips all non command characters from a program. 

|Name | Description |
|-----|------|
|program: |THe program to be sanitized.|
**Returns**: The sanitized version of the program.



---


