using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Experimentation.Models;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class CalendarPage : ContentPage
    {
        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances { get; set; }
        public ObservableCollection<ScheduleWalk> pastWalksList = new ObservableCollection<ScheduleWalk>();
        public ObservableCollection<ScheduleWalk> todayWalksList = new ObservableCollection<ScheduleWalk>();
        public ObservableCollection<ScheduleWalk> futureWalksList = new ObservableCollection<ScheduleWalk>();

        public CalendarPage()
        {
            InitializeComponent();

            for (int i = 0; i < 3; i++)
            {
                pastWalksList.Add(new ScheduleWalk() { photo = "walkiePhoto", name = "Walk with David", timeInterval = "1:00 PM - 1:30 PM" });
            }

            for (int i = 0; i < 3; i++)
            {
                todayWalksList.Add(new ScheduleWalk() { photo = "walkiePhoto", name = "Walk with David", timeInterval = "1:00 PM - 1:30 PM" });
            }

            for (int i = 0; i < 3; i++)
            {
                futureWalksList.Add(new ScheduleWalk() { photo = "walkiePhoto", name = "Walk with David", timeInterval = "1:00 PM - 1:30 PM" });
            }


            Attendances = new ObservableCollection<XamForms.Controls.SpecialDate>();

            var pastDate1 = new XamForms.Controls.SpecialDate(new DateTime(2021, 08, 3));

            pastDate1.BackgroundColor = Color.FromHex("#EFEFEF");
            pastDate1.TextColor = Color.DarkGray;
            pastDate1.Selectable = true;

            var pastDate2 = new XamForms.Controls.SpecialDate(new DateTime(2021, 08, 4));

            pastDate2.BackgroundColor = Color.FromHex("#EFEFEF");
            pastDate2.TextColor = Color.DarkGray;
            pastDate2.Selectable = true;

            var currentDate = new XamForms.Controls.SpecialDate(new DateTime(2021, 08, 5));

            currentDate.BackgroundColor = Color.FromHex("#F87F1B");
            currentDate.TextColor = Color.White;
            currentDate.BorderWidth = 1;
            currentDate.Selectable = true;

            var futureDate1 = new XamForms.Controls.SpecialDate(new DateTime(2021, 08, 6));

            futureDate1.BackgroundColor = Color.FromHex("#FFF2EA");
            futureDate1.TextColor = Color.DarkGray;
            futureDate1.BorderColor = Color.FromHex("#F87F1B");
            futureDate1.BorderWidth = 1;
            futureDate1.Selectable = true;

            var futureDate2 = new XamForms.Controls.SpecialDate(new DateTime(2021, 08, 7));

            futureDate2.BackgroundColor = Color.FromHex("#FFF2EA");
            futureDate2.TextColor = Color.DarkGray;
            futureDate2.BorderColor = Color.FromHex("#F87F1B");
            futureDate2.BorderWidth = 1;
            futureDate2.Selectable = true;


            Attendances.Add(pastDate1);
            Attendances.Add(pastDate2);
            Attendances.Add(currentDate);
            Attendances.Add(futureDate1);
            Attendances.Add(futureDate2);

            //calendar.SpecialDates = Attendances;
        }

        void NavigateToSchedule(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SchedulePage(), false);
        }

        void SelectedDate(System.Object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            try
            {
                bool dateIsValid = false;
                string type = "";
                var d = e.DateTime;
                foreach (XamForms.Controls.SpecialDate date in Attendances)
                {
                    if (date.Date.ToString("yyyy-MM-dd") == d.ToString("yyyy-MM-dd"))
                    {
                        dateIsValid = true;
                        if (date.BackgroundColor == Color.FromHex("#EFEFEF"))
                        {
                            type = "PAST";
                        }
                        else if (date.BackgroundColor == Color.FromHex("#F87F1B"))
                        {
                            type = "CURRENT";
                        }
                        else if (date.BackgroundColor == Color.FromHex("#FFF2EA"))
                        {
                            type = "FUTURE";
                        }
                        break;
                    }
                }

                if (dateIsValid)
                {
                    noScheduledWalks.IsVisible = false;
                    scheduledWalks.IsVisible = true;
                    if (type == "PAST")
                    {
                        scheduleDateLabel.Text = d.ToString("MMM dd, yyyy");
                        scheduleWalksLabel.Text = "Past walks";
                        scheduledWalksCollection.ItemsSource = pastWalksList;
                    }
                    else if (type == "CURRENT")
                    {
                        scheduleDateLabel.Text = "Today";
                        scheduleWalksLabel.Text = "Upcoming Walks";
                        scheduledWalksCollection.ItemsSource = todayWalksList;
                    }
                    else if (type == "FUTURE")
                    {
                        var todayDate = DateTime.Now;

                        if (d.ToString("yyyy-MM-dd") == todayDate.AddDays(1).ToString("yyyy-MM-dd"))
                        {
                            scheduleDateLabel.Text = "Tomorrow";
                        }
                        else
                        {
                            scheduleDateLabel.Text = d.ToString("MMM dd, yyyy");
                        }

                        scheduleWalksLabel.Text = "Upcoming Walks";
                        scheduledWalksCollection.ItemsSource = futureWalksList;
                    }
                }
                else
                {
                    noScheduledWalks.IsVisible = true;
                    scheduledWalks.IsVisible = false;
                }
            }
            catch (Exception a)
            {
                Debug.WriteLine(a.Message);
            }
        }

        void NavigateBack(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
