﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Algorand;
using Account = Algorand.Account;
using Algorand.Algod.Client.Api;
using Algorand.Algod.Client.Model;
using Algorand.Algod.Client;
using Transaction = Algorand.Transaction;

using Org.BouncyCastle.Crypto.Parameters;
using System.IO;
using Xamarin.Essentials;
using System.Numerics;

namespace algorandapp
{
    public partial class Accounts : ContentPage
    {

        public const string ALGOD_API_TOKEN = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";
        public const string ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps1";
        public const string ALGOD_API_ADDR_TESTNET = "https://testnet-algorand.api.purestake.io/ps1";
        public const string ALGOD_API_ADDR_BETANET = "https://betanet-algorand.api.purestake.io/ps1";

        //public const string ALGOD_API_TOKEN = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
        //public const string ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";

        //   https://betanet-algorand.api.purestake.io/ps1
        public AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);
       // public AlgodClient client = new AlgodClient();
        

        
        //Purestake
        // https://testnet-algorand.api.purestake.io/ps1
        // WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb
//        final String ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps1";
                
//        AlgodClient client = new AlgodClient();

//        client.addDefaultHeader("X-API-Key", "......");
        
//        client.setBasePath(ALGOD_API_ADDR);

//        AlgodApi algodApiInstance = new AlgodApi(client);

//        NodeStatus status = null;
        
//        try {
//            status = algodApiInstance.getStatus();
//        } catch (Exception e) {
//            System.err.print("Failed to get algod status: " + e.getMessage());
//        }
        
//        if(status!=null) {
//            System.out.println("algod last round: " + status.getLastRound());
//System.out.println("algod time since last round: " + status.getTimeSinceLastRound());
//System.out.println("algod catchup: " + status.getCatchupTime());
//System.out.println("algod latest version: " + status.getLastConsensusVersion());

//BigInteger lastRound = status.getLastRound();
//            try {
//                Block block = algodApiInstance.getBlock(lastRound);
//System.out.println("Block info: " + block.toString());
//            } catch (Exception e) {
//                System.err.print("Failed to get block info: " + e.getMessage());
//            }
//        }



        public Accounts()
        {
 
        InitializeComponent();
        Appearing += Accounts_Appearing;




            // client.addDefaultHeader("X-API-Key", "......");
            //AlgodClient client = new AlgodClient();
            //client.Configuration.DefaultHeader.Add("X-API-Key", ALGOD_API_TOKEN);
            //client.RestClient.BaseUrl = new Uri(ALGOD_API_ADDR);

            ////  AlgodApi algodApiInstance = new AlgodApi(client);
            //AlgodApi algodApiInstance = new AlgodApi(client);
        }

        private void Accounts_Appearing(object sender, EventArgs e)
        {
            buttonstate();
        }

