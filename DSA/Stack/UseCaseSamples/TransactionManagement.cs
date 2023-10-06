namespace DSA.Stack.UseCaseSamples;
class TransactionManagement
{
    private Dictionary<string, string> data;
    private Stack<Dictionary<string, string>> transactionStack;

    public TransactionManagement()
    {
        data = new Dictionary<string, string>();
        transactionStack = new Stack<Dictionary<string, string>>();
    }

    public void Set(string key, string value)
    {
        data[key] = value;
    }

    public string Get(string key)
    {
        if (data.ContainsKey(key))
            return data[key];
        else
            return null;
    }

    public void BeginTransaction()
    {
        // Save a snapshot of the current database state onto the transaction stack.
        transactionStack.Push(new Dictionary<string, string>(data));
    }

    public void CommitTransaction()
    {
        // Commit the current transaction by removing it from the stack.
        if (transactionStack.Count > 0)
        {
            transactionStack.Pop();
        }
        else
        {
            Console.WriteLine("No transaction to commit.");
        }
    }

    public void RollbackTransaction()
    {
        // Roll back the current transaction by restoring the previous database state.
        if (transactionStack.Count > 0)
        {
            data = transactionStack.Pop();
        }
        else
        {
            Console.WriteLine("No transaction to rollback.");
        }
    }

    public static void Apply()
    {
        TransactionManagement db = new TransactionManagement();

        Console.WriteLine("Transaction Management Example:");

        // Initial state
        db.Set("key1", "value1");
        Console.WriteLine("Key1 = " + db.Get("key1"));

        // Start a transaction
        db.BeginTransaction();

        // Perform some operations within the transaction
        db.Set("key2", "value2");
        Console.WriteLine("Key2 = " + db.Get("key2"));

        // Commit the transaction
        db.CommitTransaction();

        // After the commit, key2 should still be accessible
        Console.WriteLine("After Commit:");
        Console.WriteLine("Key2 = " + db.Get("key2"));

        // Start another transaction
        db.BeginTransaction();

        // Perform some operations within the new transaction
        db.Set("key2", "new_value2");
        Console.WriteLine("Key2 (inside transaction) = " + db.Get("key2"));

        // Rollback the transaction
        db.RollbackTransaction();

        // After the rollback, key2 should retain its previous value
        Console.WriteLine("After Rollback:");
        Console.WriteLine("Key2 = " + db.Get("key2"));
    }
}