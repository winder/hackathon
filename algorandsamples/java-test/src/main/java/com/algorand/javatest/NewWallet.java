package com.algorand.javatest;

import com.algorand.algosdk.kmd.client.ApiException;
import com.algorand.algosdk.kmd.client.KmdClient;
import com.algorand.algosdk.kmd.client.api.KmdApi;
import com.algorand.algosdk.kmd.client.auth.ApiKeyAuth;
import com.algorand.algosdk.kmd.client.model.APIV1POSTWalletResponse;
import com.algorand.algosdk.kmd.client.model.CreateWalletRequest;
import com.algorand.algosdk.kmd.client.model.GenerateKeyRequest;
import com.algorand.algosdk.kmd.client.model.InitWalletHandleTokenRequest;

/**
 * Hello world!
 *
 */
public class NewWallet {
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

        APIV1POSTWalletResponse wallet;
        try {
            // create the REST request
            CreateWalletRequest req = new CreateWalletRequest().walletName("mywallet").walletPassword("test")
                    .walletDriverName("sqlite");
            // create the wallet
            wallet = kmdApiInstance.createWallet(req);
            String wallId = wallet.getWallet().getId();
            // create REST request to get wallet token
            InitWalletHandleTokenRequest walletHandleRequest = new InitWalletHandleTokenRequest();
            walletHandleRequest.setWalletId(wallId);
            walletHandleRequest.setWalletPassword("test");
            // execute request to get the wallet token
            String token = kmdApiInstance.initWalletHandleToken(walletHandleRequest).getWalletHandleToken();
            // create REST request to create new key with wallet token
            GenerateKeyRequest genAcc = new GenerateKeyRequest();
            genAcc.setWalletHandleToken(token);
            // execute request to generate new key(account)
            String newAccount = kmdApiInstance.generateKey(genAcc).getAddress();
            System.out.println("New Address = " + newAccount);

        } catch (ApiException e) {
            e.printStackTrace();
        }
    }

}
