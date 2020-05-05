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
        //public string myPublicKey;
        //public Block myBlock;
        //public Transaction mtTxn;

        public MainPage()
        {


            InitializeComponent();
        }
        // create client
        public async void myButton_click(object sender, EventArgs e)
        {
       // kmd
       //      ForKMDHTTPAPI.Standard.Configuration.XKMDAPIToken = "X-KMD-API-Token";
       //      ForKMDHTTPAPI.Standard.ForKMDHTTPAPIClient clientkmd = new ForKMDHTTPAPI.Standard.ForKMDHTTPAPIClient();
       // Algod
            //var address = "http://hackathon.algodev.network:9100";
            //AlgodRESTAPI.Standard.Configuration.XAlgoAPIToken = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
            //AlgodRESTAPI.Standard.Configuration.BaseUri = new Uri(address).ToString();
            //client = new AlgodRESTAPI.Standard.AlgodRESTAPIClient();
            //myVersion = await client.Client.GetVersionAsync();
            //myLabel.Text = "Build Version = "+ myVersion.Build.Major.ToString() +"." + myVersion.Build.Minor.ToString();
            //myLabel2.Text = "GenesisId = " + myVersion.GenesisId.ToString() ;
        }


        public async void myButtonGenerateAccount_click(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new Accounts());

        }

        public async void openurl (string url, string account)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                await Launcher.OpenAsync(url + "&account=" + account);

            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                await Launcher.OpenAsync(url + "&account=" + account);
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                await Launcher.OpenAsync(url + "&account=" + account);
            }
        }

        public async void myButtonClientInfo_click(object sender, EventArgs e)
        {
        //    myAccountInfo = await client.Client.GetAccountInformationAsync("WRTJ62AAOJ6W5XEXVUZ63D57IOI2XXRBBPB4RROQMHJEK4HW7EDWPXSP6Y");
        ////    myAccountInfo = await client.Client.GetAccountInformationAsync(myPublicKey);
            
        //    Debug.Print(myAccountInfo.Address);
        //    Debug.Print(myAccountInfo.Amount.ToString());
        //    Debug.Print(myAccountInfo.Round.ToString());
        //    Debug.Print(myAccountInfo.Status);
        //    if (myAccountInfo.Thisassettotal != null)
        //        Debug.Print(myAccountInfo.Thisassettotal.ToString());
        //    if (myAccountInfo.Assets != null)
        //        Debug.Print(myAccountInfo.Assets.ToString());

        //    myLabel.Text = "Account Address = " + myAccountInfo.Address.ToString();
        //    myLabel2.Text = "Amount = " + myAccountInfo.Amount.ToString();
        }

        public async void myButtonBlock_click(object sender, EventArgs e)
        {
            ////myBlock = await client.Client.GetBlockAsync(myAccountInfo.Round);
            //myBlock = await client.Client.GetBlockAsync(3499281);
      
            //Debug.Print(myBlock.Round.ToString());
            //myLabel.Text = "Client Block = " + myBlock.Round.ToString();
            //if (myBlock.Txns.Transactions == null)
            //{
            //    myLabel2.Text = "Transaction Count = 0";
            //}
            //else
            //{
            //myLabel2.Text = "Transaction Count = " + myBlock.Txns.Transactions.Count();
            //}
        }

        public async void myButtonTxInfo_click(object sender, EventArgs e)
        {
            //mtTxn = await client.Client.GetTransactionAsync("KJ2AOIYYJNI36TIYP3BNR4L3SMNDUO3MPDTEH2IH3LOXQODBWNQA");
            //Debug.Print("Tx= " + mtTxn.Tx.ToString());
            //myLabel.Text = "Tx Info = " + mtTxn.Tx;
            //myLabel2.Text = "Payment = " + mtTxn.Payment.Amount.ToString();
            //  "KJ2AOIYYJNI36TIYP3BNR4L3SMNDUO3MPDTEH2IH3LOXQODBWNQA"

        }
        //   crypto.FromPublicKey()
        //        const atoken = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
        //          const aserver = "http://hackathon.algodev.network";


    }


}

