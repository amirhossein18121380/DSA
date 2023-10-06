namespace DSA.Queue;

using System;
using System.Collections.Generic;

public class TaskQueue
{
    private readonly Queue<string> tasks = new();

    // Add a new task to the queue
    public void EnqueueTask(string task)
    {
        tasks.Enqueue(task);
        Console.WriteLine($"Task '{task}' added to the queue.");
    }

    // Get and remove the next task from the queue
    public string DequeueTask()
    {
        if (tasks.Count > 0)
        {
            string task = tasks.Dequeue();
            Console.WriteLine($"Task '{task}' dequeued.");
            return task;
        }
        else
        {
            Console.WriteLine("Queue is empty. No tasks to dequeue.");
            return null;
        }
    }

    // Get the next task without removing it from the queue
    public string PeekTask()
    {
        if (tasks.Count > 0)
        {
            string task = tasks.Peek();
            Console.WriteLine($"Next task to dequeue: '{task}'.");
            return task;
        }
        else
        {
            Console.WriteLine("Queue is empty. No tasks to peek.");
            return null;
        }
    }

    // Check if the queue is empty
    public bool IsEmpty()
    {
        return tasks.Count == 0;
    }

    // Get the number of tasks in the queue
    public int Count()
    {
        return tasks.Count;
    }
    public void Clear()
    {
        tasks.Clear();
    }

    public void Display()
    {
        foreach (var item in tasks)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public static void Apply()
    {
        TaskQueue taskQueue = new TaskQueue();

        taskQueue.EnqueueTask("Task 1");
        taskQueue.EnqueueTask("Task 2");
        taskQueue.EnqueueTask("Task 3");

        Console.WriteLine($"Number of tasks in the queue: {taskQueue.Count()}");

        taskQueue.PeekTask();

        string completedTask = taskQueue.DequeueTask();
        Console.WriteLine($"Completed task: '{completedTask}'");

        Console.WriteLine($"Number of tasks in the queue after dequeuing: {taskQueue.Count()}");

        taskQueue.PeekTask();

        while (!taskQueue.IsEmpty())
        {
            taskQueue.DequeueTask();
        }

        Console.WriteLine($"Number of tasks in the queue after dequeuing all: {taskQueue.Count()}");
    }
}