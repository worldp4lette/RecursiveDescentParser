namespace RecursiveDescentParser;

public interface IScanner
{
    public void Load(string programText);
    public IList<Token> Tokenize();
}