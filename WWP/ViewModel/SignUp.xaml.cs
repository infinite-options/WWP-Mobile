using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WWP.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {

        public SignUp()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            Console.WriteLine("Width = " + width.ToString());
            Console.WriteLine("Height = " + height.ToString());

            InitializeComponent();
        }

        void signUpClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignUpCongrats();
        }

        void clickedSeePassword(System.Object sender, System.EventArgs e)
        {
            if (loginPassword.IsPassword == true)
                loginPassword.IsPassword = false;
            else loginPassword.IsPassword = true;
        }

        void clickedSeePassword2(System.Object sender, System.EventArgs e)
        {
            if (confirmLoginPassword.IsPassword == true)
                confirmLoginPassword.IsPassword = false;
            else confirmLoginPassword.IsPassword = true;
        }

    }
}
