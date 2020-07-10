package com.algorand.javatest.assets.v1;

import java.math.BigInteger;

import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.algod.client.AlgodClient;
import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.algod.client.model.AssetHolding;
import com.algorand.algosdk.algod.client.model.TransactionID;
import com.algorand.algosdk.algod.client.model.TransactionParams;
import com.algorand.algosdk.crypto.Digest;
import com.algorand.algosdk.transaction.SignedTransaction;
import com.algorand.algosdk.transaction.Transaction;
import com.algorand.algosdk.util.Encoder;

/**
 * Show Creating, modifying, sending and listing assets
 */
public class OptinAsset {

    public AlgodApi algodApiInstance = null;

    // utility function to connect to a node
    private AlgodApi connectToNetwork(){

        //sandbox
        final String ALGOD_API_ADDR = "http://localhost:4001";
        final String ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        AlgodClient client = (AlgodClient) new AlgodClient().setBasePath(ALGOD_API_ADDR);
        ApiKeyAuth api_key = (ApiKeyAuth) client.getAuthentication("api_key");
        api_key.setApiKey(ALGOD_API_TOKEN);
        algodApiInstance = new AlgodApi(client);   
        return algodApiInstance;
    }

    // Inline class to handle changing block parameters
    // Throughout the example
    public class ChangingBlockParms {
        public BigInteger fee;
        public BigInteger firstRound;
        public BigInteger lastRound;
        public String genID;
        public Digest genHash;

        public ChangingBlockParms() {
            this.fee = BigInteger.valueOf(0);
            this.firstRound = BigInteger.valueOf(0);
            this.lastRound = BigInteger.valueOf(0);
            this.genID = "";
            this.genHash = null;
        }
    };

    // Utility function to wait on a transaction to be confirmed

    public void waitForConfirmation(String txID) throws Exception {
        if (algodApiInstance == null)
            connectToNetwork();
        long lastRound = algodApiInstance.getStatus().getLastRound().longValue();
        while (true) {
            try {
                // Check the pending tranactions
                com.algorand.algosdk.algod.client.model.Transaction pendingInfo = algodApiInstance
                        .pendingTransactionInformation(txID);
                if (pendingInfo.getRound() != null && pendingInfo.getRound().longValue() > 0) {
                    // Got the completed Transaction
                    System.out.println("Transaction " + pendingInfo.getTx() + " confirmed in round "
                            + pendingInfo.getRound().longValue());
                    break;
                }
                lastRound++;
                algodApiInstance.waitForBlock(BigInteger.valueOf(lastRound));
            } catch (Exception e) {
                throw (e);
            }
        }
    }

    // Utility function to update changing block parameters
    public ChangingBlockParms getChangingParms(AlgodApi algodApiInstance) throws Exception {
        ChangingBlockParms cp = new OptinAsset.ChangingBlockParms();
        try {
            TransactionParams params = algodApiInstance.transactionParams();
            cp.fee = params.getFee();
            cp.firstRound = params.getLastRound();
            cp.lastRound = cp.firstRound.add(BigInteger.valueOf(1000));
            cp.genID = params.getGenesisID();
            cp.genHash = new Digest(params.getGenesishashb64());

        } catch (ApiException e) {
            throw (e);
        }
        return (cp);
    }

    // Utility function for sending a raw signed transaction to the network
    public TransactionID submitTransaction(SignedTransaction signedTx) throws Exception {
        try {
            // Msgpack encode the signed transaction
            byte[] encodedTxBytes = Encoder.encodeToMsgPack(signedTx);
            TransactionID id = algodApiInstance.rawTransaction(encodedTxBytes);
            return (id);
        } catch (ApiException e) {
            throw (e);
        }
    }

    public static void main(String args[]) throws Exception {

        OptinAsset ex = new OptinAsset();
        AlgodApi algodApiInstance= ex.connectToNetwork();

        // recover example accounts

        // final String account1_mnemonic = "<your-25-word-mnemonic>";             
        // final String account2_mnemonic = "<your-25-word-mnemonic>";             
        // final String account3_mnemonic = "<your-25-word-mnemonic>";  

        // final String account1_mnemonic = "buzz genre work meat fame favorite rookie stay tennis demand panic busy hedgehog snow morning acquire ball grain grape member blur armor foil ability seminar";             
        // final String account2_mnemonic = "design country rebuild myth square resemble flock file whisper grunt hybrid floor letter pet pull hurry choice erase heart spare seven idea multiply absent seven";             
        // final String account3_mnemonic = "news slide thing empower naive same belt evolve lawn ski chapter melody weasel supreme abuse main olive sudden local chat candy daughter hand able drip";  

        final String account1_mnemonic = "canal enact luggage spring similar zoo couple stomach shoe laptop middle wonder eager monitor weather number heavy skirt siren purity spell maze warfare ability ten";
        final String account2_mnemonic = "beauty nurse season autumn curve slice cry strategy frozen spy panic hobby strong goose employ review love fee pride enlist friend enroll clip ability runway";
        final String account3_mnemonic = "picnic bright know ticket purity pluck stumble destroy ugly tuna luggage quote frame loan wealth edge carpet drift cinnamon resemble shrimp grain dynamic absorb edge";

        Account acct1  = new Account(account1_mnemonic); 
        Account acct2  = new Account(account2_mnemonic);
        Account acct3  = new Account(account3_mnemonic);                           
        System.out.println("Account1: " + acct1.getAddress());
        System.out.println("Account2: " + acct2.getAddress());
        System.out.println("Account3: " + acct3.getAddress());

 
        // Get changing network parameters
        ChangingBlockParms cp = null;
        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }
        // insert opt in here
        // copy assetID from Create an Asset tutorial
        BigInteger assetID = BigInteger.valueOf(2653901);

        // Opt in to Receiving the Asset

        Transaction tx = Transaction.AssetAcceptTransactionBuilder().acceptingAccount(acct3.getAddress()).fee(0)
                .firstValid(cp.firstRound).lastValid(cp.lastRound).genesisHash(cp.genHash).assetIndex(assetID).build();

        // Update the fee based on the network suggested fee
        Account.setFeeByFeePerByte(tx, cp.fee);
        // The transaction must be signed by the current manager account
        SignedTransaction signedTx = acct3.signTransaction(tx);
        com.algorand.algosdk.algod.client.model.Account act;
        // send the transaction to the network and
        try {
            TransactionID id = ex.submitTransaction(signedTx);
            System.out.println("Transaction ID: " + id);
            ex.waitForConfirmation(signedTx.transactionID);
            // We can now list the account information for acct3
            // and see that it can accept the new asseet
            act = algodApiInstance.accountInformation(acct3.getAddress().toString());
            AssetHolding ah = act.getHolding(assetID);
            System.out.println("Account 3 Asset Holding: " + ah.getAmount());

        } catch (Exception e) {
            e.printStackTrace();
            return;
        }


    }
}

// resource https://developer.algorand.org/docs/features/asa/