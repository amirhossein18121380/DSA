namespace DSA.Queue;

public class CircularQueueList<T>
{
    private List<T> list;
    private int capacity;
    private int front;
    private int rear;

    public CircularQueueList(int size)
    {
        capacity = size + 1; // One extra space to differentiate between empty and full
        list = new List<T>(capacity);
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

        list[rear] = item;
        rear = (rear + 1) % capacity;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        T item = list[front];
        front = (front + 1) % capacity;
        return item;
    }
}
