namespace RecursiveDescentParser;

public class Parser : IParser
{
    private IList<Token> _tokens = new List<Token>();
    private int _position;
    private Token _token = new Token(TokenType.None);

    private SyntaxTree _syntaxTree = new();

    private Token NextToken()
    {
        if (_position < _tokens.Count - 1)
        {
            _position++;
        }

        return _tokens[_position];
    }
    
    public void Load(IList<Token> tokens)
    {
        if (tokens.Count < 1)
        {
            throw new ArgumentException("Parser must be loaded with a list of tokens that contains at least one token.");
        }

        if (tokens.Last().Type != TokenType.EOF)
        {
            throw new ArgumentException("The last token must be EOF.");
        }
        
        _tokens = tokens;
        _position = -1;
    }

    public SyntaxTree Parse()
    {
        if (_tokens.Count < 1)
        {
            throw new InvalidOperationException("Parser is not loaded yet.");
        }
        
        _token = NextToken();

        if (_token.Type == TokenType.EOF)
        {
            throw new InvalidOperationException("First token is an EOF.");
        }

        _syntaxTree.Root = Expr();

        return _syntaxTree;
    }

    private SyntaxNode Expr()
    {
        var leftChild = Term();
        var (op, rightChild) = ExprPrime();

        if (op.Type == TokenType.None)
        {
            return new SyntaxNode(leftChild.Value, true, null, null);
        }
        
        return new SyntaxNode(op, false, leftChild, rightChild);
    }

    private PrimeReturnType ExprPrime()
    {
        if (_token.IsPlus())
        {
            _token = NextToken();
            return new PrimeReturnType(new Token(TokenType.Operator, "+"), Expr());
        }
        
        if (_token.IsMinus())
        {
            _token = NextToken();
            return new PrimeReturnType(new Token(TokenType.Operator, "-"), Expr());
        }

        return new PrimeReturnType(new Token(TokenType.None), null);
    }

    private SyntaxNode Term()
    {
        var leftChild = Factor();
        var (op, rightChild) = TermPrime();

        if (op.Type == TokenType.None)
        {
            return new SyntaxNode(leftChild.Value, true, null, null);
        }

        return new SyntaxNode(op, false, leftChild, rightChild);
    }

    private PrimeReturnType TermPrime()
    {
        if (_token.IsMul())
        {
            _token = NextToken();
            return new PrimeReturnType(new Token(TokenType.Operator, "*"), Expr());
        }
        
        if (_token.IsDiv())
        {
            _token = NextToken();
            return new PrimeReturnType(new Token(TokenType.Operator, "/"), Expr());
        }
        
        return new PrimeReturnType(new Token(TokenType.None), null);
    }

    private SyntaxNode Factor()
    {
        switch (_token.Type)
        {
            case TokenType.Number:
            case TokenType.Id:
                var node = new SyntaxNode(_token, true, null, null);
                _token = NextToken();

                return node;

            case TokenType.None:
            case TokenType.Operator:
            case TokenType.EOF:
            default:
                throw new InvalidOperationException($"Unexpected token type: {_token.Type}");
        }
    }
}

public record PrimeReturnType(Token Value, SyntaxNode? RightChild);