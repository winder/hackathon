const algosdk = require('algosdk');

//Retrieve the token, server and port values for your installation in the algod.net
//and algod.token files within the data directory or use a standup instance if available

    // sandbox
    const token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const server = "http://localhost";
    const port = 4001;

//Hackathon TestNet
// const token = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
// const server = "http://hackathon.algodev.network";
// const port = 9100;

//Recover the account

var mnemonic = "awake used crawl list cruel harvest useful flag essay speed glad salmon camp sudden ride symptom test kind version together project inquiry diet abandon budget";

var recoveredAccount = algosdk.mnemonicToSecretKey(mnemonic);
console.log(recoveredAccount.addr);
//check to see if account is valid
var isValid = algosdk.isValidAddress(recoveredAccount.addr);
console.log("Is this a valid address: " + isValid);

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

//submit the transaction
(async() => {
    //Get the relevant params from the algod
    let params = await algodclient.getTransactionParams();
    console.log("here" + params);
    let endRound = params.lastRound + parseInt(1000);
    let fee = await algodclient.suggestedFee();

    // create LogicSig object and sign with our secret key
    // let program = Uint8Array.from([1, 32, 1, 0, 34]);  
    // For int 1 use program that returns 0 or (true)
    // int 0 => never transfer money use ASABACI= (false)
    let program = new Uint8Array(Buffer.from("ASABICI=", "base64"));
    // makeLogicSig method takes the program and parameters
    // in this example we have no parameters
    // If we did have parameters you would add them like
    // let args = [
    //    Uint8Array.from("123"),
    //    Uint8Array.from("456")
    // ];
    // And remember TEAL parameters are order specfic
    console.log("program " + program);
    let lsig = algosdk.makeLogicSig(program);
    // sign the logic with your accounts secret
    // key. This is essentially giving your
    // key authority to anyone with the lsig
    // and if the logic returns true
    // exercise extreme care
    // If this were a escrow account usage (Contract Account)
    // you would not do this sign operation

    // Account delegation
    lsig.sign(recoveredAccount.sk);

    // At this point you can save the lsig off and share
    // as your delegated signature.
    // The LogicSig class supports serialization and
    // provides the lsig.toByte and fromByte methods
    // to easily convert for file saving and 
    // reconstituting and LogicSig object

    //create a transaction
    let txn = {
        "from": recoveredAccount.addr,
        "to": "SOEI4UA72A7ZL5P25GNISSVWW724YABSGZ7GHW5ERV4QKK2XSXLXGXPG5Y",
        "fee": params.fee,
        "amount": 200000,
        "firstRound": params.lastRound,
        "lastRound": endRound,
        "genesisID": params.genesisID,
        "genesisHash": params.genesishashb64
    };

    // create logic signed transaction.
    // Had this been an escrow the lsig would not contain the
    // signature but would be submitted the same way
    let rawSignedTxn = algosdk.signLogicSigTransaction(txn, lsig);

    //Submit the lsig signed transaction
    let tx = (await algodclient.sendRawTransaction(rawSignedTxn.blob));
    console.log("Transaction : " + tx.txId);
    // wait for transaction to be confirmed
    await waitForConfirmation(algodclient, tx.txId);

})().catch(e => {
    console.log(e);
});

// const token = "61bddc5e84ed1ea3f8e9143a70da8fbe3478ae5e06caa52064332b09d158bb70";
// const server = "http://127.0.0.1";
// const port = 8080;

// goal node start -d data
// const token = "ec068ed68dc176e61a07b897c53835c6fb956271ce70aad3b204f0db6fa34f6d"
// const server = "http://127.0.0.1";
// const port = 8080 

//GMHW5XK626T5PUNRWP2DKEISTCDP2VFR3ZLW4D5WBYYXWPPN4R6JOCER7A
//THQHGD4HEESOPSJJYYF34MWKOI57HXBX4XR63EPBKCWPOJG5KUPDJ7QJCM
// var mnemonic = "portion never forward pill lunch organ biology" +
//     " weird catch curve isolate plug innocent skin grunt" +
//     " bounce clown mercy hole eagle soul chunk type absorb trim";
//var mnemonic = "bread potato flip monster maid voice modify brown swing pride ski detail brick vague require surround smile vacant about fame regret update indicate ability dice";
