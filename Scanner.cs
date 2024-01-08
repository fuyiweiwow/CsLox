namespace CsLox
{
    public class Scanner(string source)
    {
        string _source = source;
        List<Token> _tokens = new List<Token>();
        int _start = 0;
        int _current = 0;
        int _line = 1;
        private static Dictionary<string, TokenType> _keywordMap = new()
        {
            {"and", TokenType.And},
            {"or", TokenType.Or},
            {"class", TokenType.Class},
            {"else", TokenType.Else},
            {"false", TokenType.False},
            {"fun", TokenType.Fun},
            {"for", TokenType.For},
            {"if", TokenType.If},
            {"nil", TokenType.Nil},
            {"print", TokenType.Print},
            {"return", TokenType.Return},
            {"super", TokenType.Super},
            {"this", TokenType.This},
            {"var", TokenType.Var},
            {"while", TokenType.While},
        };


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
                    return;
                default:
                    if(char.IsDigit(token))
                    {
                        ScanNumber();
                        return;
                    }
                    else if(IsAlaph(token))
                    {
                        ScanIdentifier();
                        return;
                    }
                    else
                    {
                        CsLoxController.Instance.Error( _line, $"Unexpected character '{token}'.");
                    }
                    
                    break;
            }

            if(tt == TokenType.Undefined || tt == TokenType.Useless)
                return;

            AddToken(tt);
        }

        /// <summary>
        /// Do scan an legal identifier
        /// </summary>
        private void ScanIdentifier()
        {
            while(IsAlaphOrDigit(Peek()))
            {
                Advance();
            }

            //filter keywords
            string text = _source.Substring(_start, _current - 1);
            
            TokenType realType = TokenType.Undefined;
            if(_keywordMap.TryGetValue(text, out TokenType t))
            {
                realType = t;
            }

            AddToken(realType);
        }

        private bool IsAlaphOrDigit(char c)
        {
            return char.IsDigit(c) || IsAlaph(c);
        }


        /// <summary>
        /// Check whether a token is a legallly none keyword
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsAlaph(char c)
        {
            return char.IsLetter(c) || c == '_'; 
        }

        /// <summary>
        /// Do scan a number like 1.234, exclude things like .1234 or 1234.
        /// </summary>
        private void ScanNumber()
        {
            while(char.IsDigit(Peek()))
            {
                Advance();
            }

            if(Peek() == '.' && char.IsDigit(PeekNext()))
            {
                Advance();
                while(char.IsDigit(Peek()))
                {
                    Advance();
                }
            }

            AddToken(TokenType.Number, 
            Double.TryParse(_source.Substring(_start, _current - 1), out double result) 
            ? result 
            : double.NaN);
        }

        /// <summary>
        /// Do scan a string starts and ends with '"'
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

        /// <summary>
        /// Get next character (if it exists)
        /// </summary>
        /// <returns></returns>
        private char PeekNext()
        {
            if(_current == _source.Length - 1)
            {
                return '\0';
            }

            return _source[_current + 1];
        }

        /// <summary>
        /// Get current character
        /// </summary>
        /// <returns></returns>
        private char Peek()
        {
            if(IsAtEnd())
            {
                return '\0';
            }

            return _source[_current];
        }


        /// <summary>
        /// Check token with two characters
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

        /// <summary>
        /// Move cursor in character array
        /// </summary>
        /// <returns></returns>
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