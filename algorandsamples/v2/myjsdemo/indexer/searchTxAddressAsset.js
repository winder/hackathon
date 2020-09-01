// SearchTxAddressAsset.js
// requires algosdk@1.6.1 or higher 
// verify installed version
// npm list algosdk

const algosdk = require('algosdk');
// const indexer_token = "";
// const indexer_server = "http://localhost";
// const indexer_port = 8980;
const indexer_server = "https://testnet-algorand.api.purestake.io/idx2/";
const indexer_port = "";
const indexer_token = {
    'X-API-key': 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab',
}
// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);
    
(async () => {
    let address = "AMF3CVE4MFZM24CCFEWRCOCWW7TEDJQS3O26OUBRHZ3KWKUBE5ZJRNZ3OY";
    let asset_id = 12215366;
    let min_amount = 5;

    let response = await indexerClient.searchForTransactions()
            .address(address)
            .currencyGreaterThan(min_amount)
            .assetID(asset_id).do();
    console.log("Information for Transaction search: " + JSON.stringify(response, undefined, 2));
    }   
)().catch(e => {
    console.log(e);
    console.trace();
});