using System.Collections.Immutable;

namespace RecursiveDescentParser;

public static class CharExtensions
{
    private static readonly ImmutableHashSet<char> Ops = "+-*/".ToImmutableHashSet();

    private static bool IsOperator(this char ch) => Ops.Contains(ch);

    public static SymbolType GetSymbolType(this char ch)
    {
        if (char.IsDigit(ch))
        {
            return SymbolType.Digit;
        }

        if (char.IsLetter(ch))
        {
            return SymbolType.Alphabet;
        }

        return ch.IsOperator() ? SymbolType.Operator : SymbolType.None;
    }

    public static bool IsLetterOrDigit(this char ch)
    {
        var symbolType = GetSymbolType(ch);
        return symbolType is SymbolType.Alphabet or SymbolType.Digit;
    }
}

public enum SymbolType
{
    None,
    Digit,
    Operator,
    Alphabet
}