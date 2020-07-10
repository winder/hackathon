using System;

namespace Tutorials
{
    class Tutorial
    {
        static void Main(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("args is null"); // Check for null array
            }
            else
            {
                args = new string[2];
                // sandbox - see https://github.com/algorand/sandbox
                args[0] = "http://localhost:4001";
                args[1] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            }         
            CreateThreeAccounts.Main(args); return;
        }

    }


}
