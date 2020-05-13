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


        public Account account1;
        public Account account2;
        public Account account3;

        //   default to TESTNET
        public AlgodApi algodApiInstance;

        public static helper helper = new helper();

        public string network = "";
        public ulong? assetID = 0;

        public ASA()
        {
            InitializeComponent();
            Appearing += ASA_Appearing;
        }

        private async void ASA_Appearing(object sender, EventArgs e)
        {
            algodApiInstance = await helper.CreateApiInstance();
            network = await helper.GetNetwork();

            // restore accounts
            var accounts = await helper.RestoreAccounts();
            account1 = accounts[0];
            account2 = accounts[1];
            account3 = accounts[2];
            buttonstate();

        }
        public async void buttonstate()
        {

            NetworkLabel.Text = "Network: " + network;
            var AssetID = await SecureStorage.GetAsync(helper.StorageAssetIDName);
            if (!String.IsNullOrEmpty(AssetID))
            { 
                myAsset.Text = "Asset ID = " + AssetID;
                assetID = Convert.ToUInt64(AssetID);
            }

        }

        private async void CreateAsset_click(System.Object sender, System.EventArgs e)
        {

            var transParams = algodApiInstance.TransactionParams();

            // The following parameters are asset specific
            // and will be re-used throughout the example. 

            // Create the Asset
            // Total number of this asset available for circulation
     
            var ap = new AssetParams(creator: account1.Address.ToString(), assetname: "latikum22",
                unitname: "LAT", defaultfrozen: false, total: 10000,
                url: "http://this.test.com", metadatahash: Convert.ToBase64String(
                    Encoding.ASCII.GetBytes("16efaa3924a6fd9d3a4880099a4ac65d")))
            {
                Managerkey = account2.Address.ToString()
            };

            // Specified address can change reserve, freeze, clawback, and manager
            // you can leave as default, by default the sender will be manager/reserve/freeze/clawback
            // the following code only set the freeze to account1
            var tx = Utils.GetCreateAssetTransaction(ap, transParams, "asset tx message");

            // Sign the Transaction by sender
            SignedTransaction signedTx = account1.SignTransaction(tx);
            // send the transaction to the network and
            // wait for the transaction to be confirmed
         
            try
            {
                TransactionID id = Utils.SubmitTransaction(algodApiInstance, signedTx);
                Console.WriteLine("Transaction ID: " + id);
                Console.WriteLine(Utils.WaitTransactionToComplete(algodApiInstance, id.TxId));
                // Now that the transaction is confirmed we can get the assetID
                Algorand.Algod.Client.Model.Transaction ptx = algodApiInstance.PendingTransactionInformation(id.TxId);
                assetID = ptx.Txresults.Createdasset;
                var assetIDstr = assetID.ToString();
                await SecureStorage.SetAsync(helper.StorageAssetIDName,assetIDstr);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.StackTrace);
                return;
            }
            Console.WriteLine("AssetID = " + assetID);
            // now the asset already created
            myLabel.Text = "AssetID created = " + assetID.ToString();
            Entry3.Text = "AssetID created = " + assetID.ToString();
            myAsset.Text = "AssetID = " + assetID.ToString();

            buttonstate();
        }

        async void GetAssetInfo_click(System.Object sender, System.EventArgs e)
        {
            myLabel.Text = "AssetID = " + assetID.ToString();
            var act = algodApiInstance.AccountInformation(account1.Address.ToString());
            myLabel2.Text = "Asset Info = " + act.ToString();
          //  Entry3.Text = "Assets = " + act.Assets.Values.ToString();
         
        }

        void CongfigureManagerRole_click(System.Object sender, System.EventArgs e)
        {
            // Change Asset Configuration:
            // Next we will change the asset configuration
            // First we update standard Transaction parameters
            // To account for changes in the state of the blockchain
            var transParams = algodApiInstance.TransactionParams();
            var ap = algodApiInstance.AssetInformation((long?)assetID);

            // Note that configuration changes must be done by
            // The manager account, which is currently account2
            // Note in this transaction we are re-using the asset
            // creation parameters and only changing the manager
            // and transaction parameters like first and last round
            // now update the manager to account1
            ap.Managerkey = account1.Address.ToString();
            var tx = Utils.GetConfigAssetTransaction(account2.Address, assetID, ap, transParams, "config trans");

            // The transaction must be signed by the current manager account
            // We are reusing the signedTx variable from the first transaction in the example    
            var signedTx = account2.SignTransaction(tx);
            // send the transaction to the network and
            // wait for the transaction to be confirmed
            try
            {
                TransactionID id = Utils.SubmitTransaction(algodApiInstance, signedTx);
                Console.WriteLine("Transaction ID: " + id.TxId);
                Console.WriteLine(Utils.WaitTransactionToComplete(algodApiInstance, id.TxId));
             
            }
            catch (Exception err)
            {
                //e.printStackTrace();
                Console.WriteLine(err.Message);
                return;
            }
        }

        void GetManagerInfo_click(System.Object sender, System.EventArgs e)
        {
        }
    }
}

