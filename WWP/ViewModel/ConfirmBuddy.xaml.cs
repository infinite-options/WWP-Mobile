using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class ConfirmBuddy : ContentPage
    {
        public ConfirmBuddy()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();
        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileSummary();
        }
    }
}
