using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Experimentation.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class SchedulePage : ContentPage
    {
        public ObservableCollection<Schedule> scheduleSource = new ObservableCollection<Schedule>();

        public ObservableCollection<PickerTimeHour> hourSourceStart = new ObservableCollection<PickerTimeHour>();
        public ObservableCollection<PickerTimeHour> hourSourceEnd = new ObservableCollection<PickerTimeHour>();
        public ObservableCollection<PickerTimeMinute> minuteSource = new ObservableCollection<PickerTimeMinute>();
        public ObservableCollection<PickerTime> timeSourceStart = new ObservableCollection<PickerTime>();
        public ObservableCollection<PickerTime> timeSourceEnd = new ObservableCollection<PickerTime>();
        public Dictionary<string, List<List<Time>>> selectedSchedule = new Dictionary<string, List<List<Time>>>();
        public Dictionary<string, List<string[]>> timesRecorded = new Dictionary<string, List<string[]>>();

        public DateTime today = DateTime.Now;

        public string dayToAddScheduleTime = "";

        public SchedulePage()
        {
            InitializeComponent();

            SetSchedule(scheduleView);
            SetDictionary();

        }


        void SetSchedule(CollectionView view)
        {
            SetHoursStart();

            SetTimeStart();

            SetMinutes();

            SetHoursEnd();

            SetTimeEnd();

            string[] weekdays = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            foreach (string day in weekdays)
            {
                var position = new DayRow { key = day, row = 0 };
                var positionString = JsonConvert.SerializeObject(position);
                Debug.WriteLine("POSITION: " + positionString);
                scheduleSource.Add(new Schedule
                {
                    colorValue = Color.Black,
                    isEnabledValue = false,
                    opacityValue = 0.5,
                    day = day,
                    row = positionString,
                    startHour = hourSourceStart,
                    startMinute = minuteSource,
                    startTime = timeSourceStart,
                    endHour = hourSourceEnd,
                    endMinute = minuteSource,
                    endTime = timeSourceEnd,
                });
            }
            view.ItemsSource = scheduleSource;
            view.HeightRequest = scheduleSource.Count * 55;

        }

        void SetDictionary()
        {
            string[] weekdays = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            foreach (string day in weekdays)
            {
                selectedSchedule.Add(day, new List<List<Time>>());
            }

        }

        void SetHoursStart()
        {
            try
            {
                hourSourceStart.Add(new PickerTimeHour { hour = "08" });
                for (int i = 1; i <= 12; i++)
                {
                    string value = "";
                    if (i <= 9)
                    {
                        value = "0" + i;
                    }
                    else
                    {
                        value = i + "";
                    }
                    hourSourceStart.Add(new PickerTimeHour { hour = value });
                }

            }
            catch (Exception setHourIssue)
            {
                Debug.WriteLine("Error: " + setHourIssue.Message);
            }

        }

        void SetHoursEnd()
        {
            try
            {
                hourSourceEnd.Add(new PickerTimeHour { hour = "05" });
                for (int i = 1; i <= 12; i++)
                {
                    string value = "";
                    if (i <= 9)
                    {
                        value = "0" + i;
                    }
                    else
                    {
                        value = i + "";
                    }
                    hourSourceEnd.Add(new PickerTimeHour { hour = value });
                }

            }
            catch (Exception setHourIssue)
            {
                Debug.WriteLine("Error: " + setHourIssue.Message);
            }

        }

        void SetMinutes()
        {
            try
            {
                minuteSource.Add(new PickerTimeMinute { minute = "00" });
                minuteSource.Add(new PickerTimeMinute { minute = "15" });
                minuteSource.Add(new PickerTimeMinute { minute = "30" });
                minuteSource.Add(new PickerTimeMinute { minute = "45" });

            }
            catch (Exception setHourIssue)
            {
                Debug.WriteLine("Error: " + setHourIssue.Message);
            }

        }

        void SetTimeStart()
        {
            try
            {
                timeSourceStart.Add(new PickerTime { time = "AM" });
                timeSourceStart.Add(new PickerTime { time = "PM" });

            }
            catch (Exception setHourIssue)
            {
                Debug.WriteLine("Error: " + setHourIssue.Message);
            }
        }

        void SetTimeEnd()
        {
            try
            {
                timeSourceEnd.Add(new PickerTime { time = "PM" });
                timeSourceEnd.Add(new PickerTime { time = "AM" });

            }
            catch (Exception setHourIssue)
            {
                Debug.WriteLine("Error: " + setHourIssue.Message);
            }
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }


        void hourListStart_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            try
            {
                var list = (CollectionView)sender;
                list.ScrollTo(hourSourceStart[e.CenterItemIndex]);
                //Debug.WriteLine("CLASS ID: " + list.ClassId);

                var position = JsonConvert.DeserializeObject<DayRow>(list.ClassId);

                selectedSchedule[position.key][position.row][0].hour = hourSourceStart[e.CenterItemIndex].hour;
            }
            catch
            {

            }

        }

        void minuteListStart_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            try
            {
                var list = (CollectionView)sender;
                list.ScrollTo(minuteSource[e.CenterItemIndex]);
                //Debug.WriteLine("CLASS ID: " + list.ClassId);

                var position = JsonConvert.DeserializeObject<DayRow>(list.ClassId);

                selectedSchedule[position.key][position.row][0].minute = minuteSource[e.CenterItemIndex].minute;
            }
            catch { }
        }

        void timeListStart_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            try
            {
                var list = (CollectionView)sender;
                list.ScrollTo(timeSourceStart[e.CenterItemIndex]);
                //Debug.WriteLine("CLASS ID: " + list.ClassId);

                var position = JsonConvert.DeserializeObject<DayRow>(list.ClassId);

                selectedSchedule[position.key][position.row][0].time = timeSourceStart[e.CenterItemIndex].time;

            }
            catch { }
        }

        void hourListEnd_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            try
            {
                var list = (CollectionView)sender;
                list.ScrollTo(hourSourceEnd[e.CenterItemIndex]);
                //Debug.WriteLine("CLASS ID: " + list.ClassId);

                var position = JsonConvert.DeserializeObject<DayRow>(list.ClassId);

                selectedSchedule[position.key][position.row][1].hour = hourSourceEnd[e.CenterItemIndex].hour;

            }
            catch { }
        }

        void minuteListEnd_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            try
            {
                var list = (CollectionView)sender;
                list.ScrollTo(minuteSource[e.CenterItemIndex]);
                //Debug.WriteLine("CLASS ID: " + list.ClassId);

                var position = JsonConvert.DeserializeObject<DayRow>(list.ClassId);

                selectedSchedule[position.key][position.row][1].minute = minuteSource[e.CenterItemIndex].minute;
            }
            catch { }
        }

        void timeListEnd_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            try
            {
                var list = (CollectionView)sender;
                list.ScrollTo(timeSourceEnd[e.CenterItemIndex]);
                //Debug.WriteLine("CLASS ID: " + list.ClassId);

                var position = JsonConvert.DeserializeObject<DayRow>(list.ClassId);

                selectedSchedule[position.key][position.row][1].time = timeSourceEnd[e.CenterItemIndex].time;
            }
            catch { }
        }

        async Task<bool> ProcessRequest()
        {
            //var client = new HttpClient();
            //var content = new MultipartFormDataContent();
            //var scheduleToSubmit = new ScheduleToSubmit();

            //foreach (string day in selectedSchedule.Keys)
            //{
            //    if (selectedSchedule[day].Count != 0)
            //    {
            //        //DateTime today = DateTime.Now;
            //        string todayString = today.ToString("yyyy-MM-dd");

            //        foreach (List<Time> interval in selectedSchedule[day])
            //        {
            //            string startTime = todayString;
            //            string endTime = todayString;

            //            startTime += " " + interval[0].hour + ":" + interval[0].minute + " " + interval[0].time;
            //            endTime += " " + interval[1].hour + ":" + interval[1].minute + " " + interval[1].time;

            //            Debug.WriteLine("START:" + startTime);
            //            Debug.WriteLine("END: " + endTime);

            //            string mStartTime = DateTime.Parse(startTime).ToString("HH:mm:ss");
            //            string mEndTime = DateTime.Parse(endTime).ToString("HH:mm:ss");

            //            var array = new List<string>();
            //            array.Add(mStartTime);
            //            array.Add(mEndTime);
            //            var list = new List<string[]>();
            //            list.Add(array.ToArray());
            //            if (timesRecorded.ContainsKey(day))
            //            {
            //                timesRecorded[day].Add(array.ToArray());
            //            }
            //            else
            //            {
            //                timesRecorded.Add(day, list);
            //            }

            //        }
            //    }
            //    else
            //    {
            //        timesRecorded.Add(day, new List<string[]>());
            //    }
            //}

            //scheduleToSubmit.sunday = timesRecorded["Sunday"];
            //scheduleToSubmit.monday = timesRecorded["Monday"];
            //scheduleToSubmit.tuesday = timesRecorded["Tuesday"];
            //scheduleToSubmit.wednesday = timesRecorded["Wednesday"];
            //scheduleToSubmit.thursday = timesRecorded["Thursday"];
            //scheduleToSubmit.friday = timesRecorded["Friday"];
            //scheduleToSubmit.saturday = timesRecorded["Saturday"];

            //var scheduleToSubmitString = JsonConvert.SerializeObject(scheduleToSubmit);
            //Debug.WriteLine("TIMES: " + scheduleToSubmitString);
            //var businessIDs = "";
            //foreach (string id in businessSelected)
            //{
            //    businessIDs += id + ",";
            //}

            //if (businessIDs != "")
            //{
            //    businessIDs = businessIDs.Remove(businessIDs.Length - 1);
            //}

            //var account = JsonConvert.DeserializeObject<SignUp>(accountString);

            //var first_name = new StringContent(account.first_name, Encoding.UTF8);
            //var last_name = new StringContent(account.last_name, Encoding.UTF8);
            //var business_uid = new StringContent(businessIDs, Encoding.UTF8);
            //var referral_source = new StringContent(account.referral_source, Encoding.UTF8);
            //var driver_hours = new StringContent(scheduleToSubmitString, Encoding.UTF8);
            //var street = new StringContent(account.street, Encoding.UTF8);
            //var unit = new StringContent(account.unit, Encoding.UTF8);
            //var city = new StringContent(account.city, Encoding.UTF8);
            //var state = new StringContent(account.state, Encoding.UTF8);
            //var zipcode = new StringContent(account.zipcode, Encoding.UTF8);
            //var latitude = new StringContent(account.latitude, Encoding.UTF8);
            //var longitude = new StringContent(account.longitude, Encoding.UTF8);
            //var email = new StringContent(account.email, Encoding.UTF8);
            //var phone = new StringContent(account.phone, Encoding.UTF8);
            //var ssn = new StringContent(account.ssn, Encoding.UTF8);
            //var license_num = new StringContent(account.license_num, Encoding.UTF8);
            //var license_exp = new StringContent(account.license_exp, Encoding.UTF8);
            //var driver_car_year = new StringContent(account.driver_car_year, Encoding.UTF8);
            //var driver_car_model = new StringContent(account.driver_car_model, Encoding.UTF8);
            //var driver_car_make = new StringContent(account.driver_car_make, Encoding.UTF8);
            //var driver_insurance_carrier = new StringContent(account.driver_insurance_carrier, Encoding.UTF8);
            //var driver_insurance_num = new StringContent(account.driver_insurance_num, Encoding.UTF8);
            //var driver_insurance_exp_date = new StringContent(account.driver_insurance_exp_date, Encoding.UTF8);
            //var contact_name = new StringContent(account.contact_name, Encoding.UTF8);
            //var contact_phone = new StringContent(account.contact_phone, Encoding.UTF8);
            //var contact_relation = new StringContent(account.contact_relation, Encoding.UTF8);
            //var bank_acc_info = new StringContent(account.bank_acc_info, Encoding.UTF8);
            //var bank_routing_info = new StringContent(account.bank_routing_info, Encoding.UTF8);
            //var password = new StringContent(account.password, Encoding.UTF8);
            //var social = new StringContent(account.social, Encoding.UTF8);
            //var mobile_access_token = new StringContent(account.mobile_access_token, Encoding.UTF8);
            //var mobile_refresh_token = new StringContent(account.mobile_refresh_token, Encoding.UTF8);
            //var user_access_token = new StringContent(account.user_access_token, Encoding.UTF8);
            //var user_refresh_token = new StringContent(account.user_refresh_token, Encoding.UTF8);
            //var social_id = new StringContent(account.social_id, Encoding.UTF8);
            //var userImageContent = new ByteArrayContent(insurancePicture);


            //// CONTENT, NAME
            //content.Add(first_name, "first_name");
            //content.Add(last_name, "last_name");
            //content.Add(business_uid, "business_uid");
            //content.Add(referral_source, "referral_source");
            //content.Add(driver_hours, "driver_hours");
            //content.Add(street, "street");
            //content.Add(unit, "unit");
            //content.Add(city, "city");
            //content.Add(state, "state");
            //content.Add(zipcode, "zipcode");
            //content.Add(latitude, "latitude");
            //content.Add(longitude, "longitude");
            //content.Add(email, "email");
            //content.Add(phone, "phone");
            //content.Add(ssn, "ssn");
            //content.Add(license_num, "license_num");
            //content.Add(license_exp, "license_exp");
            //content.Add(driver_car_year, "driver_car_year");
            //content.Add(driver_car_model, "driver_car_model");
            //content.Add(driver_car_make, "driver_car_make");
            //content.Add(driver_insurance_carrier, "driver_insurance_carrier");
            //content.Add(driver_insurance_num, "driver_insurance_num");
            //content.Add(driver_insurance_exp_date, "driver_insurance_exp_date");
            //content.Add(contact_name, "contact_name");
            //content.Add(contact_phone, "contact_phone");
            //content.Add(contact_relation, "contact_relation");
            //content.Add(bank_acc_info, "bank_acc_info");
            //content.Add(bank_routing_info, "bank_routing_info");
            //content.Add(password, "password");
            //content.Add(social, "social");
            //content.Add(mobile_access_token, "mobile_access_token");
            //content.Add(mobile_refresh_token, "mobile_refresh_token");
            //content.Add(user_access_token, "user_access_token");
            //content.Add(user_refresh_token, "user_refresh_token");
            //content.Add(social_id, "social_id");

            //// CONTENT, NAME, FILENAME
            //content.Add(userImageContent, "driver_insurance_picture", "product_image.png");

            //var request = new HttpRequestMessage();

            //request.RequestUri = new Uri(Constant.SignUpUrl);
            //request.Method = HttpMethod.Post;
            //request.Content = content;

            //var response = await client.SendAsync(request);
            //if (response.IsSuccessStatusCode)
            //{
            //    var contentString = await response.Content.ReadAsStringAsync();
            //    Debug.WriteLine("contentString: " + contentString);
            //    return true;
            //}
            return true;
        }

        async void SubmitApplication(System.Object sender, System.EventArgs e)
        {
            //UserDialogs.Instance.ShowLoading("We are processing your request and checking that all schedule entries are valid...");
            var timesAreInvalid = false;
            foreach (string day in selectedSchedule.Keys)
            {
                if (!timesValidation(selectedSchedule[day]))
                {
                    timesAreInvalid = true;
                    break;
                }
            }



            if (!timesAreInvalid)
            {

                if (isScheduleEmpty())
                {
                    //UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Oops", "Your schedule is empty. We can't process your request. Please add a valid entry to your schedule.", "OK");
                }
                else
                {
                    var result = await ProcessRequest();
                    if (result)
                    {
                        //UserDialogs.Instance.HideLoading();
                        //await DisplayAlert("Congratulations!", "Your application is in process. We will notify you of your result via email.", "OK");
                        //Application.Current.MainPage = new LogInPage();
                        await Navigation.PushAsync(new WalkieProfile());
                    }
                    else
                    {
                        //UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Oops", "Unfortunately, we weren't able to sign you up. Please try again later.", "OK");
                    }
                }
            }
            else
            {
                //UserDialogs.Instance.HideLoading();
                await DisplayAlert("Oops", "We see that one or more times in your schedule are not valid entries. Please make sure no time in your schedule overlaps.", "OK");
            }

        }

        bool isScheduleEmpty()
        {
            // string[] weekdays = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            var result = false;
            if (
                   selectedSchedule["Sunday"].Count == 0
                && selectedSchedule["Monday"].Count == 0
                && selectedSchedule["Tuesday"].Count == 0
                && selectedSchedule["Wednesday"].Count == 0
                && selectedSchedule["Thursday"].Count == 0
                && selectedSchedule["Friday"].Count == 0
                && selectedSchedule["Saturday"].Count == 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        async void SelectDay(System.Object sender, System.EventArgs e)
        {
            var frame = (Frame)sender;
            var gesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            var selected = (Schedule)gesture.CommandParameter;

            if (selected.colorValue == Color.Black)
            {
                selected.updateColorValue = Color.FromHex("#F87F1B");
                selected.updateIsEnabledValue = true;
                selected.updateOpacityValue = 1;

                var times = new List<Time>();
                var startTime = new Time { hour = selected.startHour[0].hour, minute = selected.startMinute[0].minute, time = selected.startTime[0].time };
                var endTime = new Time { hour = selected.endHour[0].hour, minute = selected.endMinute[0].minute, time = selected.endTime[0].time };

                times.Add(startTime);
                times.Add(endTime);
                selectedSchedule[selected.day].Add(times);
                if (!timesValidation(selectedSchedule[selected.day]))
                {
                    await DisplayAlert("Oops", "We see that one or more times in your schedule are not valid entries. Please make sure no time in your schedule overlaps.", "OK");
                }
            }
            else
            {
                selected.updateColorValue = Color.Black;
                selected.updateIsEnabledValue = false;
                selected.updateOpacityValue = 0.5;
                var position = JsonConvert.DeserializeObject<DayRow>(selected.row);
                if (position.row < selectedSchedule[selected.day].Count)
                {
                    selectedSchedule[selected.day].RemoveAt(position.row);
                }
            }

            var zeroDatesSelected = true;

            foreach(Schedule element in scheduleSource)
            {
                if(element.colorValue == Color.FromHex("#F87F1B"))
                {
                    zeroDatesSelected = false;
                    break;
                }
            }

            if (zeroDatesSelected)
            {
                addMoreTimesAndNextButtonSection.IsVisible = false;
                zeroDates.IsVisible = true;
            }
            else
            {
                addMoreTimesAndNextButtonSection.IsVisible = true;
                zeroDates.IsVisible = false;
            }
        }

        // We are procressing your request and checking that all times in your schedule are valid...
        // Oops we see that one or more times in your schedule are not valid entries. Please make sure no time in your schedule overlops. 

        List<DateTime> CovertTimeToDateTime(string sTime, string eTime)
        {
            var list = new List<DateTime>();

            string todayString = today.ToString("yyyy-MM-dd");
            string startTime = todayString;
            string endTime = todayString;

            startTime += sTime;
            endTime += eTime;

            Debug.WriteLine("START:" + startTime);
            Debug.WriteLine("END: " + endTime);

            var mStartTime = DateTime.Parse(startTime);
            var mEndTime = DateTime.Parse(endTime);

            list.Add(mStartTime);
            list.Add(mEndTime);

            return list;
        }

        bool timesValidation(List<List<Time>> collection)
        {
            var result = false;
            var listDateTimes = new List<List<DateTime>>();
            // Step 1. Covert Time struct to DateTime for comparisons
            foreach (List<Time> interval in collection)
            {
                var sTime = " " + interval[0].hour + ":" + interval[0].minute + " " + interval[0].time;
                var eTime = " " + interval[1].hour + ":" + interval[1].minute + " " + interval[1].time;
                var dateTimeInterval = CovertTimeToDateTime(sTime, eTime);
                listDateTimes.Add(dateTimeInterval);
            }

            if (listDateTimes.Count == 0)
            {
                result = true;
            }
            else
            {
                for (int i = 0; i < listDateTimes.Count; i++)
                {
                    for (int j = 0; j < listDateTimes.Count; j++)
                    {
                        if (i != j)
                        {
                            var oldStart = listDateTimes[i][0];
                            var oldEnd = listDateTimes[i][1];
                            var newStart = listDateTimes[j][0];
                            var newEnd = listDateTimes[j][1];

                            if (
                                   oldStart != newStart
                                && !(oldStart < newStart && newStart < oldEnd)
                                && oldEnd != newEnd
                                && !(oldStart < newEnd && newEnd < oldEnd)
                                && newEnd > newStart)
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                                break;
                            }
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        void GetDay(System.Object sender, System.EventArgs e)
        {
            var stack = (StackLayout)sender;
            var gesture = (TapGestureRecognizer)stack.GestureRecognizers[0];
            var selected = (Schedule)gesture.CommandParameter;

            dayToAddScheduleTime = selected.day;
        }

        async void AddAnoherTime(System.Object sender, System.EventArgs e)
        {
            var day = await DisplayActionSheet("Select one of the following days when you would like to add another available time.", "Cancel", null, new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            if (day != null && day != "" && day != "Cancel")
            {
                //UserDialogs.Instance.ShowLoading("Updating your schedule...");
                var position = new DayRow { key = day, row = 0 };
                var positionString = JsonConvert.SerializeObject(position);
                Debug.WriteLine("POSITION: " + positionString);
                scheduleSource.Add(new Schedule
                {
                    colorValue = Color.Black,
                    isEnabledValue = false,
                    opacityValue = 0.5,
                    day = day,
                    row = positionString,
                    startHour = hourSourceStart,
                    startMinute = minuteSource,
                    startTime = timeSourceStart,
                    endHour = hourSourceEnd,
                    endMinute = minuteSource,
                    endTime = timeSourceEnd,
                });

                var tempSource = new ObservableCollection<Schedule>();

                foreach (Schedule s in scheduleSource)
                {
                    Debug.WriteLine("DAY: " + s.day);
                }

                string[] weekdays = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

                foreach (string dayToSort in weekdays)
                {
                    var rowIndex = 0;
                    foreach (Schedule schedule in scheduleSource)
                    {
                        if (schedule.day == dayToSort)
                        {
                            var p = new DayRow { key = dayToSort, row = rowIndex };
                            var pString = JsonConvert.SerializeObject(p);
                            schedule.row = pString;
                            tempSource.Add(schedule);
                            rowIndex++;
                        }
                    }
                }

                scheduleSource.Clear();

                foreach (Schedule schedule in tempSource)
                {
                    scheduleSource.Add(schedule);
                }

                scheduleView.ItemsSource = scheduleSource;
                scheduleView.HeightRequest = scheduleSource.Count * 55;
                if (!timesValidation(selectedSchedule[day]))
                {
                    //Oops, we see that one or more times in your schedule are not valid entries. Please make sure no time in your schedule overlaps.
                    await DisplayAlert("Oops", "We see that one or more times in your schedule are not valid entries. Please make sure no time in your schedule overlaps.", "OK");
                }
                //UserDialogs.Instance.HideLoading();

            }
        }

        void NavigateBack(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
