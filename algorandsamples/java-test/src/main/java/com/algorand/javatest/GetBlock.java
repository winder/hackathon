package com.algorand.javatest;

import com.algorand.algosdk.algod.client.AlgodClient;

import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.algod.client.model.*;

/**
 * Get Block Example
 *
 */
public class GetBlock {
    public static void main(String args[]) throws Exception {

        // Get the values for the following two settings in the
        // algod.net and algod.token files within the data directory
        // of your node.

        // Algorand Hackathon
        final String ALGOD_API_TOKEN = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
        final String ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";

        // your own node
        // final String ALGOD_API_ADDR = "http://localhost:8080";
        // final String ALGOD_API_TOKEN = "your ALGOD_API_TOKEN";
        // Purestake 
        // final String ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps1";
        // final String ALGOD_API_TOKEN = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab";

        // Create an instance of the algod API client
        AlgodClient client = (AlgodClient) new AlgodClient().setBasePath(ALGOD_API_ADDR);
        // needed for Purestake , else ignored
        client.addDefaultHeader("X-API-Key", ALGOD_API_TOKEN);
        ApiKeyAuth api_key = (ApiKeyAuth) client.getAuthentication("api_key");
        api_key.setApiKey(ALGOD_API_TOKEN);
        AlgodApi algodApiInstance = new AlgodApi(client);
        // Get the lastest Block
        try {
            NodeStatus status = algodApiInstance.getStatus();
            System.out.println("Algorand network status: " + status);
            // Get block for the latest round
            Block blk = algodApiInstance.getBlock(status.getLastRound());
            System.out.println(blk.toString());
        } catch (ApiException e) {
            System.err.println("Exception when calling algod#getStatus or getBlock");
            e.printStackTrace();
        }

    }

}
