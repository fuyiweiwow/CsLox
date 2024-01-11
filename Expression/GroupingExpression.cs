namespace CsLox
{
    public class GroupingExpression(Expression expression) : Expression
    {
        Expression _expression = expression;

        public Expression Expression { get => _expression; set => _expression = value; }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitGroupingExpression(this);
        }
    }




}