namespace DSA.Stack.UseCaseSamples;

public class FunctionCallManagement
{
    public class CommandInterpreter
    {
        private Dictionary<string, Action<string[]>> commandTable;
        private Stack<CommandContext> commandStack;

        public CommandInterpreter()
        {
            commandTable = new Dictionary<string, Action<string[]>>();
            commandStack = new Stack<CommandContext>();
        }

        private class CommandContext
        {
            public string CommandName { get; }
            public List<string> Arguments { get; }

            public CommandContext(string commandName, string[] args)
            {
                CommandName = commandName;
                Arguments = new List<string>(args);
            }
        }

        public void RegisterCommand(string commandName, Action<string[]> commandAction)
        {
            commandTable[commandName] = commandAction;
        }

        public void ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ');
            string commandName = parts[0].ToLower();

            if (commandTable.ContainsKey(commandName))
            {
                string[] args = new string[parts.Length - 1];
                Array.Copy(parts, 1, args, 0, args.Length);

                commandStack.Push(new CommandContext(commandName, args));
                commandTable[commandName].Invoke(args);
            }
            else
            {
                Console.WriteLine($"Command '{commandName}' not found.");
                throw new ArgumentException("Command '{commandName}' not found.");
            }
        }

        public void UndoLastCommand()
        {
            if (commandStack.Count > 0)
            {
                CommandContext lastCommand = commandStack.Pop();
                Console.WriteLine(
                    $"Undoing command: {lastCommand.CommandName} {string.Join(" ", lastCommand.Arguments)}");
            }
            else
            {
                Console.WriteLine("No commands to undo.");
                throw new ArgumentException("No commands to undo");
            }
        }
    }
    public static void Apply()
    {
        var interpreter = new CommandInterpreter();

        // Register some sample commands
        interpreter.RegisterCommand("print", args => { Console.WriteLine(string.Join(" ", args)); });

        interpreter.RegisterCommand("set", args =>
        {
            // Simulate setting a variable or configuration value
            Console.WriteLine($"Setting '{args[0]}' to '{args[1]}'");
        });

        interpreter.RegisterCommand("list", args =>
        {
            Console.WriteLine("List of items:");
            foreach (var item in args)
            {
                Console.WriteLine("- " + item);
            }
        });

        // Execute some sample commands
        interpreter.ExecuteCommand("set username Alice");
        interpreter.ExecuteCommand("set port 8080");
        interpreter.ExecuteCommand("print Hello, world!");
        interpreter.ExecuteCommand("list item1 item2 item3");

        // Undo the last command
        interpreter.UndoLastCommand();
    }
}