using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WWP.Model.Login.Constants;
using WWP.Model.Login.LoginClasses;
using WWP.Model.Login.LoginClasses.Apple;
using Xamarin.Auth;
using Xamarin.Forms;

namespace WWP.Model
{
    public class SignIn
    {
        private string deviceId = "";
        private string accessToken;
        private string refreshToken;
        private string platform;
        private AuthenticatorCompletedEventArgs googleAccount;

        public SignIn()
        {
            googleAccount = null;
            platform = "";
            accessToken = "";
            refreshToken = "";
        }

        public OAuth2Authenticator GetFacebookAuthetication()
        {
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
            return authenticator;
        }

        public OAuth2Authenticator GetGoogleAuthetication()
        {
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
            return authenticator;
        }

        public FacebookResponse GetFacebookUser(string accessToken)
        {

            FacebookResponse facebookData = null;

            var client = new HttpClient();
            var facebookResponse = client.GetStringAsync(Constant.FacebookUserInfoUrl + accessToken);
            var facebookUserData = facebookResponse.Result;

            facebookData = JsonConvert.DeserializeObject<FacebookResponse>(facebookUserData);
            return facebookData;
        }

        public async Task<GoogleResponse> GetGoogleUser(AuthenticatorCompletedEventArgs googleAccount)
        {
            GoogleResponse googleDate = null;
            var request = new OAuth2Request("GET", new Uri(Constant.GoogleUserInfoUrl), null, googleAccount.Account);
            var GoogleResponse = await request.GetResponseAsync();
            var googelUserData = GoogleResponse.GetResponseText();

            googleDate = JsonConvert.DeserializeObject<GoogleResponse>(googelUserData);
            return googleDate;
        }

