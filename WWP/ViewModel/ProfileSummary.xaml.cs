using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WWP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using static WWP.ViewModel.SignUpOptions;

namespace WWP.ViewModel
{
    public partial class ProfileSummary : ContentPage
    {
        public ProfileSummary(ObservableCollection<Hobbies> hobbyColl)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();

            hobbyCollView.HeightRequest = 80 * ((hobbyColl.Count / 3) + 1);
            hobbyCollView.ItemsSource = hobbyColl;
        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileHobbies();
        }

        void startClicked(object sender, EventArgs e)
        {
            if(flow == "walker")
            {
                Navigation.PushAsync(new CalendarPage());
            }
            else if (flow == "walkie")
            {
                Navigation.PushAsync(new WalkSchedule());
            }
            
            //Application.Current.MainPage = new WalkSchedule();
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