        public async void buttonstate ()
        {

            var account1 = await SecureStorage.GetAsync("Account 1");
            var account2 = await SecureStorage.GetAsync("Account 2");
            var account3 = await SecureStorage.GetAsync("Account 3");
            var network = await SecureStorage.GetAsync("Network");
            var msig = await SecureStorage.GetAsync("Multisig");
            var transaction = await SecureStorage.GetAsync("Transaction");
            var multisigtransaction = await SecureStorage.GetAsync("MultisigTransaction");
            EnableNetworkToggles(network);
            CreateMultiSig.IsVisible = true;
            Transaction.IsVisible = true;
            MultisigTransaction.IsVisible = true;
            if (account1 == null)
            {
                // this account is not generated yet
                GenerateAccount1.IsEnabled = true;
                GetAccount1Info.IsVisible = false;
                GenerateAccount1.Text = "Generate Account 1";

            }
            else
            {
                GenerateAccount1.Text = "Account 1 created";
                GenerateAccount1.IsEnabled = false;
                GetAccount1Info.IsVisible = true;
                DisableNetworkToggles(network);
            }
            if (account2 == null)
            {
                // this account is not generated yet
                GenerateAccount2.IsEnabled = true;
                GetAccount2Info.IsVisible = false;
                GenerateAccount2.Text = "Generate Account 2";

            }
            else
            {
                GenerateAccount2.Text = "Account 2 created";
                GenerateAccount2.IsEnabled = false;
                GetAccount2Info.IsVisible = true;
                DisableNetworkToggles(network);
            }
            if (account3 == null)
            {
                // this account is not generated yet
                GenerateAccount3.IsEnabled = true;
                GetAccount3Info.IsVisible = false;
                GenerateAccount3.Text = "Generate Account 3";
            }
            else
            {

                GenerateAccount3.Text = "Account 3 created";
                GenerateAccount3.IsEnabled = false;
                GetAccount3Info.IsVisible = true;
                DisableNetworkToggles(network);
            }

            if ((account1 != null) & (account2 != null) & (account3 != null))
            {
                // all accounts created - leave state

                DisableNetworkToggles(network);
                myLabel.Text = "Accounts 1, 2 and 3 have been created on " + network;
                myLabel2.Text = "";
                Entry3.Text = "";


                if (msig == null)
                {
                    CreateMultiSig.IsEnabled = true;
                    CreateMultiSig.Text = "Create Multisig Address";
                    GetMultiSig.IsVisible = false;
                    // disbale multisig transaction
                }
                else
                {
                    CreateMultiSig.IsEnabled = false;
                    CreateMultiSig.Text = "Multisig created = " + msig.ToString();
                    GetMultiSig.IsVisible = true;
                    myLabel2.Text = "Multisig created - version = 1, threshold = 2, number of accounts = 3";
                    // enable send multisig transaction
                }
    
                if (transaction == null)
                {
                    Transaction.IsEnabled = true;
                    Transaction.Text = "Transaction from Account 1 to 2";
                    GetTransaction.IsVisible = false;
                }
                else
                {
                    Transaction.IsEnabled = true;
                    Transaction.Text = "Transaction sent from account 1 to 2";
                    GetTransaction.IsVisible = true;   
                }
                if (msig != null)
                {
                    if (multisigtransaction == null)
                    {
                        // only enable if multisigaddress created
                        MultisigTransaction.IsEnabled = true;
                        MultisigTransaction.Text = "Send Multisig Transaction to Account 3";
                        GetMultiSigTx.IsVisible = false;
                    }
                    else
                    {
                        MultisigTransaction.IsEnabled = false;
                        MultisigTransaction.Text = "Sent Multisig Transaction";
                        GetMultiSigTx.IsVisible = true;
                    }
                }

            }

        }

        private void DisableNetworkToggles(string network)
        {
            if (network == "TestNet")
            {
                BetaNetToggle.IsToggled = false;
                TestNetToggle.IsToggled = true;
                BetaNetToggle.IsEnabled = false;
                TestNetToggle.IsEnabled = false;
            }
            else
            {
                BetaNetToggle.IsToggled = true;
                TestNetToggle.IsToggled = false;
                BetaNetToggle.IsEnabled = false;
                TestNetToggle.IsEnabled = false;
            }
        }

        private void EnableNetworkToggles(string network)
        {
            if (network == "TestNet")
            {
                BetaNetToggle.IsToggled = false;
                TestNetToggle.IsToggled = true;
                BetaNetToggle.IsEnabled = true;
                TestNetToggle.IsEnabled = true;
            }
            else
            {
                BetaNetToggle.IsToggled = true;
                TestNetToggle.IsToggled = false;
                BetaNetToggle.IsEnabled = true;
                TestNetToggle.IsEnabled = true;
            }
        }
        public async void GenerateAccount1_click(System.Object sender, System.EventArgs e)
        {      


            var accountnumber = 1;
            createaccounts(accountnumber);
            GenerateAccount1.Text = "Account 1 created";
            GenerateAccount1.IsEnabled = false;
            GetAccount1Info.IsVisible = true;
            var network = await SecureStorage.GetAsync("Network");
            DisableNetworkToggles(network);
            buttonstate();
            

        }
        public async void GenerateAccount2_Clicked(System.Object sender, System.EventArgs e)

