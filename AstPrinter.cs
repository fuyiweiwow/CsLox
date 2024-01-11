namespace CsLox
{
    public class AstPrinter : IVisitor<string>
    {
        public string Print(Expression expr)
        {
            return expr.Accept(this);
        }

        public string VisitBinaryExpression(BinaryExpression expr)
        {
            throw new NotImplementedException();
        }

        public string VisitGroupingExpression(GroupingExpression expr)
        {
            throw new NotImplementedException();
        }

        public string VisitLiteralExpression(LiteralExpression expr)
        {
            throw new NotImplementedException();
        }

        public string VisitUnaryExpression(UnaryExpression expr)
        {
            throw new NotImplementedException();
        }
    }


}