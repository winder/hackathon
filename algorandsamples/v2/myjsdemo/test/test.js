const algosdk = require('algosdk');
// function used to wait for a tx confirmation
const waitForConfirmation = async function (algodclient, txId) {
    let status = (await algodclient.status().do());
    let lastRound = status["last-round"];
    while (true) {
        const pendingInfo = await algodclient.pendingTransactionInformation(txId).do();
        if (pendingInfo["confirmed-round"] !== null && pendingInfo["confirmed-round"] > 0) {
            //Got the completed Transaction
            console.log("Transaction " + txId + " confirmed in round " + pendingInfo["confirmed-round"]);
            break;
        }
        lastRound++;
        await algodclient.statusAfterBlock(lastRound).do();
    }
};
async function gettingStartedExample() {
    try {
        const token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        const server = "http://localhost";
        const port = 4001;
        let algodClient = new algosdk.Algodv2(token, server, port);
        const receiver = "GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A";
        const passphrase = "liquid million govern habit nasty danger spoil air monitor lobster solar misery confirm problem tuna hollow ritual assume mean return enrich mistake seven abstract tent";
        let myAccount = algosdk.mnemonicToSecretKey(passphrase);
        console.log("My address: %s", myAccount.addr);
        let accountInfo = await algodClient.accountInformation(myAccount.addr).do();
        console.log("Account balance: %d microAlgos", accountInfo.amount);
        let params = await algodClient.getTransactionParams().do();
        console.log(params);
        let note = algosdk.encodeObj("Hello World");
        console.log(note);
        //comment out the next two lines to use suggested fee
        params.fee = 1000;
        params.flatFee = true;
        let txn = algosdk.makePaymentTxnWithSuggestedParams(myAccount.addr, receiver, 1000000, undefined, note, params);
        let signedTxn = txn.signTxn(myAccount.sk)
        let txId = txn.txID().toString();
        console.log("Signed transaction with txID: %s", txId);
        await algodClient.sendRawTransaction(signedTxn).do();
        // Wait for confirmation
        await waitForConfirmation(algodClient, txId);
        // Read the transaction from the blockchain
        let confirmedTxn = await algodClient.pendingTransactionInformation(txId).do();
        console.log("Transaction information: %o", confirmedTxn.txn.txn);
        console.log("Decoded note: %s", algosdk.decodeObj(confirmedTxn.txn.txn.note));
    }
    catch (err) {
        console.log("err", err.text);
    }
};
gettingStartedExample();