using System;
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




namespace algorandapp
{
    public partial class Accounts : ContentPage
    {

        public const string ALGOD_API_TOKEN = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";
        public const string ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps1";

        //public const string ALGOD_API_TOKEN = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
        //public const string ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";

    

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

            if (account1 == null)
            {
                await SecureStorage.SetAsync("Account 1","");
                account1 = await SecureStorage.GetAsync("Account 1");
            }
            if (account2 == null)
            {
                await SecureStorage.SetAsync("Account 2", "");
                account2 = await SecureStorage.GetAsync("Account 2");
            }
            if (account3 == null)
            {
                await SecureStorage.SetAsync("Account 3", "");
                account2 = await SecureStorage.GetAsync("Account 3");
            }

            if ((account1 != "") & (account2 != "") & (account3 != ""))
            {
                // this account is already generated - leave state
                myButtonGenerateAccount1.Text = "Account 1 created";
                myButtonGenerateAccount2.Text = "Account 2 created";
                myButtonGenerateAccount3.Text = "Account 3 created";
                myButtonGenerateAccount1.IsEnabled = false;
                myButtonGenerateAccount2.IsEnabled = false;
                myButtonGenerateAccount3.IsEnabled = false;
                myButtonGetAccount1Info.IsVisible = true;
                myButtonGetAccount2Info.IsVisible = true;
                myButtonGetAccount3Info.IsVisible = true;
                var network = await SecureStorage.GetAsync("Network");
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
                myLabel.Text = "Accounts 1, 2 and 3 have been created on " + network;
                myLabel2.Text = "";
                myButtonCreateMultiSig.IsVisible = true;
                var msig = await SecureStorage.GetAsync("Multisig");
                if (msig == null)
                {
                    await SecureStorage.SetAsync("Multisig", "");
                    msig = await SecureStorage.GetAsync("Multisig");
                    myButtonCreateMultiSig.IsEnabled = true;
                    myButtonGetMultiSig.IsVisible = false;
                }
                else
                {

               
                    if (msig != "")
                    {
                        myButtonCreateMultiSig.IsEnabled = false;
                        myButtonCreateMultiSig.Text = "Multisig created";
                        myButtonGetMultiSig.IsVisible = true;
                    }
                    else
                    {
                        myButtonCreateMultiSig.IsEnabled = true;
                        myButtonGetMultiSig.IsVisible = false;
                    }
                }
            }
            

            else
            {
            // new accounts need to be generated
           
            if (account1 != "")
            {
               myButtonGenerateAccount2.IsEnabled = true;
            }
          
            if (account2 != "")
            {
              myButtonGenerateAccount3.IsEnabled = true;
            }
            
            if (account3 != "")
            {
                // enable clear button
                TestNetToggle.IsToggled = true;
                BetaNetToggle.IsToggled = false;

                myButtonGenerateAccount1.IsEnabled = true;
                myButtonGenerateAccount2.IsEnabled = false;
                myButtonGenerateAccount3.IsEnabled = false;
                
            }
            await SecureStorage.SetAsync("Network", "TestNet");
            myButtonGetAccount1Info.IsVisible = false;
            }

        }

        public async void myButtonGenerateAccount1_click(System.Object sender, System.EventArgs e)
        {      


            var accountnumber = 1;
            createaccounts(accountnumber);
            myButtonGenerateAccount1.Text = "Account 1 created";
            myButtonGenerateAccount1.IsEnabled = false;
            myButtonGenerateAccount2.IsEnabled = true;
            myButtonGenerateAccount3.IsEnabled = false;

            BetaNetToggle.IsEnabled = false;
            TestNetToggle.IsEnabled = false;
            myButtonGetAccount1Info.IsVisible = true;

        }
        public async void myButtonGenerateAccount2_Clicked(System.Object sender, System.EventArgs e)

