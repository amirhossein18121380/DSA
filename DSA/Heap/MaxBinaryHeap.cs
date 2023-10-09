namespace DSA.Heap;

using System;

public class MaxBinaryHeap
{
    private int[] heap;
    private int size;
    private int capacity;

    public MaxBinaryHeap(int capacity)
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
        while (currentIndex > 0 && heap[currentIndex] > heap[Parent(currentIndex)])
        {
            Swap(currentIndex, Parent(currentIndex));
            currentIndex = Parent(currentIndex);
        }
    }

    public int ExtractMax()
    {
        if (size == 0)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        int max = heap[0];

        // Replace the root with the last element and then heapify down
        heap[0] = heap[size - 1];
        size--;

        HeapifyDown(0);

        return max;
    }

    private void HeapifyDown(int i)
    {
        int maxIndex = i;
        int left = LeftChild(i);
        int right = RightChild(i);

        if (left < size && heap[left] > heap[maxIndex])
        {
            maxIndex = left;
        }

        if (right < size && heap[right] > heap[maxIndex])
        {
            maxIndex = right;
        }

        if (i != maxIndex)
        {
            Swap(i, maxIndex);
            HeapifyDown(maxIndex);
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
        if (index > 0 && heap[index] > heap[Parent(index)])
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
        while (i > 0 && heap[i] > heap[Parent(i)])
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

public static class MaxHeapProgram
{
    public static void Apply()
    {
        MaxBinaryHeap maxHeap = new MaxBinaryHeap(10);

        maxHeap.Insert(4);
        maxHeap.Insert(8);
        maxHeap.Insert(2);
        maxHeap.Insert(6);
        maxHeap.Insert(1);

        Console.WriteLine("Max Heap:");
        maxHeap.Print(); // Should print: 8 6 2 4 1

        Console.WriteLine("Peek (Max): " + maxHeap.Peek()); // Should print: 8
        Console.WriteLine("Extracting Max: " + maxHeap.ExtractMax()); // Should print: 8
        Console.WriteLine("Max Heap after ExtractMax:");
        maxHeap.Print(); // Should print: 6 4 2 1

        maxHeap.Insert(10);
        Console.WriteLine("Max Heap after Inserting 10:");
        maxHeap.Print(); // Should print: 10 6 2 4 1

        maxHeap.Delete(6);
        Console.WriteLine("Max Heap after Deleting 6:");
        maxHeap.Print(); // Should print: 10 4 2 1
    }
}