        {
            var accountnumber = 2;   
            createaccounts(accountnumber);
            GenerateAccount2.Text = "Account 2 created";
            GenerateAccount2.IsEnabled = false;
            GetAccount2Info.IsVisible = true;
            var network = await SecureStorage.GetAsync("Network");
            DisableNetworkToggles(network);
            buttonstate();
        }

        public async void GenerateAccount3_Clicked(System.Object sender, System.EventArgs e)
        {
            var accountnumber = 3;
            createaccounts(accountnumber);
            GenerateAccount3.Text = "Account 3 created";
            GenerateAccount3.IsEnabled = false;
            GetAccount3Info.IsVisible = true;
            var network = await SecureStorage.GetAsync("Network");
            DisableNetworkToggles(network);
            buttonstate();

        }


        public async void createaccounts(int accountnumber)
        {
            var helper = new helper();
            var network = await SecureStorage.GetAsync("Network");
            string[] myAccountInfo = await helper.CreateAccount(accountnumber);

            var myAccountAddress = myAccountInfo[0].ToString();
            var myMnemonic = myAccountInfo[1].ToString();

            myLabel.Text = "Account " + accountnumber.ToString() + " Address = " + myAccountAddress.ToString();
            myLabel2.Text = "Account " + accountnumber.ToString() + " Mnemonic = " + myMnemonic.ToString();
            Entry3.Text = "Account = " + accountnumber.ToString() + " Address = " + myAccountAddress.ToString(); ;
            try
            {
                await SecureStorage.SetAsync("Account " + accountnumber.ToString(), myMnemonic);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // Possible that device doesn't support secure storage on device.
            }
          
            if (network == "TestNet")
            {

                // https://bank.testnet.algorand.network/
                helper.openurl("https://bank.testnet.algorand.network/", myAccountAddress);

            }
            else
            {
                helper.openurl("https://bank.betanet.algodev.network/", myAccountAddress);

            }
        }



        public async void ClearAccounts_Clicked(System.Object sender, System.EventArgs e)
        {
            string action = await DisplayActionSheet("ActionSheet: Are you sure you want to remove all accounts from this device?", "Cancel", null, "Yes", "No");
            Debug.WriteLine("Action: " + action);
            if (action == "Yes")
            {
                try
                {

                    await SecureStorage.SetAsync("Account 1", "");
                    await SecureStorage.SetAsync("Account 2", "");
                    await SecureStorage.SetAsync("Account 3", "");
                    await SecureStorage.SetAsync("Multisig", "");
                    await SecureStorage.SetAsync("Transaction", "");
                    await SecureStorage.SetAsync("MultisigTransaction", "");
                    await SecureStorage.SetAsync("Network", "TestNet");

                    GenerateAccount1.IsEnabled = true;
                    GenerateAccount2.IsEnabled = true;
                    GenerateAccount3.IsEnabled = true;

                    MultisigTransaction.IsVisible = false;
                    GetMultiSigTx.IsVisible = false;
                    CreateMultiSig.IsVisible = false;
                    GetMultiSig.IsVisible = false;
                    Transaction.IsVisible = false;
                    GetTransaction.IsVisible = false;
                    GetAccount1Info.IsVisible = false;
                    GetAccount2Info.IsVisible = false;
                    GetAccount3Info.IsVisible = false;


                    CreateMultiSig.IsEnabled = true;
                    Transaction.IsEnabled = true;
                    MultisigTransaction.IsEnabled = true;

                    GenerateAccount1.Text = "Generate Account 1";
                    GenerateAccount2.Text = "Generate Account 2";
                    GenerateAccount3.Text = "Generate Account 3";
                    CreateMultiSig.Text = "Create Multisig Address";
                    Transaction.Text = "Trasaction from account A to B";
                    MultisigTransaction.Text = "Send Multisig Tranaction to account C";
               

                    myLabel.Text = "";
                    myLabel2.Text = "";
                    Entry3.Text = "";
                    EnableNetworkToggles("TestNet");
                    buttonstate();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    // Possible that device doesn't support secure storage on device.
                }
            }


        }

