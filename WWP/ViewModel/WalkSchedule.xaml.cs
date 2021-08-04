using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WWP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class WalkSchedule : ContentPage
    {
        ObservableCollection<Appointment> scheduleColl = new ObservableCollection<Appointment>();

        public WalkSchedule()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();
            fillSchedule();
        }

        void fillSchedule()
        {

        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileSummary();
        }

        void buddyClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PickABuddy());
            //Application.Current.MainPage = new ProfileSummary();
        }
    }
}
