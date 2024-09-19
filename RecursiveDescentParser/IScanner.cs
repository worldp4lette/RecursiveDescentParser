namespace RecursiveDescentParser;

public interface IScanner
{
    public void Load(string programText);
    public IEnumerable<Token> Tokenize();
}