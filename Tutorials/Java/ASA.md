# Java Workshop – Algorand Standard Assets (ASA)

Estimated time 70 minutes

# Summary

The Algorand protocol supports the creation of on-chain assets that benefit from the same security, compatibility, speed, and ease of use as the Algo. The official name for assets on Algorand is Algorand Standard Assets (ASA). ASA  represents Algorand’s ability to digitize any asset and
have both it and its ownership represented on-chain. The Algorand platform is a general-purpose economic exchange system which
represents an extremely broad market. A given platform’s attractiveness and
effectiveness as a means of economic exchange can be defined by the combination
of what you can own and how you can transact.

Completed code for this workshop can be found here [TutorialCreateAccounts.java](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialCreateAccounts.java) and [TutorialAssetExample.java](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialAssetExample.java).



**Figure 1** Workshop Completed Code Tutorial...java

# Requirements

* [Algorand Sandbox Installation](https://github.com/algorand/sandbox) or set up a node as described [here](https://developer.algorand.org/docs/build-apps/setup/#how-do-i-obtain-an-algod-address-and-token).

* Knowledge of setup for a Java solution, including directory structure.

* [Algorand Java SDK Installation](https://developer.algorand.org/docs/reference/sdks/#java)

* Optional: API testing tool such as [Postman](https://www.postman.com/)

# Background

Algorand Standard Assets could be
fungible (for example currencies, stable coins, loyalty points, system credits,
in-game points, etc) or non-fungible (for example real estate, collectables,
supply chain, in-game items, tickets, etc). Also, our functionality
allows restrictions to be placed on the assets where needed (for example:
securities, certifications, compliance, etc).

Algorand has implemented named assets as a truly layer 1 asset. This allows any
asset created on Algorand to enjoy:

**Increased security** - New assets will enjoy the same security and safety as
Algos, the native currency on Algorand

**Inherent compatibility** - Apps that support any Algorand asset will support
all Algorand assets

**High ease of use** - Create your asset with a single transaction to the
network

Here are several things to be aware of before getting started with assets.

* A single Algorand account is permitted to create up to 1000 assets.

* For every asset an account creates or owns, its minimum balance is increased by 0.1 Algos (100,000 microAlgos).

* Before a new asset can be transferred to a specific account the receiver must opt-in to receive the asset. This process is described below in Receiving an Asset.

* If any transaction is issued that would violate the maximum number of assets for an account or not meet the minimum balance requirements, the transaction will fail.


## 1. Create 3 Accounts and add Algos to the Accounts

Time estimate - 10 minutes

Assets are created at the account level. So, before starting the ASA workshop, 3 new accounts will be created for this step for ASA transactions. Once created, copy off the account mnemonic and account address values.

**Task 1-1** Setup. Clone the [Hackathon Repository](https://github.com/algorand-devrel/hackathon).

**Task 1-2** Open the workspace or folder. You will need to start a new instance of VS Code for this workshop. Navigate to the algorandsamples folder using Finder/Explorer. 

Right click on the file _java.code-workspace_ and select Open With ... and select Visual Studio Code or add it by selecting Other... If using another IDE or opening from VS Code, open the completed source code folder  algorandsamples/java-test folder. 

![Figure Step 1-2 Open WorkSpace from Finder/Explorer](/imgs/TutorialASA-03.png)
**Figure Step 1-2** Open WorkSpace from Finder/Explorer.

**Task 1-3** Create your code. Create an empty code file in the source folder (/algorandsamples/java-test/src/main/java/com/algorand/javatest) and name it  _myTutorialCreateAccounts.java_

Simply copy and insert this snippet below and run it. This code will generate an account address and recovery mnemonic phrase for 3 accounts. Then copy off the account addresses and mnemonics from the output window. We will use these in the next task. Mnemonics are for demonstration purposes. **NEVER** reveal secret mnemonics in practice as these are used to derive the secret (or private) key.

```java
package com.algorand.javatest;
import com.algorand.algosdk.account.Account;

public class myTutorialCreateAccounts {
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
```
**Task 1-4** 
In order to run transactions, the accounts need to have TestNet Algo funds to cover transaction fees. Load the accounts from the Algorand [TestNet Dispenser](https://bank.testnet.algorand.network/) 


## 2. Setup Accounts, Utility Functions and Tools

Time estimate - 10 minutes

This step will use three TestNet accounts that have been pre-created in Step 1. Be sure to dispense Algos to these accounts before continuing, using the [TestNet Dispenser](https://bank.testnet.algorand.network/).

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

The tutorial code below is separated into snippets categorized by ASA core functions and is laid out in order. The remainder of the workshop is one solution and should be coded as a single script. 

All of the completed code below runs without any modifications. By default, the code uses the sandbox endpoints.

Sandbox uses the following API endpoints:

address: http://localhost:4001

token: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

 The completed code is in [algroandsamples java-test folder](https://github.com/algorand-devrel/hackathon/tree/master/algorandsamples/java-test/src/main/java/com/algorand/javatest) 
 
 Another option to the sandbox endpoints is to use a stand-up hackathon instance endpoints ... so these endpoints can be used if not using the sandbox:

* address: http://hackathon.algodev.network:9100

* token: "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

**Task 2-1** Create an empty code file called _myTutorialAssetExample.java_ in the same location as Step 1. Then simply copy the code below and paste it into the empty file and run it. Then append each snippet after the last line of code in the prior step as you read through this workshop.

```java
package com.algorand.javatest;

import java.math.BigInteger;

import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.algod.client.AlgodClient;
import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.algod.client.model.AssetHolding;
import com.algorand.algosdk.algod.client.model.AssetParams;
import com.algorand.algosdk.algod.client.model.TransactionID;
import com.algorand.algosdk.algod.client.model.TransactionParams;
import com.algorand.algosdk.crypto.Address;
import com.algorand.algosdk.crypto.Digest;
import com.algorand.algosdk.transaction.SignedTransaction;
import com.algorand.algosdk.transaction.Transaction;
import com.algorand.algosdk.util.Encoder;

/**
 * Show Creating, modifying, sending and listing assets
 */
public class myTutorialAssetExample {

    public AlgodApi algodApiInstance = null;

    // utility function to connect to a node
    private AlgodApi connectToNetwork() {

        // hackathon / workshop
        // final String ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";
        // final String ALGOD_API_TOKEN =
        // "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";

        // your own node
        // final String ALGOD_API_ADDR = "http://127.0.0.1:8080";
        // final String ALGOD_API_TOKEN = "your token in your node/data/algod.token
        // file";

        // sandbox
        final String ALGOD_API_ADDR = "http://localhost:4001";
        final String ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

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

    // Utility function to update changing block parameters
    public ChangingBlockParms getChangingParms(AlgodApi algodApiInstance) throws Exception {
        ChangingBlockParms cp = new myTutorialAssetExample.ChangingBlockParms();
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

        myTutorialAssetExample ex = new myTutorialAssetExample();
        AlgodApi algodApiInstance = ex.connectToNetwork();

        // recover example accounts

        // final String account1_mnemonic = "<your-25-word-mnemonic>";
        // final String account2_mnemonic = "<your-25-word-mnemonic>";
        // final String account3_mnemonic = "<your-25-word-mnemonic>";

        // CHANGE THESE VALUES WITH YOUR MNEMONICS FROM STEP 1

        final String account1_mnemonic = "neutral blade diesel guard punch glide pepper cancel wise soul legend second capital load hover extra witness forward enlist flee pitch taxi impulse absent common";
        final String account2_mnemonic = "cute ask spread arena glide way feed else this case parade fly diamond cargo satoshi clever pear apple dream champion effort near flee absent gate";
        final String account3_mnemonic = "number define pet usual brave day traffic peasant style goddess wisdom cart mouse fork ecology jungle impose border dad please worth surprise sort abstract mechanic";

        Account acct1 = new Account(account1_mnemonic);
        Account acct2 = new Account(account2_mnemonic);
        Account acct3 = new Account(account3_mnemonic);
        System.out.println("Account1: " + acct1.getAddress());
        System.out.println("Account2: " + acct2.getAddress());
        System.out.println("Account3: " + acct3.getAddress());
  
    }
} 
// Terminal output should look similar to this

// Account1: 6N6CJF3NSBSOGQ6YKPVC5NDOXLZJ3T7H22HBQRNFXWLTSEAYARSJLPSZVE
// Account2: RXBOOCP3P3MNR5PQYD7UUUDL4P4CHKWSR6IENVKWV6KRVUBSOOU5JNDXNY
// Account3: LTNB7QHOJOFGIH7DHDODWY2GZAWXD6Y3EC4FT3YDG4MYF7B35GFNJ6M4JE
```


# 3. Create a New Asset

Time estimate - 10 minutes

The ability to create asserts directly on the blockchain is an exciting capability of the Algorand Blockchain. Possible uses include currency, game leader boards, points in a loyalty system, shares of an asset, and securities such as stocks, bonds, and derivatives.  


**Info:**
    The decimals value determines the placement of the decimal. For example, when `decimals = 2`, and the `amount = 1000`, the actual amount is 10.00. So, when a  transfer amount of 10 is made, the actual transfer is .10


**Note:** Paste this snippet at the end of the `main` function, before the final two curly braces `}}`

**Task 3-1** Account 1 creates an asset called myunit and sets Account 2 as the manager, reserve, freeze, and clawback address.

```java
        // Create a new asset
        // get changing network parameters
        ChangingBlockParms cp = null;
        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }

        // The following parameters are asset specific
        // and will be re-used throughout the example.

        // Total number of this asset available for circulation

        BigInteger assetTotal = BigInteger.valueOf(10000);
 
        // Whether user accounts will need to be unfrozen before transacting

        boolean defaultFrozen = false;
        // Used to display asset units to user
        String unitName = "myunit";
        // Friendly name of the asset
        String  assetName = "my longer asset name";
        String url = "http://this.test.com";
        String assetMetadataHash = "16efaa3924a6fd9d3a4824799a4ac65d";
        
        // The following parameters are the only ones
        // that can be changed, and they have to be changed
        // by the current manager
        // Specified address can change reserve, freeze, clawback, and manager
        
        Address manager  = acct2.getAddress();
        Address reserve = acct2.getAddress();
        Address freeze = acct2.getAddress();
        Address clawback = acct2.getAddress();
        
        // Decimals specifies the number of digits to display after the decimal
        // place when displaying this asset. A value of 0 represents an asset
        // that is not divisible, a value of 1 represents an asset divisible
        // into tenths, and so on. This value must be between 0 and 19

        Integer decimals = 0;
        Transaction tx = Transaction.createAssetCreateTransaction(acct1.getAddress(), 
            BigInteger.valueOf( 1000 ), cp.firstRound, cp.lastRound, null, cp.genID, 
            cp.genHash, assetTotal, decimals, defaultFrozen, unitName, assetName, url, 
            assetMetadataHash.getBytes(), manager, reserve, freeze, clawback);
        // Update the fee as per what the BlockChain is suggesting
        Account.setFeeByFeePerByte(tx, cp.fee);

        // Sign the Transaction with creator account
        SignedTransaction signedTx = acct1.signTransaction(tx);
        BigInteger assetID = null;
        try{
            TransactionID id = ex.submitTransaction( signedTx );
            System.out.println( "Transaction ID: " + id );
            ex.waitForConfirmation( signedTx.transactionID);
            // Now that the transaction is confirmed we can get the assetID
            com.algorand.algosdk.algod.client.model.Transaction ptx = 
                algodApiInstance.pendingTransactionInformation(id.getTxId());
            assetID = ptx.getTxresults().getCreatedasset();

        } catch (Exception e){
            e.printStackTrace();
            return;
        }
        System.out.println( "AssetID = " +  assetID);
// Terminal Output should look similar to:
// Transaction ID: class TransactionID {
//     txId: CTXVAMXC7L4QLJL6SX4EWGGW77JT2JO4NULKMYHRA2ZQLIB35XJQ
// }
// Transaction CTXVAMXC7L4QLJL6SX4EWGGW77JT2JO4NULKMYHRA2ZQLIB35XJQ confirmed in round 5443680
// AssetID = 214303
```

# 4. Configure Asset Manager

Time estimate - 10 minutes

Assets can be managed as to which accounts have roles for overall manager, reserve, freeze, and clawback functions. By default, all of these roles are set to the creator account. 

Asset reconfiguration allows the address specified as manager to change any of the special addresses for the asset, such as the reserve address. To keep an address the same, it must be re-specified in each new configuration transaction. Supplying an empty address is the same as turning the associated feature off for this asset. Once a special address is set to the empty address, it can never change again. For example, if an asset configuration transaction specifying clawback="" were issued, the associated asset could never be revoked from asset holders, and clawback="" would be true for all time. The strictEmptyAddressChecking argument can help with this behavior: when set to its default true, makeAssetConfigTxn will throw an error if any undefined management addresses are passed.

**Task 4-1** Here, the current manager (Account 2) issues an asset configuration transaction that assigns Account 1 as the new manager.

**Note:** Every time this script is run, for each added step, a new asset id will be generated. So, if you wish to use the same one each time you can use the Asset ID from the prior step and comment out the prior step. 

`
BigInteger assetID = BigInteger.valueOf(value from prior step);
`

```java
        // Change Asset Configuration:
        // Next we will change the asset configuration
        // Note that configuration changes must be done by
        // The manager account, which is currently acct2
        // Note in this transaction we are re-using the asset
        // creation parameters and only changing the manager
        // and transaction parameters like first and last round

        // First we update standard Transaction parameters
        // To account for changes in the state of the blockchain

        // Get changing network parameters
        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }
        // configuration changes must be done by
        // the manager account - changing manager of the asset
        tx = Transaction.createAssetConfigureTransaction(acct2.getAddress(), 
                BigInteger.valueOf( 1000 ),cp.firstRound, cp.lastRound, null, 
                cp.genID, cp.genHash, assetID, acct1.getAddress(), reserve, 
                freeze, clawback, false);
        // update the fee as per what the BlockChain is suggesting
        Account.setFeeByFeePerByte(tx, cp.fee);
        // the transaction must be signed by the current manager account   
        signedTx = acct2.signTransaction(tx);
        // send the transaction to the network
        try{
            TransactionID id = ex.submitTransaction( signedTx );
            System.out.println( "Transaction ID: " + id );
            ex.waitForConfirmation( signedTx.transactionID);
        } catch (Exception e){
            e.printStackTrace();
            return;
        }  
        // list the asset
        AssetParams assetInfo = algodApiInstance.assetInformation(assetID);
        // the manager should now be the same as the creator
        System.out.println(assetInfo);

// Terminal output should look similar to this.

// Transaction ID: class TransactionID {
//     txId: RDUJJYNSTJCPUA6J4BNE53XSZMBVCNHFZJ2AVT7ZMBL5HZULRSCA
// }
// Transaction RDUJJYNSTJCPUA6J4BNE53XSZMBVCNHFZJ2AVT7ZMBL5HZULRSCA confirmed in round 5444044
// class AssetParams {
//     assetname: my longer asset name
//     clawbackaddr: RXBOOCP3P3MNR5PQYD7UUUDL4P4CHKWSR6IENVKWV6KRVUBSOOU5JNDXNY
//     decimals: 0
//     defaultfrozen: false
//     freezeaddr: RXBOOCP3P3MNR5PQYD7UUUDL4P4CHKWSR6IENVKWV6KRVUBSOOU5JNDXNY
//     managerkey: 6N6CJF3NSBSOGQ6YKPVC5NDOXLZJ3T7H22HBQRNFXWLTSEAYARSJLPSZVE
//     metadatahash: [B@79dc5318
//     reserveaddr: RXBOOCP3P3MNR5PQYD7UUUDL4P4CHKWSR6IENVKWV6KRVUBSOOU5JNDXNY
//     total: 10000
//     unitname: myunit
//     url: http://this.test.com
```

# 5. Opt-in to Receive Asset

Time estimate - 5 minutes

Once the asset has been created, the next thing to do is send assets to other accounts. 

Before a user can begin transacting with an asset, the user must first issue an asset acceptance transaction. This is a special case of the asset transfer transaction, where the user sends 0 assets to themself. After issuing this transaction, the user can begin transacting with the asset. Each new accepted asset increases the user's minimum balance.

**Task 5-1** Account 3 opts-in to receive the new asset by sending a 0 amount transfer of the asset to itself.

```java tab="Java"
        // Opt in to Receiving the Asset
        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }
        tx = Transaction.createAssetAcceptTransaction(acct3.getAddress(), 
            BigInteger.valueOf( 1000 ), cp.firstRound, 
            cp.lastRound, null, cp.genID, cp.genHash, assetID);
        // Update the fee based on the network suggested fee
        Account.setFeeByFeePerByte(tx, cp.fee);
        // The transaction must be signed by the current manager account  
        signedTx = acct3.signTransaction(tx);
        com.algorand.algosdk.algod.client.model.Account act;
        // send the transaction to the network and
        try{
            TransactionID id = ex.submitTransaction( signedTx );
            System.out.println( "Transaction ID: " + id );
            ex.waitForConfirmation( signedTx.transactionID);
            // We can now list the account information for acct3 
            // and see that it can accept the new asseet
            act = algodApiInstance.accountInformation(acct3.getAddress().toString());
            AssetHolding ah = act.getHolding(assetID);
            System.out.println( "Asset Holding: " + ah.getAmount() );

        } catch (Exception e){
            e.printStackTrace();
            return;
        }  
// terminal output should look similar to this
// Transaction ID: class TransactionID {
//     txId: A7RV4IQVL4NOMWK3MVGTUCX5223C5BCI6GPEGDGHOU3WJLH36BBA
// }
// Transaction A7RV4IQVL4NOMWK3MVGTUCX5223C5BCI6GPEGDGHOU3WJLH36BBA confirmed in round 5444319
// Asset Holding: 0
```

# 6. Transfer an Asset

Time estimate - 5 minutes

Transfer an asset allows users to transact with assets, after they have issued asset acceptance transactions. The optional closeRemainderTo argument can be used to stop transacting with a particular asset. Now that the opt-in has been done on a potential receiving account, assets can be transferred.

**Note:**
    A frozen account can always close out to the asset creator.

**Task 6-1** This code has Account 1 sending 10 myunit to Account 3.

```java
        // Transfer the Asset:
        // Now that account3 can recieve the new asset we can tranfer assets
        // from the creator (account 1) to account3

        // get changing network parameters
        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }
        // set asset xfer specific parameters
        // We set the assetCloseTo to null so we do not close the asset out
        Address assetCloseTo = new Address();
        BigInteger assetAmount = BigInteger.valueOf(10);
        tx = Transaction.createAssetTransferTransaction(acct1.getAddress(), 
            acct3.getAddress(), assetCloseTo, assetAmount, BigInteger.valueOf( 1000 ), 
            cp.firstRound, cp.lastRound, null, cp.genID, cp.genHash, assetID);        
        // Update the fee based on the network suggested fee
        Account.setFeeByFeePerByte(tx, cp.fee);
        // The transaction must be signed by the sender account  
        signedTx = acct1.signTransaction(tx);
        // send the transaction to the network 
        try{
            TransactionID id = ex.submitTransaction( signedTx );
            System.out.println( "Transaction ID: " + id );
            ex.waitForConfirmation( signedTx.transactionID);
            // list the account information for acct3 
            // and see that it now has 5 of the new asset
            act = algodApiInstance.accountInformation(acct3.getAddress().toString());
            System.out.println( act.getHolding(assetID).getAmount() );
        } catch (Exception e){
            e.printStackTrace();
            return;
        }
        // Terminal output should look similar to this
        //         Transaction ID: class TransactionID {
        //     txId: TMPIHIT4KDFGFZHE54ZSUZRLLGDT54FFM7NEX4VLRT3TNQYJGFSQ
        // }
        // Transaction TMPIHIT4KDFGFZHE54ZSUZRLLGDT54FFM7NEX4VLRT3TNQYJGFSQ confirmed in round 5444418
        // 10
```

# 7. Freeze an Asset

Time estimate - 10 minutes

To freeze or unfreeze an asset, this transaction must be sent from the account specified as the freeze manager for the asset.

**Task 7-1** The freeze address (Account 2) freezes Account 3's myunit holdings.

```java
        // Freeze the Asset:
        // The asset was created and configured to allow freezing an account
        // If the freeze address is blank, it will no longer be possible to do this.
        // In this example we will now freeze account3 from transacting with the
        // The newly created asset.
        // The freeze transaction is sent from the freeze acount
        // Which in this example is account2
        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }
        // set asset specific parameters
        boolean freezeState = true;
        // The sender should be freeze account
        tx = Transaction.createAssetFreezeTransaction(acct2.getAddress(), 
            acct3.getAddress(), freezeState, BigInteger.valueOf( 1000 ), 
            cp.firstRound, cp.lastRound, null, cp.genHash, assetID);
        // Update the fee based on the network suggested fee
        Account.setFeeByFeePerByte(tx, cp.fee);
        // The transaction must be signed by the freeze account   
        signedTx = acct2.signTransaction(tx);
        // send the transaction to the network
        try{
            TransactionID id = ex.submitTransaction( signedTx );
            System.out.println( "Transaction ID: " + id );
            ex.waitForConfirmation( signedTx.transactionID);
        } catch (Exception e){
            e.printStackTrace();
            return;
        }

```

Optional: Let's verify this value was indeed set to true, with an API testing tool such as [Postman](https://www.postman.com/)

GET http://localhost:4001/v1/account/:address

Header 
X-Algo-API-Token = aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

Params
address = Account 3 address

Search on your asset ID and it should be true.

![Goalseeker](/imgs/TutorialASA-02.png)

**Figure Step 7-1** REST API Call in Postman.

More info:

[REST API call documentation](https://developer.algorand.org/docs/reference/rest-apis/algod/#get-swaggerjson)


[Generate a swagger file for REST API endpoints ](https://developer.algorand.org/docs/reference/sdks/#rest-endpoints) - there is one for algod and one for kmd


# 8. Revoke an Asset

Time estimate - 5 minutes

Revoking an asset allows an asset's revocation manager to transfer assets on behalf of another user. It will only work when issued by the asset's revocation manager.

**Task 8-1** The clawback address (Account 2) revokes 10 myunit from Account 3 and places it back with Account 1.

```java
        // Revoke the asset:

        // The asset was also created with the ability for it to be revoked by
        // clawbackaddress. If the asset was created or configured by the manager
        // not allow this by setting the clawbackaddress to a blank address
        // then this would not be possible.
        // We will now clawback the 10 assets in account3. Account2
        // is the clawbackaccount and must sign the transaction
        // The sender will be be the clawback adress.
        // the recipient will also be be the creator acct1 in this case

        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }
        // set asset specific parameters
        assetAmount = BigInteger.valueOf( 10 );
        tx = Transaction.createAssetRevokeTransaction(acct2.getAddress(), 
            acct3.getAddress(), acct1.getAddress(), assetAmount, 
            BigInteger.valueOf( 1000 ), cp.firstRound, 
        cp.lastRound, null, cp.genID, cp.genHash, assetID);
        // Update the fee based on the network suggested fee
        Account.setFeeByFeePerByte(tx, cp.fee);
        // The transaction must be signed by the clawback account  
        signedTx = acct2.signTransaction(tx);
        // send the transaction to the network and
        // wait for the transaction to be confirmed
        try{
            TransactionID id = ex.submitTransaction( signedTx );
            System.out.println( "Transaction ID: " + id );
            ex.waitForConfirmation( signedTx.transactionID);
            // list the account information 
            act = algodApiInstance.accountInformation(acct3.getAddress().toString());
            System.out.println( act.getHolding(assetID).getAmount() );
        } catch (Exception e){
            e.printStackTrace();
            return;
        }  
// Terminal Output should look similar to this
// Transaction BYXUHK5NQOVRQVQ53UGS4AWEIXS42TDGSLD5JJKQ77CGIHLJV4IQ confirmed in round 5445173
// Transaction ID: class TransactionID {
//     txId: RGI456PPFWADOKDCUIPEZE47BZPYLVIIOEPR3F2GOKR2XCZZOD2A
// }
// Transaction RGI456PPFWADOKDCUIPEZE47BZPYLVIIOEPR3F2GOKR2XCZZOD2A confirmed in round 5445175
//0
```

# 9. Destroy an Asset

Time estimate - 5 minutes

Asset destruction allows the creator to remove the asset from the ledger, if all outstanding assets are held by the creator. 

**Task 9-1** With all assets back in the creator's account, the manager (Account 1) destroys the asset.

```java
        // Destroy the Asset:
        // All assets should now be back in
        // creators account
        try {
            cp = ex.getChangingParms(algodApiInstance);
        } catch (ApiException e) {
            e.printStackTrace();
            return;
        }
        // set asset specific parameters
        // The manager must sign and submit the transaction
        tx = Transaction.createAssetDestroyTransaction(acct1.getAddress(), 
                BigInteger.valueOf( 1000 ), cp.firstRound, cp.lastRound, 
                null, cp.genHash, assetID);
        // Update the fee based on the network suggested fee
        Account.setFeeByFeePerByte(tx, cp.fee);
        // The transaction must be signed by the manager account  
        signedTx = acct1.signTransaction(tx);
        // send the transaction to the network 
        try{
            TransactionID id = ex.submitTransaction( signedTx );
            System.out.println( "Transaction ID: " + id );
            ex.waitForConfirmation( signedTx.transactionID);
            // We list the account information for acct1 
            // and check that the asset is no longer exist
            
            act = algodApiInstance.accountInformation(acct1.getAddress().toString());
            // expected to be null and will cause error.
            System.out.println( "Does AssetID: " + assetID + " exist? " + 
                act.getThisassettotal().containsKey(assetID) );
        } catch (Exception e){
            e.printStackTrace();
            return;
        }  

```

Conclusion
----------

ASA is a very powerful layer 1 feature of the Algorand Blockchain. We created an asset in this workshop and showed how to do the following functions:

* Create
* Opt-In
* Manage
* Transfer
* Freeze
* Revoke
* Destroy

Source code for the completed solution can be found here:

[Create Accounts](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialCreateAccounts.java)

[Asset Sample](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialAssetExample.java)

