using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WWP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class PickABuddy : ContentPage
    {
        public ObservableCollection<BuddyAvailability> BuddyObsColl = new ObservableCollection<BuddyAvailability>();

        public PickABuddy()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();
            getWalkBuddies();
        }

        void getWalkBuddies()
        {
            //temporary fillers
            BuddyObsColl.Add(new BuddyAvailability
            {
                name = "John Doe",
                profileImg = "",
                sunday = "SatOrange.png",
                monday = "MonOrange.png",
                tuesday = "TuesOrange.png",
                wednesday = "WedOrange.png",
                thursday = "TuesOrange.png",
                friday = "FriOrange.png",
                saturday = "SatOrange.png"
            });

            BuddyObsColl.Add(new BuddyAvailability
            {
                name = "Jane Johnson",
                profileImg = "",
                sunday = "SatOrange.png",
                monday = "MonGray.png",
                tuesday = "TuesOrange.png",
                wednesday = "WedOrange.png",
                thursday = "TuesGray.png",
                friday = "FriOrange.png",
                saturday = "SatOrange.png"
            });

            BuddyObsColl.Add(new BuddyAvailability
            {
                name = "Jordan Cane",
                profileImg = "",
                sunday = "SatOrange.png",
                monday = "MonGray.png",
                tuesday = "TuesGray.png",
                wednesday = "WedGray.png",
                thursday = "TuesGray.png",
                friday = "FriOrange.png",
                saturday = "SatOrange.png"
            });

            buddyCollView.HeightRequest = 160 * BuddyObsColl.Count;
            buddyCollView.ItemsSource = BuddyObsColl;
        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileSummary();
        }

        void buddyClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConfirmBuddy());
            //Application.Current.MainPage = new ProfileSummary();
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
