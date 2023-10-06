namespace DSA.Stack.UseCaseSamples;

using System;
using System.Collections.Generic;

public class SampleMemoryManager
{
    public readonly List<MemoryBlock> memoryBlocks;
    public readonly Dictionary<string, MemoryBlock> variableMap;

    public SampleMemoryManager(int memorySize)
    {
        memoryBlocks = new List<MemoryBlock>
        {
            new MemoryBlock(0, memorySize - 1)
        };
        variableMap = new Dictionary<string, MemoryBlock>();
    }

    public int AllocateMemory(string variableName, int size)
    {
        if (variableMap.ContainsKey(variableName))
        {
            Console.WriteLine($"Variable '{variableName}' already exists.");
            return -1;
        }

        MemoryBlock allocatedBlock = FindAvailableMemory(size);

        if (allocatedBlock != null)
        {
            variableMap.Add(variableName, allocatedBlock);
            Console.WriteLine($"Allocated memory for '{variableName}' at address {allocatedBlock.StartAddress}");
            ConsolidateFreeMemory();
            PrintMemoryStatus();
            return allocatedBlock.StartAddress;
        }
        else
        {
            Console.WriteLine($"Memory allocation failed for '{variableName}'. Not enough memory.");
            return -1;
        }
    }

    private MemoryBlock FindAvailableMemory(int size)
    {
        foreach (MemoryBlock block in memoryBlocks)
        {
            if (block.Size >= size)
            {
                if (block.Size > size)
                {
                    MemoryBlock newBlock = new MemoryBlock(block.StartAddress + size, block.EndAddress);
                    memoryBlocks.Add(newBlock);
                }
                memoryBlocks.Remove(block);
                return new MemoryBlock(block.StartAddress, block.StartAddress + size - 1);
            }
        }
        return null; // Not enough memory available
    }

    public void DeallocateMemory(string variableName)
    {
        if (variableMap.TryGetValue(variableName, out MemoryBlock allocatedBlock))
        {
            variableMap.Remove(variableName);
            memoryBlocks.Add(allocatedBlock);
            memoryBlocks.Sort((x, y) => x.StartAddress.CompareTo(y.StartAddress));
            Console.WriteLine($"Deallocated memory for '{variableName}'");
            ConsolidateFreeMemory();
            PrintMemoryStatus();
        }
        else
        {
            Console.WriteLine($"Variable '{variableName}' not found.");
        }
    }

    private void ConsolidateFreeMemory()
    {
        memoryBlocks.Sort((x, y) => x.StartAddress.CompareTo(y.StartAddress));

        for (int i = 0; i < memoryBlocks.Count - 1;)
        {
            MemoryBlock current = memoryBlocks[i];
            MemoryBlock next = memoryBlocks[i + 1];

            if (current.EndAddress + 1 == next.StartAddress)
            {
                current.EndAddress = next.EndAddress;
                memoryBlocks.RemoveAt(i + 1);
            }
            else
            {
                i++;
            }
        }
    }

    public void PrintMemoryStatus()
    {
        Console.WriteLine("Memory Status:");

        if (memoryBlocks.Count == 0)
        {
            Console.WriteLine("All memory is allocated.");
            return;
        }

        foreach (MemoryBlock block in memoryBlocks)
        {
            Console.WriteLine($"Free: [{block.StartAddress}-{block.EndAddress}] ({block.Size} bytes)");
        }
    }

    public static void Apply()
    {
        int memorySize = 10;
        SampleMemoryManager memoryManager = new SampleMemoryManager(memorySize);

        int var1Address = memoryManager.AllocateMemory("var1", 3);
        if (var1Address != -1)
        {
            Console.WriteLine($"Allocated memory for 'var1' at address {var1Address}");
        }

        int var2Address = memoryManager.AllocateMemory("var2", 2);
        if (var2Address != -1)
        {
            Console.WriteLine($"Allocated memory for 'var2' at address {var2Address}");
        }

        memoryManager.DeallocateMemory("var1");
        Console.WriteLine("Deallocated memory for 'var1'");

        int var3Address = memoryManager.AllocateMemory("var3", 4);
        if (var3Address != -1)
        {
            Console.WriteLine($"Allocated memory for 'var3' at address {var3Address}");
        }

        int var4Address = memoryManager.AllocateMemory("var4", 2);
        if (var4Address != -1)
        {
            Console.WriteLine($"Allocated memory for 'var4' at address {var4Address}");
        }
        //if i give var with 2 bytes doesn't work but two of 1 bytes like below it works. why?
        int var5Address = memoryManager.AllocateMemory("var5", 1);
        int var6Address = memoryManager.AllocateMemory("var6", 1);

        int var7Address = memoryManager.AllocateMemory("var7", 1);
    }
}
public class MemoryBlock
{
    public int StartAddress { get; set; }
    public int EndAddress { get; set; }
    public int Size => EndAddress - StartAddress + 1;

    public MemoryBlock(int startAddress, int endAddress)
    {
        StartAddress = startAddress;
        EndAddress = endAddress;
    }
}