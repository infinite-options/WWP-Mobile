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
            //if there are walks scheduled, show the schedule collection view, or else show the noWalksStack
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
            ObservableCollection<Slot> slotColl1 = new ObservableCollection<Slot>();
            ObservableCollection<Slot> slotColl2 = new ObservableCollection<Slot>();
            slotColl1.Add(new Slot
            {
                eventName = "Walk with John",
                time = "1:00PM"
            });

            slotColl1.Add(new Slot
            {
                eventName = "Walk with Jane",
                time = "2:00PM"
            });

            slotColl2.Add(new Slot
            {
                eventName = "Walk with Jane",
                time = "5:00PM"
            });

            slotColl2.Add(new Slot
            {
                eventName = "Walk with Berry",
                time = "12:00PM"
            });

            scheduleColl.Add(new Appointment
            {
                day = "Today",
                fullDate = "Friday, June 29",
                slotColl = slotColl1,
                slotCollHeight = slotColl1.Count * 140
            });

            scheduleColl.Add(new Appointment
            {
                day = "Tomorrow",
                fullDate = "Saturday, June 30",
                slotColl = slotColl2,
                slotCollHeight = slotColl2.Count * 140
            });

            schedule.ItemsSource = scheduleColl;
        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileSummary();
        }

        void eventClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Feedback());
            //Application.Current.MainPage = new ProfileSummary();
        }

        void buddyClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PickABuddy());
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


        //walker menu functions
        void menuWalkerClicked(object sender, EventArgs e)
        {
            menu.IsVisible = false;
            openWalkerMenuGrid.IsVisible = true;
        }

        void openMenuWalkerClicked(object sender, EventArgs e)
        {
            menu.IsVisible = true;
            openWalkerMenuGrid.IsVisible = false;
        }

        void profileWalkerClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new WalkSchedule());
        }

        void logoutWalkerClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainLogin());
        }
        //end walker menu functions
    }
}
