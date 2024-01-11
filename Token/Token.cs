namespace CsLox
{
    public class Token(TokenType type, string lexeme, object? literal, int line)
    {
        TokenType _type = type;
        string _lexeme = lexeme;
        object? _literal = literal;
        int _line = line;

        public TokenType Type { get => _type; set => _type = value; }
        public string Lexeme { get => _lexeme; set => _lexeme = value; }
        public object? Literal { get => _literal; set => _literal = value; }
        public int Line { get => _line; set => _line = value; }

        public override string ToString()
        {
            return $@"{Type}"" ""{Lexeme}"" ""{Literal}";
        }
    }

}