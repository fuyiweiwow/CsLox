namespace CsLox
{
    public enum TokenType
    {
        Undefined,
        Useless,
        //Single character
        LeftParen = 0,  //'('
        RightParen,     //')'
        LeftBrace,      //'{'
        RightBrace,     //'}'
        Comma,          //','
        Dot,            //'.'
        Minus,          //'-'
        Plus,           //'+'
        SemiColon,      //';'
        Slash,          //'/'
        Star,           //'*'

        //One or two characters
        Not,            //'!'
        NotEqual,       //'!='
        Equal,          //'='
        DoubleEqual,    //'=='
        Greater,        //'>'
        GreaterAndEqual,//'>='
        Less,           //'<'
        LessAndEqual,   //'<='

        //literals
        Identifier,
        String,
        Number,

        //Keywords 
        And,
        Class,
        Else,
        False,
        True,
        Fun,
        For,
        If,
        Nil,
        Or,
        Print,
        Return,
        Super,
        This,
        Var,
        While,
        EOF
    }


}