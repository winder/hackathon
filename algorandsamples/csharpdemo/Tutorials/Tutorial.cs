using System;
using System.Collections.Generic;
using Algorand;
using Account = Algorand.Account;
using Algorand.Algod.Client.Api;
using Algorand.Algod.Client.Model;
using Algorand.Algod.Client;
using Transaction = Algorand.Transaction;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;

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
                args[0] = "http://hackathon.algodev.network:9100";
                args[1] = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
            }
            
            CreateThreeAccounts.Main(args); return;


        }

    }


}
