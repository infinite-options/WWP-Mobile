using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WWP.ViewModel
{
    public partial class WalkerTimer : ContentPage
    {
        public List<Pin> pinList = new List<Pin>();
        public int num = 1;
        public WalkerTimer()
        {
            InitializeComponent();
            SetWalkieName(walkie, "David");
            GetLastKnownLocation();
            StartTimer();
        }

        void SetWalkieName(Label label, string name)
        {
            label.Text = "Walk with " + name;
        }

        async void GetLastKnownLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var currentLocation = new Pin();

                    currentLocation.Address = "1658 Sacramento St, San Francisco, CA, 94109";
                    currentLocation.Label = "Current Location";
                    currentLocation.Position = new Position(location.Latitude, location.Longitude);

                    map.Pins.Add(currentLocation);

                    pinList.Add(currentLocation);

                    var Span = new MapSpan(currentLocation.Position, 0.001, 0.001);
                    map.MoveToRegion(Span);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        //identifier: com.ct.Experimentation

        bool timerOn = true;

        void StartTimer()
        {
            var date = new DateTime(new TimeSpan(0, 45, 0).Ticks);
            //var previousMinute = 19;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {

                minutes.Text = date.Minute.ToString();

                if (date.Second == 0)
                {
                    second.Text = "00";
                }
                else
                {
                    second.Text = date.Second.ToString();
                }

                date = date.AddSeconds(-1);

                //if(previousMinute == date.Minute)
                //{
                //    Debug.WriteLine("RECORD LOCATION");
                //    previousMinute--;
                //}

                if (date.Second % 10 == 0)
                {
                    Debug.WriteLine("RECORD LOCATION");
                    //GetLastKnownLocationAfterTimeBegins();
                }

                var result = timerOn;



                return result;
            });
        }

        void NavigateToWellnessReportPage(System.Object sender, System.EventArgs e)
        {
            timerOn = false;
            Navigation.PushAsync(new WellnessReportPage(), false);
        }

        async void GetLastKnownLocationAfterTimeBegins()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var currentLocation = new Pin();

                    currentLocation.Address = "RECORD SCREENSHOT NUMBER: " + num;
                    currentLocation.Label = "JJ";
                    currentLocation.Position = new Position(location.Latitude, location.Longitude);

                    num++;

                    //pinList.Add(currentLocation);
                    map.Pins.Add(currentLocation);
                    //map.Pins.Clear();

                    foreach (Pin pin in pinList)
                    {
                        map.Pins.Add(pin);
                    }

                    map.MoveToRegion(new MapSpan(currentLocation.Position, 0.001, 0.001));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        void NavigateToSchedule(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WalkerProfilePage(), false);
        }

        void NavigateBackToWalkSummary(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }

        void MakeCall(System.Object sender, System.EventArgs e)
        {
            try
            {
                var phoneNumber = "4158329643";

                PhoneDialer.Open(phoneNumber);
            }
            catch
            {

            }
        }

        async void TakePicture(System.Object sender, System.EventArgs e)
        {
            try
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { SaveToAlbum = true, Name = "Photo1.png" });
                //if (true)
                if (photo != null)
                {
                    //Get the public album path

                    var aPpath = photo.AlbumPath;
                    //p = aPpath;
                    Debug.WriteLine("PATH: " + aPpath);
                    Debug.WriteLine("PHOTO: " + photo.Path);
                    //var message = new SmsMessage("Hello JD", new[] { "4158329643" });
                    //await Sms.ComposeAsync(message);

                    //var smsMessanger = CrossMessaging.Current.SmsMessenger;
                    //if (smsMessanger.CanSendSms)
                    //{
                    //    smsMessanger.SendSms("14158329643", "Welcome to Xamarin.Forms");
                    //}

                    //DependencyService.Get<INativeMessage>().OpenUrl("sms://open?addresses=4084760001,4158329643&body=Hello%20Prashant,%20This%20is%20Just%20Delivered.%20We%20just%20delivered%20your%20package%21");

                    //DependencyService.Get<INativeMessage>().OpenUrl("f");

                    //SendSMS(new[] { "14158329643"}, "Hello everyone! This is JD in development mode");
                    //Application.Current.MainPage = new DeliveriesPage(CurrentIndex);

                    //var path = photo.Path;
                    //photoStream = photo.GetStream();

                    //var ar = File.ReadAllBytes(path);
                    //t = ar;
                    //isPictureAvailable = true;
                    //f.Source = ImageSource.FromStream(() => { return photo.GetStream(); });


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //await DisplayAlert("Permission required", "We'll need permission to access your camara, so that you can take a photo of the delivered product", "OK");
                return;
            }
        }
    }
}
