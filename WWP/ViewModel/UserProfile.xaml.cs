﻿using WWP.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Net;
using Xamarin.Forms.Maps;
using System.Diagnostics;

namespace WWP.ViewModel
{
    public partial class UserProfile : ContentPage
    {
        public ObservableCollection<Plans> customerProfileInfo = new ObservableCollection<Plans>();
        public ObservableCollection<PaymentInfo> NewPlan = new ObservableCollection<PaymentInfo>();
        PaymentInfo orderInfo;
        ArrayList itemsArray = new ArrayList();
        ArrayList purchIdArray = new ArrayList();
        ArrayList namesArray = new ArrayList();
        JObject info_obj;
        JObject info_obj2;
        string cust_firstName; string cust_lastName; string cust_email;
        public bool isAddessValidated = false;
        bool withinZones = false;
        bool socialMediaLogin = false;
        Address addr;

        public UserProfile()
        {
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
            //addr = new Address();
            InitializeComponent();
            //BindingContext = this;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            //checkPlatform(height, width);
            //getInfo();

        }

        public void checkPlatform(double height, double width)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                

            }
            else //android
            {
                
            }
        }

        //async void getInfo()
        //{
        //    try
        //    {
        //        var request = new HttpRequestMessage();
        //        Console.WriteLine("user_id: " + (string)Application.Current.Properties["user_id"]);
        //        string url = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/Profile/" + (string)Application.Current.Properties["user_id"];
        //        //string url = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/meals_selected?customer_uid=" + (string)Application.Current.Properties["user_id"];
        //        //string url = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/meals_selected?customer_uid=" + "100-000256";
        //        request.RequestUri = new Uri(url);
        //        //request.RequestUri = new Uri("https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/get_delivery_info/400-000453");
        //        request.Method = HttpMethod.Get;
        //        var client = new HttpClient();
        //        HttpResponseMessage response = await client.SendAsync(request);

        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            HttpContent content = response.Content;
        //            Console.WriteLine("content: " + content);
        //            var userString = await content.ReadAsStringAsync();
        //            info_obj = JObject.Parse(userString);
        //            this.customerProfileInfo.Clear();

        //            Console.WriteLine("user social media: " + (info_obj["result"])[0]["user_social_media"].ToString());

        //            if ((info_obj["result"])[0]["user_social_media"].ToString() != "NULL")
        //            {
        //                emailEntry.Text = (info_obj["result"])[0]["customer_email"].ToString();

        //                Console.WriteLine("social media login");
        //                if ((info_obj["result"])[0]["user_social_media"].ToString() == "GOOGLE")
        //                    platformSignedIn.Source = "profileGoogle.png";
        //                else if ((info_obj["result"])[0]["user_social_media"].ToString() == "APPLE")
        //                    platformSignedIn.Source = "profileApple.png";
        //                else platformSignedIn.Source = "profileFb.png";

        //                platformSignedIn.IsVisible = true;
        //                //passwordHeading.IsVisible = false;
        //                //divider6.IsVisible = false;
        //                passwordGrid.IsVisible = false;
        //                confirmPasswordGrid.IsVisible = false;
        //                //Email.IsVisible = false;
        //                //confirmEmail.IsVisible = false;
        //                saveChanges.IsVisible = false;
        //                socialMediaLogin = true;
        //            }
        //            else
        //            {
        //                passwordGrid.IsVisible = true;
        //                confirmPasswordGrid.IsVisible = true;
        //                platformSignedIn.IsVisible = false;
        //                saveChanges.IsVisible = true;
        //                emailEntry.Text = (info_obj["result"])[0]["customer_email"].ToString();
        //                socialMediaLogin = false;
        //            }

        //            FNameEntry.Text = (info_obj["result"])[0]["customer_first_name"].ToString();
        //            LNameEntry.Text = (info_obj["result"])[0]["customer_last_name"].ToString();
        //            emailEntry.Text = (info_obj["result"])[0]["customer_email"].ToString();
        //            //AddressEntry.Text = (info_obj["result"])[0]["customer_address"].ToString();
        //            //AptEntry.Text = (info_obj["result"])[0]["customer_unit"].ToString();

        //            //if (AptEntry.Text == "NULL")
        //            //    AptEntry.Text = "";

        //            //CityEntry.Text = (info_obj["result"])[0]["customer_city"].ToString();
        //            //StateEntry.Text = (info_obj["result"])[0]["customer_state"].ToString();
        //            //ZipEntry.Text = (info_obj["result"])[0]["customer_zip"].ToString();
        //            //PhoneEntry.Text = (info_obj["result"])[0]["customer_phone_num"].ToString();

        //            //addressList.IsVisible = false;
        //            //UnitCityState.IsVisible = true;
        //            //ZipPhone.IsVisible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Generic gen = new Generic();
        //        gen.parseException(ex.ToString());
        //    }
        //}


        async void clickedPfp(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync(false);
        }

      

        //async void clickedSavePassword(System.Object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        PasswordInfo passwordUpdate = new PasswordInfo();

        //        //customer_uid, old_password, new_password
        //        passwordUpdate.customer_uid = (info_obj["result"])[0]["customer_uid"].ToString();

        //        SHA512 sHA512 = new SHA512Managed();
        //        byte[] data = sHA512.ComputeHash(Encoding.UTF8.GetBytes(passwordEntry.Text + (info_obj["result"])[0]["password_salt"].ToString())); // take the password and account salt to generate hash
        //        string hashedPassword = BitConverter.ToString(data).Replace("-", string.Empty).ToLower(); // convert hash to hex
        //                                                                                                  //passwordUpdate.old_password = hashedPassword;

        //        if (passwordEntry.Text == null)
        //        {
        //            await DisplayAlert("Error", "Please enter your new password", "OK");
        //            return;
        //        }
        //        else if (confirmPasswordEntry.Text == null)
        //        {
        //            await DisplayAlert("Error", "Please re-enter your new password", "OK");
        //            return;
        //        }
        //        else if (passwordEntry.Text == confirmPasswordEntry.Text)
        //        {
        //            //passwordUpdate.old_password = Preferences.Get("hashed_password", "");
        //            passwordUpdate.old_password = Preferences.Get("user_password", "");
        //            //passwordUpdate.new_password = hashedPassword;
        //            passwordUpdate.new_password = passwordEntry.Text;
        //            var newPaymentJSONString = JsonConvert.SerializeObject(passwordUpdate);
        //            // Console.WriteLine("newPaymentJSONString" + newPaymentJSONString);
        //            var content2 = new StringContent(newPaymentJSONString, Encoding.UTF8, "application/json");
        //            Console.WriteLine("Content: " + content2);
        //            var client = new HttpClient();
        //            var response = client.PostAsync("https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/change_password", content2);
        //            DisplayAlert("Success", "password updated!", "close");
        //            Console.WriteLine("RESPONSE TO CHECKOUT   " + response.Result);
        //            Console.WriteLine("CHECKOUT JSON OBJECT BEING SENT: " + newPaymentJSONString);
        //            Console.WriteLine("clickedSave Func ENDED!");
        //        }
        //        else DisplayAlert("Error", "passwords don't match", "close");




        //        //await Navigation.PushAsync(new UserProfile(), false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Generic gen = new Generic();
        //        gen.parseException(ex.ToString());
        //    }
        //}


        //async void ValidateAddressClick(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        /*if (emailEntry.Text == null)
        //        {
        //            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
        //        }

        //        if (confirmEmailEntry.Text != null)
        //        {
        //            if (!emailEntry.Text.Equals(confirmEmailEntry.Text))
        //            {
        //                await DisplayAlert("Error", "Your email doesn't match", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
        //        }

        //        if (passwordEntry.Text == null)
        //        {
        //            await DisplayAlert("Error", "Please enter a password", "OK");
        //        }

        //        if (confirmPasswordEntry.Text == null)
        //        {
        //            if (!passwordEntry.Text.Equals(confirmPasswordEntry.Text))
        //            {
        //                await DisplayAlert("Error", "Your password doesn't match", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Please enter a valid password.", "OK");
        //        }

        //        if (FNameEntry.Text == null)
        //        {
        //            await DisplayAlert("Error", "Please your first name.", "OK");
        //        }

        //        if (LNameEntry.Text == null)
        //        {
        //            await DisplayAlert("Error", "Please your last name.", "OK");
        //        }*/

        //        //if (AddressEntry.Text == null)
        //        //{
        //        //    await DisplayAlert("Error", "Please enter your address", "OK");
        //        //    return;
        //        //}

        //        //if (CityEntry.Text == null)
        //        //{
        //        //    await DisplayAlert("Error", "Please enter your city", "OK");
        //        //    return;
        //        //}

        //        //if (StateEntry.Text == null)
        //        //{
        //        //    await DisplayAlert("Error", "Please enter your state", "OK");
        //        //    return;
        //        //}

        //        //if (ZipEntry.Text == null)
        //        //{
        //        //    await DisplayAlert("Error", "Please enter your zipcode", "OK");
        //        //    return;
        //        //}

        //        //if (PhoneEntry.Text == null && PhoneEntry.Text.Length == 10)
        //        //{
        //        //    await DisplayAlert("Error", "Please enter your phone number", "OK");
        //        //}

        //        //if (AptEntry.Text == null)
        //        //{
        //        //    AptEntry.Text = "";
        //        //}

        //        // Setting request for USPS API
        //        XDocument requestDoc = new XDocument(
        //            new XElement("AddressValidateRequest",
        //            new XAttribute("USERID", "400INFIN1745"),
        //            new XElement("Revision", "1"),
        //            new XElement("Address",
        //            new XAttribute("ID", "0"),
        //            new XElement("Address1", AddressEntry.Text.Trim()),
        //            new XElement("Address2", AptEntry.Text.Trim()),
        //            new XElement("City", CityEntry.Text.Trim()),
        //            new XElement("State", StateEntry.Text.Trim()),
        //            new XElement("Zip5", ZipEntry.Text.Trim()),
        //            new XElement("Zip4", "")
        //                 )
        //             )
        //         );
        //        var url = "https://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=" + requestDoc;
        //        Console.WriteLine(url);
        //        var client = new WebClient();
        //        var response = client.DownloadString(url);

        //        var xdoc = XDocument.Parse(response.ToString());
        //        Console.WriteLine("xdoc begin");
        //        Console.WriteLine(xdoc);

        //        //int startIndex = xdoc.ToString().IndexOf("<Address2>") + 10;
        //        //int length = xdoc.ToString().IndexOf("</Address2>") - startIndex;

        //        //string xdocAddress = xdoc.ToString().Substring(startIndex, length);
        //        //Console.WriteLine("xdoc address: " + xdoc.ToString().Substring(startIndex, length));
        //        //Console.WriteLine("xdoc end");

        //        //if (xdocAddress != AddressEntry.Text.ToUpper().Trim())
        //        //{
        //        //    //DisplayAlert("heading", "changing address", "ok");
        //        //    AddressEntry.Text = xdocAddress;
        //        //}

        //        //startIndex = xdoc.ToString().IndexOf("<State>") + 7;
        //        //length = xdoc.ToString().IndexOf("</State>") - startIndex;
        //        //string xdocState = xdoc.ToString().Substring(startIndex, length);

        //        //if (xdocAddress != StateEntry.Text.ToUpper().Trim())
        //        //{
        //        //    //DisplayAlert("heading", "changing address", "ok");
        //        //    StateEntry.Text = xdocState;
        //        //}


        //        string latitude = "0";
        //        string longitude = "0";
        //        foreach (XElement element in xdoc.Descendants("Address"))
        //        {
        //            if (GetXMLElement(element, "Error").Equals(""))
        //            {
        //                if (GetXMLElement(element, "DPVConfirmation").Equals("Y") && GetXMLElement(element, "Zip5").Equals(ZipEntry.Text.Trim()) && GetXMLElement(element, "City").Equals(CityEntry.Text.ToUpper().Trim())) // Best case
        //                {
        //                    // Get longitude and latitide because we can make a deliver here. Move on to next page.
        //                    // Console.WriteLine("The address you entered is valid and deliverable by USPS. We are going to get its latitude & longitude");
        //                    //GetAddressLatitudeLongitude();
        //                    Geocoder geoCoder = new Geocoder();

        //                    IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(AddressEntry.Text.Trim() + "," + CityEntry.Text.Trim() + "," + StateEntry.Text.Trim());
        //                    Position position = approximateLocations.FirstOrDefault();

        //                    latitude = $"{position.Latitude}";
        //                    longitude = $"{position.Longitude}";

        //                    map.MapType = MapType.Street;
        //                    var mapSpan = new MapSpan(position, 0.001, 0.001);

        //                    Pin address = new Pin();
        //                    address.Label = "Delivery Address";
        //                    address.Type = PinType.SearchResult;
        //                    address.Position = position;

        //                    map.MoveToRegion(mapSpan);
        //                    map.Pins.Add(address);

        //                    //https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/categoricalOptions/-121.8866517,37.2270928 long,lat
        //                    //var request2 = new HttpRequestMessage();
        //                    //Console.WriteLine("user_id: " + (string)Application.Current.Properties["user_id"]);
        //                    //Debug.WriteLine("latitude: " + latitude + ", longitude: " + longitude);
        //                    //string url2 = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/categoricalOptions/" + longitude + "," + latitude;
        //                    //request2.RequestUri = new Uri(url2);
        //                    //request2.Method = HttpMethod.Get;
        //                    //var client2 = new HttpClient();
        //                    //HttpResponseMessage response2 = await client2.SendAsync(request2);

        //                    //if (response2.StatusCode == System.Net.HttpStatusCode.OK)
        //                    //{
        //                    //    HttpContent content2 = response2.Content;
        //                    //    Console.WriteLine("content: " + content2);
        //                    //    var userString2 = await content2.ReadAsStringAsync();
        //                    //    Debug.WriteLine("userString2: " + userString2);
        //                    //    info_obj2 = JObject.Parse(userString2);
        //                    //    if (info_obj2["result"].ToString() == "[]")
        //                    //    {
        //                    //        withinZones = false;
        //                    //    }
        //                    //    else withinZones = true;
        //                    //}
        //                    string url3 = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/categoricalOptions/" + longitude + "," + latitude;
        //                    Debug.WriteLine("categorical options url: " + url3);
        //                    var client4 = new WebClient();
        //                    var content = client4.DownloadString(url3);
        //                    var obj = JsonConvert.DeserializeObject<ZonesDto>(content);

        //                    if (obj.Result.Length == 0)
        //                    {
        //                        withinZones = false;
        //                        //await DisplayAlert("Invalid address", "The address you entered is not in any of our delivery zones", "OK");
        //                        break;
        //                    }
        //                    else
        //                    {

        //                        Debug.WriteLine("first business: " + obj.Result[0].business_name);

        //                        withinZones = true;
        //                    }

        //                    break;
        //                }
        //                else if (GetXMLElement(element, "DPVConfirmation").Equals("D"))
        //                {
        //                    //await DisplayAlert("Alert!", "Address is missing information like 'Apartment number'.", "Ok");
        //                    //return;
        //                }
        //                else
        //                {
        //                    //await DisplayAlert("Alert!", "Seems like your address is invalid.", "Ok");
        //                    //return;
        //                }
        //            }
        //            else
        //            {   // USPS sents an error saying address not found in there records. In other words, this address is not valid because it does not exits.
        //                //Console.WriteLine("Seems like your address is invalid.");
        //                //await DisplayAlert("Alert!", "Error from USPS. The address you entered was not found.", "Ok");
        //                //return;
        //            }
        //        }
        //        if (latitude == "0" || longitude == "0")
        //        {
        //            await DisplayAlert("We couldn't find your address", "Please check for errors.", "Ok");
        //        }
        //        else if (withinZones == false)
        //        {
        //            await DisplayAlert("Invalid address", "The address you entered is not in any of our delivery zones", "OK");
        //        }
        //        //else if (withinZones == false)
        //        //{
        //        //    await DisplayAlert("Invalid Address", "Address is not within any of our delivery zones.", "OK");
        //        //}
        //        else
        //        {
        //            int startIndex = xdoc.ToString().IndexOf("<Address2>") + 10;
        //            int length = xdoc.ToString().IndexOf("</Address2>") - startIndex;

        //            string xdocAddress = xdoc.ToString().Substring(startIndex, length);
        //            //Console.WriteLine("xdoc address: " + xdoc.ToString().Substring(startIndex, length));
        //            //Console.WriteLine("xdoc end");

        //            if (xdocAddress != AddressEntry.Text.ToUpper().Trim())
        //            {
        //                //DisplayAlert("heading", "changing address", "ok");
        //                AddressEntry.Text = xdocAddress;
        //            }

        //            startIndex = xdoc.ToString().IndexOf("<State>") + 7;
        //            length = xdoc.ToString().IndexOf("</State>") - startIndex;
        //            string xdocState = xdoc.ToString().Substring(startIndex, length);

        //            if (xdocAddress != StateEntry.Text.ToUpper().Trim())
        //            {
        //                //DisplayAlert("heading", "changing address", "ok");
        //                StateEntry.Text = xdocState;
        //            }

        //            isAddessValidated = true;
        //            await DisplayAlert("We validated your address", "Please click on the Sign up button to create your account!", "OK");
        //            await Application.Current.SavePropertiesAsync();
        //            //await tagUser(emailEntry.Text.Trim(), ZipEntry.Text.Trim());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Generic gen = new Generic();
        //        gen.parseException(ex.ToString());
        //    }
        //}

        public static string GetXMLElement(XElement element, string name)
        {
            var el = element.Element(name);
            if (el != null)
            {
                return el.Value;
            }
            return "";
        }

        async Task tagUser(string email, string zipCode)
        {
            try
            {
                var guid = Preferences.Get("guid", null);
                if (guid == null)
                {
                    return;
                }
                var tags = "email_" + email + "," + "zip_" + zipCode;

                MultipartFormDataContent updateRegistrationInfoContent = new MultipartFormDataContent();
                StringContent guidContent = new StringContent(guid, Encoding.UTF8);
                StringContent tagsContent = new StringContent(tags, Encoding.UTF8);
                updateRegistrationInfoContent.Add(guidContent, "guid");
                updateRegistrationInfoContent.Add(tagsContent, "tags");

                var updateRegistrationRequest = new HttpRequestMessage();
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        updateRegistrationRequest.RequestUri = new Uri("https://phaqvwjbw6.execute-api.us-west-1.amazonaws.com/dev/api/v1/update_registration_guid_iOS");
                        //updateRegistrationRequest.RequestUri = new Uri("http://10.0.2.2:5000/api/v1/update_registration_guid_iOS");
                        break;
                    case Device.Android:
                        updateRegistrationRequest.RequestUri = new Uri("https://phaqvwjbw6.execute-api.us-west-1.amazonaws.com/dev/api/v1/update_registration_guid_android");
                        //updateRegistrationRequest.RequestUri = new Uri("http://10.0.2.2:5000/api/v1/update_registration_guid_android");
                        break;
                }
                updateRegistrationRequest.Method = HttpMethod.Post;
                updateRegistrationRequest.Content = updateRegistrationInfoContent;
                var updateRegistrationClient = new HttpClient();
                HttpResponseMessage updateRegistrationResponse = await updateRegistrationClient.SendAsync(updateRegistrationRequest);
            }
            catch (Exception ex)
            {
                Generic gen = new Generic();
                gen.parseException(ex.ToString());
            }
        }

        // Auto-complete
        private ObservableCollection<AddressAutocomplete> _addresses;
        public ObservableCollection<AddressAutocomplete> Addresses
        {
            get => _addresses ?? (_addresses = new ObservableCollection<AddressAutocomplete>());
            set
            {
                if (_addresses != value)
                {
                    _addresses = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _addressText;
        public string AddressText
        {
            get => _addressText;
            set
            {
                if (_addressText != value)
                {
                    _addressText = value;
                    OnPropertyChanged();
                }
            }
        }

        //private async void OnAddressChanged(object sender, EventArgs eventArgs)
        //{
        //    addressList.IsVisible = true;
        //    UnitCityState.IsVisible = false;
        //    ZipPhone.IsVisible = false;
        //    addressList.ItemsSource = await addr.GetPlacesPredictionsAsync(AddressEntry.Text);
        //    //addr.OnAddressChanged(addressList, Addresses, _addressText);
        //}

        private void addressEntryFocused(object sender, EventArgs eventArgs)
        {
            //addr.addressEntryFocused(addressList, new Grid[] { UnitCityState, ZipPhone });
        }

        //private void addressEntryUnfocused(object sender, EventArgs eventArgs)
        //{
        //    addr.addressEntryUnfocused(addressList, new Grid[] { UnitCityState, ZipPhone });
        //}

        //async void addressSelected(System.Object sender, System.EventArgs e)
        //{
        //    addr.addressSelected(addressList, new Grid[] { UnitCityState, ZipPhone }, AddressEntry, CityEntry, StateEntry, ZipEntry);
        //    addressList.IsVisible = false;
        //    UnitCityState.IsVisible = true;
        //    ZipPhone.IsVisible = true;
        //}

        //start of menu functions
        

        //async void clickedLanding(System.Object sender, System.EventArgs e)
        //{
        //    await Navigation.PushAsync(new MainPage(cust_firstName, cust_lastName, cust_email), false);
        //    //Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        //}

        async void clickedMealPlan(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MealPlans(cust_firstName, cust_lastName, cust_email), false);
            //Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }

        

        async void clickedSubscription(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SubscriptionPage(cust_firstName, cust_lastName, cust_email), false);
            //Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }

        async void clickedSubHistory(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SubscriptionHistory(cust_firstName, cust_lastName, cust_email), false);
            //Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }


        void clickedLogout(System.Object sender, System.EventArgs e)
        {
            Application.Current.Properties.Remove("user_id");
            Application.Current.Properties["platform"] = "GUEST";
            Application.Current.Properties.Remove("time_stamp");
            //Application.Current.Properties.Remove("platform");
            Application.Current.MainPage = new MainPage();
        }
        //end of menu functions

        //menu functions
        void profileClicked(System.Object sender, System.EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new UserProfile());
            Navigation.PushAsync(new UserProfile());
        }

        void menuClicked(System.Object sender, System.EventArgs e)
        {
            openMenuGrid.IsVisible = true;
            //whiteCover.IsVisible = true;
            menu.IsVisible = false;
        }

        void openedMenuClicked(System.Object sender, System.EventArgs e)
        {
            openMenuGrid.IsVisible = false;
            //whiteCover.IsVisible = false;
            menu.IsVisible = true;
        }

        void browseClicked(System.Object sender, System.EventArgs e)
        {
            //Application.Current.MainPage = new FoodBanksMap();
            Navigation.PushAsync(new FoodBanksMap());
        }

        void logoutClicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }

        //end of menu functions
    }
}
