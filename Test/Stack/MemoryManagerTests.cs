using DSA.Stack.UseCaseSamples;

namespace Test.Stack;

public class MemoryManagerTests
{
    [Fact]
    public void AllocateMemory_Success()
    {
        // Arrange
        int memorySize = 10;
        SampleMemoryManager memoryManager = new SampleMemoryManager(memorySize);

        // Act
        int var1Address = memoryManager.AllocateMemory("var1", 3);

        // Assert
        Assert.True(var1Address != -1);
    }

    [Fact]
    public void AllocateMemory_Failure()
    {
        // Arrange
        int memorySize = 2; // Small memory size to force allocation failure
        SampleMemoryManager memoryManager = new SampleMemoryManager(memorySize);

        // Act
        int var1Address = memoryManager.AllocateMemory("var1", 3);

        // Assert
        Assert.Equal(-1, var1Address);
    }

    [Fact]
    public void DeallocateMemory_Success()
    {
        // Arrange
        int memorySize = 10;
        SampleMemoryManager memoryManager = new SampleMemoryManager(memorySize);
        int var1Address = memoryManager.AllocateMemory("var1", 3);

        // Act
        memoryManager.DeallocateMemory("var1");

        // Assert
        Assert.DoesNotContain("var1", memoryManager.variableMap.Keys);
    }

    [Fact]
    public void DeallocateMemory_NotFound()
    {
        // Arrange
        int memorySize = 10;
        SampleMemoryManager memoryManager = new SampleMemoryManager(memorySize);

        // Act
        memoryManager.DeallocateMemory("var1");

        // Assert (variable not found)
        Assert.Empty(memoryManager.variableMap);
    }
}