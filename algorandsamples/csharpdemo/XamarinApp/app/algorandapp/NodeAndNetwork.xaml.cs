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

    public partial class NodeAndNetwork : ContentPage
    {
        // Purestake
        //public const string ALGOD_API_TOKEN_BETANET = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";
        //public const string ALGOD_API_ADDR_BETANET = "https://betanet-algorand.api.purestake.io/ps1";
        //public const string ALGOD_API_ADDR_TESTNET = "https://testnet-algorand.api.purestake.io/ps1";
        //public const string ALGOD_API_TOKEN_TESTNET = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";

        // Standalone instance
        public const string ALGOD_API_TOKEN_BETANET = "050e81d219d12a0888dafddaeafb5ff8d181bf1256d1c749345995678b16902f";
        public const string ALGOD_API_ADDR_BETANET = "http://betanet-hackathon.algodev.network:8180";
        public const string ALGOD_API_TOKEN_TESTNET = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
        public const string ALGOD_API_ADDR_TESTNET = "http://hackathon.algodev.network:9100";
        //   default to TESTNET
        public AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR_TESTNET, ALGOD_API_TOKEN_TESTNET);



        public NodeAndNetwork()
        {
            InitializeComponent();
        }

        //void PurestakeTestNet_click(System.Object sender, System.EventArgs e)
        //{
        //}

        private async void Switch_Toggled_TestNet(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (TestNetToggle.IsToggled)
            {
                await SecureStorage.SetAsync("Network", "BetaNet");
                algodApiInstance = new AlgodApi(ALGOD_API_ADDR_BETANET, ALGOD_API_TOKEN_BETANET);


            }
            else
            {
                await SecureStorage.SetAsync("Network", "TestNet");

                algodApiInstance = new AlgodApi(ALGOD_API_ADDR_TESTNET, ALGOD_API_TOKEN_TESTNET);



            }
        }
    }

}
