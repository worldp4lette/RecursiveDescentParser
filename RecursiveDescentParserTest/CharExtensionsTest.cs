using RecursiveDescentParser;

namespace RecursiveDescentParserTest;

[TestFixture]
public class CharExtensionsTests
{
    [TestCase('1', SymbolType.Digit)]
    [TestCase('5', SymbolType.Digit)]
    [TestCase('a', SymbolType.Alphabet)]
    [TestCase('T', SymbolType.Alphabet)]
    [TestCase('*', SymbolType.Operator)]
    public void Test_GetSymbolType(char ch, SymbolType expectedType)
    {
        Assert.That(ch.GetSymbolType(), Is.EqualTo(expectedType));
    }

    [TestCase('8', SymbolType.Alphabet)]
    [TestCase('Q', SymbolType.Digit)]
    [TestCase('/', SymbolType.Digit)]
    public void TestNegative_GetSymbolType(char ch, SymbolType expectedType)
    {
        Assert.That(ch.GetSymbolType(), Is.Not.EqualTo(expectedType));
    }

    [TestCase("ab12")]
    [TestCase("aab022")]
    public void Test_IsLetterOrDigit(string text)
    {
        foreach (var ch in text)
        {
            Assert.That(ch.IsLetterOrDigit(), Is.True);
        }
    }

    [TestCase("a+1252")]
    public void TestNegative_IsLetterOrDigit(string text)
    {
        var hasInvalidChar = false;
        
        foreach (var ch in text)
        {
            if (ch.IsLetterOrDigit() is false)
            {
                hasInvalidChar = true;
            }
        }
        
        Assert.That(hasInvalidChar, Is.True);
    }
}