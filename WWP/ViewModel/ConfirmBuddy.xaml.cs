using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WWP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using static WWP.ViewModel.WalkSchedule;
namespace WWP.ViewModel
{
    public partial class ConfirmBuddy : ContentPage
    {
        ObservableCollection<Hobbies> hobbiesObsColl = new ObservableCollection<Hobbies>();

        public ConfirmBuddy()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();

            hobbyCollView.HeightRequest = 80 * ((hobbiesObsColl.Count / 3) + 1);
            hobbyCollView.ItemsSource = hobbiesObsColl;

            SetConfirmButtonText(comfirmButton);
        }

        public void SetConfirmButtonText(Button comfirmButton)
        {
            if(option == "pick_a_buddy")
            {
                comfirmButton.Text = "Schedule Walk";
            }
            else if (option == "pick_a_date")
            {
                comfirmButton.Text = "Confirm";
            }
        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileSummary();
        }

        void confirmClicked(object sender, EventArgs e)
        {
            var s = (Button)sender;

            if(s.Text == "Schedule Walk")
            {
                Navigation.PushAsync(new PickWalkCalendarPage());
            }
            else if (s.Text == "Confirm")
            {
                Navigation.PushAsync(new WalkSummary());
            }
           
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
