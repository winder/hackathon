package com.algorand.javatest;

import com.algorand.algosdk.algod.client.AlgodClient;
import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.crypto.Address;
import com.algorand.algosdk.algod.client.model.TransactionParams;
import com.algorand.algosdk.crypto.Digest;
import com.algorand.algosdk.crypto.Ed25519PublicKey;
import com.algorand.algosdk.algod.client.model.TransactionID;
import com.algorand.algosdk.transaction.Transaction;
import com.algorand.algosdk.util.Encoder;

import com.algorand.algosdk.crypto.MultisigAddress;
import com.algorand.algosdk.transaction.SignedTransaction;

import java.math.BigInteger;
import java.util.ArrayList;
import java.util.List;
import java.nio.file.Files;
import java.nio.file.Paths;

public class SaveMultisigTransactionOffline {
    public AlgodApi algodApiInstance = null;

    // utility function to connect to a node
    private AlgodApi connectToNetwork() {

        // Initialize an algod client
        final String ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";
        final String ALGOD_API_TOKEN = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";

        // final String ALGOD_API_ADDR = "algod-address<PLACEHOLDER>";
        // final String ALGOD_API_TOKEN = "algod-token<PLACEHOLDER>";
        AlgodClient client = (AlgodClient) new AlgodClient().setBasePath(ALGOD_API_ADDR);
        ApiKeyAuth api_key = (ApiKeyAuth) client.getAuthentication("api_key");
        api_key.setApiKey(ALGOD_API_TOKEN);
        algodApiInstance = new AlgodApi(client);
        return algodApiInstance;
    }

    // utility function to wait on a transaction to be confirmed
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

    public void writeMultisigUnsignedTransaction() {
        // connect to node
        if (algodApiInstance == null)
            connectToNetwork();
        try {
            // either create and fund new accounts...
            // Account acct1 = new Account();
            // Account acct2 = new Account();
            // Account acct3 = new Account();
            // System.out.println("Account 1 Address: " + acct1.getAddress());
            // System.out.println("Account 2 Address: " + acct2.getAddress());
            // System.out.println("Account 3 Address: " + acct3.getAddress());
            // System.out.println("Account 1 Mnemonic: " + acct1.toMnemonic());
            // System.out.println("Account 2 Mnemonic: " + acct2.toMnemonic());
            // System.out.println("Account 3 Mnemonic: " + acct3.toMnemonic());
            // Please go to: https://bank.testnet.algorand.network/ to fund your accounts.

            // ... or recover existing accounts
            final String account1_mnemonic = "buzz genre work meat fame favorite rookie stay tennis demand panic busy hedgehog snow morning acquire ball grain grape member blur armor foil ability seminar";
            final String account2_mnemonic = "design country rebuild myth square resemble flock file whisper grunt hybrid floor letter pet pull hurry choice erase heart spare seven idea multiply absent seven";
            final String account3_mnemonic = "news slide thing empower naive same belt evolve lawn ski chapter melody weasel supreme abuse main olive sudden local chat candy daughter hand able drip";
 
                    // final String account1_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            // final String account2_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            // final String account3_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
       
       
            final Account acct1 = new Account(account1_mnemonic);
            final Account acct2 = new Account(account2_mnemonic);
            final Account acct3 = new Account(account3_mnemonic);

            List<Ed25519PublicKey> publicKeys = new ArrayList<>();
            publicKeys.add(acct1.getEd25519PublicKey());
            publicKeys.add(acct2.getEd25519PublicKey());
            publicKeys.add(acct3.getEd25519PublicKey());
            // Please go to: https://bank.testnet.algorand.network/ to fund your accounts.
            MultisigAddress msig = new MultisigAddress(1, 2, publicKeys);
            System.out.println("Multisig Address: " + msig.toString());

            // Save multisig to a file
            Files.write(Paths.get("./unsigned.msig"), Encoder.encodeToMsgPack(msig));

            // read multisig from file - not working can not instantiate from JSON object
            // (missing default constructor or creator, or perhaps need to add/enable type
            // information?

            // MultisigAddress decodedMultisig = Encoder
            // .decodeFromMsgPack(Files.readAllBytes(Paths.get("./unsigned.msig")),
            // MultisigAddress.class);
            // System.out.println("Multisig Address: " + decodedMultisig.toString());

            // msig = decodedMultisig;

            // final String DEST_ADDR = "transaction-reciever<PLACEHOLDER>";
            // final String SRC_ADDR = "transaction-sender<PLACEHOLDER>";

            final String DEST_ADDR = "WICXIYCKG672UGFCCUPBAJ7UYZ2X7GZCNBLSAPBXW7M6DZJ5YY6SCXML4A";
            final Address SRC_ADDR = msig.toAddress();

            // Get suggested parameters from the node
            TransactionParams params = algodApiInstance.transactionParams();
            BigInteger firstRound = params.getLastRound();
            String genId = params.getGenesisID();
            Digest genesisHash = new Digest(params.getGenesishashb64());

            // create transaction
            BigInteger lastRound = firstRound.add(BigInteger.valueOf(1000));
            BigInteger fee = BigInteger.valueOf(1000);
         //  fee = params.getFee();
            fee = BigInteger.valueOf(1000);
            BigInteger amount = BigInteger.valueOf(1000000);
            Transaction tx = new Transaction(SRC_ADDR, fee, firstRound, lastRound, null, amount, new Address(DEST_ADDR),
                    genId, genesisHash);

            System.out.println("Unsigned transaction with txid: " + tx.txID());

            // NOTE: save as signed even though it has not been
            SignedTransaction stx = new SignedTransaction();
            stx.tx = tx;
            // Save transaction to a file
            Files.write(Paths.get("./unsigned.txn"), Encoder.encodeToMsgPack(stx));
            System.out.println("Transaction written to a file");

        } catch (Exception e) {
            System.out.println("Save Exception: " + e);
        }

    }

