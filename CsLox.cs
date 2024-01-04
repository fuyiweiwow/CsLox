using System;

namespace CsLox
{
    public class CsLox
    {
        public static void Main(string[] args)
        {
            if(args.Length > 1)
            {
                Console.WriteLine("Usage: CsLox [script]")
            }

            if(args.Length == 1)
            {
                //run File
                RunFile(args[0]);
                return;
            }

            //run prompt


        }

        private static void RunFile(string path)
        {
            var bytes = 
        }


    }
}
