package com.algorand.javatest;

import java.math.BigInteger;

import com.algorand.algosdk.account.Account;
import com.algorand.algosdk.algod.client.AlgodClient;
import com.algorand.algosdk.algod.client.ApiException;
import com.algorand.algosdk.algod.client.api.AlgodApi;
import com.algorand.algosdk.algod.client.auth.ApiKeyAuth;
import com.algorand.algosdk.algod.client.model.NodeStatus;
import com.algorand.algosdk.algod.client.model.Transaction;
import com.algorand.algosdk.algod.client.model.TransactionList;

import org.threeten.bp.LocalDate;

/**
 * Get transactions for an account in the
 *
 */
public class GetAccountTransactions {
    public static void main(String args[]) throws Exception {

        // Algorand Hackathon
        final String ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";
        final String ALGOD_API_TOKEN = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";

        // your own node
        // final String ALGOD_API_ADDR = "http://localhost:8080";
        // final String ALGOD_API_TOKEN = "your ALGOD_API_TOKEN";

        // Purestake 
        // final String ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps1";
        // final String ALGOD_API_TOKEN = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab";

        final String SRC_ACCOUNT_BACKUP = "your account Mnemonic phrase";

        AlgodClient client = (AlgodClient) new AlgodClient().setBasePath(ALGOD_API_ADDR);
        // needed for Purestake , else ignored
        client.addDefaultHeader("X-API-Key", ALGOD_API_TOKEN);

        ApiKeyAuth api_key = (ApiKeyAuth) client.getAuthentication("api_key");
        api_key.setApiKey(ALGOD_API_TOKEN);
        AlgodApi algodApiInstance = new AlgodApi(client);

        // First, get network status
        try {
            NodeStatus status = algodApiInstance.getStatus();
            BigInteger lastRound = status.getLastRound();
            BigInteger maxtx = BigInteger.valueOf(30);
            BigInteger firstRound = lastRound.subtract(BigInteger.valueOf(1000)); // 1000
            Account recoveredAccount = new Account(SRC_ACCOUNT_BACKUP);
            // Get the transactions for the address in the last 1k rounds
            // Note that this call requires that the node is an archival node as we are
            // going back 1k rounds
            LocalDate today = LocalDate.now();
            LocalDate yesterday = today.minusDays(1);

            TransactionList tList = algodApiInstance.transactions(recoveredAccount.getAddress().toString(), firstRound,
                    lastRound, yesterday, today, maxtx);
            for (Transaction tx : tList.getTransactions()) {
                System.out.println(tx.toString());
            }
            System.out.println("Finished");
        } catch (ApiException e) {
            e.printStackTrace();
        }

    }

}
