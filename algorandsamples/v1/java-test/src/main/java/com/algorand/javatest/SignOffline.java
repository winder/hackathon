package com.algorand.javatest;

import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.crypto.Address;
import com.algorand.algosdk.crypto.Digest;
import com.algorand.algosdk.transaction.SignedTransaction;
import com.algorand.algosdk.transaction.Transaction;


import java.io.FileOutputStream;
import java.io.ObjectOutputStream;

import java.math.BigInteger;


/**
 * Sign Offline
 *
 */
public class SignOffline {
    public static void main(String args[]) throws Exception {


        // Using a backup mnemonic to recover the source account to send tokens from
         final String DEST_ADDR = "KV2XGKMXGYJ6PWYQA5374BYIQBL3ONRMSIARPCFCJEAMAHQEVYPB7PL3KU";
        // if an account drops below the minimum where should the remainding funds be
        // sent
        //final String REM_ADDR = "KV2XGKMXGYJ6PWYQA5374BYIQBL3ONRMSIARPCFCJEAMAHQEVYPB7PL3KU";

        // Instaniate the transaction with a new account
        //If you are doing this offline you need to either instantiate 
        //the account with a backup mnemonic or have the pk/sk pair stored somewhere
        Account src = new Account();
        System.out.println( "New Account Address: " + src.getAddress());
        //the last two parameters are for the genesishash and genesisid. When mainnet goes live only
        //genesishash parameter will be needed. the genesisid parameter will be deprecated. you will
        //either need to contact a server to ge the genesishash or get it manually and just hardcode into the offline app
        //It is also better to connect to the algod to get suggested parameters
        //If you do this on another computer you can just serialize the tx like we do later in this example
        //for the signed tx
        Transaction tx = new Transaction(src.getAddress(),  new Address(DEST_ADDR), BigInteger.valueOf(1000), BigInteger.valueOf(100), BigInteger.valueOf(600700), BigInteger.valueOf(601700), "", new Digest());
        // Sign the Transaciton
        SignedTransaction signedTx = src.signTransaction(tx);
        try { 
            System.out.println("Signed transaction with txid: " + signedTx.transactionID);
            //Saving of object in a file 
            FileOutputStream file = new FileOutputStream("./tx.sav"); 
            ObjectOutputStream out = new ObjectOutputStream(file); 
            out.writeObject(signedTx);
            out.close(); 
            file.close();
        } catch (Exception e) { 
            System.out.println("Exception: " + e); 
        }


    }

}
