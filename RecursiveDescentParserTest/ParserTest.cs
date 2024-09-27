using RecursiveDescentParser;

namespace RecursiveDescentParserTest;

[TestFixture]
public class ParserTest
{
    private static readonly IList<Token> Program1Tokens = 
    [
        new Token(TokenType.Number, "3"),
        new Token(TokenType.Operator, "+"),
        new Token(TokenType.Number, "2"),
        new Token(TokenType.EOF)
    ];

    private static readonly IList<Token> Program2Tokens = 
    [
        new Token(TokenType.Number, "3"),
        new Token(TokenType.Operator, "+"),
        new Token(TokenType.Number, "2"),
        new Token(TokenType.Operator, "*"),
        new Token(TokenType.Id, "x"),
        new Token(TokenType.EOF)
    ];

    private static readonly IList<Token> Program3Tokens = 
    [
        new Token(TokenType.Number, "3"),
        new Token(TokenType.EOF)
    ];

    private static readonly IList<Token> Program4Tokens = 
    [
        new Token(TokenType.Number, "3"),
        new Token(TokenType.Operator, "/"),
        new Token(TokenType.Number, "5"),
        new Token(TokenType.Operator, "+"),
        new Token(TokenType.Number, "2"),
        new Token(TokenType.Operator, "*"),
        new Token(TokenType.Id, "x"),
        new Token(TokenType.EOF)
    ];

    private static readonly string Program1Traversal = "+ 3 2";

    private static readonly string Program2Traversal = "+ 3 * 2 x";

    private static readonly string Program3Traversal = "3";

    private static readonly string Program4Traversal = "+ / 3 5 * 2 x";
    
    private IParser _parser;

    [SetUp]
    public void SetUp()
    {
        _parser = new Parser();
    }
    
    [Test]
    public void Test_Parse()
    {
        _parser.Load(Program1Tokens);
        var parseResult1 = _parser.Parse().PreOrder();
        Assert.That(parseResult1.Equals(Program1Traversal, StringComparison.Ordinal));
        
        _parser.Load(Program2Tokens);
        var parseResult2 = _parser.Parse().PreOrder();
        Assert.That(parseResult2.Equals(Program2Traversal, StringComparison.Ordinal));
        
        _parser.Load(Program3Tokens);
        var parseResult3 = _parser.Parse().PreOrder();
        Assert.That(parseResult3.Equals(Program3Traversal, StringComparison.Ordinal));
        
        _parser.Load(Program4Tokens);
        var parseResult4 = _parser.Parse().PreOrder();
        Assert.That(parseResult4.Equals(Program4Traversal, StringComparison.Ordinal));
    }
    
}