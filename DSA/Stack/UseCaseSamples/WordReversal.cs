namespace DSA.Stack.UseCaseSamples;

class WordReversal
{
    public static string ReverseWord(string word)
    {
        Stack<char> charStack = new Stack<char>();

        // Push each character of the word onto the stack
        foreach (char c in word)
        {
            charStack.Push(c);
        }

        // Pop characters from the stack to construct the reversed word
        char[] reversedArray = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            reversedArray[i] = charStack.Pop();
        }

        return new string(reversedArray);
    }

    public static void Apply()
    {
        Console.Write("Enter a word to reverse: ");
        string inputWord = Console.ReadLine();

        string reversedWord = ReverseWord(inputWord);

        Console.WriteLine("Reversed word: " + reversedWord);
    }
}

