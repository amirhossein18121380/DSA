namespace Test.LinkedList;

using DSA.LinkedList.UseCaseSamples;
using Xunit;

public class UndoRedoManagerTests
{
    [Fact]
    public void UndoRedoManager_AddStateAndGetState()
    {
        // Arrange
        UndoRedoManager undoRedoManager = new UndoRedoManager();

        // Act
        undoRedoManager.AddState("State 1");
        undoRedoManager.AddState("State 2");

        // Assert
        Assert.Equal("State 2", undoRedoManager.GetCurrentState());
    }

    [Fact]
    public void UndoRedoManager_Undo()
    {
        // Arrange
        UndoRedoManager undoRedoManager = new UndoRedoManager();
        undoRedoManager.AddState("State 1");
        undoRedoManager.AddState("State 2");

        // Act
        undoRedoManager.Undo();

        // Assert
        Assert.Equal("State 1", undoRedoManager.GetCurrentState());
    }

    [Fact]
    public void UndoRedoManager_Redo()
    {
        // Arrange
        UndoRedoManager undoRedoManager = new UndoRedoManager();
        undoRedoManager.AddState("State 1");
        undoRedoManager.AddState("State 2");
        undoRedoManager.Undo();

        // Act
        undoRedoManager.Redo();

        // Assert
        Assert.Equal("State 2", undoRedoManager.GetCurrentState());
    }

    [Fact]
    public void UndoRedoManager_CannotUndoAfterAddingFirstState()
    {
        // Arrange
        UndoRedoManager undoRedoManager = new UndoRedoManager();

        // Act and Assert
        Assert.False(undoRedoManager.CanUndo());
    }

    [Fact]
    public void UndoRedoManager_CannotRedoAfterAddingFirstState()
    {
        // Arrange
        UndoRedoManager undoRedoManager = new UndoRedoManager();

        // Act and Assert
        Assert.False(undoRedoManager.CanRedo());
    }

    [Fact]
    public void UndoRedoManager_CanUndoAfterAddingState()
    {
        // Arrange
        UndoRedoManager undoRedoManager = new UndoRedoManager();
        undoRedoManager.AddState("State 1");

        // Act and Assert
        Assert.True(!undoRedoManager.CanUndo());
    }

    [Fact]
    public void UndoRedoManager_CannotRedoAfterAddingState()
    {
        // Arrange
        UndoRedoManager undoRedoManager = new UndoRedoManager();
        undoRedoManager.AddState("State 1");

        // Act and Assert
        Assert.False(undoRedoManager.CanRedo());
    }
}
