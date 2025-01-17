const algosdk = require('algosdk');

// const token = "<algod-token>";
// const server = "<algod-address>";
// const port = <algod-port>;
// sandbox
// const token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
// const server = "http://localhost";
// const port = 4001;
const token = "6b3a2ae3896f23be0a1f0cdd083b6d6d046fbeb594a3ce31f2963b717f74ad43"
const server = "http://127.0.0.1"
const port = 54746;
// Import the filesystem module 
const fs = require('fs'); 
// import your private key mnemonic
// let PASSPHRASE = "<25-word-mnemonic>";
let PASSPHRASE = "awake used crawl list cruel harvest useful flag essay speed glad salmon camp sudden ride symptom test kind version together project inquiry diet abandon budget";

var myAccount = algosdk.mnemonicToSecretKey(PASSPHRASE);
console.log("My Address: " + myAccount.addr);
// Function used to wait for a tx confirmation
const waitForConfirmation = async function (algodclient, txId) {
    let response = await algodclient.status().do();
    let lastround = response["last-round"];
    while (true) {
        const pendingInfo = await algodclient.pendingTransactionInformation(txId).do();
        if (pendingInfo["confirmed-round"] !== null && pendingInfo["confirmed-round"] > 0) {
            //Got the completed Transaction
            console.log("Transaction " + txId + " confirmed in round " + pendingInfo["confirmed-round"]);
            break;
        }
        lastround++;
        await algodclient.statusAfterBlock(lastround).do();
    }
};
// create an algod v2 client
let algodclient = new algosdk.Algodv2(token, server, port);

(async () => {

    // get suggested parameter
    let params = await algodclient.getTransactionParams().do();
    // comment out the next two lines to use suggested fee 
    params.fee = 1000;
    params.flatFee = true;
    console.log(params);
    // create logic sig
    // samplearg.teal
    // This code is meant for learning purposes only
    // It should not be used in production
    // arg_0
    // btoi
    // int 123
    // ==
    // see more info here: https://developer.algorand.org/docs/features/asc1/sdks/#accessing-teal-program-from-sdks
    var fs = require('fs'),
        path = require('path'),
        filePath = path.join(__dirname, 'samplearg.teal');
    // filePath = path.join(__dirname, <'fileName'>);
    let data = fs.readFileSync(filePath);
    let results = await algodclient.compile(data).do();
    console.log("Hash = " + results.hash);
    console.log("Result = " + results.result);
    // let program = new Uint8Array(Buffer.from(<"base64-encoded-program">, "base64"));
    let program = new Uint8Array(Buffer.from(results.result, "base64"));
    // Use this if no args
    // let lsig = algosdk.makeLogicSig(program);

    // String parameter
    // let args = ["<my string>"];
    // let lsig = algosdk.makeLogicSig(program, args);
    // Integer parameter
    let args = [[123]];
    let lsig = algosdk.makeLogicSig(program, args);

    // sign the logic signature with an account sk
    lsig.sign(myAccount.sk);
    
    // Setup a transaction
    let sender = myAccount.addr;
    let receiver = "SOEI4UA72A7ZL5P25GNISSVWW724YABSGZ7GHW5ERV4QKK2XSXLXGXPG5Y";
    // let receiver = "<receiver-address>"";
    let amount = 10000;
    let closeToRemaninder = undefined;
    let note = undefined;
    let txn = algosdk.makePaymentTxnWithSuggestedParams(sender, receiver, amount, closeToRemaninder, note, params)
    
    // source debugging
    dryrunResponse = await dryrunDebugging(lsig, txn, data);
    var textedJson = JSON.stringify(dryrunResponse, undefined, 4);
    console.log("source Response ");  
    console.log(textedJson);

    // compile debugging
    dryrunResponse = await dryrunDebugging(lsig, txn, null);
    var textedJson = JSON.stringify(dryrunResponse, undefined, 4);
    console.log("compile Response ");   
    console.log(textedJson);

    // Create the LogicSigTransaction with contract account LogicSig
    let rawSignedTxn = algosdk.signLogicSigTransactionObject(txn, lsig);
    // fs.writeFileSync("simple.stxn", rawSignedTxn.blob);
    // send raw LogicSigTransaction to network 

    let tx = (await algodclient.sendRawTransaction(rawSignedTxn.blob).do());
    console.log("Transaction : " + tx.txId);    
    await waitForConfirmation(algodclient, tx.txId);

})().catch(e => {
    console.log(e.message);
    console.log(e);
});
async function dryrunDebugging(lsig, txn, data) {
    if (data == null)
    {
        //compile
        txns = [{
            lsig: lsig,
            txn: txn,
        }];        
    }
    else
    {
        // source
        txns = [{
            txn: txn,
        }];
        sources = [new algosdk.modelsv2.DryrunSource("lsig", data.toString("utf8"), 0)];
    }
    const dr = new algosdk.modelsv2.DryrunRequest({
        txns: txns,
        sources: sources,
    });
    dryrunResponse = await algodclient.dryrun(dr).do();
    return dryrunResponse;
}
async function dryrunDebuggingdr(lsig, txn, data) {
    if (data == null) {
        //compile
        txns = [{
            lsig: lsig,
            txn: txn,
        }];
    }
    else {
        // source
        txns = [{
            txn: txn,
        }];
        sources = [new algosdk.modelsv2.DryrunSource("lsig", data.toString("utf8"), 0)];
    }
    const dr = new algosdk.modelsv2.DryrunRequest({
        txns: txns,
        sources: sources,
    });

    return dr;
}
// output should look like this
// {
//     "error": "",
//         "protocol-version": "https://github.com/algorandfoundation/specs/tree/e5f565421d720c6f75cdd186f7098495caf9101f",
//             "txns": [
//                 {
//                     "disassembly": [
//                         "// version 1",
//                         "intcblock 123",
//                         "arg_0",
//                         "btoi",
//                         "intc_0",
//                         "==",
//                         ""
//                     ],
//                     "logic-sig-messages": [
//                         "PASS"
//                     ],
//                     "logic-sig-trace": [
//                         {
//                             "line": 1,
//                             "pc": 1,
//                             "stack": []
//                         },
//                         {
//                             "line": 2,
//                             "pc": 4,
//                             "stack": []
//                         },
//                         {
//                             "line": 3,
//                             "pc": 5,
//                             "stack": [
//                                 {
//                                     "bytes": "ew==",
//                                     "type": 1,
//                                     "uint": 0
//                                 }
//                             ]
//                         },
//                         {
//                             "line": 4,
//                             "pc": 6,
//                             "stack": [
//                                 {
//                                     "bytes": "",
//                                     "type": 2,
//                                     "uint": 123
//                                 }
//                             ]
//                         },
//                         {
//                             "line": 5,
//                             "pc": 7,
//                             "stack": [
//                                 {
//                                     "bytes": "",
//                                     "type": 2,
//                                     "uint": 123
//                                 },
//                                 {
//                                     "bytes": "",
//                                     "type": 2,
//                                     "uint": 123
//                                 }
//                             ]
//                         },
//                         {
//                             "line": 6,
//                             "pc": 8,
//                             "stack": [
//                                 {
//                                     "bytes": "",
//                                     "type": 2,
//                                     "uint": 1
//                                 }
//                             ]
//                         }
//                     ]
//                 }
//             ]
// }
