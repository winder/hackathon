package com.algorand.javatest;

import java.io.ByteArrayOutputStream;
import java.math.BigInteger;

import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.algod.client.AlgodClient;
import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.algod.client.model.TransactionID;
import com.algorand.algosdk.algod.client.model.TransactionParams;
import com.algorand.algosdk.crypto.Digest;
import com.algorand.algosdk.transaction.SignedTransaction;
import com.algorand.algosdk.transaction.Transaction;
import com.algorand.algosdk.transaction.TxGroup;
import com.algorand.algosdk.util.Encoder;

public class TutorialGroupedTransaction {

    public AlgodApi algodApiInstance = null;

    // utility function to connect to a node
    private AlgodApi connectToNetwork(){
        final String ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";
        final String ALGOD_API_TOKEN = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";

        // your own node
        // final String ALGOD_API_ADDR = "http://localhost:8080";
        // final String ALGOD_API_TOKEN = "your ALGOD_API_TOKEN";

        // Purestake
        // final String ALGOD_API_ADDR =
        // "https://testnet-algorand.api.purestake.io/ps1";
        // final String ALGOD_API_TOKEN = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab";

        AlgodClient client = (AlgodClient) new AlgodClient().setBasePath(ALGOD_API_ADDR);
        ApiKeyAuth api_key = (ApiKeyAuth) client.getAuthentication("api_key");
        api_key.setApiKey(ALGOD_API_TOKEN);
        algodApiInstance = new AlgodApi(client);   
        return algodApiInstance;
    }

    // Inline class to handle changing block parameters
    // Throughout the example
    static class ChangingBlockParms {
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

    // Utility function to update changing block parameters
    public static ChangingBlockParms getChangingParms(AlgodApi algodApiInstance) throws Exception {
        ChangingBlockParms cp = new TutorialGroupedTransaction.ChangingBlockParms();
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

    public void waitForConfirmation(String txID) throws Exception {
        if (algodApiInstance == null)
            connectToNetwork();
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
                algodApiInstance
                        .waitForBlock(BigInteger.valueOf(algodApiInstance.getStatus().getLastRound().longValue() + 1));
            } catch (Exception e) {
                throw (e);
            }
        }

    }

    public void AtomicTransfer() throws Exception {

        if( algodApiInstance == null ) connectToNetwork();;

        // final String account1_mnemonic = "<25-word-passphrase>";
        // final String account2_mnemonic = "<25-word-passphrase>";
        // final String account3_mnemonic = "<25-word-passphrase>";

        final String account1_mnemonic = "buzz genre work meat fame favorite rookie stay tennis demand panic busy hedgehog snow morning acquire ball grain grape member blur armor foil ability seminar";
        final String account2_mnemonic = "design country rebuild myth square resemble flock file whisper grunt hybrid floor letter pet pull hurry choice erase heart spare seven idea multiply absent seven";
        final String account3_mnemonic = "news slide thing empower naive same belt evolve lawn ski chapter melody weasel supreme abuse main olive sudden local chat candy daughter hand able drip";

        // recover account A, B, C
        Account acctA  = new Account(account1_mnemonic); 
        Account acctB  = new Account(account2_mnemonic);
        Account acctC  = new Account(account3_mnemonic); 

        // get node suggested parameters
        ChangingBlockParms cp = null;
        try {
            cp = getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }           

        // Create the first transaction
        Transaction tx1 = new Transaction(acctA.getAddress(), 
            acctC.getAddress(), 10000, cp.firstRound.intValue(), 
            cp.lastRound.intValue(), null, cp.genHash);
        tx1.fee = BigInteger.valueOf(1000);

        // Create the second transaction
        Transaction tx2 = new Transaction(acctB.getAddress(), 
            acctA.getAddress(), 20000, cp.firstRound.intValue(), 
            cp.lastRound.intValue(), null, cp.genHash);
        tx2.fee = BigInteger.valueOf(1000);

        // group transactions an assign ids
        Digest gid = TxGroup.computeGroupID(new Transaction[]{tx1, tx2});
        tx1.assignGroupID(gid);
        tx2.assignGroupID(gid);

        // sign individual transactions
        SignedTransaction signedTx1 = acctA.signTransaction(tx1);;
        SignedTransaction signedTx2 = acctB.signTransaction(tx2);;

        try {
            // put both transaction in a byte array 
            ByteArrayOutputStream byteOutputStream = new ByteArrayOutputStream( );
            byte[] encodedTxBytes1 = Encoder.encodeToMsgPack(signedTx1);
            byte[] encodedTxBytes2 = Encoder.encodeToMsgPack(signedTx2);
            byteOutputStream.write(encodedTxBytes1);
            byteOutputStream.write(encodedTxBytes2);
            byte groupTransactionBytes[] = byteOutputStream.toByteArray();

            // write transaction to node
            TransactionID id = algodApiInstance.rawTransaction(groupTransactionBytes);
            System.out.println("Successfully sent tx group with first tx id: " + id);
            waitForConfirmation(id.getTxId());

        } catch (Exception e) {
            System.out.println("Submit Exception: " + e); 
        }
    }

    public static void main(String args[]) throws Exception {
        TutorialGroupedTransaction mn = new TutorialGroupedTransaction();
        mn.AtomicTransfer();
    }
}