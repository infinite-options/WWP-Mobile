using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class WalkerProfilePage : ContentPage
    {
        public WalkerProfilePage()
        {
            InitializeComponent();
        }

        void NavigateToWalkerPrepPage(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WalkerPrepPage(), false);
        }
    }
}
