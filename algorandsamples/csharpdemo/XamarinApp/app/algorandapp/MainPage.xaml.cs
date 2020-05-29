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
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public Account myAccount;
        public static helper helper = new helper();
        public string network;
        public MainPage()
        {
            InitializeComponent();
            Appearing += MainPage_Appearing;
        }

        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            network = await helper.GetNetwork();
            var nodetype = await SecureStorage.GetAsync(helper.StorageNodeType);
            NetworkLabel.Text = "Network: " + network + " " + nodetype;
            var lastHomebutton = await SecureStorage.GetAsync(helper.StorageLastHomeButton);
            if (string.IsNullOrEmpty(lastHomebutton))
            {
                buttonstate("init");
            }
            else
            {
                buttonstate(lastHomebutton);
            }
        }
        public async void buttonstate(string buttonclicked)
        {
            NetworkLabel.Text = "Network: " + network;
            var account1 = await SecureStorage.GetAsync(helper.StorageAccountName1);
            var account2 = await SecureStorage.GetAsync(helper.StorageAccountName2);
            var account3 = await SecureStorage.GetAsync(helper.StorageAccountName3);
            if ((string.IsNullOrEmpty(account1) || string.IsNullOrEmpty(account2)) || string.IsNullOrEmpty(account3))
            {
                StackGenerateAccount.IsEnabled = true;
                GenerateAccount.Opacity = 1;

                StackASA.IsEnabled = false;
                ASA.Opacity = .4;

                StackASC1.IsEnabled = false;
                ASC1.Opacity = .4;

                StackAtomicTransfers.IsEnabled = false;
                AtomicTransfers.Opacity = .4;
            }
            else
            {
                StackGenerateAccount.IsEnabled = true;
                GenerateAccount.Opacity = 1;

                StackASA.IsEnabled = true;
                ASA.Opacity = 1;

                StackASC1.IsEnabled = true;
                ASC1.Opacity = 1;

                StackAtomicTransfers.IsEnabled = true;
                AtomicTransfers.Opacity = 1;
            }



        }
        public async void NodeNetwork_click(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new NodeAndNetwork());
        }


        public async void GenerateAccount_click(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new Accounts());
        }

        public async void ASA_click(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new ASA());
        }
        public async void AtomicTransfers_click(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new AtomicTransfers());
        }




        public async void Settings_Clicked(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new Settings());
        }

        async void ASC1_Clicked(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new ASC());
        }

        void Reset_Clicked(System.Object sender, System.EventArgs e)
        {
            //public string StorageAccountName1 = "Account 1";
            //public string StorageAccountName2 = "Account 2";
            //public string StorageAccountName3 = "Account 3";
            //public string StorageALGOD_API_TOKEN_BETANET = "ALGOD_API_TOKEN_BETANET";
            //public string StorageALGOD_API_ADDR_BETANET = "ALGOD_API_ADDR_BETANET";
            //public string StorageALGOD_API_TOKEN_TESTNET = "ALGOD_API_TOKEN_TESTNET";
            //public string StorageALGOD_API_ADDR_TESTNET = "ALGOD_API_ADDR_TESTNET";
            //public string StorageNetwork = "Network";
            //public string StorageMultisig = "Multisig";
            //public string StorageTransaction = "Transaction";
            //public string StorageMultisigTransaction = "MultisigTransaction";
            //public string StorageAssetIDName = "AssetID";
            //public string StorageTestNet = "TestNet";
            //public string StorageBetaNet = "BetaNet";

            //public string StoragePurestake = "Purestake";
            //public string StorageHackathon = "Hackathon";
            //public string StoragemyNode = "myNode";
            //public string StorageSavedBetaNetwork = "SavedBetaNetwork";
            //public string StorageSavedTestNetwork = "SavedTestNetwork";
            //public string StorageTestNetToken = "TestNetToken";
            //public string StorageTestNetAddress = "TestNetAddress";
            //public string StorageBetaNetToken = "BetaNetToken";
            //public string StorageBetaNetAddress = "BetaNetAddress";
            //public string StorageNodeType = "NodeType";
            //public string StorageLastASAButton = "LastASAButton";


        }
    }


}

