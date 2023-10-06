namespace DSA.Queue;

using System;
using System.Collections.Generic;

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap;

    public PriorityQueue()
    {
        heap = new List<T>();
    }

    public int Count
    {
        get { return heap.Count; }
    }

    public void Enqueue(T item)
    {
        heap.Add(item);
        HeapifyUp();
    }

    public T Dequeue()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("The priority queue is empty.");
        }

        T root = heap[0];
        int lastIndex = heap.Count - 1;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);
        HeapifyDown();

        return root;
    }

    private void HeapifyUp()
    {
        int currentIndex = heap.Count - 1;

        while (currentIndex > 0)
        {
            int parentIndex = (currentIndex - 1) / 2;

            if (heap[currentIndex].CompareTo(heap[parentIndex]) > 0)
            {
                Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
            }
            else
            {
                break;
            }
        }
    }

    private void HeapifyDown()
    {
        int currentIndex = 0;
        int leftChildIndex, rightChildIndex, largerChildIndex;

        while (true)
        {
            leftChildIndex = 2 * currentIndex + 1;
            rightChildIndex = 2 * currentIndex + 2;
            largerChildIndex = currentIndex;

            if (leftChildIndex < heap.Count && heap[leftChildIndex].CompareTo(heap[largerChildIndex]) > 0)
            {
                largerChildIndex = leftChildIndex;
            }

            if (rightChildIndex < heap.Count && heap[rightChildIndex].CompareTo(heap[largerChildIndex]) > 0)
            {
                largerChildIndex = rightChildIndex;
            }

            if (largerChildIndex != currentIndex)
            {
                Swap(currentIndex, largerChildIndex);
                currentIndex = largerChildIndex;
            }
            else
            {
                break;
            }
        }
    }

    private void Swap(int index1, int index2)
    {
        T temp = heap[index1];
        heap[index1] = heap[index2];
        heap[index2] = temp;
    }

    public void PrintQueue()
    {
        foreach (T item in heap)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public static void Apply()
    {
        PriorityQueue<int> priorityQueue = new PriorityQueue<int>();

        priorityQueue.Enqueue(3);
        priorityQueue.Enqueue(4);
        priorityQueue.Enqueue(9);
        priorityQueue.Enqueue(5);
        priorityQueue.Enqueue(2);

        Console.WriteLine("Max-Heap Priority Queue:");
        priorityQueue.PrintQueue();

        int removedItem = priorityQueue.Dequeue();
        Console.WriteLine($"Removed item: {removedItem}");
        Console.WriteLine("Updated Priority Queue:");
        priorityQueue.PrintQueue();
    }
}