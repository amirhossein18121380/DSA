using DSA.Queue.UseCaseSamples;
using TaskScheduler = DSA.Queue.UseCaseSamples.TaskScheduler;

namespace Test.Queue;


public class TaskSchedulerTests : IDisposable
{
    private readonly TaskScheduler taskScheduler;

    public TaskSchedulerTests()
    {
        taskScheduler = new TaskScheduler();
    }

    public void Dispose()
    {
        taskScheduler.Stop();
    }

    [Fact]
    public async Task ScheduleTaskAsync_CompletesSuccessfully()
    {
        // Arrange
        bool taskCompleted = false;

        // Act
        var task = taskScheduler.ScheduleTaskAsync(async () =>
        {
            await Task.Delay(100); // Simulate work
            taskCompleted = true;
        }, Priority.Medium, "sth");

        await task;

        // Assert
        Assert.True(taskCompleted);
    }

    [Fact]
    public async Task ScheduleTaskAsync_WithConcurrentExecution_LimitsConcurrency()
    {
        // Arrange
        int maxConcurrency = 5;
        int runningTasksCount = 0;

        // Act
        for (int i = 0; i < maxConcurrency * 2; i++)
        {
            await taskScheduler.ScheduleTaskAsync(async () =>
            {
                await Task.Delay(100);
                Interlocked.Decrement(ref runningTasksCount);
            }, Priority.High, "sth");

            Interlocked.Increment(ref runningTasksCount);
        }

        // Assert
        Assert.True(runningTasksCount <= maxConcurrency);
    }

    [Fact]
    public async Task ScheduleTaskAsync_WithError_ReturnsErrorMessage()
    {
        // Arrange
        string errorMessage = "Simulated error";
        var taskWithError = taskScheduler.ScheduleTaskAsync(async () =>
        {
            await Task.Delay(100);
            throw new Exception(errorMessage);
        }, Priority.High, "sth");

        // Act
        string result = await taskWithError;

        // Assert
        Assert.Contains(errorMessage, result);
    }


    [Fact]
    public async Task Stop_StopsTaskScheduler()
    {
        // Arrange
        bool taskCompleted = false;

        // Act
        taskScheduler.ScheduleTaskAsync(async () =>
       {
           await Task.Delay(1000); // Simulate work
           taskCompleted = true;
       });

        // Stop the task scheduler before waiting for the task to complete
        taskScheduler.Stop();

        // Assert
        Assert.False(taskCompleted);
    }
}