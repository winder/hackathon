const algosdk = require('algosdk');
// purestake
// const baseServer = "https://testnet-algorand.api.purestake.io/ps1";
// const port = "";
// const token = {
//     'X-API-Key': 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab'
// };
//local
// const token = "ec068ed68dc176e61a07b897c53835c6fb956271ce70aad3b204f0db6fa34f6d";
// const baseServer = "http://127.0.0.1";
// const port = 8080; 

// sandbox
const token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
const server = "http://localhost";
const port = 4001;


//Recover accounts used in example
// var account1_mnemonic = "portion never forward pill lunch organ biology" +
//     " weird catch curve isolate plug innocent skin grunt" +
//     " bounce clown mercy hole eagle soul chunk type absorb trim";

// var account2_mnemonic = "place blouse sad pigeon wing warrior wild script" +
//     " problem team blouse camp soldier breeze twist mother" +
//     " vanish public glass code arrow execute convince ability" +
//     " there";
// var account3_mnemonic = "image travel claw climb bottom spot path roast" +
//     " century also task cherry address curious save item" +
//     " clean theme amateur loyal apart hybrid steak about blanket";

// var account1_mnemonic = "flash ready garden rabbit couple talent quiz buzz manual lottery twist corn ivory hat trade actual goddess rice swing dinner shove gravity paddle abandon jacket";
// var account2_mnemonic = "enact solution token firm beach legend series rubber display begin angry bachelor play record captain vocal thought boss dish share brave rabbit frown about such";
// var account3_mnemonic = "awake used crawl list cruel harvest useful flag essay speed glad salmon camp sudden ride symptom test kind version together project inquiry diet abandon budget"


var account1_mnemonic = "portion never forward pill lunch organ biology" +
    " weird catch curve isolate plug innocent skin grunt" +
    " bounce clown mercy hole eagle soul chunk type absorb trim";
var account2_mnemonic = "place blouse sad pigeon wing warrior wild script" +
    " problem team blouse camp soldier breeze twist mother" +
    " vanish public glass code arrow execute convince ability" +
    " there";
var account3_mnemonic = "image travel claw climb bottom spot path roast" +
    " century also task cherry address curious save item" +
    " clean theme amateur loyal apart hybrid steak about blanket"


//instantiate the algod wrapper
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

    // var acct = algosdk.generateAccount();
    // account = acct.addr;
    // console.log(account);
    
    let params = await algodclient.getTransactionParams();
    let endRound = params.lastRound + parseInt(1000);
    let fee = await algodclient.suggestedFee();
    var recoveredAccount1 = algosdk.mnemonicToSecretKey(account1_mnemonic);
    var recoveredAccount2 = algosdk.mnemonicToSecretKey(account2_mnemonic);
    var recoveredAccount3 = algosdk.mnemonicToSecretKey(account3_mnemonic);
    console.log(recoveredAccount1.addr);
    console.log(recoveredAccount2.addr);
    console.log(recoveredAccount3.addr);
 
    // let tx1 = algosdk.makePaymentTxn(recoveredAccount1.addr, recoveredAccount3.addr, params.fee, 200000, params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);
    // let tx2 = algosdk.makePaymentTxn(recoveredAccount2.addr, recoveredAccount3.addr, params.fee, 100000, params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);
    // let tx3 = algosdk.makePaymentTxn(recoveredAccount3.addr, recoveredAccount2.addr, params.fee, 50000, params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);

    // let tx1 = algosdk.makePaymentTxn(recoveredAccount1.addr, recoveredAccount3.addr, params.fee, 200000, "NNDEXTDAQNUMTFKGMIMPTGONGNJJUQTJ5WCTGZLULCLIFCORNXJR26PSTU" , params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);
    // let tx2 = algosdk.makePaymentTxn(recoveredAccount2.addr, recoveredAccount3.addr, params.fee, 100000, "NNDEXTDAQNUMTFKGMIMPTGONGNJJUQTJ5WCTGZLULCLIFCORNXJR26PSTU", params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);
    // let tx3 = algosdk.makePaymentTxn(recoveredAccount3.addr, recoveredAccount2.addr, params.fee, 50000, "NNDEXTDAQNUMTFKGMIMPTGONGNJJUQTJ5WCTGZLULCLIFCORNXJR26PSTU", params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);

    let tx1 = algosdk.makePaymentTxn(recoveredAccount1.addr, recoveredAccount3.addr, params.fee, 200000, undefined, params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);
    let tx2 = algosdk.makePaymentTxn(recoveredAccount2.addr, recoveredAccount3.addr, params.fee, 100000, undefined, params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);
    let tx3 = algosdk.makePaymentTxn(recoveredAccount3.addr, recoveredAccount2.addr, params.fee, 50000, undefined, params.lastRound, endRound, new Uint8Array(0), params.genesishashb64, params.genesisID);

    let txns = [tx1, tx2, tx3]; // array of unsigned transactions
    let sks = [recoveredAccount1, recoveredAccount2, recoveredAccount3]; // array of appropriate secret keys

    // assign group id
    let txgroup = algosdk.assignGroupID(txns);
    //sign all transactions, and group together
    let signed = [];
    for (let idx in txgroup) {
        console.log("signing");
        signed.push(txgroup[idx].signTxn(sks[idx].sk));
        console.log("signed by:", sks[idx].addr);
        console.log("signing done");
    }
    // send the group
    let tx = (await algodclient.sendRawTransactions(signed));
    console.log("Transaction : " + tx.txId);
    // wait for transaction to be confirmed
    await waitForConfirmation(algodclient, tx.txId);
    
})().catch(e => {
    console.log(e);
});