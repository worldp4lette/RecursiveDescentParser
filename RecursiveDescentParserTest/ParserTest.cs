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

    private static readonly string Program1Traversal = "3 + 2";
    
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
    }
    
}