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
using System.Numerics;

namespace algorandapp
{
    public partial class ASA : ContentPage
    {
        // Purestake
        //public const string ALGOD_API_TOKEN_BETANET = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";
        //public const string ALGOD_API_ADDR_BETANET = "https://betanet-algorand.api.purestake.io/ps1";
        //public const string ALGOD_API_ADDR_TESTNET = "https://testnet-algorand.api.purestake.io/ps1";
        //public const string ALGOD_API_TOKEN_TESTNET = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";

        // Standalone instance
        //public const string ALGOD_API_TOKEN_BETANET = "050e81d219d12a0888dafddaeafb5ff8d181bf1256d1c749345995678b16902f";
        //public const string ALGOD_API_ADDR_BETANET = "http://betanet-hackathon.algodev.network:8180";
        //public const string ALGOD_API_TOKEN_TESTNET = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
        //public const string ALGOD_API_ADDR_TESTNET = "http://hackathon.algodev.network:9100";
        public string ALGOD_API_TOKEN_BETANET = "";
        public string ALGOD_API_ADDR_BETANET = "";
        public string ALGOD_API_TOKEN_TESTNET = "";
        public string ALGOD_API_ADDR_TESTNET = "";

        public Account account1;
        public Account account2;
        public Account account3;

        //   default to TESTNET
        public AlgodApi algodApiInstance;

        public ASA()
        {
            InitializeComponent();
            Appearing += ASA_Appearing;
        }

        private async void ASA_Appearing(object sender, EventArgs e)
        {
            ALGOD_API_TOKEN_BETANET = await SecureStorage.GetAsync("ALGOD_API_TOKEN_BETANET");
            ALGOD_API_TOKEN_TESTNET = await SecureStorage.GetAsync("ALGOD_API_TOKEN_TESTNET");
            ALGOD_API_ADDR_TESTNET = await SecureStorage.GetAsync("ALGOD_API_ADDR_TESTNET");
            ALGOD_API_ADDR_BETANET = await SecureStorage.GetAsync("ALGOD_API_ADDR_BETANET");
            var network = await SecureStorage.GetAsync("Network");

            if (network == "TestNet")
            {

                algodApiInstance = new AlgodApi(ALGOD_API_ADDR_TESTNET, ALGOD_API_TOKEN_TESTNET);
            }
            else
            {
                algodApiInstance = new AlgodApi(ALGOD_API_ADDR_BETANET, ALGOD_API_TOKEN_BETANET);
            }
            buttonstate();

        }
        public async void buttonstate()
        {
            var network = await SecureStorage.GetAsync("Network");


        }

        private async void CreateAsset_click(System.Object sender, System.EventArgs e)
        {
            await getAccounts();

        }

        private async Task getAccounts()
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


        }
    }
}

