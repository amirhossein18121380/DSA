namespace DSA.LinkedList;

public class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}

public class LinkedListSample<T> where T : IComparable<T>
{
    public Node<T> head;

    // Insert at the beginning
    public void InsertAtBeginning(T data)
    {
        Node<T> newNode = new Node<T>(data);
        newNode.Next = head;
        head = newNode;
    }

    // Insert at the end
    public void InsertAtEnd(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }
    }

    // Insert at the middle (after a given element)
    public void InsertAfter(T afterData, T data)
    {
        Node<T> newNode = new Node<T>(data);
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(afterData))
            {
                newNode.Next = current.Next;
                current.Next = newNode;
                break;
            }

            current = current.Next;
        }
    }

    // Delete from the beginning
    public void DeleteFromBeginning()
    {
        if (head != null)
        {
            head = head.Next;
        }
    }

    // Delete from the end
    public void DeleteFromEnd()
    {
        if (head != null)
        {
            if (head.Next == null)
            {
                head = null;
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null && current.Next.Next != null)
                {
                    current = current.Next;
                }

                current.Next = null;
            }
        }
    }

    // Delete from the middle (based on element value)
    public void DeleteNode(T data)
    {
        if (head != null)
        {
            if (head.Data.Equals(data))
            {
                head = head.Next;
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null)
                {
                    if (current.Next.Data.Equals(data))
                    {
                        current.Next = current.Next.Next;
                        break;
                    }

                    current = current.Next;
                }
            }
        }
    }

    // Search for an element
    public bool Contains(T data)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    // Traverse the linked list and print its elements
    public void Traverse()
    {
        Node<T> current = head;
        while (current != null)
        {
            Console.Write($"{current.Data} -> ");
            current = current.Next;
        }

        Console.WriteLine("null");
    }

    public void Sort()
    {
        head = MergeSort(head);
    }

    // Merge Sort algorithm to sort the elements of a singly linked list 
    private Node<T> MergeSort(Node<T> head)
    {
        if (head == null || head.Next == null)
        {
            return head;
        }

        Node<T> middle = GetMiddle(head);
        Node<T> nextOfMiddle = middle.Next;

        middle.Next = null;

        Node<T> left = MergeSort(head);
        Node<T> right = MergeSort(nextOfMiddle);

        return Merge(left, right);
    }

    private Node<T> GetMiddle(Node<T> head)
    {
        if (head == null)
        {
            return null;
        }

        Node<T> slowPtr = head;
        Node<T> fastPtr = head;

        while (fastPtr.Next != null && fastPtr.Next.Next != null)
        {
            slowPtr = slowPtr.Next;
            fastPtr = fastPtr.Next.Next;
        }

        return slowPtr;
    }

    private Node<T> Merge(Node<T> left, Node<T> right)
    {
        Node<T> result = null;

        if (left == null)
        {
            return right;
        }

        if (right == null)
        {
            return left;
        }

        if (left.Data.CompareTo(right.Data) <= 0)
        {
            result = left;
            result.Next = Merge(left.Next, right);
        }
        else
        {
            result = right;
            result.Next = Merge(left, right.Next);
        }

        return result;
    }

    public void Sort2()
    {
        if (head == null || head.Next == null)
        {
            return;
        }

        Node<T> sorted = null; // Sorted part of the list

        while (head != null)
        {
            Node<T> current = head; // Current node to insert
            head = head.Next; // Move head to the next node

            if (sorted == null || current.Data.CompareTo(sorted.Data) <= 0)
            {
                // Insert at the beginning of the sorted list
                current.Next = sorted;
                sorted = current;
            }
            else
            {
                // Traverse the sorted list to find the correct position
                Node<T> temp = sorted;
                while (temp.Next != null && current.Data.CompareTo(temp.Next.Data) > 0)
                {
                    temp = temp.Next;
                }
                // Insert after temp
                current.Next = temp.Next;
                temp.Next = current;
            }
        }

        head = sorted; // Update the head to the sorted list
    }
}
public static class LinkedListSampleProgram
{
    public static void Apply()
    {
        LinkedListSample<int> myList = new LinkedListSample<int>();

        myList.InsertAtEnd(1);
        myList.InsertAtEnd(2);
        myList.InsertAtEnd(3);
        myList.InsertAtEnd(4);

        Console.WriteLine("Original Linked List:");
        myList.Traverse();

        myList.InsertAtBeginning(0);
        Console.WriteLine("After Inserting at the Beginning:");
        myList.Traverse();

        myList.InsertAfter(2, 5);
        Console.WriteLine("After Inserting After Element 2:");
        myList.Traverse();

        myList.DeleteFromBeginning();
        Console.WriteLine("After Deleting from the Beginning:");
        myList.Traverse();

        myList.DeleteFromEnd();
        Console.WriteLine("After Deleting from the End:");
        myList.Traverse();

        myList.DeleteNode(2);
        Console.WriteLine("After Deleting Node with Value 2:");
        myList.Traverse();
        myList.Sort();
        Console.WriteLine("After Sorting:");
        myList.Traverse();


        Console.WriteLine("Is 3 in the Linked List? " + myList.Contains(3)); // Should return false

        Console.WriteLine("Is 5 in the Linked List? " + myList.Contains(5)); // Should return true
    }
}