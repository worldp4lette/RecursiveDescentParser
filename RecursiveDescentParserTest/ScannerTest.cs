using RecursiveDescentParser;

namespace RecursiveDescentParserTest;

[TestFixture]
public class ScannerTest
{
    private static readonly string Program1 = "1 + 32 + x42 * 66";

    private static readonly List<Token> Program1Tokens =
    [
        new Token(TokenType.Number, "1"),
        new Token(TokenType.Operator, "+"),
        new Token(TokenType.Number, "32"),
        new Token(TokenType.Operator, "+"),
        new Token(TokenType.Id, "x42"),
        new Token(TokenType.Operator, "*"),
        new Token(TokenType.Number, "66"),
        new Token(TokenType.EOF)
    ];

    private static readonly string Program2 = @"
412
    + xx12se / 341";

    private static readonly List<Token> Program2Tokens =
    [
        new Token(TokenType.Number, "412"),
        new Token(TokenType.Operator, "+"),
        new Token(TokenType.Id, "xx12se"),
        new Token(TokenType.Operator, "/"),
        new Token(TokenType.Number, "341"),
        new Token(TokenType.EOF)
    ];

    private static readonly string Program3 = "abc **?!adsf";

    private IScanner _scanner;

    [SetUp]
    public void SetUp()
    {
        _scanner = new Scanner();
    }

    [Test]
    public void Test_Tokenize()
    {
        _scanner.Load(Program1);
        Assert.That(_scanner.Tokenize().SequenceEqual(Program1Tokens), Is.True);
        
        _scanner.Load(Program2);
        Assert.That(_scanner.Tokenize().SequenceEqual(Program2Tokens), Is.True);
    }

    [Test]
    public void TestNegative_Tokenize()
    {
        _scanner.Load(Program3);
        Assert.Throws<InvalidOperationException>(() => _scanner.Tokenize());
    }
}