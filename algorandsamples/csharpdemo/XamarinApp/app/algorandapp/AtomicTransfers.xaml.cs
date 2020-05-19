using System;
using System.Collections.Generic;
using Algorand;
using Algorand.Algod.Client;
using Algorand.Algod.Client.Api;
using Algorand.Algod.Client.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Account = Algorand.Account;

namespace algorandapp
{
    public partial class AtomicTransfers : ContentPage
    {
        public Account account1;
        public Account account2;
        public Account account3;

        //   default to TESTNET
        public AlgodApi algodApiInstance;

        public static helper helper = new helper();

        public string network = "";
        public string nodetype = "";
        public AtomicTransfers()
        {
            InitializeComponent();
            Appearing += AtomicTransfers_Appearing;
        }

        private async void AtomicTransfers_Appearing(object sender, EventArgs e)
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
        async void AtomicTransfer_Clicked(System.Object sender, System.EventArgs e)
        {
            //var transParams = algodApiInstance.TransactionParams();

            TransactionParams transParams = null;

            try
            {
                transParams = algodApiInstance.TransactionParams();
            }
            catch (ApiException err)
            {
                throw new Exception("Could not get params", err);
            }
            // let's create a transaction group
            var amount = Utils.AlgosToMicroalgos(1); 
            var tx = Utils.GetPaymentTransaction(account1.Address, account2.Address, amount, "pay message", transParams);
            var tx2 = Utils.GetPaymentTransaction(account1.Address, account3.Address, amount, "pay message", transParams);
            //SignedTransaction signedTx2 = src.SignTransactionWithFeePerByte(tx2, feePerByte);
            Digest gid = TxGroup.ComputeGroupID(new Algorand.Transaction[] { tx, tx2 });
            tx.AssignGroupID(gid);
            tx2.AssignGroupID(gid);
            // already updated the groupid, sign
            var signedTx = account1.SignTransaction(tx);
            var signedTx2 = account1.SignTransaction(tx2);
            try
            {
                //contact the signed msgpack
                List<byte> byteList = new List<byte>(Algorand.Encoder.EncodeToMsgPack(signedTx));
                byteList.AddRange(Algorand.Encoder.EncodeToMsgPack(signedTx2));
                var act = algodApiInstance.AccountInformation(account1.Address.ToString());
                myLabel.Text = "Account 1 balance before: " + act.Amount.ToString();
                var id = algodApiInstance.RawTransaction(byteList.ToArray());
                var wait = Utils.WaitTransactionToComplete(algodApiInstance, id.TxId);
                Console.WriteLine(wait);

                // Console.WriteLine("Successfully sent tx group with first tx id: " + id);

                Entry3.Text = wait;
                act = algodApiInstance.AccountInformation(account1.Address.ToString());
                myLabel2.Text = "Account 1 balance after: " + act.Amount.ToString();
                await SecureStorage.SetAsync(helper.StorageTransaction, wait.ToString());
            }
            catch (ApiException err)
            {
                // This is generally expected, but should give us an informative error message.
                Console.WriteLine("Exception when calling algod#rawTransaction: " + err.Message);
            }


        }

        async void AtomicTransferInfo_Clicked(System.Object sender, System.EventArgs e)
        {
            var act = algodApiInstance.AccountInformation(account1.Address.ToString());
            Entry3.Text = await SecureStorage.GetAsync(helper.StorageTransaction);
           
            Entry4.Text = "Account 1 balance : " + act.Amount.ToString();



        }
    }
}
