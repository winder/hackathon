const algosdk = require('algosdk');

// const token = "algod-token"<PLACEHOLDER>;
// const server = "algod-address"<PLACEHOLDER>;
// const port = algod - port<PLACEHOLDER>;
// sandbox
const token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
const server = "http://localhost";
const port = 4001;
//Recover the account

var mnemonic = "awake used crawl list cruel harvest useful flag essay speed glad salmon camp sudden ride symptom test kind version together project inquiry diet abandon budget";

var recoveredAccount = algosdk.mnemonicToSecretKey(mnemonic);
console.log(recoveredAccount.addr);
//check to see if account is valid
var isValid = algosdk.isValidAddress(recoveredAccount.addr);
console.log("Is this a valid address: " + isValid);

// create an algod client
let algodclient = new algosdk.Algod(token, server, port);

// Function used to wait for a tx confirmation
const waitForConfirmation = async function (algodclient, txId) {
    let lastround = (await algodclient.status()).lastRound;
    while (true) {
        const pendingInfo = await algodclient.pendingTransactionInformation(txId);
        if (pendingInfo.round !== null && pendingInfo.round > 0) {
            //Got the completed Transaction
            console.log("Transaction " + pendingInfo.tx + " confirmed in round " + pendingInfo.round);
            break;
        }
        lastround++;
        await algodclient.statusAfterBlock(lastround);
    }
};

(async () => {

    // get suggested parameters
    let params = await algodclient.getTransactionParams();
    let endRound = params.lastRound + parseInt(1000);
    let fee = await algodclient.suggestedFee();

    // create logic sig
    // b64 example "ASABACI=" ==> Uint8Array.from([1, 32, 1, 0, 34]) 
    // which is INT 1(false)

    let program = new Uint8Array(Buffer.from("ASABICI=", "base64"));
    let lsig = algosdk.makeLogicSig(program);

    // create a transaction
    let txn = {
        "from": lsig.address(),
        "to": "SOEI4UA72A7ZL5P25GNISSVWW724YABSGZ7GHW5ERV4QKK2XSXLXGXPG5Y",
        "fee": params.fee,
        "amount": 200000,
        "firstRound": params.lastRound,
        "lastRound": endRound,
        "genesisID": params.genesisID,
        "genesisHash": params.genesishashb64
    };

    // Create the LogicSigTransaction with contract account LogicSig 
    let rawSignedTxn = algosdk.signLogicSigTransaction(txn, lsig);

    // send raw LogicSigTransaction to network
    let tx = (await algodclient.sendRawTransaction(rawSignedTxn.blob));
    console.log("Transaction : " + tx.txId);
    // wait for transaction to be confirmed
    await waitForConfirmation(algodclient, tx.txId);
    //this should fail as the program retures false
})().catch(e => {
    console.log(e);
});