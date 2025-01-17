// LookupApplication.java
// requires java-algorand-sdk 1.4.0 or higher (see pom.xml)
package com.algorand.javatest.indexer;

import com.algorand.algosdk.v2.client.common.IndexerClient;
import com.algorand.algosdk.v2.client.common.Client;
import com.algorand.algosdk.crypto.Address;
import org.json.JSONObject;
import com.algorand.algosdk.v2.client.model.ApplicationResponse;
import com.algorand.algosdk.v2.client.common.Response;

public class LookupApplication {
    public Client indexerInstance = null;
    // utility function to connect to a node
    private Client connectToNetwork(){
        // final String INDEXER_API_ADDR = "http://localhost";
        // final int INDEXER_API_PORT = 59998;
        // final String INDEXER_API_ADDR = "https://indexer-internal-betanet.aws.algodev.network/";
        // final String INDEXER_TOKEN = "YddOUGbAjHLr1uPZtZwHOvMDmXvR1Zvw1f3Roj2PT1ufenXbNyIxIz0IeznrLbDsF";
        // final int INDEXER_API_PORT = 443;
        // final String INDEXER_API_ADDR = "localhost";
        // final String INDEXER_TOKEN = "";
        // final int INDEXER_API_PORT = 8980;
        final String INDEXER_API_ADDR = "https://testnet-algorand.api.purestake.io/idx2";
        final int INDEXER_API_PORT = 443;
        final String INDEXER_TOKEN = "";

     //   IndexerClient indexerClient = new IndexerClient(INDEXER_API_ADDR, INDEXER_API_PORT);
        IndexerClient indexerClient = new IndexerClient(INDEXER_API_ADDR, INDEXER_API_PORT, INDEXER_TOKEN); 
        return indexerClient;
    }

    public static void main(String args[]) throws Exception {
        String[] headers = { "X-API-Key" };
        String[] values = { "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab" };
        LookupApplication ex = new LookupApplication();
        IndexerClient indexerClientInstance = (IndexerClient)ex.connectToNetwork();
        // Long application_id = Long.valueOf(2672020);
         Long application_id = Long.valueOf(12397001);   
        // Response<ApplicationResponse> response = indexerClientInstance.lookupApplicationByID(application_id).execute(); 
        Response<ApplicationResponse> response = indexerClientInstance.lookupApplicationByID(application_id).execute(headers, values);    
        JSONObject jsonObj = new JSONObject(response.body().toString());
        System.out.println("Response Info: " + jsonObj.toString(2)); // pretty print json
    }
 }

// response information should look similar to this...
//  Response Info:
//  {
//   "application": {
//     "id": 22,
//     "params": {
//       "global-state": [],
//       "creator": "GHFRLVOMKJNTJ4HY3P74ZR4CNE2PB7CYAUAJ6HVAVVDX7ZKEMLJX6AAF4M",
//       "local-state-schema": {
//         "num-uint": 0,
//         "num-byte-slice": 0
//       },
//       "global-state-schema": {
//         "num-uint": 0,
//         "num-byte-slice": 0
//       }
//     }
//   },
//   "current-round": 377
// }