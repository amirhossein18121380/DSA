namespace DSA.Stack.UseCaseSamples;
using System;
using System.Collections.Generic;

public interface ITextEditorExtension
{
    void Apply(UndoAndRedoFunctionality textEditor);
}

public class UndoAndRedoFunctionality
{
    private string text = "";
    private Stack<string> undoStack = new Stack<string>();
    private Stack<string> redoStack = new Stack<string>();

    public string Text
    {
        get { return text; }
        set
        {
            if (text != value)
            {
                undoStack.Push(text);
                text = value;
                redoStack.Clear();
            }
        }
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            redoStack.Push(text);
            text = undoStack.Pop();
        }
        else
        {
            Console.WriteLine("Nothing to undo.");
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(text);
            text = redoStack.Pop();
        }
        else
        {
            Console.WriteLine("Nothing to redo.");
        }
    }

    public void ApplyExtension(ITextEditorExtension extension)
    {
        extension.Apply(this);
    }



    public static void Implement()
    {
        var textEditor = new UndoAndRedoFunctionality();
        bool continueEditing = true;

        Console.WriteLine("Text Editor with Undo/Redo and Dynamic Text Input");
        Console.WriteLine("Type text or enter 'undo' or 'redo' to perform operations.");

        while (continueEditing)
        {
            Console.Write("Current Text: " + textEditor.Text + " > ");
            string input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "undo":
                    textEditor.Undo();
                    break;

                case "redo":
                    textEditor.Redo();
                    break;

                case "exit":
                    continueEditing = false;
                    break;

                default:
                    textEditor.Text += input;
                    break;
            }
        }
    }
}

class UppercaseExtension : ITextEditorExtension
{
    public void Apply(UndoAndRedoFunctionality textEditor)
    {
        textEditor.Text = textEditor.Text.ToUpper();
    }
}

class LowercaseExtension : ITextEditorExtension
{
    public void Apply(UndoAndRedoFunctionality textEditor)
    {
        textEditor.Text = textEditor.Text.ToLower();
    }
}
