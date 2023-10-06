namespace DSA.LinkedList;

using System;

public class DoublyNode<T>
{
    public T Data { get; }
    public DoublyNode<T> Next { get; set; }
    public DoublyNode<T> Previous { get; set; }

    public DoublyNode(T data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }
}

class DoublyLinkedList<T>
{
    private DoublyNode<T> head;
    private DoublyNode<T> tail;

    public DoublyLinkedList()
    {
        head = null;
        tail = null;
    }

    public void Add(T data)
    {
        DoublyNode<T> newNode = new DoublyNode<T>(data);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Previous = tail;
            tail.Next = newNode;
            tail = newNode;
        }
    }

    public void Remove(T data)
    {
        DoublyNode<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                if (current == head)
                {
                    head = current.Next;
                    if (head != null)
                    {
                        head.Previous = null;
                    }
                }
                else if (current == tail)
                {
                    tail = current.Previous;
                    tail.Next = null;
                }
                else
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                }
                return;
            }
            current = current.Next;
        }
    }

    public void Print()
    {
        DoublyNode<T> current = head;
        while (current != null)
        {
            Console.Write(current.Data + " <-> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }
}

public static class DoublyNodeProgram
{
    public static void Apply()
    {
        DoublyLinkedList<int> myList = new DoublyLinkedList<int>();

        myList.Add(1);
        myList.Add(2);
        myList.Add(3);

        Console.WriteLine("Original Doubly Linked List:");
        myList.Print();

        myList.Remove(2);
        Console.WriteLine("After Removing 2:");
        myList.Print();
    }
}
