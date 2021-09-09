using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;

using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using WWP.Model.Login;
using System.Threading.Tasks;
using WWP.Model.Database;
using System.Windows.Input;
using Xamarin.Auth;
using System.Diagnostics;
using System.Collections;
using WWP.Model.User;
using System.IO;

using System.Collections.Generic;
using System.ComponentModel;
using WWP.Model.Login.LoginClasses.Apple;
using WWP.Model.Login.LoginClasses;
using WWP.Model.Login.Constants;
using WWP.LogInClasses;
using Newtonsoft.Json.Linq;
using WWP.Model;
using System.Collections.ObjectModel;
using WWP.Interfaces;
using Acr.UserDialogs;

namespace WWP.ViewModel
{
    public partial class MainLogin : ContentPage
    {
        public string deviceId = "";
        public string direction = "";

        public MainLogin()
        {
            try
            {
                InitializeComponent();
                enableAppleLoginButton();
            }
            catch (Exception issueMainPageConstructor)
            {
                Generic gen = new Generic();
                gen.parseException(issueMainPageConstructor.ToString());
            }
        }

        void enableAppleLoginButton()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                appleLogInButton.IsEnabled = false;
            }
        }

        void setAppVersionBuild()
        {
            string version = "";
            string build = "";

            version = DependencyService.Get<IAppVersionAndBuild>().GetVersionNumber();
            build = DependencyService.Get<IAppVersionAndBuild>().GetBuildNumber();
        }

        private void checkPlatform(double height, double width)
        {
            if (width == 1125 && height == 2436) //iPhone X only
            {
               
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {

            }
            else //android
            {
                userFrame.HeightRequest = 42;
                passFrame.HeightRequest = 42;
            }
        }

        private async void directLogInClick(System.Object sender, System.EventArgs e)
        {
            try
            {
                loginButton.IsEnabled = false;
                if (String.IsNullOrEmpty(loginUsername.Text) || String.IsNullOrEmpty(loginPassword.Text))
                {
                    //if (messageList != null)
                    //{
                    //    if (messageList.ContainsKey("701-000032"))
                    //    {
                    //        await DisplayAlert(messageList["701-000032"].title, messageList["701-000032"].message, messageList["701-000032"].responses);
                    //    }
                    //    else
                    //    {
                    //        await DisplayAlert("Error", "Please fill in all fields", "OK");
                    //    }
                    //}
                    //else
                    //{
                    //    await DisplayAlert("Error", "Please fill in all fields", "OK");
                    //}
                    await DisplayAlert("Error", "Please fill in all fields", "OK");
                    loginButton.IsEnabled = true;
                }
                else
                {
                    var accountSalt = await retrieveAccountSalt(loginUsername.Text.ToLower().Trim());

                    if (accountSalt != null)
                    {
                        var loginAttempt = await logInUser(loginUsername.Text.ToLower().Trim(), loginPassword.Text, accountSalt);

                        if (loginAttempt != null && loginAttempt.message != "Request failed, wrong password.")
                        {
                            Debug.WriteLine("YOU HAVE SUCCESFULLY LOGGED IN");
                            Application.Current.MainPage = new NavigationPage(new WalkSchedule());

                            //var client = new HttpClient();
                            //var request = new RequestUserInfo();
                            //request.uid = loginAttempt.result[0].customer_uid;

                            //var requestSelializedObject = JsonConvert.SerializeObject(request);
                            //var requestContent = new StringContent(requestSelializedObject, Encoding.UTF8, "application/json");

                            //var clientRequest = await client.PostAsync(Constant.GetUserInfoUrl, requestContent);

                            //if (clientRequest.IsSuccessStatusCode)
                            //{
                            //    try
                            //    {
                            //        var SFUser = await clientRequest.Content.ReadAsStringAsync();
                            //        var userData = JsonConvert.DeserializeObject<UserInfo>(SFUser);

                            //        DateTime today = DateTime.Now;
                            //        DateTime expDate = today.AddDays(Constant.days);

                            //        user.setUserID(userData.result[0].customer_uid);
                            //        user.setUserSessionTime(expDate);
                            //        user.setUserPlatform("DIRECT");
                            //        user.setUserType("CUSTOMER");
                            //        user.setUserEmail(userData.result[0].customer_email);
                            //        user.setUserFirstName(userData.result[0].customer_first_name);
                            //        user.setUserLastName(userData.result[0].customer_last_name);
                            //        user.setUserPhoneNumber(userData.result[0].customer_phone_num);
                            //        user.setUserAddress(userData.result[0].customer_address);
                            //        user.setUserUnit(userData.result[0].customer_unit);
                            //        user.setUserCity(userData.result[0].customer_city);
                            //        user.setUserState(userData.result[0].customer_state);
                            //        user.setUserZipcode(userData.result[0].customer_zip);
                            //        user.setUserLatitude(userData.result[0].customer_lat);
                            //        user.setUserLongitude(userData.result[0].customer_long);

                            //        SaveUser(user);

                            //        deviceId = Preferences.Get("guid", null);

                            //        if (deviceId != null)
                            //        {
                            //            NotificationPost notificationPost = new NotificationPost();

                            //            notificationPost.uid = user.getUserID();
                            //            notificationPost.guid = deviceId.Substring(5);
                            //            user.setUserDeviceID(deviceId.Substring(5));
                            //            notificationPost.notification = "TRUE";

                            //            var notificationSerializedObject = JsonConvert.SerializeObject(notificationPost);
                            //            Debug.WriteLine("Notification JSON Object to send: " + notificationSerializedObject);

                            //            var notificationContent = new StringContent(notificationSerializedObject, Encoding.UTF8, "application/json");

                            //            var clientResponse = await client.PostAsync(Constant.NotificationsUrl, notificationContent);

                            //            Debug.WriteLine("Status code: " + clientResponse.IsSuccessStatusCode);

                            //            if (clientResponse.IsSuccessStatusCode)
                            //            {
                            //                System.Diagnostics.Debug.WriteLine("We have post the guid to the database");
                            //            }
                            //            else
                            //            {
                            //                if (messageList != null)
                            //                {
                            //                    if (messageList.ContainsKey("701-000033"))
                            //                    {
                            //                        await DisplayAlert(messageList["701-000033"].title, messageList["701-000033"].message, messageList["701-000033"].responses);
                            //                    }
                            //                    else
                            //                    {
                            //                        await DisplayAlert("Ooops!", "Something went wrong. We are not able to send you notification at this moment", "OK");
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    await DisplayAlert("Ooops!", "Something went wrong. We are not able to send you notification at this moment", "OK");
                            //                }
                            //            }
                            //        }

                            //        //Application.Current.MainPage = new NavigationPage(new WalkSchedule());

                            //    }
                            //    catch (Exception issueParsingUserData)
                            //    {
                            //        Generic gen = new Generic();
                            //        gen.parseException(issueParsingUserData.ToString());
                            //    }
                            //}
                            //else
                            //{
                            //    if (messageList != null)
                            //    {
                            //        if (messageList.ContainsKey("701-000034"))
                            //        {
                            //            await DisplayAlert(messageList["701-000034"].title, messageList["701-000034"].message, messageList["701-000034"].responses);
                            //        }
                            //        else
                            //        {
                            //            await DisplayAlert("Alert!", "Our internal system was not able to retrieve your user information. We are working to solve this issue.", "OK");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        await DisplayAlert("Alert!", "Our internal system was not able to retrieve your user information. We are working to solve this issue.", "OK");
                            //    }
                            //}
                        }
                        else
                        {

                            //if (messageList != null)
                            //{
                            //    if (messageList.ContainsKey("701-000035"))
                            //    {
                            //        await DisplayAlert(messageList["701-000035"].title, messageList["701-000035"].message, messageList["701-000035"].responses);
                            //    }
                            //    else
                            //    {
                            //        await DisplayAlert("Error", "Wrong password was entered", "OK");
                            //    }
                            //}
                            //else
                            //{
                            //    await DisplayAlert("Error", "Wrong password was entered", "OK");
                            //}
                            await DisplayAlert("Error", "Wrong password was entered", "OK");
                            loginButton.IsEnabled = true;
                        }
                    }
                    loginButton.IsEnabled = true;
                }
            }
            catch (Exception errorDirectLogin)
            {
                Generic gen = new Generic();
                gen.parseException(errorDirectLogin.ToString());
            }
        }

        private async Task<AccountSalt> retrieveAccountSalt(string userEmail)
        {
            try
            {
                Debug.WriteLine(userEmail);

                SaltPost saltPost = new SaltPost();
                saltPost.email = userEmail;

                var saltPostSerilizedObject = JsonConvert.SerializeObject(saltPost);
                var saltPostContent = new StringContent(saltPostSerilizedObject, Encoding.UTF8, "application/json");

                Debug.WriteLine(saltPostSerilizedObject);

                var client = new HttpClient();
                var DRSResponse = await client.PostAsync(Constant.accountSaltEndpoint, saltPostContent);
                var DRSMessage = await DRSResponse.Content.ReadAsStringAsync();
                Debug.WriteLine(DRSMessage);

                AccountSalt userInformation = null;

                if (DRSResponse.IsSuccessStatusCode)
                {
                    var result = await DRSResponse.Content.ReadAsStringAsync();

                    AcountSaltCredentials data = new AcountSaltCredentials();
                    data = JsonConvert.DeserializeObject<AcountSaltCredentials>(result);

                    if (data.code.ToString() == Constant.UseSocialMediaLogin)
                    {
                        await DisplayAlert("Oops!", data.message, "OK");
                    }
                    else if (data.code.ToString() == Constant.EmailNotFound)
                    {

                        //if (messageList != null)
                        //{
                        //    if (messageList.ContainsKey("701-000036"))
                        //    {
                        //        await DisplayAlert(messageList["701-000036"].title, messageList["701-000036"].message, messageList["701-000036"].responses);
                        //    }
                        //    else
                        //    {
                        //        await DisplayAlert("Oops!", "Our records show that you don't have an accout. Please sign up!", "OK");
                        //    }
                        //}
                        //else
                        //{
                        //    await DisplayAlert("Oops!", "Our records show that you don't have an accout. Please sign up!", "OK");
                        //}
                        await DisplayAlert("Oops!", "Our records show that you don't have an accout. Please sign up!", "OK");
                    }
                    else
                    {
                        userInformation = new AccountSalt
                        {
                            user_password_algorithm = data.result[0].user_password_algorithm,
                            user_password_salt = data.result[0].user_password_salt
                        };
                    }
                }

                return userInformation;
            }
            catch (Exception issueRetrieveAccountSalt)
            {
                Generic gen = new Generic();
                gen.parseException(issueRetrieveAccountSalt.ToString());
                return null;
            }
        }

        private async Task<LogInResponse> logInUser(string userEmail, string userPassword, AccountSalt accountSalt)
        {
            try
            {
                SHA512 sHA512 = new SHA512Managed();
                var client = new HttpClient();
                byte[] data = sHA512.ComputeHash(Encoding.UTF8.GetBytes(userPassword + accountSalt.user_password_salt));
                string hashedPassword = BitConverter.ToString(data).Replace("-", string.Empty).ToLower();

                LogInPost loginPostContent = new LogInPost();
                loginPostContent.email = userEmail;
                loginPostContent.password = hashedPassword;
                loginPostContent.social_id = "NULL";
                loginPostContent.signup_platform = "NULL";

                string loginPostContentJson = JsonConvert.SerializeObject(loginPostContent);
                Debug.WriteLine(loginPostContentJson);
                var httpContent = new StringContent(loginPostContentJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Constant.loginEndpoint, httpContent);
                var message = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(message);

                if (message.Contains(Constant.AutheticatedSuccesful))
                {

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LogInResponse>(responseContent);
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception issueLoginUser)
            {
                Generic gen = new Generic();
                gen.parseException(issueLoginUser.ToString());
                return null;
            }
        }

        public void FacebookLogInClick(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
            string clientID = string.Empty;
            string redirectURL = string.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientID = Constant.FacebookiOSClientID;
                    redirectURL = Constant.FacebookiOSRedirectUrl;
                    break;
                case Device.Android:
                    clientID = Constant.FacebookAndroidClientID;
                    redirectURL = Constant.FacebookAndroidRedirectUrl;
                    break;
            }

            var authenticator = new OAuth2Authenticator(clientID, Constant.FacebookScope, new Uri(Constant.FacebookAuthorizeUrl), new Uri(redirectURL), null, false);
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();

            authenticator.Completed += FacebookAuthenticatorCompleted;
            authenticator.Error += FacebookAutheticatorError;

            presenter.Login(authenticator);
        }

        public async void FacebookAuthenticatorCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= FacebookAuthenticatorCompleted;
                authenticator.Error -= FacebookAutheticatorError;
            }

            if (e.IsAuthenticated)
            {
                try
                {
                    // Application.Current.MainPage = new SelectionPage(e.Account.Properties["access_token"], "", null, null, "FACEBOOK");

                    var client = new SignIn();
                    UserDialogs.Instance.ShowLoading("Retrieving your WWP account...");
                    var status = await client.VerifyUserCredentials(e.Account.Properties["access_token"], "", null, null, "FACEBOOK");
                    RedirectUserBasedOnVerification(status, direction);
                }
                catch (Exception errorFacebookAuthenticatorCompleted)
                {
                    Generic gen = new Generic();
                    gen.parseException(errorFacebookAuthenticatorCompleted.ToString());
                }
            }
            else
            {
                // Application.Current.MainPage = new LogInPage();
                //await DisplayAlert("Error", "Facebook was not able to autheticate your account", "OK");
            }
        }


        public async void AppleLogIn(string accessToken = "", string refreshToken = "", AuthenticatorCompletedEventArgs googleAccount = null, AppleAccount appleCredentials = null, string platform = "")
        {
            var client = new SignIn();
            UserDialogs.Instance.ShowLoading("Retrieving your WWP account...");
            var status = await client.VerifyUserCredentials(accessToken, refreshToken, googleAccount, appleCredentials, platform);
            RedirectUserBasedOnVerification(status, direction);
        }

        public async void RedirectUserBasedOnVerification(string status, string direction)
        {
            try
            {
                if (status == "LOGIN USER")
                {
                    UserDialogs.Instance.HideLoading();
                    //await Application.Current.MainPage.DisplayAlert("Great!", "You have successfully loged in!", "OK");

                    Application.Current.MainPage = new NavigationPage(new WalkSchedule());

                }
                else if (status == "USER NEEDS TO SIGN UP")
                {
                    UserDialogs.Instance.HideLoading();
                    //if (messageList != null)
                    //{
                    //    if (messageList.ContainsKey("701-000037"))
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert(messageList["701-000037"].title, messageList["701-000037"].message, messageList["701-000037"].responses);
                    //    }
                    //    else
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("Oops", "It looks like you don't have an account with Serving Fresh. Please sign up!", "OK");
                    //    }
                    //}
                    //else
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("Oops", "It looks like you don't have an account with Serving Fresh. Please sign up!", "OK");
                    //}
                    await Application.Current.MainPage.DisplayAlert("Oops", "It looks like you don't have an account with WWP. Please sign up!", "OK");

                    await Navigation.PushAsync(new SignUpOptions());
                }
                else if (status == "WRONG SOCIAL MEDIA TO SIGN IN")
                {
                    UserDialogs.Instance.HideLoading();
                    //if (messageList != null)
                    //{
                    //    if (messageList.ContainsKey("701-000038"))
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert(messageList["701-000038"].title, messageList["701-000038"].message, messageList["701-000038"].responses);
                    //    }
                    //    else
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("Oops", "Our records show that you have attempted to log in with a different social media account. Please log in through the correct social media platform. Thanks!", "OK");
                    //    }
                    //}
                    //else
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("Oops", "Our records show that you have attempted to log in with a different social media account. Please log in through the correct social media platform. Thanks!", "OK");
                    //}
                    await Application.Current.MainPage.DisplayAlert("Oops", "Our records show that you have attempted to log in with a different social media account. Please log in through the correct social media platform. Thanks!", "OK");
                }
                else if (status == "SIGN IN DIRECTLY")
                {
                    UserDialogs.Instance.HideLoading();
                    //if (messageList != null)
                    //{
                    //    if (messageList.ContainsKey("701-000039"))
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert(messageList["701-000039"].title, messageList["701-000039"].message, messageList["701-000039"].responses);
                    //    }
                    //    else
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("Oops", "Our records show that you have attempted to log in with a social media account. Please log in through our direct log in. Thanks!", "OK");
                    //    }
                    //}
                    //else
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("Oops", "Our records show that you have attempted to log in with a social media account. Please log in through our direct log in. Thanks!", "OK");
                    //}
                    await Application.Current.MainPage.DisplayAlert("Oops", "Our records show that you have attempted to log in with a social media account. Please log in through our direct log in. Thanks!", "OK");
                }
                else if (status == "ERROR1")
                {
                    UserDialogs.Instance.HideLoading();
                    //if (messageList != null)
                    //{
                    //    if (messageList.ContainsKey("701-000040"))
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert(messageList["701-000040"].title, messageList["701-000040"].message, messageList["701-000040"].responses);
                    //    }
                    //    else
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                    //    }
                    //}
                    //else
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                    //}
                    await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                }
                else if (status == "ERROR2")
                {
                    UserDialogs.Instance.HideLoading();
                    //if (messageList != null)
                    //{
                    //    if (messageList.ContainsKey("701-000040"))
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert(messageList["701-000040"].title, messageList["701-000040"].message, messageList["701-000040"].responses);
                    //    }
                    //    else
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                    //    }
                    //}
                    //else
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                    //}
                    await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    //if (messageList != null)
                    //{
                    //    if (messageList.ContainsKey("701-000040"))
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert(messageList["701-000040"].title, messageList["701-000040"].message, messageList["701-000040"].responses);
                    //    }
                    //    else
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                    //    }
                    //}
                    //else
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                    //}
                    await Application.Current.MainPage.DisplayAlert("Oops", "There was an error getting your account. Please contact customer service", "OK");
                }
            }
            catch (Exception errorRedirectUserBaseOnVerification)
            {
                Generic gen = new Generic();
                gen.parseException(errorRedirectUserBaseOnVerification.ToString());
            }
        }


        private async void FacebookAutheticatorError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= FacebookAuthenticatorCompleted;
                authenticator.Error -= FacebookAutheticatorError;
            }
            Application.Current.MainPage = new MainLogin();
            await DisplayAlert("Authentication error: ", e.Message, "OK");
        }

        public void GoogleLogInClick(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
            string clientId = string.Empty;
            string redirectUri = string.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constant.GoogleiOSClientID;
                    redirectUri = Constant.GoogleRedirectUrliOS;
                    break;

                case Device.Android:
                    clientId = Constant.GoogleAndroidClientID;
                    redirectUri = Constant.GoogleRedirectUrlAndroid;
                    break;
            }

            var authenticator = new OAuth2Authenticator(clientId, string.Empty, Constant.GoogleScope, new Uri(Constant.GoogleAuthorizeUrl), new Uri(redirectUri), new Uri(Constant.GoogleAccessTokenUrl), null, true);
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();

            authenticator.Completed += GoogleAuthenticatorCompleted;
            authenticator.Error += GoogleAuthenticatorError;

            AuthenticationState.Authenticator = authenticator;
            presenter.Login(authenticator);

        }
        private async void GoogleAuthenticatorCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {

            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= GoogleAuthenticatorCompleted;
                authenticator.Error -= GoogleAuthenticatorError;
            }

            if (e.IsAuthenticated)
            {
                try
                {
                    var client = new SignIn();
                    UserDialogs.Instance.ShowLoading("Retrieving your WWP account...");
                    var status = await client.VerifyUserCredentials(e.Account.Properties["access_token"], e.Account.Properties["refresh_token"], e, null, "GOOGLE");
                    RedirectUserBasedOnVerification(status, direction);
                }
                catch (Exception errorGoogleAutheticatorCompleted)
                {
                    Generic gen = new Generic();
                    gen.parseException(errorGoogleAutheticatorCompleted.ToString());
                }

            }
            else
            {
                //Application.Current.MainPage = new LogInPage();
                //await DisplayAlert("Error", "Google was not able to autheticate your account", "OK");
            }
        }


        private async void GoogleAuthenticatorError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= GoogleAuthenticatorCompleted;
                authenticator.Error -= GoogleAuthenticatorError;
            }
            Application.Current.MainPage = new MainLogin();
            await DisplayAlert("Authentication error: ", e.Message, "OK");
        }

        public void AppleLogInClick(System.Object sender, System.EventArgs e)
        {
            if (Device.RuntimePlatform != Device.Android)
            {
                OnAppleSignInRequest();
            }
        }

        private async void AppleError(object sender, EventArgs e)
        {
            //if (messageList != null)
            //{
            //    if (messageList.ContainsKey("701-000041"))
            //    {
            //        await DisplayAlert(messageList["701-000041"].title, messageList["701-000041"].message, messageList["701-000041"].responses);
            //    }
            //    else
            //    {
            //        await DisplayAlert("Error", "We weren't able to set an account for you", "OK");
            //    }
            //}
            //else
            //{
            //    await DisplayAlert("Error", "We weren't able to set an account for you", "OK");
            //}

            await DisplayAlert("Error", "We weren't able to set an account for you", "OK");
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
                            //Application.Current.Properties[account.UserId.ToString()] = account.Email;
                            Debug.WriteLine((string)Application.Current.Properties[account.UserId.ToString()]);
                        }
                        else
                        {
                            Application.Current.Properties[account.UserId.ToString()] = account.Email;
                        }
                    }
                    if (account.Email == null) { account.Email = ""; }
                    if (account.Name == null) { account.Name = ""; }

                    if (Application.Current.Properties.ContainsKey(account.UserId.ToString()))
                    {
                        account.Email = (string)Application.Current.Properties[account.UserId.ToString()];
                        //Application.Current.MainPage = new SelectionPage("", "", null, account, "APPLE");
                        //var root = (LogInPage)Application.Current.MainPage;
                        //root.AppleLogIn("", "", null, account, "APPLE");

                        var client = new SignIn();
                        UserDialogs.Instance.ShowLoading("Retrieving your WWP account...");
                        var status = await client.VerifyUserCredentials("", "", null, account, "APPLE");
                        RedirectUserBasedOnVerification(status, direction);
                        //AppleUserProfileAsync(account.UserId, account.Token, (string)Application.Current.Properties[account.UserId.ToString()], account.Name);
                    }
                }
                else
                {
                    //AppleError?.Invoke(this, default(EventArgs));

                }
            }
            catch (Exception errorAppleSignInRequest)
            {
                Generic gen = new Generic();
                gen.parseException(errorAppleSignInRequest.ToString());
            }
        }

        void signUpClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignUpOptions();
        }

        async void clickedForgotPass(System.Object sender, System.EventArgs e)
        {
            //reset password (get): https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/reset_password?email=welks@gmail.com
            //change password (post): https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/change_password
            try
            {
                if (loginUsername.Text == "" || loginUsername.Text == null)
                {
                    DisplayAlert("Error", "please enter your email into the username box first", "OK");
                    return;
                }
                //if endpoint returns email not found, display an alert
                else
                {
                    string url = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/reset_password?email=" + loginUsername.Text;
                    var request = new HttpRequestMessage();
                    request.RequestUri = new Uri(url);
                    request.Method = HttpMethod.Get;
                    var client = new HttpClient();
                    HttpResponseMessage response = await client.SendAsync(request);
                    HttpContent content = response.Content;
                    Console.WriteLine("content: " + content.ToString());
                    var userString = await content.ReadAsStringAsync();
                    Debug.WriteLine("userString: " + userString);
                    JObject info_obj = JObject.Parse(userString);
                    Debug.WriteLine("info_obj");
                }

                //for testing 
                Application.Current.MainPage = new changePassword(loginUsername.Text);
            }
            catch (Exception ex)
            {
                Generic gen = new Generic();
                gen.parseException(ex.ToString());
            }
        }

        void clickedSeePassword(System.Object sender, System.EventArgs e)
        {
            if (loginPassword.IsPassword == true)
                loginPassword.IsPassword = false;
            else loginPassword.IsPassword = true;
        }

        void clickedFb(System.Object sender, System.EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.facebook.com/Meals-For-Me-101737768566584"));
        }

        void clickedIns(System.Object sender, System.EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.instagram.com/mealsfor.me/"));
        }
    }
}


