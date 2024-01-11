namespace CsLox
{
    public class UnaryExpression(Token token, Expression right) : Expression
    {
        Token _token = token;
        Expression _right = right;

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitUnaryExpression(this);
        }
    }



}