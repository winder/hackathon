//SearchTransactionsNote.js
const algosdk = require('algosdk');
const indexer_token = "";
const indexer_server = "http://localhost";
const indexer_port = 8980;

// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);

// Buffer() requires a number, array or string as the first parameter, and an optional encoding type as the second parameter. 
// Default is utf8, possible encoding types are ascii, utf8, ucs2, base64, binary, and hex
// let buffer = new Buffer.from('showing prefix');

(async () => {

    //   let s = buffer.toString('base64');   
    let s = "c2hvd2luZyBwcmVmaXg=";
    let transactionInfo = await indexerClient.searchForTransactions()
        .notePrefix(s).do();
    console.log("Information for Transaction search: " + JSON.stringify(transactionInfo, undefined, 2));
})().catch(e => {
    console.log(e);
    console.trace();
});