using RecursiveDescentParser;

namespace RecursiveDescentParserTest;

[TestFixture]
public class TokenTest
{
    [Test]
    public void Test_IsOperator()
    {
        var plus = new Token(TokenType.Operator, "+");
        var minus = new Token(TokenType.Operator, "-");
        var mul = new Token(TokenType.Operator, "*");
        var div = new Token(TokenType.Operator, "/");
        
        Assert.That(plus.IsPlus(), Is.True);
        Assert.That(plus.IsMinus, Is.False);
        Assert.That(div.IsDiv, Is.True);
        Assert.That(mul.IsMul, Is.True);
        Assert.That(minus.IsMinus, Is.True);
    }
}