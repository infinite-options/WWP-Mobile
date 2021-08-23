using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WWP.Model;
using Xamarin.Forms;
using static WWP.ViewModel.WalkSchedule;
namespace WWP.ViewModel
{
    public partial class PickWalkCalendarPage : ContentPage
    {

        public ObservableCollection<AvailableTime> availableTimeSource = new ObservableCollection<AvailableTime>();
        public PickWalkCalendarPage()
        {
            InitializeComponent();
            calendar.SelectedDate = DateTime.Now;
            selectedDate.Text = "Today, " + DateTime.Now.ToString("MMM dd");

            SetAvailableTimes();

            SetButtonText(comfirmButton);
        }

        public void SetButtonText(Button comfirmButton)
        {
            if(option == "pick_a_buddy")
            {
                comfirmButton.Text = "Confirm";
            }
            else if (option == "pick_a_date")
            {
                comfirmButton.Text = "Pick a Buddy";
            }
        }

        void SetAvailableTimes()
        {
            availableTimeSource.Clear();
           

            availableTimeSource.Add(new AvailableTime() { availableTime = "1:00 PM", color = Color.White });
            availableTimeSource.Add(new AvailableTime() { availableTime = "2:00 PM", color = Color.White });
            availableTimeSource.Add(new AvailableTime() { availableTime = "3:00 PM", color = Color.White });
            availableTimeSource.Add(new AvailableTime() { availableTime = "4:00 PM", color = Color.White });
            availableTimeSource.Add(new AvailableTime() { availableTime = "5:00 PM", color = Color.White });

            availableTimeList.ItemsSource = availableTimeSource;
        }

        void calendar_DateClicked(System.Object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            var today = DateTime.Now;
            if (e.DateTime.ToString("MMM dd") == today.ToString("MMM dd"))
            {
                selectedDate.Text = "Today, " + e.DateTime.ToString("MMM dd");
            }
            else if (e.DateTime.ToString("MMM dd") == today.AddDays(1).ToString("MMM dd"))
            {
                selectedDate.Text = "Tomorrow, " + e.DateTime.ToString("MMM dd");
            }
            else
            {
                selectedDate.Text = e.DateTime.ToString("MMM dd, yyyy");
            }
        }

        void SelectAvailableTime(System.Object sender, System.EventArgs e)
        {
            var s = (StackLayout)sender;
            var recognizer = (TapGestureRecognizer)s.GestureRecognizers[0];
            var time = (AvailableTime)recognizer.CommandParameter;

            if (time.color == Color.White)
            {
                availableTime.Text = time.availableTime;
                time.updateColor = Color.FromHex("#FFF2EA");

                foreach(AvailableTime element in availableTimeSource)
                {
                    if(element.availableTime != time.availableTime)
                    {
                        element.updateColor = Color.White;
                    }
                }
            }
        }

        void ShowAvailableTimes(System.Object sender, System.EventArgs e)
        {
            if (availableTimeView.IsVisible == false)
            {
                availableTimeView.IsVisible = true;
                availableTime.IsVisible = false;
            }
            else
            {
                availableTimeView.IsVisible = false;
                availableTime.IsVisible = true;
            }
        }

        void NavigateToWalkSummaryOrPickABuddy(System.Object sender, System.EventArgs e)
        {
            var s = (Button)sender;

            if(s.Text == "Confirm")
            {
                Navigation.PushAsync(new WalkSummary());
            }
            else if (s.Text == "Pick a Buddy")
            {
                Navigation.PushAsync(new PickABuddy());
            }
        }

        void NavigateBack(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }
    }
}
