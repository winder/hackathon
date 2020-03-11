package com.algorand.javatest;
import com.algorand.algosdk.account.Account;

public class TutorialCreateAccounts {
    public static void main(String args[]) throws Exception {
        Account myAccount1 = new Account();
        System.out.println("My Address1: " + myAccount1.getAddress());
        System.out.println("My Passphrase1: " + myAccount1.toMnemonic());
        Account myAccount2 = new Account();
        System.out.println("My Address2: " + myAccount2.getAddress());
        System.out.println("My Passphrase2: " + myAccount2.toMnemonic());
        Account myAccount3 = new Account();
        System.out.println("My Address3: " + myAccount3.getAddress());
        System.out.println("My Passphrase3: " + myAccount3.toMnemonic());

        //Copy off accounts and mnemonics    
        //Dispense TestNet Algos to each account: https://bank.testnet.algorand.network/
    }
}