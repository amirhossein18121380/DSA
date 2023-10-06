using static DSA.Stack.UseCaseSamples.FunctionCallManagement;

namespace Test.Stack;
public class FunctionCallManagementTest
{
    [Fact]
    public void ExecuteCommand_PrintCommand()
    {
        // Arrange
        var interpreter = new CommandInterpreter();
        string output = null;
        interpreter.RegisterCommand("print", args =>
        {
            output = string.Join(" ", args);
        });

        // Act
        interpreter.ExecuteCommand("print Hello, world!");

        // Assert
        Assert.Equal("Hello, world!", output);
    }

    [Fact]
    public void ExecuteCommand_SetCommand()
    {
        // Arrange
        var interpreter = new CommandInterpreter();
        string variableName = null;
        string value = null;
        interpreter.RegisterCommand("set", args =>
        {
            variableName = args[0];
            value = args[1];
        });

        // Act
        interpreter.ExecuteCommand("set username Alice");

        // Assert
        Assert.Equal("username", variableName);
        Assert.Equal("Alice", value);
    }

    [Fact]
    public void ExecuteCommand_ListCommand()
    {
        // Arrange
        var interpreter = new CommandInterpreter();
        string[] items = null;
        interpreter.RegisterCommand("list", args =>
        {
            items = args;
        });

        // Act
        interpreter.ExecuteCommand("list item1 item2 item3");

        // Assert
        Assert.Equal(new[] { "item1", "item2", "item3" }, items);
    }

    [Fact]
    public void ExecuteCommand_UnregisteredCommand()
    {
        // Arrange
        var interpreter = new CommandInterpreter();

        // Act and Assert
        Assert.Throws<ArgumentException>(() => interpreter.ExecuteCommand("unknown"));
    }



    [Fact]
    public void UndoLastCommand_UndoCommand()
    {
        // Arrange
        var interpreter = new CommandInterpreter();
        string output = null;
        interpreter.RegisterCommand("print", args =>
        {
            string.Join(" ", args);
        });

        // Act
        interpreter.ExecuteCommand("print Hello, world!");
        interpreter.UndoLastCommand();



        // Verify that attempting to undo again throws an ArgumentException
        var exception = Assert.Throws<ArgumentException>(() => interpreter.UndoLastCommand());
        Assert.Equal("No commands to undo", exception.Message);
    }

    [Fact]
    public void UndoLastCommand_NoCommandsToUndo()
    {
        var interpreter = new CommandInterpreter();

        var exception = Assert.Throws<ArgumentException>(() => interpreter.UndoLastCommand());
        Assert.Equal("No commands to undo", exception.Message);
    }
}
