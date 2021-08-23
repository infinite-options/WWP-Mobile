using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class SignUpOptions : ContentPage
    {
        public static string flow = "";
        public SignUpOptions()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();
        }

        void WalkerFlow(object sender, EventArgs e)
        {
            flow = "walker";

            Application.Current.MainPage = new SignUp();
        }

        void WalkieFlow(System.Object sender, System.EventArgs e)
        {
            flow = "walkie";

            Application.Current.MainPage = new SignUp();
        }
    }
}
