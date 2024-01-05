using System;
using System.Text;

namespace CsLox
{
    public class CsLox
    {
        public static void Main(string[] args)
        {
            if(args.Length > 1)
            {
                Console.WriteLine("Usage: CsLox [script]");
                Environment.Exit(64);
            }

            if(args.Length == 1)
            {
                RunFile(args[0]);
                return;
            }

            RunPrompt();
        }

        /// <summary>
        /// Read a file and run it
        /// </summary>
        /// <param name="path">source file path</param>
        private static void RunFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(Path.GetFullPath(path));
            Run(Encoding.Default.GetString(bytes));
            if (CsLoxController.Instance.HasError) 
            {
                Environment.Exit(65);
            }
        }

        /// <summary>
        /// Run with console
        /// </summary>
        private static void RunPrompt()
        {
            while(true)
            {
                Console.Write(">>");
                string? line = Console.ReadLine();
                if(line is null)
                    break;
                Run(line);
                CsLoxController.Instance.HasError = false;
            }
        }

        /// <summary>
        /// The real run
        /// </summary>
        /// <param name="source">code source</param>
        private static void Run(string source)
        {
            var scanner = new Scanner(source);
            List<Token> tokens = scanner.ScanTokens();

            foreach(var token in tokens)
            {
                Console.WriteLine(token);
            }
        }




    }
}
