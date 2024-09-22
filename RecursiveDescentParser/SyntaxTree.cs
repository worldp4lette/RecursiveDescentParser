namespace RecursiveDescentParser;

public class SyntaxTree
{
    public SyntaxNode Root { get; set; } = new SyntaxNode();

    public void PreOrder()
    {
        throw new NotImplementedException();
    }
}

public class SyntaxNode
{
    public Token Value { get; init; }
    public bool IsLeaf { get; init; }
    public SyntaxNode? LeftChild { get; init; }
    public SyntaxNode? RightChild { get; init; }

    public SyntaxNode(Token value, bool isLeaf, SyntaxNode? leftChild, SyntaxNode? rightChild)
    {
        switch (isLeaf)
        {
            case true:
                if (value.Type == TokenType.Operator)
                {
                    throw new ArgumentException("Leaf node cannot be an operator.");
                }

                break;

            case false:
                if (value.Type != TokenType.Operator)
                {
                    throw new ArgumentException("Intermediate node should be an operator.");
                }

                if (leftChild is not null || rightChild is not null)
                {
                    throw new ArgumentException("Operator node should have both left and right children.");
                }
                
                break;
        }

        Value = value;
        IsLeaf = isLeaf;
        LeftChild = leftChild;
        RightChild = rightChild;
    }

    public SyntaxNode()
    {
        Value = new Token(TokenType.None);
        IsLeaf = true;
        LeftChild = null;
        RightChild = null;
    }
}