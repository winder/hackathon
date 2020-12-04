const algosdk = require('algosdk');

// Check the status of pending transactions
const checkPending = async function (client, txid, numRoundTimeout) {

    if (client == null || txid == null || numRoundTimeout < 0) {
        throw "Bad arguments.";
    }
    let status = (await client.status().do());
    if (status == undefined) throw "Unable to get node status";

    let startingRound = status["last-round"];
    let nextRound = startingRound;
    while (nextRound < startingRound + numRoundTimeout) {
        // Check the pending tranactions
        let pendingInfo = await client.pendingTransactionInformation(txid).do();
        if (pendingInfo != undefined) {
            if (pendingInfo["confirmed-round"] !== null && pendingInfo["confirmed-round"] > 0) {
                //Got the completed Transaction
                console.log("Transaction " + txid + " confirmed in round " + pendingInfo["confirmed-round"]);
                return pendingInfo;
            }
            if (pendingInfo["pool-error"] != null && pendingInfo["pool-error"].length > 0) {
                // If there was a pool error, then the transaction has been rejected!
                return "Transaction Rejected";
            }

        }
        nextRound++;
        await client.statusAfterBlock(nextRound).do();
    }

    if (pendingInfo != null) {
        return "Transaction Still Pending";
    }

    return null;
}

// Wait for a transaction to be confirmed
const waitForConfirmation = async function (algodclient, txId) {

    let cp = await checkPending(algodclient, txId, 4);
    if (cp == null || cp == "Transaction Rjected") throw "Transaction Rejected";
    if (cp == "Transaction Still Pending") throw "Transaction Still Pending";
    console.log("Transaction confirmed in round " + cp["confirmed-round"]);
};

// const token = "<your-api-token>";
// const server = "<http://your-sever>";
// const port = 8080;

// sandbox
const token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
const server = "http://localhost";
const port = 4001;


//Recover the account
var mnemonic = "price clap dilemma swim genius fame lucky crack torch hunt maid palace ladder unlock symptom rubber scale load acoustic drop oval cabbage review abstract embark"
var recoveredAccount = algosdk.mnemonicToSecretKey(mnemonic);
console.log(recoveredAccount.addr);
//check to see if account is valid
var isValid = algosdk.isValidAddress(recoveredAccount.addr);
console.log("Is this a valid address: " + isValid);

//instantiate the algod wrapper
let algodClient = new algosdk.Algodv2(token, server, port);

//submit the transaction
(async () => {

    //Check your balance
    let accountInfo = await algodClient.accountInformation(recoveredAccount.addr).do();
    console.log("Account balance: %d microAlgos", accountInfo.amount);

    // Construct the transaction
    let params = await algodClient.getTransactionParams().do();
    // comment out the next two lines to use suggested fee
    params.fee = 1000;
    params.flatFee = true;

    // receiver defined as TestNet faucet address 
    const receiver = "GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A";
   // let names = '{"firstName":"John", "LastName":"Doe"}';
    let names = "Russell Anthony Fustino - this is a test";
    const enc = new TextEncoder();
    const note = enc.encode(names);
    console.log(note);      
    let txn = algosdk.makePaymentTxnWithSuggestedParams(recoveredAccount.addr,
        receiver, 1000000, undefined, note, params);

    // Sign the transaction
    let signedTxn = txn.signTxn(recoveredAccount.sk);
    let txId = txn.txID().toString();
    console.log("Signed transaction with txID: %s", txId);

    // Submit the transaction
    await algodClient.sendRawTransaction(signedTxn).do();

    // Wait for confirmation
    await waitForConfirmation(algodClient, txId);

    // Read the transaction from the blockchain
    let confirmedTxn = await algodClient.pendingTransactionInformation(txId).do();
    //   console.log("Transaction information: %o", confirmedTxn.txn.txn);
    let mytxinfo = JSON.stringify(confirmedTxn.txn.txn, undefined, 2);
    console.log("Transaction information: %o", mytxinfo);

    console.log("Note: %s", confirmedTxn.txn.txn.note);
    var string = new TextDecoder().decode(confirmedTxn.txn.txn.note);
    console.log(Uint8Array, string)
 //   const obj = JSON.parse(string);
 //   console.log(obj.FirstName);
})().catch(e => {
    console.log(e);
});