    public void readMultisigUnsignedTransaction() {
        try {
            // recover accounts
            // final String account1_mnemonic = "portion never forward pill lunch organ biology"
            //         + " weird catch curve isolate plug innocent skin grunt"
            //         + " bounce clown mercy hole eagle soul chunk type absorb trim";
            // final String account2_mnemonic = "place blouse sad pigeon wing warrior wild script"
            //         + " problem team blouse camp soldier breeze twist mother"
            //         + " vanish public glass code arrow execute convince ability" + " there";
            // final String account3_mnemonic = "image travel claw climb bottom spot path roast "
            //         + "century also task cherry address curious save item "
            //         + "clean theme amateur loyal apart hybrid steak about blanket";
            // final String account1_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            // final String account2_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            // final String account3_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            final String account1_mnemonic = "buzz genre work meat fame favorite rookie stay tennis demand panic busy hedgehog snow morning acquire ball grain grape member blur armor foil ability seminar";
            final String account2_mnemonic = "design country rebuild myth square resemble flock file whisper grunt hybrid floor letter pet pull hurry choice erase heart spare seven idea multiply absent seven";
            final String account3_mnemonic = "news slide thing empower naive same belt evolve lawn ski chapter melody weasel supreme abuse main olive sudden local chat candy daughter hand able drip";

            final Account acct1 = new Account(account1_mnemonic);
            final Account acct2 = new Account(account2_mnemonic);
            final Account acct3 = new Account(account3_mnemonic);

            // read transaction from file
            SignedTransaction decodedTransaction = Encoder
                    .decodeFromMsgPack(Files.readAllBytes(Paths.get("./unsigned.txn")), SignedTransaction.class);
            System.out.println("Signed transaction with txid: " + decodedTransaction.tx.txID());

            // read multisig from file - not working can not instantiate from JSON object
            // (missing default constructor or creator, or perhaps need to add/enable type
            // information? hotfix forthcoming

            // MultisigAddress decodedMultisig = Encoder
            // .decodeFromMsgPack(Files.readAllBytes(Paths.get("./unsigned.msig")),
            // MultisigAddress.class);
            // System.out.println("Multisig Address: " + decodedMultisig.toString());

            // msig = decodedMultisig;
            // workaround remove these lines when above fixed

            List<Ed25519PublicKey> publicKeys = new ArrayList<>();
            publicKeys.add(acct1.getEd25519PublicKey());
            publicKeys.add(acct2.getEd25519PublicKey());
            publicKeys.add(acct3.getEd25519PublicKey());
            // Please go to: https://bank.testnet.algorand.network/ to fund your accounts.
            MultisigAddress msig = new MultisigAddress(1, 2, publicKeys);
            System.out.println("Multisig Address: " + msig.toString());

            // end workaround

            Transaction tx1 = decodedTransaction.tx;

            SignedTransaction signedTransaction = acct1.signMultisigTransaction(msig, tx1);

            SignedTransaction signedTrx2 = acct2.appendMultisigTransaction(msig, signedTransaction);

            // save signed transaction to a file
            Files.write(Paths.get("./signed.txn"), Encoder.encodeToMsgPack(signedTrx2));

            // Read the transaction from a file
            signedTrx2 = Encoder.decodeFromMsgPack(Files.readAllBytes(Paths.get("./signed.txn")),
                    SignedTransaction.class);

            System.err.println(
                    "Please go to: https://bank.testnet.algorand.network/ to fund your multisig account. Press enter when ready \n"
                            + msig.toAddress());
            // System.in.read();

            try {
                // sign transaction
                byte[] encodedTxBytes = Encoder.encodeToMsgPack(signedTrx2);
                // submit the encoded transaction to the network
                TransactionID id = algodApiInstance.rawTransaction(encodedTxBytes);
                System.out.println("Successfully sent multisig: " + id);
                waitForConfirmation(id.getTxId());
            } catch (ApiException e) {
                // This may be generally expected, but should give us an informative error
                // message.
                System.err.println("Exception when calling algod#rawTransaction: " + e.getResponseBody());
            }
        } catch (Exception e) {
            System.out.println("Submit Exception: " + e);
        }
    }

