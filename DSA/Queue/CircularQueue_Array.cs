namespace DSA.Queue;

public class CircularQueueArray<T>
{
    private T[] array;
    private int capacity;
    private int front;
    private int rear;

    public CircularQueueArray(int size)
    {
        capacity = size + 1; // One extra space to differentiate between empty and full
        array = new T[capacity];
        front = rear = 0;
    }

    public bool IsEmpty()
    {
        return front == rear;
    }

    public bool IsFull()
    {
        return (rear + 1) % capacity == front;
    }

    public void Enqueue(T item)
    {
        if (IsFull())
        {
            throw new InvalidOperationException("Queue is full.");
        }

        array[rear] = item;
        rear = (rear + 1) % capacity;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        T item = array[front];
        front = (front + 1) % capacity;
        return item;
    }
}
