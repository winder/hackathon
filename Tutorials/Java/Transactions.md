# Java Workshop – Transactions

Estimated time 10 minutes

# Summary


Completed code for this workshop can be found here [TutorialTransaction.java](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialTransaction.java) 


**Figure 1** Workshop Completed Code TutorialGroupTransaction.java

# Requirements

* [Algorand Sandbox Installation](https://github.com/algorand/sandbox) or set up a node as described [here](https://developer.algorand.org/docs/build-apps/setup/#how-do-i-obtain-an-algod-address-and-token).

* Connect to Node: https://developer.algorand.org/docs/build-apps/connect/

* Knowledge of setup for a Java solution, including directory structure.

* [Algorand Java SDK Installation](https://developer.algorand.org/docs/reference/sdks/#java)

* Use this workshop to [Create Accounts](https://github.com/algorand-devrel/hackathon/blob/master/Tutorials/Java/CreateAccounts.md)

* Optional: API testing tool such as [Postman](https://www.postman.com/)

# Background

After you successfully connect to algod using the [Algorand Sandbox](https://github.com/algorand/sandbox) and your preferred SDK, explore the methods available to read from and write to the blockchain. Remember that writing to the Algorand blockchain is simply sending a transaction to the network that is later confirmed within a block.

Follow the guide below to send your first transaction on Algorand and familiarize yourself with some of the core functions of the SDKs. Examples of goal commands and REST API calls are included when they are the same or similar, allowing you to cross-verify and gain fluency across all available tools and platforms.

Code snippets are abbreviated for conciseness and clarity. See the full code example for each SDK at the bottom of this guide.



## 1. Create Accounts

Time estimate - 10 minutes

If you have not already done so, use this workshop to [Create Accounts](https://github.com/algorand-devrel/hackathon/blob/master/Tutorials/Java/CreateAccounts.md)

**Copy off the account addresses and mnemonics from the output window.**

This workshop will use TestNet accounts that have been pre-created the CreateAccounts tutorial. Be sure to dispense Algos to these accounts before continuing, using the [TestNet Dispenser](https://bank.testnet.algorand.network/).

## 2. Setup Accounts, Utility Functions and Tools

Time estimate - 10 minutes

This step will use TestNet accounts that have been pre-created in Step 1. Be sure to dispense Algos to these accounts before continuing, using the [TestNet Dispenser](https://bank.testnet.algorand.network/).

The accounts used in this workshop  are: (yours will be different)

Account 1
`6N6CJF3NSBSOGQ6YKPVC5NDOXLZJ3T7H22HBQRNFXWLTSEAYARSJLPSZVE`

Account 2
`RXBOOCP3P3MNR5PQYD7UUUDL4P4CHKWSR6IENVKWV6KRVUBSOOU5JNDXNY`

Account 3
`LTNB7QHOJOFGIH7DHDODWY2GZAWXD6Y3EC4FT3YDG4MYF7B35GFNJ6M4JE`

**Info:**
    You may want to verify account information periodically as well as transactions
    with asset information during the course of this workshop. You can use either
    the [Algo TestNet Explorer](https://testnet.algoexplorer.io/) or use the Purestake's [Goalseeker](https://goalseeker.purestake.io/algorand/testnet), which also
    facilitates search by asset ID.

![Figure Step 2-1 Use Purestake’s [Goalseeker](https://goalseeker.purestake.io/algorand/testnet) to search on Address, Transaction, Block or AssetID](/imgs/TutorialASA-01.png)
**Figure Step 2-1** Purestake’s Goalseeker used to search Address,
Transaction, Block or AssetID.
<!-- <center>![Goalseeker](../imgs/TutorialASA-01.png)</center>
<center>**Figure Step 1A-1** Use Purestake’s Goalseeker to search Address,
Transaction, Block or AssetID.</center> -->

The tutorial code below is separated into snippets categorized by transaction core functions and is laid out in order. The remainder of the workshop is one solution and should be coded as a single script. 

All of the completed code below runs without any modifications. By default, the code uses the sandbox endpoints.

Sandbox uses the following API endpoints:

address: http://localhost:4001

token: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

 The completed code is in [algroandsamples java-test folder](https://github.com/algorand-devrel/hackathon/tree/master/algorandsamples/java-test/src/main/java/com/algorand/javatest) 
 
 Another option to the sandbox endpoints is to use a stand-up hackathon instance endpoints ... so these endpoints can be used if not using the sandbox:

* address: http://hackathon.algodev.network:9100

* token: "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

**Task 2-1** Create an empty code file called _myTransaction.java_ in the same location as [Step 1](https://github.com/algorand-devrel/hackathon/tree/master/algorandsamples/java-test/src/main/java/com/algorand/javatest). Then simply copy the code below and paste it into the empty file and run it. Then append each snippet after the last line of code in the prior step as you read through this workshop.
```java
package com.algorand.javatest;

import java.math.BigInteger;

import java.util.concurrent.TimeUnit;

import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.algod.client.AlgodClient;
import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.algod.client.model.*;
import com.algorand.algosdk.crypto.Address;
import com.algorand.algosdk.crypto.Digest;
import com.algorand.algosdk.transaction.Transaction;
import com.algorand.algosdk.transaction.SignedTransaction;
import com.algorand.algosdk.util.Encoder;

public class myTutorialTransaction {
    public AlgodApi algodApiInstance = null;

    // utility function to connect to a node
    private AlgodApi connectToNetwork() {

        // Initialize an algod client

        // sandbox
        final String ALGOD_API_ADDR = "http://localhost:4001";
        final String ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        // hackathon / workshop
        // final String ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";
        // final String ALGOD_API_TOKEN =
        // "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";

        // your own node
        // final String ALGOD_API_ADDR = "http://127.0.0.1:8080";
        // final String ALGOD_API_TOKEN = "your token in your node/data/algod.token
        // file";

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

    public void gettingStartedExample() throws Exception {

        if (algodApiInstance == null)
            connectToNetwork();

        // Import your private key mnemonic and address -
        // CHANGE THIS TO YOUR SECRET KEYS

        final String account1_mnemonic = "buzz genre work meat fame favorite rookie stay tennis demand panic busy hedgehog snow morning acquire ball grain grape member blur armor foil ability seminar";

        Account myAccount = new Account(account1_mnemonic);

        System.out.println("Account1: " + myAccount.getAddress());

        String myAddress = myAccount.getAddress().toString();
        com.algorand.algosdk.algod.client.model.Account accountInfo = algodApiInstance.accountInformation(myAddress);
        System.out.println(String.format("Account Balance: %d microAlgos", accountInfo.getAmount()));
 
    }

    public static void main(String args[]) throws Exception {
        myTutorialTransaction t = new myTutorialTransaction();
        t.gettingStartedExample();
    }
}

//Terminal Output Should look similar to this

// Account1: BJQHS7BWI4AVFYUYFOX7AK7B2HZP2WKJX4IYUM5GCGRJRI2UFXFHMZLCZY
// Account Balance: 98978000 microAlgos
```

# 3 Construct a Transaction

Construct the transaction¶
Create a transaction to send 1 Algo from your account to the TestNet faucet address (```GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A```) with the note "Hello World".

Transactions require a certain minimum set of parameters to be valid. Mandatory fields include the round validity range, the fee, and the genesis hash for the network the transaction is valid for. Read all about Transaction types, fields, and configurations in the Transactions Feature Guide. For now, construct a payment transaction as follows. Use the suggested parameters methods to initialize network-related fields.

**Note:** Paste this snippet at the end of the `gettingStartedExample` function, before the final curly brace `}`

```java
// Construct the transaction
final String RECEIVER = "GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A";
System.out.println("RECIEVER: " + RECEIVER);
BigInteger fee;
String genesisID;
Digest genesisHash;
BigInteger firstValidRound;
fee = BigInteger.valueOf(1000);
try {
    TransactionParams params = algodApiInstance.transactionParams();
    genesisHash = new Digest(params.getGenesishashb64());
    genesisID = params.getGenesisID();
    System.out.println("Minimum Fee: " + fee);
    firstValidRound = params.getLastRound();
    System.out.println("Current Round: " + firstValidRound);
} catch (ApiException e) {
    throw new RuntimeException("Could not get params", e);
}
BigInteger amount = BigInteger.valueOf(1000000); // microAlgos
BigInteger lastValidRound = firstValidRound.add(BigInteger.valueOf(1000)); // 1000 is the max tx window
String note = "Hello World";
Transaction txn = new Transaction(myAccount.getAddress(), fee, firstValidRound, lastValidRound, note.getBytes(),
        amount, new Address(RECEIVER), genesisID, genesisHash);

//Terminal output should be similar to this
// Minimum Fee: 1000
// Current Round: 5459501
```

## 4. Sign the transaction

Sign the transaction with your private key. This creates a new signed transaction object in the SDKs. Retrieve the transaction ID of the signed transaction.

```java
SignedTransaction signedTx = myAccount.signTransaction(txn);
System.out.println("Signed transaction with txId: " + signedTx.transactionID);

// Terminal output should look similar to this
// Signed transaction with txId: 75SHQYXQIZIA7RKYVHNQW5UGPOQFKM42JTZ6VUSTRBH4WTD4RVLA
```

## 5. Submit the transaction

Send the signed transaction to the network with your algod client.

```java
try {
    byte[] encodedTxBytes = Encoder.encodeToMsgPack(signedTx);
    TransactionID id = algodApiInstance.rawTransaction(encodedTxBytes);
    System.out.println("Successfully sent tx with ID: " + id);
} catch (ApiException e) {
    System.err.println("Exception when calling algod#rawTransaction: " + e.getResponseBody());
}

//Terminal output should look similar to this
//Successfully sent tx with ID: class TransactionID {
//     txId: KHY2NR7JPPSA7MJ74VPFN34O2KBW7OIDMUJBFP6475PP3553WRNQ
// }
```
## 6. Wait for confirmation

Successfully submitting your transaction to the network does not necessarily mean the network confirmed it. Always check that the network confirmed your transaction within a block before proceeding.

**Info:** On Algorand, transactions are final as soon as they are incorporated into a block and blocks are produced, on average, every 5 seconds. This means that transactions are confirmed, on average, in 5 seconds! Read more about the [Algorand's Consensus Protocol](https://developer.algorand.org/docs/algorand_consensus/) and how it achieves such high confirmation speeds and immediate transaction finality.

**Note:** If you are using a third-party service such as Purestake, it may require you to specify a Content-Type header when you send transactions to the network. Set the Content-Type to application/x-binary and add it as a header to the algod client or the specific request that sends the transaction.
For more details see this example of [Submitting a Transaction using Purestake](https://github.com/PureStake/api-examples/blob/master/java-examples/SubmitTx.java).

Insert this waitForConfirmation utility function above the gettingStartedExample function.

```java
    // utility function to wait on a transaction to be confirmed    
    public void waitForConfirmation( String txID ) throws Exception{
        if( algodApiInstance == null ) connectToNetwork();
        while(true) {
            try {
                //Check the pending tranactions
                com.algorand.algosdk.algod.client.model.Transaction pendingInfo = algodApiInstance.pendingTransactionInformation(txID);
                if (pendingInfo.getRound() != null && pendingInfo.getRound().longValue() > 0) {
                    //Got the completed Transaction
                    System.out.println("Transaction " + pendingInfo.getTx() + " confirmed in round " + pendingInfo.getRound().longValue());
                    break;
                } 
                algodApiInstance.waitForBlock(BigInteger.valueOf( algodApiInstance.getStatus().getLastRound().longValue() +1 ) );
            } catch (Exception e) {
                throw( e );
            }
        }
    }
```
After this line in gettingStartedExample()...

```java
System.out.println("Successfully sent tx with ID: " + id);
```

Add this line

```java
// Wait for transaction confirmation
waitForConfirmation(id.getTxId());
```
## 7. Read the transaction from the blockchain

Read your transaction back from the blockchain.

**Info:** Although you can read any transaction on the blockchain, only archival nodes store the whole history. By default, most nodes store only the last 1000 rounds and the APIs return errors when calling for information from earlier rounds. If you need to access data further back, make sure your algod client is connected to an archival, indexer node. Read more about node configurations in the Network Participation Guide or reach out to your service provider to understand how their node is configured.

```java
//Read the transaction from the blockchain
try {
    com.algorand.algosdk.algod.client.model.Transaction confirmedTxn =
            algodApiInstance.transactionInformation(RECEIVER, signedTx.transactionID);
    System.out.println("Transaction information (with notes): " + confirmedTxn.toString());
    System.out.println("Decoded note: " + new String(confirmedTxn.getNoteb64()));
} catch (ApiException e) {
    System.err.println("Exception when calling algod#transactionInformation: " + e.getCode());
}
// Terminal Output should be similar to this

// Transaction information (with notes): class Transaction {
//     fee: 1000
//     firstRound: 5460016
//     from: BJQHS7BWI4AVFYUYFOX7AK7B2HZP2WKJX4IYUM5GCGRJRI2UFXFHMZLCZY
//     fromrewards: 0
//     genesisID: testnet-v1.0
//     genesishashb64: [B@7393222f
//     lastRound: 5461016
//     noteb64: [B@babafc2
//     payment: class PaymentTransactionType {
//         amount: 1000000
//         close: null
//         closeamount: null
//         closerewards: 0
//         to: GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A
//         torewards: 0
//     }
//     poolerror: null
//     round: 5460019
//     tx: E34ZGAGOE5NDW7AFWKG6OYWGDKCACO2E5QYMF55TSSND4SMG3C5A
//     txresults: null
//     type: pay
// }
// Decoded note: Hello World
```

# Conclusion
----------

In this workshop, notice above the pattern of constructing a transaction, authorizing it, submitting it to the network, and confirming its inclusion in a block. This is a framework to familiarize yourself with as it appears often in blockchain-related development.

Source code for the completed solution can be found here:



[Transaction Sample](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialGroupedTransaction.java)

