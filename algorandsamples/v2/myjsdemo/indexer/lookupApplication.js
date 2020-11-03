// AccountInfo.js
// requires algosdk@1.6.1 or higher 
// verify installed version
// npm list algosdk
const algosdk = require('algosdk');

const indexer_token = "";
const indexer_server = "http://localhost";
const indexer_port = 59998;
// const indexer_server = "https://testnet-algorand.api.purestake.io/idx2/";
// const indexer_port = "";
// const indexer_token = {
//     'X-API-key': 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab',
// }

// Instantiate the indexer client wrapper
let indexerClient = new algosdk.Indexer(indexer_token, indexer_server, indexer_port);

// let response = await indexerClient.lookupApplications(12215437).do();
(async () => {
    let response = await indexerClient.lookupApplications(12397001).do();
    console.log("Response: " + JSON.stringify(response, undefined, 2));
})().catch(e => {
    console.log(e.message);
    console.trace();
});

// Repsonse should look similar to this...

// Response: {
// "application": {
//     "id": 59,
//         "params": {
//         "approval-program": "AiACAQAmAgNmb28DYmFyIihlQQAIKRJBAANCAAIjQyJD",
//             "clear-state-program": "ASABASI=",
//                 "creator": "7IROB3J2FTR7LYQA3QOUYSTKCQTSVJK4FTTC77KWSE5NMRATEZXP6TARPA",
//                     "global-state-schema": {
//             "num-byte-slice": 0,
//                 "num-uint": 0
//         },
//         "local-state-schema": {
//             "num-byte-slice": 0,
//                 "num-uint": 0
//         }
//     }
// },
// "current-round": 377
// }
