namespace DSA.Heap;

using System;
using System.Collections.Generic;

public class FibonacciHeapNode<T>
{
    public T Key { get; set; }
    public int Degree { get; set; }
    public bool IsMarked { get; set; }
    public FibonacciHeapNode<T> Parent { get; set; }
    public FibonacciHeapNode<T> Child { get; set; }
    public FibonacciHeapNode<T> Next { get; set; }
    public FibonacciHeapNode<T> Prev { get; set; }
}

public class FibonacciHeap<T>
{
    private FibonacciHeapNode<T> minNode;
    private int size;

    public FibonacciHeap()
    {
        minNode = null;
        size = 0;
    }

    public void Insert(T key)
    {
        // Create a new node and insert it into the root list
        FibonacciHeapNode<T> newNode = new FibonacciHeapNode<T> { Key = key };
        if (minNode == null)
        {
            minNode = newNode;
        }
        else
        {
            newNode.Next = minNode;
            newNode.Prev = minNode.Prev;
            minNode.Prev = newNode;
            newNode.Prev.Next = newNode;
            if (Comparer<T>.Default.Compare(newNode.Key, minNode.Key) < 0)
            {
                minNode = newNode;
            }
        }
        size++;
    }

    public T ExtractMin()
    {
        if (minNode == null)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        FibonacciHeapNode<T> min = minNode;
        if (minNode.Next == minNode)
        {
            minNode = null;
        }
        else
        {
            minNode.Prev.Next = minNode.Next;
            minNode.Next.Prev = minNode.Prev;
            minNode = minNode.Next;
        }

        // TODO: Perform consolidation and potentially remove marked nodes.

        size--;
        return min.Key;
    }

    // Other operations like decreaseKey, delete, and union need to be implemented.
}

