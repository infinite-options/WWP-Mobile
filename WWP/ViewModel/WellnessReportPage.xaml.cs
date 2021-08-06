using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class WellnessReportPage : ContentPage
    {
        public WellnessReportPage()
        {
            InitializeComponent();
        }

        void NavigateBack(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        void NavigateToSchedule(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SchedulePage(), false);
        }

        void NavigateToFeedbackPage(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Next Page", "Place JL Feedback page here. Right now you are send to the schedule page", "OK");
            Navigation.PushAsync(new CalendarPage(), false);
        }
    }
}
