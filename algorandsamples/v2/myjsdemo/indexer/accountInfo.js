// AccountInfo.js
// requires algosdk@1.6.1 or higher 
// verify installed version
// npm list algosdk
const algosdk = require('algosdk');

// const indexer_token = "";
// const indexer_server = "http://localhost";
// const indexer_port = 8980;

// purestake hackathon 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab'

const indexer_server = "https://testnet-algorand.api.purestake.io/idx2/";
const indexer_port = "";
const indexer_token = {
  'X-API-key': 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab',
}

// const indexer_server = "https://indexer-internal-testnet.aws.algodev.network/";
// const indexer_port = "443";
// const indexer_token = "YddOUGbAjHLr1uPZtZwHOvMDmXvR1Zvw1f3Roj2P"
// Instantiate the indexer client wrapper
// let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);
(async () => {

    let acct = "NI2EDLP2KZYH6XYLCEZSI5SSO2TFBYY3ZQ5YQENYAGJFGXN4AFHPTR3LXU";   
    let accountInfo = await indexerClient.lookupAccountByID(acct).do();
    console.log("Information for Account: " + JSON.stringify(accountInfo, undefined, 2));
})().catch(e => {
    console.log(e.message);
    console.trace();
});


