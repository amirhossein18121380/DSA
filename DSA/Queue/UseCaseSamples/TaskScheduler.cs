namespace DSA.Queue.UseCaseSamples;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


public class TaskScheduler
{
    private ConcurrentQueue<TaskItem> taskQueue = new ConcurrentQueue<TaskItem>();
    private Dictionary<string, Task> runningTasks = new Dictionary<string, Task>();
    private object lockObject = new object();
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private Task taskRunner;

    public TaskScheduler()
    {
        taskRunner = Task.Factory.StartNew(RunScheduledTasks, TaskCreationOptions.LongRunning);
    }

    public Task<string> ScheduleTaskAsync(Func<Task> taskFunc, Priority priority = Priority.Medium, string taskName = null)
    {
        Task<string> task = new Task<string>(() => ExecuteTask(taskFunc), cancellationTokenSource.Token);
        taskQueue.Enqueue(new TaskItem { Task = task, Priority = priority, TaskName = taskName });
        return task;
    }

    public void Stop()
    {
        cancellationTokenSource.Cancel();
        taskRunner.Wait(); // Wait for the task runner to finish
    }

    private string ExecuteTask(Func<Task> taskFunc)
    {
        try
        {
            taskFunc.Invoke().Wait();
            return "Task completed successfully.";
        }
        catch (Exception ex)
        {
            return $"Task failed with exception: {ex.Message}";
        }
    }

    private void RunScheduledTasks()
    {
        while (!cancellationTokenSource.Token.IsCancellationRequested)
        {
            if (taskQueue.TryDequeue(out var taskItem))
            {
                lock (lockObject)
                {
                    if (runningTasks.Count < 5) // Limit concurrent tasks to 5
                    {
                        runningTasks[taskItem.TaskName] = taskItem.Task;
                        taskItem.Task.Start();
                    }
                    else
                    {
                        // Task limit reached; requeue the task
                        taskQueue.Enqueue(taskItem);
                    }
                }
            }
            else
            {
                // No tasks to execute; wait for a while before checking again
                Thread.Sleep(10);
            }
        }
    }
}

internal class TaskItem
{
    public Task<string> Task { get; set; }
    public Priority Priority { get; set; }
    public string TaskName { get; set; }
}

public enum Priority
{
    Low,
    Medium,
    High
}

public class TaskSchedulerProgram
{
    public static async Task Apply()
    {
        TaskScheduler taskScheduler = new TaskScheduler();

        // Schedule some tasks with priorities and names
        var task1 = taskScheduler.ScheduleTaskAsync(() => SimulateTask("Task 1"), Priority.High, "Task 1");
        var task2 = taskScheduler.ScheduleTaskAsync(() => SimulateTask("Task 2"), Priority.Medium, "Task 2");
        var task3 = taskScheduler.ScheduleTaskAsync(() => SimulateTask("Task 3"), Priority.Low, "Task 3");

        // Wait for tasks to complete
        await Task.WhenAll(task1, task2, task3);

        // Stop the task scheduler when done
        taskScheduler.Stop();

        Console.WriteLine("All tasks are completed.");
    }

    static async Task SimulateTask(string taskName)
    {
        // Simulate a task that takes some time
        Console.WriteLine($"Task '{taskName}' is running.");
        await Task.Delay(2000); // Simulate work
        Console.WriteLine($"Task '{taskName}' is completed.");
    }
}
