using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using static WWP.ViewModel.SignUpOptions;
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
            if(flow == "walkie")
            {
                Application.Current.MainPage = new NavigationPage(new WalkieProfile());
            }
            else if (flow == "walker")
            {
                Application.Current.MainPage = new NavigationPage(new SchedulePage());
            }
        }
    }
}
