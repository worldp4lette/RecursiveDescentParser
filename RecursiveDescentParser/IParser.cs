namespace RecursiveDescentParser;

public interface IParser
{
    public SyntaxTree Parse(IEnumerable<Token> tokens);
}