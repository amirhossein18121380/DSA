namespace DSA.Heap;

using System;

public class MinBinaryHeap
{
    private int[] heap;
    private int size;
    private int capacity;

    public MinBinaryHeap(int capacity)
    {
        this.capacity = capacity;
        this.size = 0;
        this.heap = new int[capacity];
    }

    private int Parent(int i)
    {
        return (i - 1) / 2;
    }

    private int LeftChild(int i)
    {
        return 2 * i + 1;
    }

    private int RightChild(int i)
    {
        return 2 * i + 2;
    }

    private bool HasLeftChild(int i)
    {
        return LeftChild(i) < size;
    }

    private bool HasRightChild(int i)
    {
        return RightChild(i) < size;
    }

    private void Swap(int i, int j)
    {
        int temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    public void Insert(int item)
    {
        if (size == capacity)
        {
            Console.WriteLine("Heap is full. Cannot insert.");
            return;
        }

        size++;
        int currentIndex = size - 1;
        heap[currentIndex] = item;

        // Maintain the heap property by bubbling up the newly inserted element
        while (currentIndex > 0 && heap[currentIndex] < heap[Parent(currentIndex)])
        {
            Swap(currentIndex, Parent(currentIndex));
            currentIndex = Parent(currentIndex);
        }
    }

    public int ExtractMin()
    {
        if (size == 0)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        int min = heap[0];

        // Replace the root with the last element and then heapify down
        heap[0] = heap[size - 1];
        size--;

        HeapifyDown(0);

        return min;
    }

    private void HeapifyDown(int i)
    {
        int minIndex = i;
        int left = LeftChild(i);
        int right = RightChild(i);

        if (left < size && heap[left] < heap[minIndex])
        {
            minIndex = left;
        }

        if (right < size && heap[right] < heap[minIndex])
        {
            minIndex = right;
        }

        if (i != minIndex)
        {
            Swap(i, minIndex);
            HeapifyDown(minIndex);
        }
    }

    public int Peek()
    {
        if (size == 0)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        return heap[0];
    }

    public void Delete(int value)
    {
        int index = Array.IndexOf(heap, value);
        if (index == -1)
        {
            Console.WriteLine("Element not found in the heap.");
            return;
        }

        // Replace the element to be deleted with the last element
        heap[index] = heap[size - 1];
        size--;

        // Determine whether to heapify up or down based on the new element's value
        if (index > 0 && heap[index] < heap[Parent(index)])
        {
            HeapifyUp(index);
        }
        else
        {
            HeapifyDown(index);
        }
    }

    private void HeapifyUp(int i)
    {
        while (i > 0 && heap[i] < heap[Parent(i)])
        {
            Swap(i, Parent(i));
            i = Parent(i);
        }
    }

    public void Print()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write(heap[i] + " ");
        }
        Console.WriteLine();
    }
}

public static class MinHeapProgram
{
    public static void Apply()
    {
        MinBinaryHeap minHeap = new MinBinaryHeap(10);

        minHeap.Insert(4);
        minHeap.Insert(8);
        minHeap.Insert(2);
        minHeap.Insert(6);
        minHeap.Insert(1);

        Console.WriteLine("Min Heap:");
        minHeap.Print(); // Should print: 1 4 2 8 6

        Console.WriteLine("Peek (Min): " + minHeap.Peek()); // Should print: 1
        Console.WriteLine("Extracting Min: " + minHeap.ExtractMin()); // Should print: 1
        Console.WriteLine("Min Heap after ExtractMin:");
        minHeap.Print(); // Should print: 2 4 6 8

        minHeap.Insert(0);
        Console.WriteLine("Min Heap after Inserting 0:");
        minHeap.Print(); // Should print: 0 2 6 8 4

        minHeap.Delete(6);
        Console.WriteLine("Min Heap after Deleting 6:");
        minHeap.Print(); // Should print: 0 2 4 8
    }
}
