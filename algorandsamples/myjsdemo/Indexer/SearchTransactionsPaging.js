//SearchTransactionsPaging.js
const algosdk = require('algosdk');
const indexer_token = "";
const indexer_server = "http://localhost";
const indexer_port = 8980;

// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);

let nexttoken = "";
let numtx = 1;
let responseall = "";

// loop until there are no more tranactions in the response
// for the limit(max is 1000  per request)
    
(async () => {
    let min_amount = 100000000000000;
    let limit = 10;
    while (numtx > 0) {
        // execute code as long as condition is true
        let next_page = nexttoken;
        let response = await indexerClient.searchForTransactions()
            .limit(limit)
            .currencyGreaterThan(min_amount)
            .nextToken(next_page).do();
        let transactions = response['transactions'];
        numtx = transactions.length;
        // concatinate response

        if (numtx > 0)
        {
            nexttoken = response['next-token'];
         //   responseall = responseall + JSON.stringify(response)    
            responseall = responseall + JSON.stringify(response);      
        }
    }
    // json.load method converts JSON string to Python Object
    parsed = JSON.parse(responseall);

    console.log("Information for Transaction search: " + JSON.stringify(parsed, undefined, 2));
})().catch(e => {
    console.log(e);
    console.trace();
});