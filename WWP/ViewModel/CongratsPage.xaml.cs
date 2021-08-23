using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using WWP.Model;
using WWP.Model.Login.LoginClasses;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using WWP.Model.Login.Constants;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Xamarin.Forms.Maps;
using WWP.ViewModel;

namespace WWP.ViewModel
{
    public partial class CongratsPage : ContentPage
    {

        public CongratsPage()
        {
            try
            {
                NavigationPage.SetHasBackButton(this, false);
                NavigationPage.SetHasNavigationBar(this, false);
                var width = DeviceDisplay.MainDisplayInfo.Width;
                var height = DeviceDisplay.MainDisplayInfo.Height;
                Console.WriteLine("Width = " + width.ToString());
                Console.WriteLine("Height = " + height.ToString());

                InitializeComponent();

            }
            catch (Exception ex)
            {
                //Generic gen = new Generic();
                //gen.parseException(ex.ToString());
            }
        }

        void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Application.Current.MainPage = new ProfileSummary();
        }

        void scheduleClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WalkSchedule());
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
    }
}
