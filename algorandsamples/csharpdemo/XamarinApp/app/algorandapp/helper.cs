using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Account = Algorand.Account;
using System.Threading.Tasks;

namespace algorandapp
{
    public class helper
    {
        public helper()
        {

        }

        public async void openurl(string url, string account)
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

        public async Task<string[]> CreateAccount(int accountID )
        {
            string[] accountinfo = new string[2];


            Account myAccount = new Account();
            var myMnemonic = myAccount.ToMnemonic();
            Console.WriteLine("Account " + accountID.ToString() + " Address = " + myAccount.Address.ToString());
            Console.WriteLine("Account " + accountID.ToString() + " Mnemonic = " + myMnemonic.ToString());
            var myAccountAddress = myAccount.Address.ToString();
           // return myLabel;

            accountinfo[0] = myAccount.Address.ToString();
            accountinfo[1] = myMnemonic.ToString();

            return accountinfo;
        }

    }
}
