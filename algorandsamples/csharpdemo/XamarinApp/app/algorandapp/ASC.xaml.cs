using System;
using System.Collections.Generic;
using Algorand;
using Account = Algorand.Account;
using Algorand.Algod.Client.Api;
using Algorand.Algod.Client.Model;
using Xamarin.Essentials;
using Algorand.Algod.Client;
using Xamarin.Forms;


namespace algorandapp
{
    public partial class ASC : ContentPage
    {
        public Account account1;
        public Account account2;
        public Account account3;

        //   default to TESTNET
        public AlgodApi algodApiInstance;

        public static helper helper = new helper();

        public string network = "";
        public string nodetype = "";
        public ulong? assetID = 0;

        public ASC()
        {
            InitializeComponent();
            Appearing += ASC_Appearing;
        }

        private async void ASC_Appearing(object sender, EventArgs e)
        {
            algodApiInstance = await helper.CreateApiInstance();
            network = await helper.GetNetwork();

            // restore accounts
            var accounts = await helper.RestoreAccounts();
            account1 = accounts[0];
            account2 = accounts[1];
            account3 = accounts[2];

            nodetype = await SecureStorage.GetAsync(helper.StorageNodeType);
            NetworkLabel.Text = "Network: " + network + " " + nodetype;
            buttonstate();
        }
        public void buttonstate()
        {
            NetworkLabel.Text = "Network: " + network + " " + nodetype;
        }
        async void ASCContractAccount_Clicked(System.Object sender, System.EventArgs e)
        {
            ASCContractAccount.IsEnabled = false;
            Algorand.Algod.Client.Model.TransactionParams transParams = null;
            //  Algorand.Algod.Client.Model.TransactionParams transParams = algodApiInstance.TransactionParams();
            //TransactionParams transParams = null;
            try
            {
                transParams = algodApiInstance.TransactionParams();
            }
            catch (ApiException err)
            {
                throw new Exception("Could not get params", err);
            }
            // format and send logic sig
            byte[] program = { 0x01, 0x20, 0x01, 0x00, 0x22 }; // int 0, returns false, so rawTransaction will fail below
            // In the iOS project, place it in the Resources folder
            // and set the properties to Build Action of Bundled resource, copy if newer
            // In the Android project, place it in the Resources folder
            // and set the properties to Build Action of Bundled resource, copy if newer

            // works in iOS if you copy the langspec.json file to the resources folder in the iOS project build property bundled reource.

            LogicsigSignature lsig = new LogicsigSignature(program, null);
            Console.WriteLine("Escrow address: " + lsig.ToAddress().ToString());
            Algorand.Transaction tx = Utils.GetLogicSignatureTransaction(lsig, account1.Address, transParams, "logic sig message");
            if (!lsig.Verify(tx.sender))
            {
                string msg = "Verification failed";
                Console.WriteLine(msg);
            }
            else
            {
                try
                {
                    SignedTransaction stx = Account.SignLogicsigTransaction(lsig, tx);
                    byte[] encodedTxBytes = Encoder.EncodeToMsgPack(stx);
                    // int 0 is the teal program, which returns false, 
                    // so rawTransaction will fail below   

                    var id = algodApiInstance.RawTransaction(encodedTxBytes);
                    Console.WriteLine("Successfully sent tx logic sig tx id: " + id);
                    var wait = Utils.WaitTransactionToComplete(algodApiInstance, id.TxId);
                    Console.WriteLine(wait);
               //     Entry3.Text = wait;
                    var act = algodApiInstance.AccountInformation(account1.Address.ToString());
                 //   myLabel2.Text = "Account 1 balance after: " + act.Amount.ToString();

                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = @"<html><body><h3>" + wait + "</h3>" +
                        "<h3>" + "Account 1 balance after: " + act.Amount.ToString() + "</h3>" +
                        "<h3>" + "Account info: " + act.ToJson() + "</h3>" +
                        "</body></html>";

                    myWebView.Source = htmlSource;
                    await SecureStorage.SetAsync(helper.StorageTransaction, wait.ToString());

                    ASCContractAccount.IsEnabled = true;
                }
                catch (ApiException err)
                {
                    // This is generally expected, but should give us an informative error message.
                    Console.WriteLine("Fail expected");

                    Console.WriteLine("Exception when calling algod#rawTransaction: " + err.Message);
            
                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = @"<html><body>" +
                        "<h3>" + "Exception when calling algod#rawTransaction: " + err.Message + "</h3>" +
                        "</body></html>";

                    myWebView.Source = htmlSource;
                    ASCContractAccount.IsEnabled = true;

                }
            }

        }
        async void ASCContractAccountInfo_Clicked(System.Object sender, System.EventArgs e)
        {

            var act = algodApiInstance.AccountInformation(account1.Address.ToString());

         //   myLabel2.Text = "Account 1 balance after: " + act.Amount.ToString();
            var wait = await SecureStorage.GetAsync(helper.StorageTransaction);
        //    Entry3.Text = wait;

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body><h3>" + wait + "</h3>" +
                "<h3>" + "Account 1 balance after: " + act.Amount.ToString() + "</h3>" +
                "<h3>" + "Account info: " + act.ToJson() + "</h3>" +
                "</body></html>";

            myWebView.Source = htmlSource;
        }


