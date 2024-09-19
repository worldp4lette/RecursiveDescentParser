﻿namespace RecursiveDescentParser;

public record Token(TokenType Type, string Value);

public enum TokenType
{
    None,
    Number,
    Id,
    Operator
}