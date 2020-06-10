//AccountsAssetIDMinBalance.js
const algosdk = require('algosdk');
const indexer_token = "";
const indexer_server = "http://localhost";
const indexer_port = 8980;

// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);

(async () => {
    let assetIndex = 312769;
    let currencyGreater = 100;
    let accountInfo = await indexerClient.searchAccounts()
        .assetID(assetIndex)
        .currencyGreaterThan(currencyGreater).do();
    console.log("Information for Account Info for Asset: " + JSON.stringify(accountInfo, undefined, 2));
})().catch(e => {
    console.log(e);
    console.trace();
});