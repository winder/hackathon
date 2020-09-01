// SearchTxAddressTxId.js
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
    let address = "RBSTLLHK2NJDL3ZH66MKSEX3BE2OWQ43EUM7S7YRVBJ2PRDRCKBSDD3YD4";
    let txid = "GZKSVXCVQASQ2KLI7JUWZ2LP54WAKPVA3TC2ZGQVAHKIWEMMFNEQ"; 
    let response = await indexerClient.searchForTransactions()
        .address(address)
        .txid(txid).do();
    console.log("txid: GZKSVXCVQASQ2KLI7JUWZ2LP54WAKPVA3TC2ZGQVAHKIWEMMFNEQ = " + JSON.stringify(response, undefined, 2));
    }  
)().catch(e => {
    console.log(e);
    console.trace();
});