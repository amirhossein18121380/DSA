using DSA.Heap;

namespace Test.Heap;

using Xunit;

public class MinBinaryHeapTests
{
    [Fact]
    public void InsertAndPeek_MinHeap()
    {
        MinBinaryHeap minHeap = new MinBinaryHeap(10);

        minHeap.Insert(4);
        minHeap.Insert(8);
        minHeap.Insert(2);
        minHeap.Insert(6);
        minHeap.Insert(1);

        Assert.Equal(1, minHeap.Peek());
    }

    [Fact]
    public void ExtractMin_MinHeap()
    {
        MinBinaryHeap minHeap = new MinBinaryHeap(10);

        minHeap.Insert(4);
        minHeap.Insert(8);
        minHeap.Insert(2);
        minHeap.Insert(6);
        minHeap.Insert(1);

        Assert.Equal(1, minHeap.ExtractMin());
        Assert.Equal(2, minHeap.ExtractMin());
    }

    [Fact]
    public void Delete_MinHeap()
    {
        MinBinaryHeap minHeap = new MinBinaryHeap(10);

        minHeap.Insert(4);
        minHeap.Insert(8);
        minHeap.Insert(2);
        minHeap.Insert(6);
        minHeap.Insert(1);

        minHeap.Delete(6);
        minHeap.Delete(4);

        Assert.Equal(1, minHeap.ExtractMin());
        Assert.Equal(2, minHeap.ExtractMin());
    }
}

public class MaxBinaryHeapTests
{
    [Fact]
    public void InsertAndPeek_MaxHeap()
    {
        MaxBinaryHeap maxHeap = new MaxBinaryHeap(10);

        maxHeap.Insert(4);
        maxHeap.Insert(8);
        maxHeap.Insert(2);
        maxHeap.Insert(6);
        maxHeap.Insert(1);

        Assert.Equal(8, maxHeap.Peek());
    }

    [Fact]
    public void ExtractMax_MaxHeap()
    {
        MaxBinaryHeap maxHeap = new MaxBinaryHeap(10);

        maxHeap.Insert(4);
        maxHeap.Insert(8);
        maxHeap.Insert(2);
        maxHeap.Insert(6);
        maxHeap.Insert(1);

        Assert.Equal(8, maxHeap.ExtractMax());
        Assert.Equal(6, maxHeap.ExtractMax());
    }

    [Fact]
    public void Delete_MaxHeap()
    {
        MaxBinaryHeap maxHeap = new MaxBinaryHeap(10);

        maxHeap.Insert(4);
        maxHeap.Insert(8);
        maxHeap.Insert(2);
        maxHeap.Insert(6);
        maxHeap.Insert(1);

        maxHeap.Delete(6);
        maxHeap.Delete(8);

        Assert.Equal(4, maxHeap.ExtractMax());
        Assert.Equal(2, maxHeap.ExtractMax());
    }
}
