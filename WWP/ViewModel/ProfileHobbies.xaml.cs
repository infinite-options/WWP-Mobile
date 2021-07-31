using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WWP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class ProfileHobbies : ContentPage
    {
        public ObservableCollection<Hobbies> hobbiesObsColl = new ObservableCollection<Hobbies>();

        public ProfileHobbies()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                hobbiesObsColl.Add(new Hobbies
                {
                    name = "Singing",
                    bgColor = Color.FromHex("#C9E4FF")
                });

                hobbiesObsColl.Add(new Hobbies
                {
                    name = "Biking",
                    bgColor = Color.FromHex("#C9E4FF")
                });

                hobbiesObsColl.Add(new Hobbies
                {
                    name = "Woodworking",
                    bgColor = Color.FromHex("#C9E4FF")
                });
            }

            hobbiesObsColl.Add(new Hobbies
            {
                name = "Woodworking",
                bgColor = Color.FromHex("#C9E4FF")
            });

            hobbiesObsColl.Add(new Hobbies
            {
                name = "Woodworking",
                bgColor = Color.FromHex("#C9E4FF")
            });

            hobbyCollView.HeightRequest = 80 * ((hobbiesObsColl.Count / 3) + 1);
            hobbyCollView.ItemsSource = hobbiesObsColl;
            //hobbyCollView.HeightRequest = 60 * ((hobbiesObsColl.Count / 3) + 1);
        }

        void backClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new WalkieProfile();
        }

        void nextClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ProfileSummary();
        }

        void addClicked(object sender, EventArgs e)
        {
            if (customHobbyEntry.Text != null)
            {
                hobbiesObsColl.Add(new Hobbies
                {
                    name = customHobbyEntry.Text.Trim(),
                    bgColor = Color.FromHex("#C9E4FF")
                });

                hobbyCollView.HeightRequest += 80;
                hobbyCollView.ItemsSource = hobbiesObsColl;
            }
        }

        void hobbyClicked(object sender, EventArgs e)
        {
            Forms9Patch.Button button1 = (Forms9Patch.Button)sender;
            Hobbies selectedHobby = button1.BindingContext as Hobbies;
            if (selectedHobby.bgColor == Color.FromHex("#C9E4FF"))
                selectedHobby.bgColor = Color.FromHex("#FEF2EA"); //selected light orange
            else selectedHobby.bgColor = Color.FromHex("#C9E4FF"); //unselected blue
        }
    }
}
