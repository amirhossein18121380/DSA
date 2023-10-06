namespace Test.LinkedList;
using DSA.LinkedList;
using System;
using Xunit;

public class LinkedListTests
{
    [Fact]
    public void LinkedList_InsertAtBeginning()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();

        // Act
        list.InsertAtBeginning(1);
        list.InsertAtBeginning(2);

        // Assert
        Assert.Equal("2 -> 1 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_InsertAtEnd()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();

        // Act
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);

        // Assert
        Assert.Equal("1 -> 2 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_InsertAfter()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);

        // Act
        list.InsertAfter(1, 3);

        // Assert
        Assert.Equal("1 -> 3 -> 2 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_DeleteFromBeginning()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);

        // Act
        list.DeleteFromBeginning();

        // Assert
        Assert.Equal("2 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_DeleteFromEnd()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);

        // Act
        list.DeleteFromEnd();

        // Assert
        Assert.Equal("1 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_DeleteNode()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);
        list.InsertAtEnd(3);

        // Act
        list.DeleteNode(2);

        // Assert
        Assert.Equal("1 -> 3 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_Contains()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);
        list.InsertAtEnd(3);

        // Act and Assert
        Assert.True(list.Contains(2));
        Assert.False(list.Contains(4));
    }

    private string LinkedListToString(LinkedListSample<int> list)
    {
        using (var sw = new System.IO.StringWriter())
        {
            Console.SetOut(sw);
            list.Traverse();
            return sw.ToString().Trim();
        }
    }

    [Fact]
    public void LinkedList_AddToFront_AddsToTheFront()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();

        // Act
        list.InsertAtBeginning(3);
        list.InsertAtBeginning(2);
        list.InsertAtBeginning(1);

        // Assert
        Assert.Equal("1 -> 2 -> 3 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_AddToEnd_AddsToTheEnd()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();

        // Act
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);
        list.InsertAtEnd(3);

        // Assert
        Assert.Equal("1 -> 2 -> 3 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_PrintList_EmptyList()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();

        // Act and Assert
        Assert.Equal("null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_PrintList_NonEmptyList()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtBeginning(3);
        list.InsertAtBeginning(2);
        list.InsertAtBeginning(1);

        // Act and Assert
        Assert.Equal("1 -> 2 -> 3 -> null", LinkedListToString(list));
    }

    [Fact]
    public void LinkedList_Sort_EmptyList()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();

        // Act
        list.Sort();

        // Assert
        Assert.Null(list.head);
    }

    [Fact]
    public void LinkedList_Sort_SortedList()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);
        list.InsertAtEnd(3);

        // Act
        list.Sort();

        // Assert
        Assert.Equal(1, list.head.Data);
        Assert.Equal(2, list.head.Next.Data);
        Assert.Equal(3, list.head.Next.Next.Data);
    }

    [Fact]
    public void LinkedList_Sort_ReverseSortedList()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(3);
        list.InsertAtEnd(2);
        list.InsertAtEnd(1);

        // Act
        list.Sort();

        // Assert
        Assert.Equal(1, list.head.Data);
        Assert.Equal(2, list.head.Next.Data);
        Assert.Equal(3, list.head.Next.Next.Data);
    }

    [Fact]
    public void LinkedList_Sort_UnsortedList()
    {
        // Arrange
        LinkedListSample<int> list = new LinkedListSample<int>();
        list.InsertAtEnd(2);
        list.InsertAtEnd(1);
        list.InsertAtEnd(3);

        // Act
        list.Sort();

        // Assert
        Assert.Equal(1, list.head.Data);
        Assert.Equal(2, list.head.Next.Data);
        Assert.Equal(3, list.head.Next.Next.Data);
    }

}
