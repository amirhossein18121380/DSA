using DSA.Stack.UseCaseSamples;

namespace Test.Stack;
public class ExpressionEvaluatorTests
{
    [Fact]
    public void TestSimpleExpression()
    {
        string expression = "3 + 4 * 2 - 6 / 2";
        double result = ExpressionEvaluator.Evaluate(expression);
        Assert.Equal(8.0, result);
    }


    [Fact]
    public void TestExpressionWithParentheses()
    {
        string expression = "(3 + 4) * (2 - 6) / 2";
        double result = ExpressionEvaluator.Evaluate(expression);
        Assert.Equal(-14.0, result);
    }

    [Fact]
    public void TestInvalidExpression()
    {
        string expression = "3 + 4 *"; // Missing operand
        Assert.Throws<ArgumentException>(() => ExpressionEvaluator.Evaluate(expression));
    }

    [Fact]
    public void TestMismatchedParentheses()
    {
        string expression = "3 + (4 * 2 - 6 / 2))"; // Mismatched parentheses
        Assert.Throws<ArgumentException>(() => ExpressionEvaluator.Evaluate(expression));
    }

    [Fact]
    public void TestDivisionByZero()
    {
        string expression = "3 / 0"; // Division by zero
        Assert.Throws<DivideByZeroException>(() => ExpressionEvaluator.Evaluate(expression));
    }

    [Fact]
    public void TestInvalidCharacter()
    {
        string expression = "3 ^ 4"; // Invalid operator ^
        Assert.Throws<ArgumentException>(() => ExpressionEvaluator.Evaluate(expression));
    }

    [Fact]
    public void TestWhitespace()
    {
        string expression = "3 +  4 * 2"; // Extra whitespace
        double result = ExpressionEvaluator.Evaluate(expression);
        Assert.Equal(11.0, result);
    }
}
