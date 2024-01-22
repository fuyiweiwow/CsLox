namespace CsLox
{
    public class CsLoxLogger
    {
        private static readonly Lazy<CsLoxLogger> _instance = new(() => new CsLoxLogger());
        public static CsLoxLogger Instance { get{ return _instance.Value; } }

        public bool HasError { get; set; } = false;

        /// <summary>
        /// Raise an error
        /// </summary>
        /// <param name="line">Line number of source</param>
        /// <param name="message">Exact message</param>
        public void Error(int line, string message)
        {
            Report(line, "", message);
        }

        /// <summary>
        /// Record error of an exact token 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="errorMessage"></param>
        public void Error(Token token, string errorMessage)
        {
            if(token.Type == TokenType.EOF)
            {
                Report(token.Line, "at end", errorMessage);
                return;
            }

            Report(token.Line, $"at '{token.Lexeme}'", errorMessage);
        }


        /// <summary>
        /// Error message concat
        /// </summary>
        /// <param name="line">Line number of source</param>
        /// <param name="where"></param>
        /// <param name="message">Exact message</param>
        private void Report(int line, string where, string message)
        {
            Console.Error.WriteLine($@"line ""{line}"" Error{where}: {message}");
            HasError = true;
        }






    }



}