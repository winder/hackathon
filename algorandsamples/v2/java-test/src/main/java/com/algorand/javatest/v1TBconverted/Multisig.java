package com.algorand.javatest;

import java.io.Console;
import java.io.IOException;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.List;

import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.algod.client.AlgodClient;
import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.algod.client.model.TransactionID;
import com.algorand.algosdk.algod.client.model.TransactionParams;
import com.algorand.algosdk.crypto.Address;
import com.algorand.algosdk.crypto.Digest;
import com.algorand.algosdk.crypto.Ed25519PublicKey;
import com.algorand.algosdk.crypto.MultisigAddress;
import com.algorand.algosdk.transaction.SignedTransaction;
import com.algorand.algosdk.transaction.Transaction;
import com.algorand.algosdk.util.Encoder;

/**
 * Test Multisignature
 *
 */
public class Multisig {

    public static void waitForEnter(String message) {
        Console c = System.console();
        if (c != null) {
            // printf-like arguments
            if (message != null)
                c.format(message);
            c.format("\nPress ENTER to proceed.\n");
            c.readLine();
        }
    }

    public static void main(String args[]) throws Exception {
        // Algorand Hackathon
        final String ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";
        final String ALGOD_API_TOKEN = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";

        // your own node
        // final String ALGOD_API_ADDR = "http://localhost:8080";
        // final String ALGOD_API_TOKEN = "your ALGOD_API_TOKEN";

        // Purestake
        // final String ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps1";
        // final String ALGOD_API_TOKEN = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab";

        final String DEST_ADDR = "KV2XGKMXGYJ6PWYQA5374BYIQBL3ONRMSIARPCFCJEAMAHQEVYPB7PL3KU";

        // List for Pks for multisig account
        List<Ed25519PublicKey> publicKeys = new ArrayList<>();

        // Create a random new account
        Account act1 = new Account();

        // Create a random new account
        Account act2 = new Account();

        // Create a random new account
        Account act3 = new Account();

        publicKeys.add(act1.getEd25519PublicKey());
        publicKeys.add(act2.getEd25519PublicKey());
        publicKeys.add(act3.getEd25519PublicKey());

        // Instantiate the the Multisig Accout
        MultisigAddress msa = new MultisigAddress(1, 2, publicKeys);

        System.out.println("Multisignature Address: " + msa.toString());
        waitForEnter("Use TestNet Dispenser to add funds, wait for the transaction to be finalized and press enter");

        // Connect to node to create a transaction with proper parameters
        AlgodClient client = (AlgodClient) new AlgodClient().setBasePath(ALGOD_API_ADDR);
        // needed for Purestake , else ignored
        client.addDefaultHeader("X-API-Key", ALGOD_API_TOKEN);
        ApiKeyAuth api_key = (ApiKeyAuth) client.getAuthentication("api_key");
        api_key.setApiKey(ALGOD_API_TOKEN);
        AlgodApi algodApiInstance = new AlgodApi(client);

        // get last round and suggested tx fee
        BigInteger suggestedFeePerByte = BigInteger.valueOf(1);
        BigInteger firstRound = BigInteger.valueOf(301);
        String genId = null;
        Digest genesisHash = null;
        try {
            // Account act = algodApiInstance.accountInformation(address);
            // act.getAmount();
            // Get suggested parameters from the node
            TransactionParams params = algodApiInstance.transactionParams();
            suggestedFeePerByte = params.getFee();
            firstRound = params.getLastRound();
            System.out.println("Suggested Fee: " + suggestedFeePerByte);
            // genesisID and genesisHash are optional on testnet, but will be mandatory on
            // release
            // to ensure that transactions are valid for only a single chain. GenesisHash is
            // preferred.
            // genesisID will be deprecated soon.
            genId = params.getGenesisID();
            genesisHash = new Digest(params.getGenesishashb64());

        } catch (ApiException e) {
            System.err.println("Exception when calling algod#transactionParams");
            e.printStackTrace();
        }

        // add some notes to the transaction
        byte[] notes = "These are some notes encoded in some way!".getBytes();

        BigInteger amount = BigInteger.valueOf(2000000);
        BigInteger lastRound = firstRound.add(BigInteger.valueOf(1000)); // 1000 is the max tx window
        // Setup Transaction
        // Use a fee of 0 as we will set the fee per
        // byte when we sign the tx and overwrite it
        Transaction tx = new Transaction(new Address(msa.toString()), BigInteger.valueOf(1000), firstRound, lastRound,
                notes, amount, new Address(DEST_ADDR), genId, genesisHash);

        // Sign the Transaction for two accounts
        SignedTransaction signedTx = act1.signMultisigTransaction(msa, tx);
        SignedTransaction completeTx = act2.appendMultisigTransaction(msa, signedTx);

        // send the transaction to the network
        try {
            // Msgpack encode the signed transaction
            byte[] encodedTxBytes = Encoder.encodeToMsgPack(completeTx);
            TransactionID id = algodApiInstance.rawTransaction(encodedTxBytes);
            System.out.println("Successfully sent tx with id: " + id);
        } catch (ApiException e) {
            // This is generally expected, but should give us an informative error message.
            System.err.println("Exception when calling algod#rawTransaction: " + e.getResponseBody());
        }

    }

}