        public async Task<string> VerifyUserCredentials(string accessToken = "", string refreshToken = "", AuthenticatorCompletedEventArgs googleAccount = null, AppleAccount appleCredentials = null, string platform = "")
        {
            var isUserVerified = "";
            try
            {
                //var progress = UserDialogs.Instance.Loading("Loading...");
                var client = new HttpClient();
                var socialLogInPost = new SocialLogInPost();

                var googleData = new GoogleResponse();
                var facebookData = new FacebookResponse();

                if (platform == "GOOGLE")
                {
                    var request = new OAuth2Request("GET", new Uri(Constant.GoogleUserInfoUrl), null, googleAccount.Account);
                    var GoogleResponse = await request.GetResponseAsync();
                    var googelUserData = GoogleResponse.GetResponseText();

                    googleData = JsonConvert.DeserializeObject<GoogleResponse>(googelUserData);

                    socialLogInPost.email = googleData.email;
                    socialLogInPost.social_id = googleData.id;
                    Debug.WriteLine("IMAGE: " + googleData.picture);
                    //user.setUserImage(googleData.picture);
                }
                else if (platform == "FACEBOOK")
                {
                    var facebookResponse = client.GetStringAsync(Constant.FacebookUserInfoUrl + accessToken);
                    var facebookUserData = facebookResponse.Result;

                    Debug.WriteLine("FACEBOOK DATA: " + facebookUserData);
                    facebookData = JsonConvert.DeserializeObject<FacebookResponse>(facebookUserData);

                    socialLogInPost.email = facebookData.email;
                    socialLogInPost.social_id = facebookData.id;
                }
                else if (platform == "APPLE")
                {
                    socialLogInPost.email = appleCredentials.Email;
                    socialLogInPost.social_id = appleCredentials.UserId;
                }

                socialLogInPost.password = "";
                socialLogInPost.signup_platform = platform;

                var socialLogInPostSerialized = JsonConvert.SerializeObject(socialLogInPost);
                var postContent = new StringContent(socialLogInPostSerialized, Encoding.UTF8, "application/json");

                //var test = UserDialogs.Instance.Loading("Loading...");
                var RDSResponse = await client.PostAsync(Constant.loginEndpoint, postContent);
                var responseContent = await RDSResponse.Content.ReadAsStringAsync();
                var authetication = JsonConvert.DeserializeObject<SuccessfulSocialLogIn>(responseContent);
                if (RDSResponse.IsSuccessStatusCode)
                {
                    if (responseContent != null)
                    {
                        if (authetication.code.ToString() == Constant.EmailNotFound)
                        {
                            //test.Hide();
                            isUserVerified = "USER NEEDS TO SIGN UP";
                            //if (platform == "GOOGLE")
                            //{
                            //    Application.Current.MainPage = new SocialSignUp(googleData.id, googleData.given_name, googleData.family_name, googleData.email, accessToken, refreshToken, "GOOGLE");
                            //}
                            //else if (platform == "FACEBOOK")
                            //{
                            //    Application.Current.MainPage = new SocialSignUp(facebookData.id, facebookData.name, "", facebookData.email, accessToken, accessToken, "FACEBOOK");
                            //}
                            //else if (platform == "APPLE")
                            //{
                            //    Application.Current.MainPage = new SocialSignUp(appleCredentials.UserId, appleCredentials.Name, "", appleCredentials.Email, appleCredentials.Token, appleCredentials.Token, "APPLE");
                            //}
                        }
                        if (authetication.code.ToString() == Constant.AutheticatedSuccesful)
                        {
                            try
                            {
                                isUserVerified = "LOGIN USER";
                                //var data = JsonConvert.DeserializeObject<SuccessfulSocialLogIn>(responseContent);
                                //user.setUserID(data.result[0].customer_uid);

                                //UpdateTokensPost updateTokesPost = new UpdateTokensPost();
                                //updateTokesPost.uid = data.result[0].customer_uid;
                                //if (platform == "GOOGLE")
                                //{
                                //    updateTokesPost.mobile_access_token = accessToken;
                                //    updateTokesPost.mobile_refresh_token = refreshToken;
                                //}
                                //else if (platform == "FACEBOOK")
                                //{
                                //    updateTokesPost.mobile_access_token = accessToken;
                                //    updateTokesPost.mobile_refresh_token = accessToken;
                                //}
                                //else if (platform == "APPLE")
                                //{
                                //    updateTokesPost.mobile_access_token = appleCredentials.Token;
                                //    updateTokesPost.mobile_refresh_token = appleCredentials.Token;
                                //}

                                //var updateTokesPostSerializedObject = JsonConvert.SerializeObject(updateTokesPost);
                                //var updateTokesContent = new StringContent(updateTokesPostSerializedObject, Encoding.UTF8, "application/json");
                                //var updateTokesResponse = await client.PostAsync(Constant.UpdateTokensUrl, updateTokesContent);
                                //var updateTokenResponseContent = await updateTokesResponse.Content.ReadAsStringAsync();

                                //if (updateTokesResponse.IsSuccessStatusCode)
                                //{
                                //    var user1 = new RequestUserInfo();
                                //    user1.uid = data.result[0].customer_uid;

                                //    var requestSelializedObject = JsonConvert.SerializeObject(user1);
                                //    var requestContent = new StringContent(requestSelializedObject, Encoding.UTF8, "application/json");

                                //    var clientRequest = await client.PostAsync(Constant.GetUserInfoUrl, requestContent);

                                //    if (clientRequest.IsSuccessStatusCode)
                                //    {
                                //        var userSfJSON = await clientRequest.Content.ReadAsStringAsync();
                                //        var userProfile = JsonConvert.DeserializeObject<UserInfo>(userSfJSON);

                                //        DateTime today = DateTime.Now;
                                //        DateTime expDate = today.AddDays(Constant.days);

                                //        user.setUserID(data.result[0].customer_uid);
                                //        user.setUserSessionTime(expDate);
                                //        user.setUserPlatform(platform);
                                //        user.setUserType("CUSTOMER");
                                //        user.setUserEmail(userProfile.result[0].customer_email);
                                //        user.setUserFirstName(userProfile.result[0].customer_first_name);
                                //        user.setUserLastName(userProfile.result[0].customer_last_name);
                                //        user.setUserPhoneNumber(userProfile.result[0].customer_phone_num);
                                //        user.setUserAddress(userProfile.result[0].customer_address);
                                //        user.setUserUnit(userProfile.result[0].customer_unit);
                                //        user.setUserCity(userProfile.result[0].customer_city);
                                //        user.setUserState(userProfile.result[0].customer_state);
                                //        user.setUserZipcode(userProfile.result[0].customer_zip);
                                //        user.setUserLatitude(userProfile.result[0].customer_lat);
                                //        user.setUserLongitude(userProfile.result[0].customer_long);

                                //        SaveUser(user);

                                //        if (data.result[0].role == "GUEST")
                                //        {
                                //            var clientSignUp = new SignUp();
                                //            var content = clientSignUp.UpdateSocialUser(user, userProfile.result[0].mobile_access_token, userProfile.result[0].mobile_refresh_token, userProfile.result[0].social_id, platform);
                                //            var signUpStatus = await SignUp.SignUpNewUser(content);
                                //        }

                                //        isUserVerified = "LOGIN USER";

                                //        //SetMenu(guestMenuSection, customerMenuSection, historyLabel, profileLabel);
                                //        //GetBusinesses();
                                //        if (Device.RuntimePlatform == Device.iOS)
                                //        {
                                //            deviceId = Preferences.Get("guid", null);
                                //            if (deviceId != null) { Debug.WriteLine("This is the iOS GUID from Log in: " + deviceId); }
                                //        }
                                //        else
                                //        {
                                //            deviceId = Preferences.Get("guid", null);
                                //            if (deviceId != null) { Debug.WriteLine("This is the Android GUID from Log in " + deviceId); }
                                //        }

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
                                //                Debug.WriteLine("We have post the guid to the database");
                                //            }
                                //            else
                                //            {
                                //                //await DisplayAlert("Ooops!", "Something went wrong. We are not able to send you notification at this moment", "OK");
                                //            }
                                //        }
                                //        //test.Hide();
                                //        //Application.Current.MainPage = new SelectionPage();
                                //    }
                                //    else
                                //    {
                                //        isUserVerified = "ERROR1";
                                //        //test.Hide();
                                //        //await DisplayAlert("Alert!", "Our internal system was not able to retrieve your user information. We are working to solve this issue.", "OK");
                                //    }
                                //}
                                //else
                                //{
                                //    isUserVerified = "ERROR2";
                                //    //test.Hide();
                                //    //await DisplayAlert("Oops", "We are facing some problems with our internal system. We weren't able to update your credentials", "OK");
                                //}
                                //test.Hide();
                            }
                            catch (Exception second)
                            {
                                Debug.WriteLine(second.Message);
                            }
                        }
                        if (authetication.code.ToString() == Constant.ErrorPlatform)
                        {
                            var RDSCode = JsonConvert.DeserializeObject<RDSLogInMessage>(responseContent);
                            //if(RDSCode.message != null && RDSCode.message == "")
                            //{
                            //    Debug.WriteLine("DATA FROM LOGIN WHEN USING WRONG PLATFORM: " + responseContent);
                            //    isUserVerified = "PLEASE SIGN IN THROUGH";
                            //}
                            //else
                            //{
                            //    isUserVerified = "SIGN IN USING SOCIAL MEDIA";
                            //}

                            isUserVerified = "WRONG SOCIAL MEDIA TO SIGN IN";

                            //test.Hide();
                            //Application.Current.MainPage = new LogInPage();
                        }

                        if (authetication.code.ToString() == Constant.ErrorUserDirectLogIn)
                        {
                            isUserVerified = "SIGN IN DIRECTLY";
                            //test.Hide();
                            //Application.Current.MainPage = new LogInPage();
                        }
                    }
                }
                //test.Hide();
                return isUserVerified;
            }
            catch (Exception errorVerifyUserCredentials)
            {

                Generic gen = new Generic();
                gen.parseException(errorVerifyUserCredentials.ToString());
                isUserVerified = "ERROR";
                return isUserVerified;
            }
        }
    }
}
