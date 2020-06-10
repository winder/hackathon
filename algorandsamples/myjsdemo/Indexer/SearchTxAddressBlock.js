//SearchTxAddressBlock.js
const algosdk = require('algosdk');
const indexer_token = "";
const indexer_server = "http://localhost";
const indexer_port = 8980;

// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);

    
(async () => {
    let address = "XIU7HGGAJ3QOTATPDSIIHPFVKMICXKHMOR2FJKHTVLII4FAOA3CYZQDLG4";
    let round = 7048877;
    let response = await indexerClient.searchForTransactions()
        .address(address)
        .round(round).do();
    console.log("Information for Transaction search: " + JSON.stringify(response, undefined, 2));
    }
   
)().catch(e => {
    console.log(e);
    console.trace();
});