namespace CsLox
{
    public class Token(TokenType type, string lexeme, object? literal, int line)
    {
        TokenType _type = type;
        string _lexeme = lexeme;
        object? _literal = literal;
        int _line = line;

        public override string ToString()
        {
            return $@"{_type}"" ""{_lexeme}"" ""{_literal}";
        }
    }

}