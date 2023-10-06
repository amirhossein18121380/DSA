namespace DSA.Stack;

public class ArrayStack<T>
{
    private T[] stackArray;
    private int top;
    private int capacity;

    public ArrayStack(int size)
    {
        capacity = size;
        stackArray = new T[capacity];
        top = -1;
    }

    public void Push(T item)
    {
        if (top == capacity - 1)
        {
            Console.WriteLine("Stack overflow. Cannot push more elements.");
            return;
        }

        stackArray[++top] = item;
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack is empty. Cannot pop.");
            return default(T);
        }

        return stackArray[top--];
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack is empty. Cannot peek.");
            return default(T);
        }

        return stackArray[top];
    }

    public bool IsEmpty()
    {
        return top == -1;
    }

    public int Size()
    {
        return top + 1;
    }
}

public class Array_Based_Stack_Program
{
    public static void Apply()
    {
        ArrayStack<int> stack = new ArrayStack<int>(5);

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
