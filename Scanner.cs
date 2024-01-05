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
                ScanToken(_source[_current++]);
            }

            _tokens.Add(new Token(TokenType.EOF, "", null, _line));
            return _tokens;
        }

        private void ScanToken(char token)
        {
            if(!TokenExtensions.CharTokenMap.TryGetValue(token, out TokenType value))
            {
                CsLoxController.Instance.Error( _line, $"Unexpected character '{token}'.");
                return;
            }

            AddToken(value);
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