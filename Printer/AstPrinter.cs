using System.Text;

namespace CsLox
{
    public class AstPrinter : IVisitor<string>, IAstPrinter
    {
        static readonly string _groupStr = "group";
        static readonly string _nilStr = "nil";

        public string Print(Expression? expr)
        {
            if(expr is null)
            {
                return string.Empty;
            }
            
            return expr.Accept(this);
        }

        public string VisitBinaryExpression(BinaryExpression expr)
        {
            return Parenthesize(expr.Operator.Lexeme, expr.Left, expr.Right);
        }

        public string VisitGroupingExpression(GroupingExpression expr)
        {
            return Parenthesize(_groupStr, expr.Expression);
        }

        public string VisitLiteralExpression(LiteralExpression expr)
        {
            if(expr.Value is null)
            {
                return _nilStr;
            }

            var exprVal = expr.Value;
            return exprVal.ToString() ?? _nilStr;
        }

        public string VisitUnaryExpression(UnaryExpression expr)
        {
            return Parenthesize(expr.Operator.Lexeme, expr.Right);
        }

        /// <summary>
        /// produce syntax string like lisp e.g. (* (- 123) (group 45.67))
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exprs"></param>
        /// <returns></returns>
        private string Parenthesize(string name, params Expression[] exprs)
        {
            var sb = new StringBuilder();
            sb.Append('(').Append(name);

            foreach(var expr in exprs)
            {
                sb.Append(' ');
                sb.Append(expr.Accept(this));
            }

            sb.Append(')');
            return sb.ToString();
        }


    }


}