using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class FeedbackReceived : ContentPage
    {
        public FeedbackReceived()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;

            InitializeComponent();
        }

        void seeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WalkSchedule());
        }

        //walkie menu functions
        void menuClicked(object sender, EventArgs e)
        {
            menu.IsVisible = false;
            openWalkieMenuGrid.IsVisible = true;
        }

        void openMenuClicked(object sender, EventArgs e)
        {
            menu.IsVisible = true;
            openWalkieMenuGrid.IsVisible = false;
        }

        void upcomingClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WalkSchedule());
        }

        void planClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WalkSchedule());
        }

        void profileClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new WalkSchedule());
        }

        void logoutClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainLogin());
        }
        //end walkie menu functions
    }
}