        {
            var accountnumber = 2;   
            createaccounts(accountnumber);
            myButtonGenerateAccount2.Text = "Account 2 created";
            myButtonGenerateAccount1.IsEnabled = false;
            myButtonGenerateAccount2.IsEnabled = false;
            myButtonGenerateAccount3.IsEnabled = true;
            myButtonGetAccount2Info.IsVisible = true;
        }
        public async void myButtonGenerateAccount3_Clicked(System.Object sender, System.EventArgs e)
        {
            var accountnumber = 3;
            createaccounts(accountnumber);
            myButtonGenerateAccount3.Text = "Account 3 created";
            myButtonGenerateAccount1.IsEnabled = false;
            myButtonGenerateAccount2.IsEnabled = false;
            myButtonGenerateAccount3.IsEnabled = false;
            myButtonGetAccount3Info.IsVisible = true;
            myButtonCreateMultiSig.IsVisible = true;
            var network = await SecureStorage.GetAsync("Network");
            myLabel.Text = "Accounts 1, 2 and 3 have been created on " + network;
            myLabel2.Text = "";

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



        public async void myButtonClearAccounts_Clicked(System.Object sender, System.EventArgs e)
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

                    myButtonGenerateAccount1.IsEnabled = true;
                    myButtonGenerateAccount2.IsEnabled = false;
                    myButtonGenerateAccount3.IsEnabled = false;

                    await SecureStorage.SetAsync("Network", "TestNet");
                    BetaNetToggle.IsEnabled = true;
                    TestNetToggle.IsEnabled = true;
                    BetaNetToggle.IsToggled = false;
                    TestNetToggle.IsToggled = true;
                    myButtonClearAccounts.IsVisible = true;
                    myButtonGenerateAccount1.Text = "Generate Account 1";
                    myButtonGenerateAccount2.Text = "Generate Account 2";
                    myButtonGenerateAccount3.Text = "Generate Account 3";
                    myButtonGetAccount1Info.IsVisible = false;
                    myButtonGetAccount2Info.IsVisible = false;
                    myButtonGetAccount3Info.IsVisible = false;
                    myLabel.Text = "";
                    myLabel2.Text = "";
                    myButtonCreateMultiSig.IsVisible = false;


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    // Possible that device doesn't support secure storage on device.
                }
            }


        }

        public async void myButtonGetAccount1Info_Clicked(System.Object sender, System.EventArgs e)

        {

            var accountnumber = 1;

            await GetAccountInfo(accountnumber);


        }

        async Task GetAccountInfo(int accountnumber)
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
            account = new Account(mnemonic);


            Algorand.Algod.Client.Model.Account accountinfo = await algodApiInstance.AccountInformationAsync(account.Address.ToString());

            Debug.WriteLine("accountinfo: " + accountinfo);
            myLabel.Text = "Account " + accountnumber.ToString() + " Address = " + account.Address.ToString();
            myLabel2.Text = "Account amount (micro algos) = " + accountinfo.Amount.ToString();
        }
        public async void myButtonGetBlock_Clicked(System.Object sender, System.EventArgs e)
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
           


            }

        public async void Switch_Toggled_TestNet(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (TestNetToggle.IsToggled)
            {
                await SecureStorage.SetAsync("Network", "TestNet");
                BetaNetToggle.IsToggled = false;
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
            }
            else
            {
                await SecureStorage.SetAsync("Network", "TestNet");
                TestNetToggle.IsToggled = true;
            }

        }

        public async void myButtonGetAccount3Info_Clicked(System.Object sender, System.EventArgs e)
        {
            var accountnumber = 3;

            await GetAccountInfo(accountnumber);

        }

        public async void myButtonGetAccount2Info_Clicked(System.Object sender, System.EventArgs e)
        {
            var accountnumber = 2;

            await GetAccountInfo(accountnumber);
        }

        public async void myButtonCreateMultiSig_Clicked(System.Object sender, System.EventArgs e)
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

            await SecureStorage.SetAsync("Multisig", msig.ToString());

            myButtonCreateMultiSig.Text = "Multisig created";
            myButtonCreateMultiSig.IsEnabled = false;
            myButtonGetMultiSig.IsVisible = true;

            // todo? load multisig w algos?


        }

        public async void myButtonGetMultiSig_Clicked(System.Object sender, System.EventArgs e)
        {
            var msig = await SecureStorage.GetAsync("Multisig");
            myLabel.Text = "Multisig address = " + msig.ToString();
            myLabel2.Text = "";

        }
    }
}
