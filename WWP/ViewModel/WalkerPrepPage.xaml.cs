using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Experimentation.Models;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class WalkerPrepPage : ContentPage
    {
        public ObservableCollection<ItemToBring> prepListSource = new ObservableCollection<ItemToBring>();
        public ObservableCollection<ScheduleWalk> walksSource = new ObservableCollection<ScheduleWalk>();

        public string flag = "";

        public WalkerPrepPage()
        {
            InitializeComponent();


            prepListSource.Add(new ItemToBring() { isChecked = false, title = "Bring any medication you might need" });
            prepListSource.Add(new ItemToBring() { isChecked = false, title = "Bring your phone" });
            prepListSource.Add(new ItemToBring() { isChecked = false, title = "Bring some water" });

            prepItemCollection.ItemsSource = prepListSource;
            prepItemCollection.HeightRequest = 3 * 35;

            if (flag == "")
            {
                sitBackAndRelaxMessage.IsVisible = true;
                allRequiredItemsAreChecked.IsVisible = false;

                walkMessage.Text = "Your walk is not for another ";
                time.Text = "48 hours";
                reminderMessage.Text = "Here’s what you’re going to need";
            }
            else
            {
                sitBackAndRelaxMessage.IsVisible = false;
                allRequiredItemsAreChecked.IsVisible = true;

                walkMessage.Text = "Your walk is in ";
                time.Text = "30 mins";
                reminderMessage.Text = "Make you sure you got everything you need!";
            }

            for (int i = 0; i < 1; i++)
            {
                walksSource.Add(new ScheduleWalk() { photo = "walkiePhoto", name = "Walk with David", timeInterval = "1:00 PM - 1:30 PM" });
            }

            personToWalkWithCollection.ItemsSource = walksSource;

        }

        void NavigateToWellnessReport(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WellnessReportPage(), false);
        }

        void NavigateToWalkSummaryPage(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }

        void CheckItem(System.Object sender, System.EventArgs e)
        {
            var view = (StackLayout)sender;
            var recognizer = (TapGestureRecognizer)view.GestureRecognizers[0];
            var item = (ItemToBring)recognizer.CommandParameter;

            if (item.isChecked == false)
            {
                item.isCheckedUpdate = true;
            }
            else
            {
                item.isCheckedUpdate = false;
            }

            // check all items have been checked

            var status = true;

            foreach (ItemToBring element in prepListSource)
            {
                if (element.isChecked == false)
                {
                    status = false;
                    break;
                }
            }

            if (status && flag == "2")
            {
                allRequiredItemsAreChecked.IsVisible = true;
            }
            else
            {
                allRequiredItemsAreChecked.IsVisible = false;
            }
        }

        async void Okay(System.Object sender, System.EventArgs e)
        {
            var result = await DisplayAlert("Mode", "Mode 1 represent the UI when the walk is before 30 minds, Mode 2 represents the UI when the walk is in 30 mins", "Mode 1", "Mode 2");

            Debug.WriteLine("Mode 1 : " + result);
            Debug.WriteLine("Mode 2 : " + result);

            if (result)
            {
                flag = "";

                if (flag == "")
                {
                    sitBackAndRelaxMessage.IsVisible = true;
                    allRequiredItemsAreChecked.IsVisible = false;

                    walkMessage.Text = "Your walk is not for another ";
                    time.Text = "48 hours";
                    reminderMessage.Text = "Here’s what you’re going to need";
                }
                else
                {
                    walkMessage.Text = "Your walk is in ";
                    time.Text = "30 mins";
                    reminderMessage.Text = "Make you sure you got everything you need!";


                }
            }
            else
            {
                flag = "2";

                if (flag == "")
                {
                    sitBackAndRelaxMessage.IsVisible = true;
                    allRequiredItemsAreChecked.IsVisible = false;
                    sitBackAndRelaxButton.IsVisible = true;

                    walkMessage.Text = "Your walk is not for another ";
                    time.Text = "48 hours";
                    reminderMessage.Text = "Here’s what you’re going to need";
                }
                else
                {
                    sitBackAndRelaxMessage.IsVisible = false;
                    sitBackAndRelaxButton.IsVisible = false;

                    walkMessage.Text = "Your walk is in ";
                    time.Text = "30 mins";
                    reminderMessage.Text = "Make you sure you got everything you need!";
                }
            }
        }

    }
}
