namespace CsLox
{
    public class GroupingExpression(Expression expression) : Expression
    {
        Expression _expression = expression;

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitGroupingExpression(this);
        }
    }




}