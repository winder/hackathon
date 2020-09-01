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

                args[0] = "http://localhost:4001";
                args[1] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            }

            Account myAccount = new Account();
            var myMnemonic = myAccount.ToMnemonic();
            Console.WriteLine("Account 1 Address = " + myAccount.Address.ToString());
            Console.WriteLine("Account 1 Mnemonic = " + myMnemonic.ToString());

            Account myAccount2 = new Account();
            var myMnemonic2 = myAccount2.ToMnemonic();
            Console.WriteLine("Account 2 Address = " + myAccount2.Address.ToString());
            Console.WriteLine("Account 2 Mnemonic = " + myMnemonic2.ToString());

            Account myAccount3 = new Account();
            var myMnemonic3 = myAccount3.ToMnemonic();
            Console.WriteLine("Account 3 Address = " + myAccount3.Address.ToString());
            Console.WriteLine("Account 3 Mnemonic = " + myMnemonic3.ToString());

            Console.WriteLine("You have successefully created 3 accounts.");
            Console.WriteLine("Dispense funds to these 3 accounts ");
            Console.WriteLine("see TestNet dispenser https://bank.testnet.algorand.network/");

        }

    }


}
