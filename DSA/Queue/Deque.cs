using System.Collections;

namespace DSA.Queue;

using System;

public class Deque<T>
{
    private T[] array;
    private int front;
    private int rear;
    private int capacity;
    private int size;

    public Deque(int capacity)
    {
        this.capacity = capacity;
        array = new T[capacity];
        front = rear = -1;
        size = 0;
    }

    public int Count
    {
        get { return size; }
    }

    public bool IsEmpty
    {
        get { return size == 0; }
    }

    public bool IsFull
    {
        get { return size == capacity; }
    }

    public void EnqueueFront(T item)
    {
        if (IsFull)
        {
            throw new InvalidOperationException("Deque is full.");
        }

        if (front == -1)
        {
            front = rear = 0;
        }
        else if (front == 0)
        {
            front = capacity - 1;
        }
        else
        {
            front--;
        }

        array[front] = item;
        size++;
    }

    public void EnqueueRear(T item)
    {
        if (IsFull)
        {
            throw new InvalidOperationException("Deque is full.");
        }

        if (front == -1)
        {
            front = rear = 0;
        }
        else if (rear == capacity - 1)
        {
            rear = 0;
        }
        else
        {
            rear++;
        }

        array[rear] = item;
        size++;
    }

    public T DequeueFront()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        T item = array[front];
        if (front == rear)
        {
            front = rear = -1;
        }
        else if (front == capacity - 1)
        {
            front = 0;
        }
        else
        {
            front++;
        }

        size--;
        return item;
    }

    public T DequeueRear()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        T item = array[rear];
        if (front == rear)
        {
            front = rear = -1;
        }
        else if (rear == 0)
        {
            rear = capacity - 1;
        }
        else
        {
            rear--;
        }

        size--;
        return item;
    }

    public T PeekFront()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        return array[front];
    }

    public T PeekRear()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        return array[rear];
    }

    public void Clear()
    {
        front = rear = -1;
        size = 0;
    }
    public IEnumerator<T> GetEnumerator()
    {
        return new DequeEnumerator(this);
    }



    private class DequeEnumerator : IEnumerator<T>
    {
        private Deque<T> deque;
        private int currentIndex;

        public DequeEnumerator(Deque<T> deque)
        {
            this.deque = deque;
            currentIndex = -1;
        }

        public T Current
        {
            get { return deque.array[currentIndex]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            currentIndex++;
            return currentIndex < deque.size;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        public void Dispose()
        {
        }
    }
    public static void PrintDeque(Deque<int> deque)
    {
        foreach (int item in deque)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public static void Apply()
    {
        Deque<int> deque = new Deque<int>(5);

        // Enqueue elements at the rear
        deque.EnqueueRear(1);
        deque.EnqueueRear(2);
        deque.EnqueueRear(3);

        // Enqueue elements at the front
        deque.EnqueueFront(0);
        deque.EnqueueFront(-1);

        Console.WriteLine("Deque elements:");
        PrintDeque(deque);

        // Dequeue elements from the front and rear
        int frontItem = deque.DequeueFront();
        int rearItem = deque.DequeueRear();

        Console.WriteLine($"Dequeued front item: {frontItem}");
        Console.WriteLine($"Dequeued rear item: {rearItem}");

        Console.WriteLine("Deque elements after dequeuing:");
        PrintDeque(deque);

        // Peek at front and rear elements
        int frontPeek = deque.PeekFront();
        int rearPeek = deque.PeekRear();

        Console.WriteLine($"Front Peek: {frontPeek}");
        Console.WriteLine($"Rear Peek: {rearPeek}");

        // Clear the deque
        deque.Clear();

        Console.WriteLine("Deque elements after clearing:");
        PrintDeque(deque);
    }
}