        async void ASCAccountDelegation_Clicked(System.Object sender, System.EventArgs e)
        {
            ASCAccountDelegation.IsEnabled = false;
            Algorand.Algod.Client.Model.TransactionParams transParams = null;
            //  Algorand.Algod.Client.Model.TransactionParams transParams = algodApiInstance.TransactionParams();
            //TransactionParams transParams = null;
            try
            {
                transParams = algodApiInstance.TransactionParams();
            }
            catch (ApiException err)
            {
                throw new Exception("Could not get params", err);
            }
            // format and send logic sig
            byte[] program = { 0x01, 0x20, 0x01, 0x00, 0x22 }; // int 0, returns false, so rawTransaction will fail below
            // langspec.json needs to be downloaded to this project from
            // https://github.com/RileyGe/dotnet-algorand-sdk/blob/master/sdk-examples/langspec.json
            // In the iOS project, place it in the Resources folder
            // and set the properties to Build Action of Bundled resource, copy if newer
            // In the Android project, place it in the Resources folder
            // and set the properties to Build Action of Bundled resource, copy if newer


            LogicsigSignature lsig = new LogicsigSignature(program, null);

            // sign the logic signature with an account sk
            account1.SignLogicsig(lsig);

            Console.WriteLine("Escrow address: " + lsig.ToAddress().ToString());
            Algorand.Transaction tx = Utils.GetLogicSignatureTransaction(account1.Address, account2.Address, transParams, "logic sig message");
            try
            {
                    SignedTransaction stx = Account.SignLogicsigDelegatedTransaction(lsig, tx);
                    byte[] encodedTxBytes = Encoder.EncodeToMsgPack(stx);
                    // int 0 is the teal program, which returns false, 
                    // so rawTransaction will fail below   

                    var id = algodApiInstance.RawTransaction(encodedTxBytes);
                    Console.WriteLine("Successfully sent tx logic sig tx id: " + id);
                    var wait = Utils.WaitTransactionToComplete(algodApiInstance, id.TxId);
                    Console.WriteLine(wait);
               //     Entry3.Text = wait;
                    var act = algodApiInstance.AccountInformation(account1.Address.ToString());
                //    myLabel2.Text = "Account 1 balance after: " + act.Amount.ToString();
                    await SecureStorage.SetAsync(helper.StorageTransaction, wait.ToString());

                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = @"<html><body><h3>" + wait + "</h3>" +
                    "<h3>" + "Account 1 balance after: " + act.Amount.ToString() + "</h3>" +
                    "<h3>" + "Account info: " + act.ToJson() + "</h3>" +
                    "</body></html>";

                myWebView.Source = htmlSource;
                ASCAccountDelegation.IsEnabled = true;
            }
                catch (ApiException err)
                {
                    // This is generally expected, but should give us an informative error message.

                    Console.WriteLine("Exception when calling algod#rawTransaction: " + err.Message);
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = @"<html><body><h3> Exception when calling algod#rawTransaction: " + err.Message + "</h3>" + "</body></html>";

                myWebView.Source = htmlSource;
                ASCAccountDelegation.IsEnabled = true;

            }
            



        }



        void ASCAccountDelegationInfo_Clicked(System.Object sender, System.EventArgs e)
        {
            var act = algodApiInstance.AccountInformation(account1.Address.ToString());
      //      myLabel2.Text = "Account 1 balance after tx: " + act.Amount.ToString();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body>" +
                "<h3>" + "Account 1 balance after: " + act.Amount.ToString() + "</h3>" +
                "<h3>" + "Account info: " + act.ToJson() + "</h3>" +
                "</body></html>";

            myWebView.Source = htmlSource;

        }
    }
}
