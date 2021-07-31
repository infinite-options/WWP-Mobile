using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class WalkieProfile : ContentPage
    {
        public WalkieProfile()
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
            Application.Current.MainPage = new MainPage();
        }

        void hobbyClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ProfileHobbies();
        }
    }
}
