namespace RecursiveDescentParser;

public class Parser : IParser
{
    private IList<Token> _tokens;
    
    public void Load(IList<Token> tokens)
    {
        _tokens = tokens;
    }

    public SyntaxTree Parse()
    {
        
        throw new NotImplementedException();
    }
}