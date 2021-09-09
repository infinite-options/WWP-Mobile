using System;
using WWP.Model.SignUp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static WWP.ViewModel.SignUpOptions;
using static WWP.Model.SignUp.SignUpPost;
using WWP.Model.Login.LoginClasses.Apple;
using System.Diagnostics;
using Xamarin.Auth;
using WWP.Model.Login.LoginClasses;
using WWP.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WWP.Model.Login.Constants;

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



        async void signUpClicked(object sender, EventArgs e)
        {
            var user = new Model.SignUp.SignUpPost(flow.ToUpper(), "DIRECT");

            if (ValidateSignUpInfo(loginUsername, confirmLoginUsername, loginPassword, confirmLoginPassword))
            {
                if(ValidateEmails(loginUsername, confirmLoginUsername) && ValidatePasswords(loginPassword, confirmLoginPassword))
                {
                    if (IsValidEmail(loginUsername.Text))
                    {
                        CreateUser(loginUsername.Text.ToLower().Trim(), "", loginPassword.Text.Trim());
                    }
                    else
                    {
                        if (IsValidPhoneNumber(loginUsername.Text))
                        {
                            CreateUser("", loginUsername.Text.Trim(), loginPassword.Text.Trim());
                        }
                        else
                        {
                            await DisplayAlert("Oops", "Plese enter a proper phone number or email", "OK");
                        }
                    }
                    
                }
                else
                {
                    await DisplayAlert("Oops", "Please make sure that your username matches your confirm username. Same with your password.", "OK");
                }
                  
            }
            else
            {
                await DisplayAlert("Oops","Please fill in all entries.","OK");   
            }
        }

        async void CreateUser(string email, string num, string password)
        {
            var user = new Model.SignUp.SignUpPost(flow.ToUpper(), "DIRECT");

            user.first_name = "";
            user.last_name = "";
            user.phone_number = num;
            user.email = email;
            user.password = password;

            var result = await signUpUser(user);

            if (result == null || result == "")
            {
                await DisplayAlert("Oops", "Something went wrong.", "OK");
            }
            else if (result != null && result != "" && result != "USER ALREADY EXIST")
            {
                Application.Current.MainPage = new SignUpCongrats();
            }
            else if (result != null && result != "" && result == "USER ALREADY EXIST")
            {
                await DisplayAlert("Oops", "This email already exist.", "OK");
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool IsValidPhoneNumber(string num)
        {
            bool result = true;

            if(num.Length == 10)
            {
                foreach (char a in num.ToCharArray())
                {
                    if (!char.IsDigit(a))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
           
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

        void ContinueWithFacebook(System.Object sender, System.EventArgs e)
        {
            var client = new SignIn();
            var authenticator = client.GetFacebookAuthetication();
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            authenticator.Completed += FacebookAuthetication;
            authenticator.Error += Authenticator_Error;
            presenter.Login(authenticator);
        }

        void ContinueWithGoogle(System.Object sender, System.EventArgs e)
        {
            var client = new SignIn();
            var authenticator = client.GetGoogleAuthetication();
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            AuthenticationState.Authenticator = authenticator;
            authenticator.Completed += GoogleAuthetication;
            authenticator.Error += Authenticator_Error;
            presenter.Login(authenticator);
        }

        private async void FacebookAuthetication(object sender, Xamarin.Auth.AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= FacebookAuthetication;
                authenticator.Error -= Authenticator_Error;
            }

            if (e.IsAuthenticated)
            {

                try
                {
                    var clientLogIn = new SignIn();
                    var user = new Model.SignUp.SignUpPost(flow.ToUpper(), "SOCIAL");
                    var facebookUser = clientLogIn.GetFacebookUser(e.Account.Properties["access_token"]);

                    user.first_name = facebookUser.name;
                    user.last_name = "";
                    user.phone_number = "";
                    user.email = facebookUser.email;
                    user.social_id = facebookUser.id;
                    user.mobile_access_token = e.Account.Properties["access_token"];
                    user.mobile_refresh_token = e.Account.Properties["access_token"];
                    user.social = "FACEBOOK";

                    var result = await signUpUser(user);
                    if (result == null || result == "")
                    {
                        await DisplayAlert("Oops", "Something went wrong.", "OK");
                    }
                    else if (result != null && result != "" && result != "USER ALREADY EXIST")
                    {
                        Application.Current.MainPage = new SignUpCongrats();
                    }
                    else if (result != null && result != "" && result == "USER ALREADY EXIST")
                    {
                        await DisplayAlert("Oops", "This email already exist.", "OK");
                    }
                }
                catch (Exception errorFacebookAuthetication)
                {
                    Generic gen = new Generic();
                    gen.parseException(errorFacebookAuthetication.ToString());
                }
            }
        }

        private async void GoogleAuthetication(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= GoogleAuthetication;
                authenticator.Error -= Authenticator_Error;
            }

            if (e.IsAuthenticated)
            {
                try
                {
                    var clientLogIn = new SignIn();
                    var user = new Model.SignUp.SignUpPost(flow.ToUpper(), "SOCIAL");
                    var googleUser = await clientLogIn.GetGoogleUser(e);

                    user.first_name = googleUser.name;
                    user.last_name = "";
                    user.phone_number = "";
                    user.email = googleUser.email;
                    user.social_id = googleUser.id;
                    user.mobile_access_token = e.Account.Properties["access_token"];
                    user.mobile_refresh_token = e.Account.Properties["refresh_token"];
                    user.social = "GOOGLE";

                    var result = await signUpUser(user);
                    if (result == null || result == "")
                    {
                        await DisplayAlert("Oops", "Something went wrong.", "OK");
                    }
                    else if (result != null && result != "" && result != "USER ALREADY EXIST")
                    {
                        Application.Current.MainPage = new SignUpCongrats();
                    }
                    else if (result != null && result != "" && result == "USER ALREADY EXIST")
                    {
                        await DisplayAlert("Oops", "This email already exist.", "OK");
                    }

                }
                catch (Exception errorGoogleAuthetication)
                {
                    Generic gen = new Generic();
                    gen.parseException(errorGoogleAuthetication.ToString());
                }
            }
        }

        void continueWithApple(System.Object sender, System.EventArgs e)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                OnAppleSignInRequest();
            }
            else
            {
                // hide apple button?
            }
        }

        private async void Authenticator_Error(object sender, Xamarin.Auth.AuthenticatorErrorEventArgs e)
        {
            await DisplayAlert("An error occur when authenticating", "Please try again", "OK");
        }

        public async void OnAppleSignInRequest()
        {
            try
            {
                IAppleSignInService appleSignInService = DependencyService.Get<IAppleSignInService>();
                var account = await appleSignInService.SignInAsync();
                if (account != null)
                {
                    Preferences.Set(App.LoggedInKey, true);
                    await SecureStorage.SetAsync(App.AppleUserIdKey, account.UserId);

                    if (account.Token == null) { account.Token = ""; }
                    if (account.Email != null)
                    {
                        if (Application.Current.Properties.ContainsKey(account.UserId.ToString()))
                        {
                            Debug.WriteLine((string)Application.Current.Properties[account.UserId.ToString()]);
                        }
                        else
                        {
                            Application.Current.Properties[account.UserId.ToString()] = account.Email;
                        }
                    }

                    if (account.Email == null) { account.Email = ""; }
                    if (account.Name == null) { account.Name = ""; }


                    var clientLogIn = new SignIn();
                    var user = new Model.SignUp.SignUpPost(flow.ToUpper(), "SOCIAL");
                    //var content = clientSignUp.SignUpSocialUser(user, account.Token, "", account.UserId, account.Email, "APPLE");

                    user.first_name = account.Name;
                    user.last_name = "";
                    user.phone_number = "";
                    user.email = account.Email;
                    user.social_id = account.UserId;
                    user.mobile_access_token = account.Token;
                    user.mobile_refresh_token = account.Token;
                    user.social = "APPLE";

                    var result = await signUpUser(user);
                    if (result == null || result == "")
                    {
                        await DisplayAlert("Oops", "Something went wrong.", "OK");
                    }
                    else if (result != null && result != "" && result != "USER ALREADY EXIST")
                    {
                        Application.Current.MainPage = new SignUpCongrats();
                    }
                    else if (result != null && result != "" && result == "USER ALREADY EXIST")
                    {
                        await DisplayAlert("Oops", "This email already exist.", "OK");
                    }
                }
                else
                {
                   
                }
            }
            catch (Exception errorAppleSignInRequest)
            {
                Generic gen = new Generic();
                gen.parseException(errorAppleSignInRequest.ToString());
            }
        }
    }
}