    public void writeMultisigSignedTransaction() {

        // connect to node
        if (algodApiInstance == null)
            connectToNetwork();
        try {

            // Account acct1 = new Account();
            // Account acct2 = new Account();
            // Account acct3 = new Account();
            // System.out.println("Account 1 Address: " + acct1.getAddress());
            // System.out.println("Account 2 Address: " + acct2.getAddress());
            // System.out.println("Account 3 Address: " + acct3.getAddress());
            // System.out.println("Account 1 Mnemonic: " + acct1.toMnemonic());
            // System.out.println("Account 2 Mnemonic: " + acct2.toMnemonic());
            // System.out.println("Account 3 Mnemonic: " + acct3.toMnemonic());
            // Please go to: https://bank.testnet.algorand.network/ to fund your accounts.

            final String account1_mnemonic = "buzz genre work meat fame favorite rookie stay tennis demand panic busy hedgehog snow morning acquire ball grain grape member blur armor foil ability seminar";
            final String account2_mnemonic = "design country rebuild myth square resemble flock file whisper grunt hybrid floor letter pet pull hurry choice erase heart spare seven idea multiply absent seven";
            final String account3_mnemonic = "news slide thing empower naive same belt evolve lawn ski chapter melody weasel supreme abuse main olive sudden local chat candy daughter hand able drip";
            // final String account1_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            // final String account2_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            // final String account3_mnemonic = "your-25-word-mnemonic<PLACEHOLDER>";
            final Account acct1 = new Account(account1_mnemonic);
            final Account acct2 = new Account(account2_mnemonic);
            final Account acct3 = new Account(account3_mnemonic);

            List<Ed25519PublicKey> publicKeys = new ArrayList<>();
            publicKeys.add(acct1.getEd25519PublicKey());
            publicKeys.add(acct2.getEd25519PublicKey());
            publicKeys.add(acct3.getEd25519PublicKey());
            // Please go to: https://bank.testnet.algorand.network/ to fund your accounts.
            MultisigAddress msig = new MultisigAddress(1, 2, publicKeys);
            System.out.println("Multisig Address: " + msig.toString());

            // Save multisig to a file
            Files.write(Paths.get("./unsigned.msig"), Encoder.encodeToMsgPack(msig));

            // read multisig from file - not working can not instantiate from JSON object
            // (missing default constructor or creator, or perhaps need to add/enable type
            // information?

            // MultisigAddress decodedMultisig = Encoder
            // .decodeFromMsgPack(Files.readAllBytes(Paths.get("./unsigned.msig")),
            // MultisigAddress.class);
            // System.out.println("Multisig Address: " + decodedMultisig.toString());

            // msig = decodedMultisig;

            // final String DEST_ADDR = "transaction-reciever<PLACEHOLDER>";
            // final String SRC_ADDR = "transaction-sender<PLACEHOLDER>";

            final String DEST_ADDR = "WICXIYCKG672UGFCCUPBAJ7UYZ2X7GZCNBLSAPBXW7M6DZJ5YY6SCXML4A";
            final Address SRC_ADDR = msig.toAddress();

            // Get suggested parameters from the node
            TransactionParams params = algodApiInstance.transactionParams();
            BigInteger firstRound = params.getLastRound();
            String genId = params.getGenesisID();
            Digest genesisHash = new Digest(params.getGenesishashb64());

            // create transaction
            BigInteger lastRound = firstRound.add(BigInteger.valueOf(1000));
            BigInteger fee = BigInteger.valueOf(1000);
         //   fee = params.getFee();
            BigInteger amount = BigInteger.valueOf(1000000);
            Transaction tx = new Transaction(SRC_ADDR, fee, firstRound, lastRound, null, amount, new Address(DEST_ADDR),
                    genId, genesisHash);

            System.out.println("Unsigned transaction with txid: " + tx.txID());

            SignedTransaction signedTransaction = acct1.signMultisigTransaction(msig, tx);

            SignedTransaction signedTrx2 = acct2.appendMultisigTransaction(msig, signedTransaction);

            // save signed transaction to a file
            Files.write(Paths.get("./signed.txn"), Encoder.encodeToMsgPack(signedTrx2));

        } catch (Exception e) {
            System.out.println("Save Exception: " + e);
        }

    }

    public void readMultisigSignedTransaction() {

        try {
            // connect to a node
            if (algodApiInstance == null)
                connectToNetwork();

            // Read the transaction from a file
            SignedTransaction decodedSignedTransaction = Encoder
                    .decodeFromMsgPack(Files.readAllBytes(Paths.get("./signed.txn")), SignedTransaction.class);
            System.out.println("Signed transaction with txid: " + decodedSignedTransaction.transactionID);

            // Msgpack encode the signed transaction
            byte[] encodedTxBytes = Encoder.encodeToMsgPack(decodedSignedTransaction);

            // submit the encoded transaction to the network
            TransactionID id = algodApiInstance.rawTransaction(encodedTxBytes);
            System.out.println("Successfully sent tx with id: " + id);
            waitForConfirmation(id.getTxId());

        } catch (Exception e) {
            System.out.println("Submit Exception: " + e);
        }

    }

    public static void main(String args[]) throws Exception {
        SaveMultisigTransactionOffline mn = new SaveMultisigTransactionOffline();
    //    mn.writeMultisigUnsignedTransaction();
   //     mn.readMultisigUnsignedTransaction();

        mn.writeMultisigSignedTransaction();
        mn.readMultisigSignedTransaction();

    }
}
