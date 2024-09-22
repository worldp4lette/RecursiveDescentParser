namespace RecursiveDescentParser;

public record struct Token(TokenType Type, string? Value = null)
{
    public bool IsPlus()
    {
        return this.Type == TokenType.Operator && (this.Value?.Equals("+", StringComparison.Ordinal) ?? false);
    }

    public bool IsMinus()
    {
        return this.Type == TokenType.Operator && (this.Value?.Equals("-", StringComparison.Ordinal) ?? false);
    }

    public bool IsMul()
    {
        return this.Type == TokenType.Operator && (this.Value?.Equals("*", StringComparison.Ordinal) ?? false);
    }

    public bool IsDiv()
    {
        return this.Type == TokenType.Operator && (this.Value?.Equals("/", StringComparison.Ordinal) ?? false);
    }

    public override string ToString()
    {
        return Value ?? string.Empty;
    }
};

public enum TokenType
{
    None,
    Number,
    Id,
    Operator,
    EOF,
}