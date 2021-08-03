﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WWP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileSummary();
        }

        void confirmClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WalkSummary());
            //Application.Current.MainPage = new ProfileSummary();
        }
    }
}
