namespace DSA.LinkedList;

using System;

public class CircularNode<T>
{
    public T Data { get; }
    public CircularNode<T> Next { get; set; }

    public CircularNode(T data)
    {
        Data = data;
        Next = this; // Initialize Next to itself for circular behavior
    }
}

class CircularLinkedList<T>
{
    private CircularNode<T> head;

    public CircularLinkedList()
    {
        head = null;
    }

    public void Add(T data)
    {
        CircularNode<T> newNode = new CircularNode<T>(data);

        if (head == null)
        {
            head = newNode;
            newNode.Next = head;
        }
        else
        {
            CircularNode<T> current = head;
            while (current.Next != head)
            {
                current = current.Next;
            }
            current.Next = newNode;
            newNode.Next = head;
        }
    }
    public void Remove(T value)
    {
        if (head == null)
        {
            Console.WriteLine("Circular Linked List is empty.");
            return;
        }

        CircularNode<T> current = head;
        CircularNode<T> previous = null;

        // Find the node to be removed
        while (!current.Data.Equals(value))
        {
            if (current.Next == head)
            {
                Console.WriteLine("Node with value " + value + " not found.");
                return;
            }

            previous = current;
            current = current.Next;
        }

        // If the node to be removed is the head node
        if (current == head)
        {
            // Find the last node in the circular linked list
            CircularNode<T> lastNode = head;
            while (lastNode.Next != head)
            {
                lastNode = lastNode.Next;
            }

            // Update the head and last node references
            head = head.Next;
            lastNode.Next = head;
        }
        else
        {
            // Remove the node by updating the previous node's next reference
            previous.Next = current.Next;
        }

        Console.WriteLine("Node with value " + value + " removed.");
    }
    public void Remove2(T data)
    {
        if (head == null)
        {
            return;
        }

        CircularNode<T> current = head;
        CircularNode<T> previous = null;

        do
        {
            if (current.Data.Equals(data))
            {
                if (current == head)
                {
                    head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                return;
            }

            previous = current;
            current = current.Next;
        } while (current != head);
    }

    public void Print()
    {
        if (head == null)
        {
            Console.WriteLine("Circular Linked List is empty.");
            return;
        }

        CircularNode<T> current = head;
        do
        {
            Console.Write(current.Data + " -> ");
            current = current.Next;
        } while (current != head);

        Console.WriteLine(" (head)");
    }
}

public static class CircularLinkedListProgram
{
    public static void Apply()
    {
        CircularLinkedList<int> myList = new CircularLinkedList<int>();

        myList.Add(1);
        myList.Add(2);
        myList.Add(3);

        Console.WriteLine("Original Circular Linked List:");
        myList.Print();

        myList.Remove(2);
        Console.WriteLine("After Removing 2:");
        myList.Print();
    }
}
