namespace CsLox
{
    public class Scanner(string source)
    {
        string _source = source;
        List<Token> _tokens = new List<Token>();
        int _start = 0;
        int _current = 0;
        int _line = 1;

        public List<Token> ScanTokens()
        {
            while(!IsAtEnd())
            {
                _start = _current;
                ScanToken(Advance());
            }

            _tokens.Add(new Token(TokenType.EOF, "", null, _line));
            return _tokens;
        }

        private void ScanToken(char token)
        {
            TokenType tt = TokenType.Undefined;
            switch(token)
            {
                case '(':
                    tt = TokenType.LeftParen;
                    break;
                case ')':
                    tt = TokenType.RightParen; 
                    break;
                case '{':
                    tt = TokenType.LeftBrace;
                    break;
                case '}':
                    tt = TokenType.RightParen;
                    break;
                case ',':
                    tt = TokenType.Comma;
                    break;
                case '.':
                    tt =  TokenType.Dot;
                    break;
                case '-':
                    tt = TokenType.Minus;
                    break;
                case '+':
                    tt = TokenType.Plus;
                    break;
                case ';':
                    tt = TokenType.SemiColon;
                    break;
                case '*':
                    tt = TokenType.Star;
                    break;
                case '!':
                    tt = CheckNext('=') ? TokenType.NotEqual : TokenType.Not;
                    break;
                case '=':
                    tt = CheckNext('=') ? TokenType.DoubleEqual : TokenType.Equal;
                    break;
                case '>':
                    tt = CheckNext('=') ? TokenType.GreaterAndEqual : TokenType.Greater;
                    break;
                case '<':
                    tt = CheckNext('=') ? TokenType.LessAndEqual : TokenType.Less;
                    break;
                case '/':
                    if(CheckNext('/'))
                    {
                        //this is a comment
                        while(Peek() != '\n' && !IsAtEnd())
                        {
                            Advance();
                        }
                        tt = TokenType.Useless;
                    }
                    else
                    {
                        tt = TokenType.Slash;
                    }
                    break;
                case ' ':
                case '\r':
                case '\t':
                    //white space 
                    tt = TokenType.Useless;
                    break;
                case '\n':
                    tt = TokenType.Useless;
                    ++_line;
                    break;
                case '"':
                    ScanString();
                    break;
                default:
                    CsLoxController.Instance.Error( _line, $"Unexpected character '{token}'.");
                    break;
            }

            if(tt == TokenType.Undefined || tt == TokenType.Useless)
                return;

            AddToken(tt);
        }

        /// <summary>
        /// Do scan string start and end with '"'
        /// </summary>
        private void ScanString()
        {
            while(Peek() != '"' && !IsAtEnd())
            {
                if(Peek() == '\n')
                {
                    ++_line;
                }

                Advance();
            }

            if(IsAtEnd())
            {
                CsLoxController.Instance.Error(_line, "Unterminated string.");
                return;
            }

            Advance();
            string strVal = _source.Substring(_start + 1, _current - 1);
            AddToken(TokenType.String, strVal);
        }


        private char Peek()
        {
            if(IsAtEnd())
            {
                return '\0';
            }

            return _source[_current];
        }


        /// <summary>
        /// check token with two characters
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        private bool CheckNext(char next)
        {
            if(IsAtEnd())
            {
                return false;
            }

            if(_source[_current] != next)
            {
                return false;
            }

            ++_current;
            return true;
        }

        private char Advance()
        {
            return _source[_current++];
        }

        private void AddToken(TokenType tokenType)
        {
            AddToken(tokenType, null);
        }

        private void AddToken(TokenType tokenType, object? literal)
        {
            string text = _source.Substring(_start, _current);
            _tokens.Add(new Token(tokenType, text, literal, _line));
        }

        private bool IsAtEnd()
        {
            return _current >= _source.Length;
        }

    }

}