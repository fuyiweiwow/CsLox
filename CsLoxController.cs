namespace CsLox
{
    public class CsLoxController
    {
        private static readonly Lazy<CsLoxController> _instance = new(() => new CsLoxController());
        public static CsLoxController Instance { get{ return _instance.Value; } }

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