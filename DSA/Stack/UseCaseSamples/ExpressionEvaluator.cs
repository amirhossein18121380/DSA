namespace DSA.Stack.UseCaseSamples;
using System;
using System.Collections.Generic;

public class ExpressionEvaluator
{
    // Define precedence levels for operators
    private static readonly Dictionary<char, int> OperatorPrecedence = new Dictionary<char, int>
    {
        { '+', 1 },
        { '-', 1 },
        { '*', 2 },
        { '/', 2 },
    };

    public static double Evaluate(string expression)
    {
        Stack<double> operandStack = new Stack<double>();
        Stack<char> operatorStack = new Stack<char>();

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (char.IsDigit(c))
            {
                // Read the entire number and push it onto the operand stack
                string operand = c.ToString();
                while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                {
                    operand += expression[i + 1];
                    i++;
                }
                if (!double.TryParse(operand, out double num))
                {
                    throw new ArgumentException("Invalid number in the expression.");
                }
                operandStack.Push(num);
            }
            else if (c == '(')
            {
                // Push an open parenthesis onto the operator stack
                operatorStack.Push(c);
            }
            else if (c == ')')
            {
                // Pop operators and apply them until an open parenthesis is encountered
                while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                {
                    ApplyOperator(operandStack, operatorStack);
                }
                // Pop the open parenthesis
                if (operatorStack.Count == 0 || operatorStack.Pop() != '(')
                {
                    throw new ArgumentException("Mismatched parentheses in the expression.");
                }
            }
            else if (OperatorPrecedence.ContainsKey(c))
            {
                // Pop and apply operators with higher or equal precedence
                while (operatorStack.Count > 0 && operatorStack.Peek() != '(' &&
                       OperatorPrecedence[c] <= OperatorPrecedence[operatorStack.Peek()])
                {
                    ApplyOperator(operandStack, operatorStack);
                }
                // Push the current operator onto the operator stack
                operatorStack.Push(c);
            }
            else if (char.IsWhiteSpace(c))
            {
                // Ignore whitespace
                continue;
            }
            else
            {
                throw new ArgumentException("Invalid character in the expression: " + c);
            }
        }

        // Apply remaining operators
        while (operatorStack.Count > 0)
        {
            if (operatorStack.Peek() == '(')
            {
                throw new ArgumentException("Mismatched parentheses in the expression.");
            }
            ApplyOperator(operandStack, operatorStack);
        }

        if (operandStack.Count != 1 || operatorStack.Count != 0)
        {
            throw new ArgumentException("Invalid expression.");
        }

        return operandStack.Pop();
    }

    private static void ApplyOperator(Stack<double> operandStack, Stack<char> operatorStack)
    {
        char op = operatorStack.Pop();
        if (operandStack.Count < 2)
        {
            throw new ArgumentException("Invalid expression.");
        }
        double operand2 = operandStack.Pop();
        double operand1 = operandStack.Pop();
        double result = PerformOperation(operand1, operand2, op);
        operandStack.Push(result);
    }

    private static double PerformOperation(double operand1, double operand2, char op)
    {
        switch (op)
        {
            case '+':
                return operand1 + operand2;
            case '-':
                return operand1 - operand2;
            case '*':
                return operand1 * operand2;
            case '/':
                if (operand2 == 0)
                {
                    throw new DivideByZeroException("Division by zero is not allowed.");
                }
                return operand1 / operand2;
            default:
                throw new ArgumentException("Invalid operator: " + op);
        }
    }

    public static void Apply(string input)
    {
        try
        {
            if (input.Equals("exit"))
            {
                return;
            }
            //string example_expression = "3 + (1/1) + (5 * 2) - 6 / 2 - 2";
            double result = Evaluate(input);
            Console.WriteLine("Result: " + result);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
