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

        public MainPage()
        {
            InitializeComponent();
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


        public async void ASCContractAccount_Clicked(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new ASCContractAccount());
        }

        public async void ASCAccountDelegation_Clicked(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new ASCAccountDelegation());
        }

        public async void Settings_Clicked(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new Settings());
        }

        async void ASC1_Clicked(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new ASC());
        }
    }


}

