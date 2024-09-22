using System.Text;

namespace RecursiveDescentParser;

public class SyntaxTree
{
    public SyntaxNode Root { get; set; } = new SyntaxNode();

    public string PreOrder()
    {
        return Walk(Root);
    }

    private string Walk(SyntaxNode node)
    {
        if (node.IsLeaf)
        {
            return node.Value.ToString();
        }

        var left = Walk(node.LeftChild!);
        var op = node.Value.ToString();
        var right = Walk(node.RightChild!);
        
        return $"{left} {op} {right}";
    }
}

public class SyntaxNode
{
    public Token Value { get; init; }
    public bool IsLeaf { get; init; }
    public SyntaxNode? LeftChild { get; init; }
    public SyntaxNode? RightChild { get; init; }

    public SyntaxNode(Token value, bool isLeaf = true, SyntaxNode? leftChild = null, SyntaxNode? rightChild = null)
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

                if (leftChild is null || rightChild is null)
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

    public SyntaxNode(Token value, SyntaxNode leftChild, SyntaxNode rightChild) : this(value, false, leftChild, rightChild) { }

    public SyntaxNode()
    {
        Value = new Token(TokenType.None);
        IsLeaf = true;
        LeftChild = null;
        RightChild = null;
    }
}