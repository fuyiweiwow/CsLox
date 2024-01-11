namespace CsLox
{
    public interface IVisitor<T>
    {
        T VisitBinaryExpression(BinaryExpression expr);
        T VisitGroupingExpression(GroupingExpression expr);
        T VisitLiteralExpression(LiteralExpression expr);
        T VisitUnaryExpression(UnaryExpression expr);
    }

}