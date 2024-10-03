namespace RecursiveDescentParser;

public class Parser : IParser
{
    private IList<Token> _tokens = new List<Token>();
    private int _position;
    private Token _token = new Token(TokenType.None);

    private readonly SyntaxTree _syntaxTree = new();

    public int MethodCallCount = 0;

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
        MethodCallCount = 0;
    }

    public SyntaxTree Parse()
    {
        MethodCallCount += 1;
        
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
        MethodCallCount += 1;
        
        var leftChild = Term();
        var (op, rightChild) = ExprPrime();

        if (op.Type == TokenType.None)
        {
            return leftChild;
        }
        
        return new SyntaxNode(op, leftChild, rightChild);
    }

    private PrimeReturnType ExprPrime()
    {
        MethodCallCount += 1;
        
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

        return new PrimeReturnType(new Token(TokenType.None), new SyntaxNode());
    }

    private SyntaxNode Term()
    {
        MethodCallCount += 1;
        
        var leftChild = Factor();
        var (op, rightChild) = TermPrime();

        if (op.Type == TokenType.None)
        {
            return leftChild;
        }

        return new SyntaxNode(op, leftChild, rightChild);
    }

    private PrimeReturnType TermPrime()
    {
        MethodCallCount += 1;
        
        if (_token.IsMul())
        {
            _token = NextToken();
            return new PrimeReturnType(new Token(TokenType.Operator, "*"), Term());
        }
        
        if (_token.IsDiv())
        {
            _token = NextToken();
            return new PrimeReturnType(new Token(TokenType.Operator, "/"), Term());
        }
        
        return new PrimeReturnType(new Token(TokenType.None), new SyntaxNode());
    }

    private SyntaxNode Factor()
    {
        MethodCallCount += 1;
        
        switch (_token.Type)
        {
            case TokenType.Number:
            case TokenType.Id:
                var node = new SyntaxNode(_token);
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

public record PrimeReturnType(Token Value, SyntaxNode RightChild);