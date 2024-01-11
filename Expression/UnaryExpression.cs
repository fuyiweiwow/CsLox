namespace CsLox
{
    public class UnaryExpression(Token token, Expression right) : Expression
    {
        Token _operator = token;
        Expression _right = right;

        public Token Operator { get => _operator; set => _operator = value; }
        public Expression Right { get => _right; set => _right = value; }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitUnaryExpression(this);
        }
    }



}