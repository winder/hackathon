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
        //public string ALGOD_API_TOKEN_BETANET = "050e81d219d12a0888dafddaeafb5ff8d181bf1256d1c749345995678b16902f";
        //public string ALGOD_API_ADDR_BETANET = "http://betanet-hackathon.algodev.network:8180";
        //public string ALGOD_API_TOKEN_TESTNET = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
        //public string ALGOD_API_ADDR_TESTNET = "http://hackathon.algodev.network:9100";
        public string ALGOD_API_TOKEN_BETANET = "";
        public string ALGOD_API_ADDR_BETANET = "";
        public string ALGOD_API_TOKEN_TESTNET = "";
        public string ALGOD_API_ADDR_TESTNET = "";
        //   default to TESTNET

        public AlgodApi algodApiInstance = null;

        public int sizeoftoken;
        public int sizeofaddress;


        public NodeAndNetwork()
        {
            InitializeComponent();
            Appearing += NodeAndNetwork_Appearing;
            EntryBetaNetServer.TextChanged += EntryBetaNetServer_TextChanged;
            EntryTestNetServer.TextChanged += EntryTestNetServer_TextChanged;
            EntryBetaNetToken.TextChanged += EntryBetaNetToken_TextChanged;
            EntryTestNetToken.TextChanged += EntryTestNetToken_TextChanged;
        }

        private void EntryTestNetToken_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (EntryTestNetToken.Text != null)
                sizeoftoken = EntryTestNetToken.Text.Length;
            if (EntryTestNetServer.Text != null)
                sizeofaddress = EntryTestNetServer.Text.Length;

            checkifentered();
        }

        private void checkifentered()
        {
            if ((sizeoftoken > 0) && (sizeofaddress > 0))
            {
                SaveBetaNet.IsEnabled = true;
            }
            else
            {
                SaveBetaNet.IsEnabled = false;
            }
        }

        private void EntryBetaNetToken_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntryBetaNetToken.Text!=null) 
            sizeoftoken = EntryBetaNetToken.Text.Length;
            if (EntryBetaNetServer.Text!=null)
            sizeofaddress = EntryBetaNetServer.Text.Length;
            checkifentered();
        }

        private void EntryTestNetServer_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (EntryTestNetToken.Text != null)
                sizeoftoken = EntryTestNetToken.Text.Length;
            if (EntryTestNetServer.Text != null)
                sizeofaddress = EntryTestNetServer.Text.Length;


            checkifentered();
        }

        private void EntryBetaNetServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntryBetaNetToken.Text != null)
                sizeoftoken = EntryBetaNetToken.Text.Length;
            if (EntryBetaNetServer.Text != null)
                sizeofaddress = EntryBetaNetServer.Text.Length;

            checkifentered();
        }

        public async void NodeAndNetwork_Appearing(object sender, EventArgs e)
        {
            // algodApiInstance = new AlgodApi(ALGOD_API_ADDR_TESTNET, ALGOD_API_TOKEN_TESTNET);

            ALGOD_API_TOKEN_BETANET = await SecureStorage.GetAsync("ALGOD_API_TOKEN_BETANET");
            ALGOD_API_TOKEN_TESTNET =  await SecureStorage.GetAsync("ALGOD_API_TOKEN_TESTNET");
            ALGOD_API_ADDR_TESTNET = await SecureStorage.GetAsync("ALGOD_API_ADDR_TESTNET");
            ALGOD_API_ADDR_BETANET = await SecureStorage.GetAsync("ALGOD_API_ADDR_BETANET");

            var savedtest = await SecureStorage.GetAsync("SavedTestNetwork");

            var savedbeta = await SecureStorage.GetAsync("SavedBetaNetwork");

            if (savedtest == "true")
            {
                EntryTestNetToken.Text = await SecureStorage.GetAsync("TestNetToken");
                EntryTestNetServer.Text = await SecureStorage.GetAsync("TestNetAddress");

            }


            if (savedbeta == "true")
            {
                EntryBetaNetToken.Text = await SecureStorage.GetAsync("BetaNetToken");
                EntryBetaNetServer.Text = await SecureStorage.GetAsync("BetaNetAddress");
            }
 

            buttonstate();
        }
        public async void buttonstate()
        {
            var network = await SecureStorage.GetAsync("Network");
            if (String.IsNullOrEmpty(network))
            {
                //default to TestNet
                await SecureStorage.SetAsync("Network", "TestNet");
                network = "TestNet";
            }

            EnableNetworkToggles(network);
        }

        public async void EnableNetworkToggles(string network)
        {
            var savedtest = "";

            if (network == "TestNet")
            {
                BetaNetToggle.IsToggled = false;
                TestNetToggle.IsToggled = true;
                BetaNetToggle.IsEnabled = true;
                TestNetToggle.IsEnabled = true;
                BetaNetStack.IsVisible = false;
                TestNetStack.IsVisible = true;

                savedtest = await SecureStorage.GetAsync("TestNetToken");
                if (savedtest != null)
                {
                    EntryTestNetToken.Text = await SecureStorage.GetAsync("TestNetToken");
                }
                savedtest = "";
                savedtest  = await SecureStorage.GetAsync("TestNetAddress");

                if (savedtest != null)
                {
                    EntryTestNetServer.Text = await SecureStorage.GetAsync("TestNetAddress");
                }
            }
            else
            {
                BetaNetToggle.IsToggled = true;
                TestNetToggle.IsToggled = false;
                BetaNetToggle.IsEnabled = true;
                TestNetToggle.IsEnabled = true;
                BetaNetStack.IsVisible = true;
                TestNetStack.IsVisible = false;
                savedtest = "";
                savedtest = await SecureStorage.GetAsync("BetaNetToken");
                if (savedtest != null)
                {
                    EntryBetaNetToken.Text = await SecureStorage.GetAsync("BetaNetToken");
                }
                savedtest = "";
                savedtest = await SecureStorage.GetAsync("BetaNetAddress");

                if (savedtest != null)
                {
                    EntryBetaNetServer.Text = await SecureStorage.GetAsync("BetaNetAddress");
                }

            }
        }



        public async void Switch_Toggled_TestNet(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (TestNetToggle.IsToggled)
            {
                await SecureStorage.SetAsync("Network", "TestNet");
                BetaNetToggle.IsToggled = false;
                BetaNetStack.IsVisible = false;
                TestNetStack.IsVisible = true;

                //   algodApiInstance = new AlgodApi(ALGOD_API_ADDR_TESTNET, ALGOD_API_TOKEN_TESTNET);
            }
            else
            {
                await SecureStorage.SetAsync("Network", "BetaNet");
                BetaNetToggle.IsToggled = true;
                BetaNetStack.IsVisible = true;
                TestNetStack.IsVisible = false;

            }


        }

        public async void Switch_Toggled_BetaNet(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (BetaNetToggle.IsToggled)
            {
                await SecureStorage.SetAsync("Network", "BetaNet");
                TestNetToggle.IsToggled = false;

              //  algodApiInstance = new AlgodApi(ALGOD_API_ADDR_BETANET, ALGOD_API_TOKEN_BETANET);

            }
            else
            {
                await SecureStorage.SetAsync("Network", "TestNet");
                TestNetToggle.IsToggled = true;

            }

        }

        public async void PurestakeBetaNet_click(System.Object sender, System.EventArgs e)
        {
            // Purestake BetaNet
            // mine
            ALGOD_API_TOKEN_BETANET = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";
            // hackathon

            // ALGOD_API_TOKEN_BETANET = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab";
            ALGOD_API_ADDR_BETANET = "https://betanet-algorand.api.purestake.io/ps1";
      
            await SecureStorage.SetAsync("ALGOD_API_TOKEN_BETANET", ALGOD_API_TOKEN_BETANET);
            await SecureStorage.SetAsync("ALGOD_API_ADDR_BETANET", ALGOD_API_ADDR_BETANET);
            myLabel.Text = "PureStake BetaNet set";
            await SecureStorage.SetAsync("Network", "BetaNet");
        }

        public async void PurestakeTestNet_click(System.Object sender, System.EventArgs e)
        {
            // Purestake TestNet

            ALGOD_API_ADDR_TESTNET = "https://testnet-algorand.api.purestake.io/ps1";
            // mine
            ALGOD_API_TOKEN_TESTNET = "WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb";
            // hackathon
            // ALGOD_API_TOKEN_TESTNET = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab";
        
            await SecureStorage.SetAsync("ALGOD_API_TOKEN_TESTNET", ALGOD_API_TOKEN_TESTNET);
            await SecureStorage.SetAsync("ALGOD_API_ADDR_TESTNET", ALGOD_API_ADDR_TESTNET);
            await SecureStorage.SetAsync("Network", "TestNet");
            myLabel.Text = "Purestake TestNet set";
        }

        public async void HackathonBetaNet_click(System.Object sender, System.EventArgs e)
        {
                   // Standalone instance
            ALGOD_API_TOKEN_BETANET = "050e81d219d12a0888dafddaeafb5ff8d181bf1256d1c749345995678b16902f";
            ALGOD_API_ADDR_BETANET = "http://betanet-hackathon.algodev.network:8180";
            await SecureStorage.SetAsync("ALGOD_API_TOKEN_BETANET", ALGOD_API_TOKEN_BETANET);
            await SecureStorage.SetAsync("ALGOD_API_ADDR_BETANET", ALGOD_API_ADDR_BETANET);
            await SecureStorage.SetAsync("Network", "BetaNet");
            myLabel.Text = "Hackathon BetaNet set";
        }

        public async void HackathonTestNet_click(System.Object sender, System.EventArgs e)
        {

             ALGOD_API_TOKEN_TESTNET = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
             ALGOD_API_ADDR_TESTNET = "http://hackathon.algodev.network:9100";

            await SecureStorage.SetAsync("ALGOD_API_TOKEN_TESTNET", ALGOD_API_TOKEN_TESTNET);
            await SecureStorage.SetAsync("ALGOD_API_ADDR_TESTNET", ALGOD_API_ADDR_TESTNET);
            await SecureStorage.SetAsync("Network", "TestNet");
            myLabel.Text = "Hackathon TestNet set";
        }

        void myBetaNet_click(System.Object sender, System.EventArgs e)
        {
            TestNetEntry.IsVisible = false;
            BetaNetEntry.IsVisible = true;
            myLabel.Text = "";

        }

        void myTestNet_click(System.Object sender, System.EventArgs e)
        {
            TestNetEntry.IsVisible = true;
            BetaNetEntry.IsVisible = false;
            myLabel.Text = "";
        }

        public async void SaveBetaNetwork_click(System.Object sender, System.EventArgs e)
        {
            await SecureStorage.SetAsync("SavedBetaNetwork", "false");
            if ((sizeoftoken > 0) && (sizeofaddress > 0))
            {
                ALGOD_API_TOKEN_BETANET = EntryBetaNetToken.Text;
                ALGOD_API_ADDR_BETANET = EntryBetaNetServer.Text;

                await SecureStorage.SetAsync("BetaNetToken", ALGOD_API_TOKEN_BETANET);
                await SecureStorage.SetAsync("BetaNetAddress", ALGOD_API_ADDR_BETANET);
                await SecureStorage.SetAsync("ALGOD_API_TOKEN_BETANET", ALGOD_API_TOKEN_BETANET);
                await SecureStorage.SetAsync("ALGOD_API_ADDR_BETANET", ALGOD_API_ADDR_BETANET);

                await SecureStorage.SetAsync("SavedBetaNetwork", "true");
                await SecureStorage.SetAsync("Network", "BetaNet");
                myLabel.Text = "Saved BetaNet Token and Server Address";
            }
            else
            {
                myLabel.Text = "Invalid Token or Address";

            }
        }

        public async void SaveTestNetwork_click(System.Object sender, System.EventArgs e)
        {
            await SecureStorage.SetAsync("SavedTestNetwork", "false");
            if ((sizeoftoken >0) && ( sizeofaddress >0))
            {
                ALGOD_API_TOKEN_TESTNET = EntryTestNetToken.Text;
                ALGOD_API_ADDR_TESTNET = EntryTestNetServer.Text;

                await SecureStorage.SetAsync("TestNetToken", ALGOD_API_TOKEN_TESTNET);
                await SecureStorage.SetAsync("TestNetAddress", ALGOD_API_ADDR_TESTNET);
                await SecureStorage.SetAsync("ALGOD_API_TOKEN_TESTNET", ALGOD_API_TOKEN_TESTNET);
                await SecureStorage.SetAsync("ALGOD_API_ADDR_TESTNET", ALGOD_API_ADDR_TESTNET);
                await SecureStorage.SetAsync("Network", "TestNet");
                await SecureStorage.SetAsync("SavedTestNetwork", "true");

                myLabel.Text = "Saved TestNet Token and Server Address";
            }
        else
            {

            myLabel.Text = "Invalid Token or Address";
            }


        }
    }

}
