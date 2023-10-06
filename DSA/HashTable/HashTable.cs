namespace DSA.HashTable;

using System;
using System.Collections.Generic;

public class HashTable<TKey, TValue>
{
    private const int DefaultCapacity = 10;
    private const double LoadFactorThreshold = 0.75;
    //By adjusting the LoadFactorThreshold, you can control the trade-off between memory usage and performance in the hash table.
    //A lower threshold will result in more frequent resizing but lower memory usage,
    //while a higher threshold will result in less frequent resizing but potentially higher memory usage.

    private LinkedList<KeyValuePair<TKey, TValue>>[] table;
    private int count;
    public HashTable()
    {
        table = new LinkedList<KeyValuePair<TKey, TValue>>[DefaultCapacity];
        count = 0;
    }
    private int GetIndex(TKey key)
    {
        int hashCode = key.GetHashCode();
        int index = (hashCode ^ (hashCode >> 16)) % table.Length;
        return index;
    }
    public void Add(TKey key, TValue value)
    {
        EnsureCapacity();
        int index = GetIndex(key);
        var bucket = table[index];

        if (bucket == null)
        {
            bucket = new LinkedList<KeyValuePair<TKey, TValue>>();
            table[index] = bucket;
        }

        var existingPair = bucket.FirstOrDefault(p => !p.Key.Equals(key));
        if (!string.IsNullOrEmpty((string?)(object)existingPair.Key))
        {
            bucket.Remove(existingPair);
        }

        bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value)); // Add the new key-value pair
        count++;
    }
    private void EnsureCapacity()
    {
        if ((double)count / table.Length >= LoadFactorThreshold)
        {
            ResizeTable();
        }
    }
    private void ResizeTable()
    {
        int newCapacity = table.Length * 2;
        var newTable = new LinkedList<KeyValuePair<TKey, TValue>>[newCapacity];

        foreach (var pairs in table)
        {
            if (pairs != null)
            {
                foreach (var pair in pairs)
                {
                    int newIndex = GetIndex(pair.Key);
                    var newBucket = newTable[newIndex];

                    if (newBucket == null)
                    {
                        newBucket = new LinkedList<KeyValuePair<TKey, TValue>>();
                        newTable[newIndex] = newBucket;
                    }

                    var existingPair = newBucket.FirstOrDefault(p => p.Key.Equals(pair.Key));

                    if (existingPair.Key == null)
                    {
                        newBucket.AddLast(new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
                    }
                    else
                    {
                        newBucket.Remove(existingPair);
                        newBucket.AddLast(new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
                    }
                }
            }
        }

        table = newTable;
    }
    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetIndex(key);
        if (table[index] != null)
        {
            var pairs = table[index];
            foreach (var pair in pairs)
            {
                if (pair.Key.Equals(key))
                {
                    value = pair.Value;
                    return true;
                }
            }
        }
        value = default(TValue);
        return false;
    }
    public bool HasConflicts()
    {
        foreach (var bucket in table)
        {
            if (bucket != null && bucket.Count > 1)
            {
                return true;
            }
        }
        return false;
    }
}

public static class HashTableProgram
{
    public static void Apply()
    {
        HashTable<string, string> hashTable = new HashTable<string, string>();

        // Add key-value pairs
        hashTable.Add("John", "john@example.com");
        hashTable.Add("Jane", "jane@example.com");
        hashTable.Add("Mike", "mike@example.com");

        // Attempt to retrieve values by key
        string johnEmail;
        if (hashTable.TryGetValue("John", out johnEmail))
        {
            Console.WriteLine("John's email: " + johnEmail);
        }
        else
        {
            Console.WriteLine("John's email not found.");
        }

        string janeEmail;
        if (hashTable.TryGetValue("Jane", out janeEmail))
        {
            Console.WriteLine("Jane's email: " + janeEmail);
        }
        else
        {
            Console.WriteLine("Jane's email not found.");
        }

        string mikeEmail;
        if (hashTable.TryGetValue("Mike", out mikeEmail))
        {
            Console.WriteLine("Mike's email: " + mikeEmail);
        }
        else
        {
            Console.WriteLine("Mike's email not found.");
        }

        // Attempt to retrieve a non-existent key
        string alexEmail;
        if (hashTable.TryGetValue("Alex", out alexEmail))
        {
            Console.WriteLine("Alex's email: " + alexEmail);
        }
        else
        {
            Console.WriteLine("Alex's email not found.");
        }

        bool hasConflicts = hashTable.HasConflicts();
        Console.WriteLine("Has conflicts: " + hasConflicts);
    }
}

