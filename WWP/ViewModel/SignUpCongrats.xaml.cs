using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class SignUpCongrats : ContentPage
    {
        public SignUpCongrats()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;

            InitializeComponent();
        }

        void setUpClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new WalkieProfile());
        }

    }
}
