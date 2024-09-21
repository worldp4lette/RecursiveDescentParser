namespace RecursiveDescentParser;

public record Token(TokenType Type, string? Value = null);

public enum TokenType
{
    None,
    Number,
    Id,
    Operator,
    EOF,
}