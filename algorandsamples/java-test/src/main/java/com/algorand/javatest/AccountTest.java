package com.algorand.javatest;
import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.crypto.Address;
/**
 * Test Account Class
 *
 */
public class AccountTest 
{
    public static void main(String args[]) throws Exception {
            //Create a random new account
            Account act = new Account();
            //Get the new account address
            Address addr = act.getAddress();
            
            //Get the backup phrase
            String backup = act.toMnemonic();

            System.out.println("Account Address: " + addr.toString());
            System.out.println("Backup Phrase: " + backup);

            //recover an account
            Account recoveredAccount = new Account(backup);
            System.out.println("Recovered Account Address: " + recoveredAccount.getAddress().toString());
    }

}
