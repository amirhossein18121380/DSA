namespace DSA.Stack.UseCaseSamples;
using System;
using System.Collections.Generic;

public class TaskManagementSystem
{
    class Task
    {
        public int ID { get; set; }
        public Stack<string> ContextStack { get; set; }
    }

    private static Stack<Task> taskStack = new Stack<Task>();
    private static int currentTaskID = 1;

    static void CreateTask()
    {
        Task newTask = new Task
        {
            ID = currentTaskID++,
            ContextStack = new Stack<string>()
        };

        taskStack.Push(newTask);

        Console.WriteLine($"Task {newTask.ID} created.");
    }

    static void SwitchTask()
    {
        if (taskStack.Count > 1)
        {
            Task currentTask = taskStack.Pop();
            Task nextTask = taskStack.Peek();

            Console.WriteLine($"Switching from Task {currentTask.ID} to Task {nextTask.ID}");
        }
        else
        {
            Console.WriteLine("Cannot switch task. There's only one task running.");
        }
    }

    static void ExecuteTask()
    {
        Task currentTask = taskStack.Peek();
        currentTask.ContextStack.Push($"Context information for Task {currentTask.ID}");

        Console.WriteLine($"Executing Task {currentTask.ID}");
    }

    public static void Apply()
    {
        while (true)
        {
            Console.WriteLine("\nTask Management System Menu:");
            Console.WriteLine("1. Create Task");
            Console.WriteLine("2. Execute Task");
            Console.WriteLine("3. Switch Task");
            Console.WriteLine("4. Quit");

            Console.Write("Enter your choice (1/2/3/4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateTask();
                    break;
                case "2":
                    ExecuteTask();
                    break;
                case "3":
                    SwitchTask();
                    break;
                case "4":
                    Console.WriteLine("Exiting the Task Management System.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
