namespace DSA.Stack.UseCaseSamples;

using System;
using System.Collections.Generic;

public class BrowserHistory
{
    public Stack<string> backHistory;
    public Stack<string> forwardHistory;
    public string currentUrl;

    public BrowserHistory(string initialUrl)
    {
        backHistory = new Stack<string>();
        forwardHistory = new Stack<string>();
        currentUrl = initialUrl;
    }

    public void Visit(string url)
    {
        backHistory.Push(currentUrl);
        currentUrl = url;
        forwardHistory.Clear(); // Clear the forward history when a new page is visited
    }

    public string Back()
    {
        if (backHistory.Count == 0)
        {
            return "Cannot go back. No history available.";
        }

        forwardHistory.Push(currentUrl);
        currentUrl = backHistory.Pop();
        return currentUrl;
    }

    public string Forward()
    {
        if (forwardHistory.Count == 0)
        {
            return "Cannot go forward. No history available.";
        }

        backHistory.Push(currentUrl);
        currentUrl = forwardHistory.Pop();
        return currentUrl;
    }

    public string CurrentPage()
    {
        return currentUrl;
    }

    public static void Apply()
    {
        Console.WriteLine("Welcome to the Web Browser History Simulator!");
        Console.Write("Enter the initial URL: ");
        string initialUrl = Console.ReadLine();

        BrowserHistory browserHistory = new BrowserHistory(initialUrl);

        while (true)
        {
            Console.WriteLine("\nCurrent Page: " + browserHistory.CurrentPage());
            Console.WriteLine("Options:");
            Console.WriteLine("1. Visit a new page");
            Console.WriteLine("2. Go back");
            Console.WriteLine("3. Go forward");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the URL to visit: ");
                    string url = Console.ReadLine();
                    browserHistory.Visit(url);
                    break;
                case 2:
                    Console.WriteLine("Going back...");
                    Console.WriteLine("Current Page: " + browserHistory.Back());
                    break;
                case 3:
                    Console.WriteLine("Going forward...");
                    Console.WriteLine("Current Page: " + browserHistory.Forward());
                    break;
                case 4:
                    Console.WriteLine("Exiting the Web Browser History Simulator.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}


