namespace DSA.LinkedList.UseCaseSamples;

using System;
using System.Collections.Generic;

public class State
{
    public string Content { get; set; }
    public State(string content) { Content = content; }
}

public class UndoRedoManager
{
    private LinkedList<State> stateList;
    private LinkedListNode<State> currentState;

    public UndoRedoManager()
    {
        stateList = new LinkedList<State>();
        currentState = null;
    }

    public void AddState(string content)
    {
        // Remove states after the current state when a new state is added
        while (currentState != null && currentState.Next != null)
        {
            stateList.RemoveLast();
        }

        // Add the new state
        State newState = new State(content);
        stateList.AddLast(newState);
        currentState = stateList.Last;
    }

    public string GetCurrentState()
    {
        if (currentState != null)
        {
            return currentState.Value.Content;
        }
        else
        {
            return "No state available.";
        }
    }

    public bool CanUndo()
    {
        return currentState != null && currentState.Previous != null;
    }

    public bool CanRedo()
    {
        return currentState != null && currentState.Next != null;
    }

    public void Undo()
    {
        if (CanUndo())
        {
            currentState = currentState.Previous;
        }
    }

    public void Redo()
    {
        if (CanRedo())
        {
            currentState = currentState.Next;
        }
    }
}

public static class UndoRedoManagerProgram
{
    public static void Apply()
    {
        UndoRedoManager undoRedoManager = new UndoRedoManager();

        while (true)
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1. Add State");
            Console.WriteLine("2. Undo");
            Console.WriteLine("3. Redo");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter new state content: ");
                        string content = Console.ReadLine();
                        undoRedoManager.AddState(content);
                        Console.WriteLine("Current State: " + undoRedoManager.GetCurrentState());
                        break;
                    case 2:
                        if (undoRedoManager.CanUndo())
                        {
                            undoRedoManager.Undo();
                            Console.WriteLine("Undo: " + undoRedoManager.GetCurrentState());
                        }
                        else
                        {
                            Console.WriteLine("No undo available.");
                        }
                        break;
                    case 3:
                        if (undoRedoManager.CanRedo())
                        {
                            undoRedoManager.Redo();
                            Console.WriteLine("Redo: " + undoRedoManager.GetCurrentState());
                        }
                        else
                        {
                            Console.WriteLine("No redo available.");
                        }
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.WriteLine();
        }
    }
}
