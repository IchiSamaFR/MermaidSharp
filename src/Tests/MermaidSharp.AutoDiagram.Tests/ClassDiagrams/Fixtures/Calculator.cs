namespace MermaidSharp.AutoDiagram.Tests.ClassDiagrams.Fixtures
{
    /// <summary>
    /// Provides methods with and without return values for method option tests.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>The sum of both numbers.</returns>
        public int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Resets the calculator.
        /// </summary>
        public void Reset()
        {
        }
    }
}
