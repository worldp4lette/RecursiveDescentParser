namespace RecursiveDescentParser;

public class Scanner : IScanner
{
    private string _program = string.Empty;

    private IList<Token> Tokens { get; } = new List<Token>();

    private int Position { get; set; }

    public void Load(string text)
    {
        _program = text;
        Position = 0;
        Tokens.Clear();
    }

    public IList<Token> Tokenize()
    {
        _program = _program.Replace(" ", string.Empty)
            .Replace("\t", string.Empty)
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty);

        while (Position < _program.Length)
        {
            GetLeftMostToken();
        }
        
        return Tokens;
    }

    private void GetLeftMostToken()
    {
        var ch = _program[Position];
        var length = 0;
        TokenType tokenType;

        switch (ch.GetSymbolType())
        {
            case SymbolType.Digit:
                tokenType = TokenType.Number;
                while (Position + length < _program.Length 
                       && _program[Position + length].GetSymbolType() == SymbolType.Digit)
                {
                    length++;
                }
                break;
            
            case SymbolType.Alphabet:
                tokenType = TokenType.Id;
                while (Position + length < _program.Length
                       && _program[Position + length].IsLetterOrDigit())
                {
                    length++;
                }
                break;
            
            case SymbolType.Operator:
                tokenType = TokenType.Operator;
                length++;
                break;
            
            default:
                throw new InvalidOperationException("Invalid character in the program.");
        }
        
        var tokenValue = _program.Substring(Position, length);
        Tokens.Add(new Token(tokenType, tokenValue));
        Position += length;
    }
}
