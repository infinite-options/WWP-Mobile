using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class WalkerWalkSummary : ContentPage
    {
        public WalkerWalkSummary()
        {
            InitializeComponent();
        }

        void NavigateToWalkerTimer(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WalkerTimer(), false);
        }

        void NavigateToWalkerPrepPage(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WalkerPrepPage(), false);
        }
    }
}