        public async void GetAccount1Info_Clicked(System.Object sender, System.EventArgs e)

        {

            var accountnumber = 1;

            await GetAccountInfo(accountnumber);


        }

        async Task GetAccountInfo(int accountnumber)
        {
            await DisplayAccount(accountnumber);
        }

        private async Task DisplayAccount(int accountnumber)
        {
            Account account;
            string mnemonic = "";
            try
            {
                mnemonic = await SecureStorage.GetAsync("Account " + accountnumber.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // Possible that device doesn't support secure storage on device.
            }

            //restore account from mnemonic
            account = new Account(mnemonic);


            Algorand.Algod.Client.Model.Account accountinfo = await algodApiInstance.AccountInformationAsync(account.Address.ToString());

            Debug.WriteLine("accountinfo: " + accountinfo);
            myLabel.Text = "Account " + accountnumber.ToString() + " Address = " + account.Address.ToString();
            myLabel2.Text = "Account amount (micro algos) = " + accountinfo.Amount.ToString();
            Entry3.Text = "Account " + accountnumber.ToString() + " Address = " + account.Address.ToString();
        }

        public async void GetBlock_Clicked(System.Object sender, System.EventArgs e)
        {
            var status = await algodApiInstance.GetStatusAsync();
            long lastround = (long)status.LastRound;
            try {
              var block = await algodApiInstance.GetBlockAsyncWithHttpInfo(lastround);
              myLabel2.Text = "Block Info = " + block.ToString();
            }
            catch (Exception err)
            {
                myLabel2.Text = "Block Info = " + err.Message;
            }


            myLabel.Text = "Last Round = " + lastround.ToString();

            Entry3.Text = "Last Round = " + lastround.ToString();

        }

        public async void Switch_Toggled_TestNet(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (TestNetToggle.IsToggled)
            {
                await SecureStorage.SetAsync("Network", "TestNet");
                BetaNetToggle.IsToggled = false;
                algodApiInstance = new AlgodApi(ALGOD_API_ADDR_TESTNET, ALGOD_API_TOKEN);
            }
            else
            {
                await SecureStorage.SetAsync("Network", "BetaNet");
                BetaNetToggle.IsToggled = true;
                
            }


        }

        public async void Switch_Toggled_BetaNet(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (BetaNetToggle.IsToggled)
            {
                await SecureStorage.SetAsync("Network", "BetaNet");
                TestNetToggle.IsToggled = false;
                algodApiInstance = new AlgodApi(ALGOD_API_ADDR_BETANET, ALGOD_API_TOKEN);
            }
            else
            {
                await SecureStorage.SetAsync("Network", "TestNet");
                TestNetToggle.IsToggled = true;
                
            }

        }

        public async void GetAccount3Info_Clicked(System.Object sender, System.EventArgs e)
        {
            var accountnumber = 3;

            await GetAccountInfo(accountnumber);

        }

        public async void GetAccount2Info_Clicked(System.Object sender, System.EventArgs e)
        {
            var accountnumber = 2;

            await GetAccountInfo(accountnumber);
        }

        public async void CreateMultiSig_Clicked(System.Object sender, System.EventArgs e)
        {
            Account account1;
            Account account2;
            Account account3;
            string mnemonic1 = "";
            string mnemonic2 = "";
            string mnemonic3 = "";


            try
            {
                mnemonic1 = await SecureStorage.GetAsync("Account 1");
                mnemonic2 = await SecureStorage.GetAsync("Account 3");
                mnemonic3 = await SecureStorage.GetAsync("Account 3");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // Possible that device doesn't support secure storage on device.
            }
            // restore accounts
            account1 = new Account(mnemonic1);
            account2 = new Account(mnemonic2);
            account3 = new Account(mnemonic3);

            List<Ed25519PublicKeyParameters> publickeys = new List<Ed25519PublicKeyParameters>();

            publickeys.Add(account1.GetEd25519PublicKey());
            publickeys.Add(account2.GetEd25519PublicKey());
            publickeys.Add(account3.GetEd25519PublicKey());

            MultisigAddress msig = new MultisigAddress(1, 2, publickeys);

            myLabel.Text = "Multisig Address " + msig.ToString();
            myLabel2.Text = "";
            CreateMultiSig.Text = "Multisig created";
            CreateMultiSig.IsEnabled = false;
            GetMultiSig.IsVisible = true;
            await SecureStorage.SetAsync("Multisig", msig.ToString());
            buttonstate();
       


        }

        public async void GetMultiSig_Clicked(System.Object sender, System.EventArgs e)
        {
            var msig = await SecureStorage.GetAsync("Multisig");
            myLabel.Text = "Multisig address = " + msig.ToString();
            myLabel2.Text = "";

        }

        public async void Transaction_Clicked(System.Object sender, System.EventArgs e)
        {
            Account account1;
            Account account2;
            Account account3;
            string mnemonic1 = "";
            string mnemonic2 = "";
            string mnemonic3 = "";

            try
            {
                mnemonic1 = await SecureStorage.GetAsync("Account 1");
                mnemonic2 = await SecureStorage.GetAsync("Account 2");
                mnemonic3 = await SecureStorage.GetAsync("Account 3");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // Possible that device doesn't support secure storage on device.
            }
            // restore accounts
            account1 = new Account(mnemonic1);
            account2 = new Account(mnemonic2);
            account3 = new Account(mnemonic3);

            // transfer from Account 1 to 2
            TransactionParams transParams = null;

            try
            {
                transParams = algodApiInstance.TransactionParams();
            }
            catch (ApiException err)
            {
                throw new Exception("Could not get params", err);
            }
            var amount = Utils.AlgosToMicroalgos(1);
            var tx = Utils.GetPaymentTransaction(account1.Address, account2.Address, amount, "pay message", transParams);
            //Transaction tx = new Transaction(src.Address, new Address(DEST_ADDR), amount, firstRound, lastRound, genesisID, genesisHash);
            var signedTx = account1.SignTransaction(tx);

            Console.WriteLine("Signed transaction with txid: " + signedTx.transactionID);

            // send the transaction to the network
            try
            {
                var id = Utils.SubmitTransaction(algodApiInstance, signedTx);
                Console.WriteLine("Successfully sent tx with id: " + id.TxId);
                Console.WriteLine(Utils.WaitTransactionToComplete(algodApiInstance, id.TxId));

                GetTransaction.IsVisible = true;
                Transaction.Text = "Transaction successfully sent";
               
                await SecureStorage.SetAsync("Transaction", id.TxId.ToString());
                buttonstate();
            }
            catch (ApiException err)
            {
                // This is generally expected, but should give us an informative error message.
                Console.WriteLine("Exception when calling algod#rawTransaction: " + err.Message);
            }

          
            await DisplayAccount(2);
            var txid = await SecureStorage.GetAsync("Transaction");
            Entry3.Text = "Transaction ID = " + txid.ToString();
        }

        public async void GetTransaction_Clicked(System.Object sender, System.EventArgs e)
        {
            await DisplayAccount(2);
            var txid = await SecureStorage.GetAsync("Transaction");
            Entry3.Text = "Transaction ID = " + txid.ToString();

        }

        public async void GetMultiSigTx_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        public async void MultisigTransaction_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
