namespace RecursiveDescentParser;

public interface IParser
{
    public void Load(IList<Token> tokens);
    public SyntaxTree Parse();
}