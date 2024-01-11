namespace CsLox
{
    public class LiteralExpression(object value) : Expression
    {
        object _value = value;

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitLiteralExpression(this);
        }
    }



}