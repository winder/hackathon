// SearchTransactionsPaging.js
// requires algosdk@1.6.1 or higher 
// verify installed version
// npm list algosdk

const algosdk = require('algosdk');
const indexer_token = "";
const indexer_server = "http://localhost";
const indexer_port = 8980;
// const indexer_server = "https://testnet-algorand.api.purestake.io/idx2/";
// const indexer_port = "";
// const indexer_token = {
//     'X-API-key': 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab',
// }

// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);

let nexttoken = "";
let numtx = 1;
// loop until there are no more transactions in the response
// for the limit(max limit is 1000  per request)    
(async () => {
    let min_amount = 500000000000;
    let limit = 2;
    while (numtx > 0) {
        // execute code as long as condition is true
        let next_page = nexttoken;
        let response = await indexerClient.searchForTransactions()
            .limit(limit)
            .currencyGreaterThan(min_amount)
            .nextToken(next_page).do();
        let transactions = response['transactions'];
        numtx = transactions.length;
        if (numtx > 0)
        {
            nexttoken = response['next-token']; 
            console.log("Transaction Information: " + JSON.stringify(response, undefined, 2));           
        }
    }
})().catch(e => {
    console.log(e);
    console.trace();
});