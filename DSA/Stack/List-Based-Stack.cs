namespace DSA.Stack;
using System;
using System.Collections.Generic;

public class ListStack<T>
{
    private List<T> stackList;

    public ListStack()
    {
        stackList = new List<T>();
    }

    public void Push(T item)
    {
        stackList.Add(item);
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack is empty. Cannot pop.");
            return default(T);
        }
        T item = stackList[stackList.Count - 1];
        stackList.RemoveAt(stackList.Count - 1);
        return item;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack is empty. Cannot peek.");
            return default(T);
        }
        return stackList[stackList.Count - 1];
    }

    public bool IsEmpty()
    {
        return stackList.Count == 0;
    }

    public int Size()
    {
        return stackList.Count;
    }
}

public class List_Based_Stack_Program
{
    public static void Apply()
    {
        ListStack<int> stack = new ListStack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        Console.WriteLine("Stack size: " + stack.Size());
        Console.WriteLine("Peek: " + stack.Peek());
        Console.WriteLine("Pop: " + stack.Pop());
        Console.WriteLine("Stack size: " + stack.Size());

        stack.Push(4);
        stack.Push(5);

        Console.WriteLine("Stack size: " + stack.Size());

        while (!stack.IsEmpty())
        {
            Console.WriteLine("Pop: " + stack.Pop());
        }
    }
}
