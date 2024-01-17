namespace CsLox
{
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
            var expr = Comparison();
        }

        private Expression Comparison()
        {

        }

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
            return _tokens[_current].Type == typ;
        }
        

        private Token Advance()
        {
            return _tokens[_current++];
        }



    }
}