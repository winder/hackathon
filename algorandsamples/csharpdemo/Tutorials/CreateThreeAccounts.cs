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
    public class CreateThreeAccounts
    {
        public static void Main(params string[] args)
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
            
            //CreateOneAccount.Main(args); return;

            Account myAccount = new Account();
            var myMnemonic = myAccount.ToMnemonic();
            Console.WriteLine("Account 1 Address = " + myAccount.Address.ToString());
            Console.WriteLine("Account 1 Mnemonic = " + myMnemonic.ToString());
            Console.WriteLine("You have successefully created 1 account.");

            Account myAccount2 = new Account();
            var myMnemonic2 = myAccount2.ToMnemonic();
            Console.WriteLine("Account 2 Address = " + myAccount2.Address.ToString());
            Console.WriteLine("Account 2 Mnemonic = " + myMnemonic2.ToString());


            Account myAccount3 = new Account();
            var myMnemonic3 = myAccount3.ToMnemonic();
            Console.WriteLine("Account 3 Address = " + myAccount3.Address.ToString());
            Console.WriteLine("Account 3 Mnemonic = " + myMnemonic3.ToString());
            Console.WriteLine("You have successefully created 3 accounts.");
        }

    }


}
