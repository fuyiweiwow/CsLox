namespace CsLox
{
    /// <summary>
    /// TODO: complete RpnAstPrinter so that it can print structure in RPN format
    /// </summary>
    public class RpnAstPrinter : IVisitor<string>, IAstPrinter
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