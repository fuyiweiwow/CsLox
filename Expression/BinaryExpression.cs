namespace CsLox
{
    public class BinaryExpression(Expression left, Token token, Expression right)  : Expression
    {
        Expression _left = left;
        Expression _right = right;
        Token _token = token;

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitBinaryExpression(this);
        }
    }




}