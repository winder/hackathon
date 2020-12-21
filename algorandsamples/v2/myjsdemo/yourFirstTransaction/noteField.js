const algosdk = require('algosdk');

// Function used to wait for a tx confirmation
const waitForConfirmation = async function (algodclient, txId, timeout) {
    // Wait until the transaction is confirmed or rejected, or until 'timeout'
    // number of rounds have passed.
    //     Args:
    // txId(str): the transaction to wait for
    // timeout(int): maximum number of rounds to wait
    // Returns:
    // pending transaction information, or throws an error if the transaction
    // is not confirmed or rejected in the next timeout rounds
    if (algodclient == null || txId == null || timeout < 0) {
        throw "Bad arguments.";
    }
    let status = (await algodclient.status().do());
    if (status == undefined) throw new Error("Unable to get node status");
    let startround = status["last-round"] + 1;   
    let currentround = startround;

    while (currentround < (startround + timeout)) {
        await algodClient.statusAfterBlock(currentround).do();
        let pendingInfo = await algodclient.pendingTransactionInformation(txId).do();      
        if (pendingInfo != undefined) {
            if (pendingInfo["confirmed-round"] !== null && pendingInfo["confirmed-round"] > 0) {
                //Got the completed Transaction
                return pendingInfo;
            }
            else {
                if (pendingInfo["pool-error"] != null && pendingInfo["pool-error"].length > 0) {
                    // If there was a pool error, then the transaction has been rejected!
                    throw new Error("Transaction Rejected" + " pool error" + pendingInfo["pool-error"]);
                }
            }
        } 
        currentround++;
    }
    throw new Error("Pending tx not found in timeout rounds, timeout value = " + timeout);
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
    let names = '{"firstName":"John", "lastName":"Doe"}';
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
    let confirmedTxn = await waitForConfirmation(algodClient, txId, 4);
    //Got the completed Transaction
    console.log("Transaction " + txId + " confirmed in round " + confirmedTxn["confirmed-round"]);
    let mytxinfo = JSON.stringify(confirmedTxn.txn.txn, undefined, 2);
    console.log("Transaction information: %o", mytxinfo);
    console.log("Note: %s", confirmedTxn.txn.txn.note);
    var string = new TextDecoder().decode(confirmedTxn.txn.txn.note);
    console.log(Uint8Array, string);
    const obj = JSON.parse(string);
    console.log(obj.firstName);
})().catch(e => {
    console.log(e);
});