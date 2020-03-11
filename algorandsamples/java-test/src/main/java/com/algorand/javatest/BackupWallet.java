package com.algorand.javatest;

import com.algorand.algosdk.kmd.client.ApiException;
import com.algorand.algosdk.kmd.client.KmdClient;
import com.algorand.algosdk.kmd.client.api.KmdApi;
import com.algorand.algosdk.kmd.client.auth.ApiKeyAuth;
import com.algorand.algosdk.kmd.client.model.APIV1GETWalletsResponse;
import com.algorand.algosdk.kmd.client.model.APIV1POSTMasterKeyExportResponse;
import com.algorand.algosdk.kmd.client.model.APIV1Wallet;
import com.algorand.algosdk.kmd.client.model.ExportMasterKeyRequest;
import com.algorand.algosdk.kmd.client.model.InitWalletHandleTokenRequest;
import com.algorand.algosdk.mnemonic.Mnemonic;

/**
 * Hello world!
 *
 */
public class BackupWallet {
    public static void main(String args[]) throws Exception {
        // Get the values for the following two settings in the
        // kmd.net and kmd.token files within the data directory
        // of your node.
        final String KMD_API_ADDR = "http://localhost:7833";
        final String KMD_API_TOKEN = "your KMD_API_TOKEN";

        // Create a wallet with kmd rest api
        KmdClient client = new KmdClient();
        client.setBasePath(KMD_API_ADDR);
        // Configure API key authorization: api_key
        ApiKeyAuth api_key = (ApiKeyAuth) client.getAuthentication("api_key");
        api_key.setApiKey(KMD_API_TOKEN);
        KmdApi kmdApiInstance = new KmdApi(client);

        APIV1GETWalletsResponse wallets;
        String walletId = null;
        try {
            // Get all wallets from kmd
            // Loop through them and find the one we
            // are interested in them
            wallets = kmdApiInstance.listWallets();
            for (APIV1Wallet wal : wallets.getWallets()) {
                System.out.println(wal.getName());
                if (wal.getName().equals("mywallet")) {
                    walletId = wal.getId();
                    break;
                }
            }
            if (walletId != null) {
                // create REST request to get wallet token
                InitWalletHandleTokenRequest walletHandleRequest = new InitWalletHandleTokenRequest();
                walletHandleRequest.setWalletId(walletId);
                walletHandleRequest.setWalletPassword("test");
                // execute request to get the wallet token
                String token = kmdApiInstance.initWalletHandleToken(walletHandleRequest).getWalletHandleToken();
                // create REST request to create new key with wallet token
                ExportMasterKeyRequest expRequest = new ExportMasterKeyRequest();
                expRequest.setWalletHandleToken(token);
                expRequest.setWalletPassword("test");

                APIV1POSTMasterKeyExportResponse expResponse = kmdApiInstance.exportMasterKey(expRequest);
                String mnem = Mnemonic.fromKey(expResponse.getMasterDerivationKey());

                System.out.println("Backup Phrase = " + mnem);

            } else {
                System.out.println("Did not Find Wallet");
            }

        } catch (ApiException e) {
            e.printStackTrace();
        }
    }

}
