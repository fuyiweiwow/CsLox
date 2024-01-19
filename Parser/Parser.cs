namespace CsLox
{
    /// <summary>
    /// basic grammer is like follow 
    /// equality       → comparison ( ( "!=" | "==" ) comparison )* ;
    /// comparison     → term ( ( ">" | ">=" | "<" | "<=" ) term )* ;
    /// term           → factor ( ( "-" | "+" ) factor )* ;
    /// factor         → unary ( ( "/" | "*" ) unary )* ;
    /// unary          → ( "!" | "-" ) unary | primary ;
    /// primary        → NUMBER | STRING | "true" | "false" | "nil" | "(" expression ")" ;
    /// </summary>
    /// <param name="tokens"></param>
    public class Parser(List<Token> tokens)
    {
        readonly List<Token> _tokens = tokens;
        int _current = 0;

        private Expression Expression()
        {
            return Equality();
        }

        /// <summary>
        /// equality       → comparison ( ( "!=" | "==" ) comparison )* ;
        /// </summary>
        /// <returns></returns>
        private Expression Equality()
        {
            return RecursiveDescentParsing(
                    Comparison,
                    TokenType.NotEqual,
                    TokenType.DoubleEqual);
        }

        /// <summary>
        /// comparison     → term ( ( ">" | ">=" | "<" | "<=" ) term )* ;
        /// </summary>
        /// <returns></returns>
        private Expression Comparison()
        {
            return RecursiveDescentParsing(
                    Term,
                    TokenType.Greater, 
                    TokenType.GreaterAndEqual, 
                    TokenType.Less, 
                    TokenType.LessAndEqual);
        }

        /// <summary>
        /// term           → factor ( ( "-" | "+" ) factor )* ;
        /// </summary>
        /// <returns></returns>
        private Expression Term()
        {
            return RecursiveDescentParsing(
                    Factor,
                    TokenType.Minus, 
                    TokenType.Plus);
        }

        /// <summary>
        /// factor         → unary ( ( "/" | "*" ) unary )* ;
        /// </summary>
        /// <returns></returns>
        private Expression Factor()
        {
            return RecursiveDescentParsing(
                    Unary,
                    TokenType.Slash, 
                    TokenType.Star);
        }

        /// <summary>
        /// unary          → ( "!" | "-" ) unary | primary ;
        /// </summary>
        /// <returns></returns>
        private Expression Unary()
        {
            if(Match(TokenType.Not, TokenType.Minus))
            {
                Token op = Previous();
                Expression right = Unary();
                return new UnaryExpression(op, right);
            }

            return Primary();
        }

        /// <summary>
        /// primary        → NUMBER | STRING | "true" | "false" | "nil" | "(" expression ")" ;
        /// </summary>
        /// <returns></returns>
        private Expression Primary()
        {
            if(Match(TokenType.False))
            {
                return new LiteralExpression(false);
            }

            if(Match(TokenType.True))
            {
                return new LiteralExpression(true);
            }

            if(Match(TokenType.Nil))
            {
                return new LiteralExpression(null);
            }

            if(Match(TokenType.Number, TokenType.String))
            {
                return new LiteralExpression(Previous().Literal);
            }

            if(Match(TokenType.LeftParen))
            {
                var expr = Expression();
                Consume(TokenType.RightParen, "Expect ')' after expression.");
                return new GroupingExpression(expr);
            }

        }


        private Expression RecursiveDescentParsing(Func<Expression> lower, params TokenType[] types)
        {
             var expr = lower();

            while(Match(types))
            {
                Token op = Previous();
                Expression right = lower();
                expr = new BinaryExpression(expr, op, right);
            }

            return expr;
        }



        /// <summary>
        /// if match the required token ,consume it and return true ;
        /// otherwise do nothing and return false
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private bool Match(params TokenType[] types)
        {
            foreach(var typ in types)
            {
                if(!Check(typ))
                {
                    continue;
                }

                Advance();
                return true;
            }

            return false;
        }

        private bool Check(TokenType typ)
        {
            if(IsAtEnd())
            {
                return false;
            }

            return _tokens[_current].Type == typ;
        }
        

        private Token Advance()
        {
            if(!IsAtEnd())
            {
                ++_current;
            }

            return Previous();
        }

        private Token Previous()
        {
            return _tokens[_current - 1];
        }

        private Token Peek()
        {
            return _tokens[_current];
        }

        private void Consume(TokenType tokenType, string errorMessage)
        {

        }



        private bool IsAtEnd()
        {
            return _current + 1>= _tokens.Count;
        }
    }
}