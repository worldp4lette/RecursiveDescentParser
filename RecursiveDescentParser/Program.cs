namespace RecursiveDescentParser;

static class Program
{
    static void Main(string[] args)
    {
        var scanner = new Scanner();
        var parser = new Parser();

        while (true)
        {
            Console.WriteLine("Enter expression. Type 'exit' to exit.");
            var expressionStr = Console.ReadLine();

            if (expressionStr is null)
            {
                continue;
            }

            if (expressionStr == "exit")
            {
                break;
            }
            
            scanner.Load(expressionStr);
            IList<Token> tokens;

            try
            {
                tokens = scanner.Tokenize();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }
            
            parser.Load(tokens);

            try
            {
                var tree = parser.Parse();
                Console.WriteLine($"Parsed expression: {tree.PreOrder()}");
                Console.WriteLine($"{parser.MethodCallCount} function calls have been made during the parsing process.");
                Console.WriteLine();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}