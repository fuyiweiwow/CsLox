namespace CsLox
{
    public class BinaryExpression(Expression left, Token token, Expression right)  : Expression
    {
        Expression _left = left;
        Expression _right = right;
        Token _operator = token;

        public Expression Left { get => _left; set => _left = value; }
        public Expression Right { get => _right; set => _right = value; }
        public Token Operator { get => _operator; set => _operator = value; }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitBinaryExpression(this);
        }
    }




}