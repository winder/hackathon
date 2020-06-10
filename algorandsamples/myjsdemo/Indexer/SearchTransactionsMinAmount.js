//SearchTransactionsMinAmount.js
const algosdk = require('algosdk');
const indexer_token = "";
const indexer_server = "http://localhost";
const indexer_port = 8980;

// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);

(async () => {
    let currencyGreater = 10;
    let transactionInfo = await indexerClient.searchForTransactions()
        .currencyGreaterThan(currencyGreater).do();
    console.log("Information for Transaction search: " + JSON.stringify(transactionInfo, undefined, 2));
})().catch(e => {
    console.log(e);
    console.trace();
});