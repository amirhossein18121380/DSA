namespace DSA.Queue;

using System;
using System.Collections.Generic;

public class CircularQueue<T>
{
    private Queue<T> queue;
    private int capacity;

    public CircularQueue(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than 0");

        this.capacity = capacity;
        this.queue = new Queue<T>(capacity);
    }

    public int Count => queue.Count;

    public int Capacity => capacity;

    public void Enqueue(T item)
    {
        if (queue.Count == capacity)
        {
            // Dequeue the oldest item to make space for the new item
            queue.Dequeue();
        }
        queue.Enqueue(item);
    }

    public T Dequeue()
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return queue.Dequeue();
    }

    public T Peek()
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return queue.Peek();
    }

    public bool IsEmpty()
    {
        return queue.Count == 0;
    }

    public void Clear()
    {
        queue.Clear();
    }

    public void Display()
    {
        foreach (var item in queue)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public bool Contains(T item)
    {
        return queue.Contains(item);
    }

    public T[] ToArray()
    {
        return queue.ToArray();
    }

    public void CopyTo(T[] array, int index)
    {
        queue.CopyTo(array, index);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return queue.GetEnumerator();
    }

    public void Reverse()
    {
        var reversedList = new List<T>(queue);
        reversedList.Reverse();
        queue = new Queue<T>(reversedList);
    }

    public void Resize(int newCapacity)
    {
        if (newCapacity <= 0)
            throw new ArgumentException("New capacity must be greater than 0");

        if (newCapacity < Count)
            throw new ArgumentException("New capacity is smaller than the current number of elements in the queue");

        var currentItems = queue.ToArray();
        queue = new Queue<T>(newCapacity);
        foreach (var item in currentItems)
        {
            queue.Enqueue(item);
        }

        capacity = newCapacity;
    }

    public static void Apply()
    {
        CircularQueue<int> circularQueue = new CircularQueue<int>(5);

        circularQueue.Enqueue(1);
        circularQueue.Enqueue(2);
        circularQueue.Enqueue(3);
        circularQueue.Enqueue(4);
        circularQueue.Enqueue(5);

        Console.WriteLine("Initial Queue:");
        circularQueue.Display(); // Output: 1 2 3 4 5

        circularQueue.Enqueue(6); // Replaces 1 with 6
        circularQueue.Enqueue(7); // Replaces 2 with 7

        Console.WriteLine("Modified Queue:");
        circularQueue.Display(); // Output: 3 4 5 6 7

        int dequeuedItem = circularQueue.Dequeue();
        Console.WriteLine($"Dequeued Item: {dequeuedItem}"); // Output: Dequeued Item: 3

        Console.WriteLine("Queue after Dequeue:");
        circularQueue.Display(); // Output: 4 5 6 7

        Console.WriteLine("Queue contains 5: " + circularQueue.Contains(5)); // Output: Queue contains 5: True

        Console.WriteLine("Queue as an array:");
        var array = circularQueue.ToArray();
        foreach (var item in array)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine(); // Output: Queue as an array: 4 5 6 7

        var copyArray = new int[10];
        circularQueue.CopyTo(copyArray, 2);
        Console.WriteLine("Copied array:");
        foreach (var item in copyArray)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine(); // Output: Copied array: 0 0 4 5 6 7 0 0 0 0

        circularQueue.Reverse();
        Console.WriteLine("Reversed Queue:");
        circularQueue.Display(); // Output: Reversed Queue: 7 6 5 4

        circularQueue.Resize(10);
        Console.WriteLine("Resized Queue:");
        circularQueue.Display(); // Output: Resized Queue: 7 6 5 4

        circularQueue.Enqueue(8);
        circularQueue.Enqueue(9);
        circularQueue.Enqueue(10);
        Console.WriteLine("Enqueued new items:");
        circularQueue.Display(); // Output: Enqueued new items: 7 6 5 4 8 9 10
    }
}
