namespace CsLox
{
    public class BinaryExpression(Expression left, Token token, Expression right)  : Expression
    {
        Expression _left = left;
        Expression _right = right;
        Token _token = token;


    }




}