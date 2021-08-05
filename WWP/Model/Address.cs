﻿using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using WWP.Constants;
using System.ComponentModel;
using Xamarin.Forms;
using System.Diagnostics;

namespace WWP.Model
{
    public class Address
    {
        public const string GooglePlacesApiAutoCompletePath = "https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}&input={1}&components=country:us";
        public const string GooglePlacesApiDetailsPath = "https://maps.googleapis.com/maps/api/place/details/json?key={0}&place_id={1}&fields=address_components";
        private static HttpClient _httpClientInstance;
        public static HttpClient HttpClientInstance => _httpClientInstance ?? (_httpClientInstance = new HttpClient());
        private ObservableCollection<AddressAutocomplete> _addresses;
        public event PropertyChangedEventHandler PropertyChanged;
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        //string zip;

        public async Task<ObservableCollection<AddressAutocomplete>> GetPlacesPredictionsAsync(string _addressText)
        {
            try
            {
                ObservableCollection<AddressAutocomplete> list = new ObservableCollection<AddressAutocomplete>();
                CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(GooglePlacesApiAutoCompletePath, Constant.GooglePlacesApiKey, WebUtility.UrlEncode(_addressText))))
                {

                    using (HttpResponseMessage message = await HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                            PlacesLocationPredictions predictionList = await Task.Run(() => JsonConvert.DeserializeObject<PlacesLocationPredictions>(json)).ConfigureAwait(false);

                            if (predictionList.Status == "OK")
                            {
                                //Addresses.Clear();
                                if (predictionList.Predictions.Count > 0)
                                {
                                    foreach (Prediction prediction in predictionList.Predictions)
                                    {
                                        string[] predictionSplit = prediction.Description.Split(',');

                                        Console.WriteLine("Place ID: " + prediction.PlaceId);
                                        //await setZipcode(prediction.PlaceId);
                                        Console.WriteLine("After setZipcode:\n" + prediction.Description.Trim() + "\n" + predictionSplit[0].Trim() + "\n" + predictionSplit[1].Trim() + "\n" + predictionSplit[2].Trim());
                                        list.Add(new AddressAutocomplete
                                        {
                                            Address = prediction.Description.Trim(),
                                            Street = predictionSplit[0].Trim(),
                                            City = predictionSplit[1].Trim(),
                                            State = predictionSplit[2].Trim(),
                                            ZipCode = "",
                                            PredictionID = prediction.PlaceId
                                        });
                                        //addressList.ItemsSource = Addresses;
                                    }
                                }
                            }
                            //else
                            //{
                            //    Addresses.Clear();
                            //}
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Generic gen = new Generic();
                gen.parseException(ex.ToString());
                return null;
            }
        }

        public async Task<string> getZipcode(string placeId)
        {
            try
            {
                Console.WriteLine("0");
                CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;
                Console.WriteLine("1");
                string s = string.Format(GooglePlacesApiDetailsPath, Constant.GooglePlacesApiKey, WebUtility.UrlEncode(placeId));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, s);
                Console.WriteLine("2");
                HttpResponseMessage message = await HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                Console.WriteLine("3");
                if (message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                    Console.WriteLine(json);

                    PlacesDetailsResult placesDetailsResult = await Task.Run(() => JsonConvert.DeserializeObject<PlacesDetailsResult>(json)).ConfigureAwait(false);

                    foreach (var components in placesDetailsResult.Result.AddressComponents)
                    {
                        if (components.Types.Count != 0 && components.Types[0] == "postal_code")
                        {
                            return components.LongName;
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                Generic gen = new Generic();
                gen.parseException(ex.ToString());
                return null;
            }
        }

        //public void OnAddressChanged(ListView addressList, ObservableCollection<AddressAutocomplete> Addresses, string _addressText)
        //{
        //    if (!selected)
        //    {
        //        addressList.IsVisible = true;
        //    }
        //    Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
        //    Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token)
        //        .ContinueWith(
        //        delegate { GetPlacesPredictionsAsync(addressList, Addresses, _addressText); },
        //        CancellationToken.None,
        //        TaskContinuationOptions.OnlyOnRanToCompletion,
        //        TaskScheduler.FromCurrentSynchronizationContext());
        //}

        public void addressSelectedFillEntries(AddressAutocomplete selectedAddress, Entry address1, Entry address2, Entry city, Entry state, Entry zipcode)
        {
            address1.Text = selectedAddress.Street;
            address2.Text = selectedAddress.Unit;
            city.Text = selectedAddress.City;
            state.Text = selectedAddress.State;
            zipcode.Text = selectedAddress.ZipCode;
        }

        public void addressEntryFocused(ListView addressList, Grid[] grids)
        {
            //addressList.IsVisible = true;
            foreach (Grid g in grids) {
                g.IsVisible = false;
            }
        }

      
        public void addressEntryUnfocused(ListView addressList, Grid[] grids)
        {
            addressList.IsVisible = false;
            foreach (Grid g in grids)
            {
                g.IsVisible = true;
            }
        }

        public void addressEntryUnfocused(ListView addressList)
        {
            addressList.IsVisible = false;
        }

        public void addressSelected(ListView addressList, Grid[] grids, Entry AddressEntry, Entry CityEntry, Entry StateEntry, Entry ZipEntry)
        {
            try
            {
                foreach (Grid g in grids)
                {
                    g.IsVisible = true;
                }

                AddressEntry.Text = ((AddressAutocomplete)addressList.SelectedItem).Street;
                CityEntry.Text = ((AddressAutocomplete)addressList.SelectedItem).City;
                StateEntry.Text = ((AddressAutocomplete)addressList.SelectedItem).State;
                //ZipEntry.Text = ((AddressAutocomplete)addressList.SelectedItem).ZipCode;
                ZipEntry.Text = getZipcode(((AddressAutocomplete)addressList.SelectedItem).PredictionID).Result;
            }
            catch (Exception ex)
            {
                Generic gen = new Generic();
                gen.parseException(ex.ToString());
            }
        }

        public void addressSelected(ListView addressList, Entry AddressEntry)
        {
            try
            {
                AddressEntry.Text = ((AddressAutocomplete)addressList.SelectedItem).Street + ", " + ((AddressAutocomplete)addressList.SelectedItem).City
                    + ", " + ((AddressAutocomplete)addressList.SelectedItem).State + ", " + getZipcode(((AddressAutocomplete)addressList.SelectedItem).PredictionID).Result;
            }
            catch (Exception ex)
            {
                Generic gen = new Generic();
                gen.parseException(ex.ToString());
            }
        }

        public void addressEntryFocused(ListView addressList)
        {
            addressList.IsVisible = true;
            //foreach (Grid g in grids)
            //{
            //    g.IsVisible = false;
            //}
        }

        public void addressEntryFocused(ListView addressList, Frame frame)
        {
            addressList.IsVisible = true;
            frame.IsVisible = true;
            //foreach (Grid g in grids)
            //{
            //    g.IsVisible = false;
            //}
        }

        public void addressEntryUnfocused(ListView addressList, Frame frame)
        {
            addressList.IsVisible = false;
            frame.IsVisible = false;
            //foreach (Grid g in grids)
            //{
            //    g.IsVisible = true;
            //}
        }

        

        public AddressAutocomplete addressSelected(ListView addressList, Entry entry, Frame frame)
        {
            AddressAutocomplete selectedAddress = new AddressAutocomplete();
            addressList.IsVisible = false;
            frame.IsVisible = false;
            entry.Text = ((AddressAutocomplete)addressList.SelectedItem).Street + ", " + ((AddressAutocomplete)addressList.SelectedItem).City + ", " + ((AddressAutocomplete)addressList.SelectedItem).State + ", " + ((AddressAutocomplete)addressList.SelectedItem).ZipCode;
            //entry.Text = ((AddressAutocomplete)addressList.SelectedItem).Street;
            selectedAddress.Street = ((AddressAutocomplete)addressList.SelectedItem).Street;
            selectedAddress.City = ((AddressAutocomplete)addressList.SelectedItem).City;
            selectedAddress.State = ((AddressAutocomplete)addressList.SelectedItem).State;
            selectedAddress.ZipCode = ((AddressAutocomplete)addressList.SelectedItem).ZipCode;
            selectedAddress.PredictionID = ((AddressAutocomplete)addressList.SelectedItem).PredictionID;
            return selectedAddress;
        }

        public AddressAutocomplete addressSelected(ListView addressList, Frame frame)
        {
            AddressAutocomplete selectedAddress = new AddressAutocomplete();
            addressList.IsVisible = false;
            frame.IsVisible = false;
            //entry.Text = ((AddressAutocomplete)addressList.SelectedItem).Street + ", " + ((AddressAutocomplete)addressList.SelectedItem).City + ", " + ((AddressAutocomplete)addressList.SelectedItem).State + ", " + ((AddressAutocomplete)addressList.SelectedItem).ZipCode;
            //entry.Text = ((AddressAutocomplete)addressList.SelectedItem).Street;
            selectedAddress.Street = ((AddressAutocomplete)addressList.SelectedItem).Street;
            selectedAddress.City = ((AddressAutocomplete)addressList.SelectedItem).City;
            selectedAddress.State = ((AddressAutocomplete)addressList.SelectedItem).State;
            selectedAddress.ZipCode = ((AddressAutocomplete)addressList.SelectedItem).ZipCode;
            selectedAddress.PredictionID = ((AddressAutocomplete)addressList.SelectedItem).PredictionID;
            return selectedAddress;
        }

        public AddressAutocomplete addressSelected(ListView addressList, Entry address, Frame frame, Entry unit, Grid grid, Entry city, Entry state, Entry zipcode)
        {
            AddressAutocomplete selectedAddress = new AddressAutocomplete();

            addressList.IsVisible = false;
            frame.IsVisible = false;
            unit.IsVisible = true;
            grid.IsVisible = true;

            address.Text = ((AddressAutocomplete)addressList.SelectedItem).Street;
            selectedAddress.Address = ((AddressAutocomplete)addressList.SelectedItem).Address;
            selectedAddress.Street = ((AddressAutocomplete)addressList.SelectedItem).Street;
            selectedAddress.City = ((AddressAutocomplete)addressList.SelectedItem).City;
            selectedAddress.State = ((AddressAutocomplete)addressList.SelectedItem).State;
            selectedAddress.ZipCode = ((AddressAutocomplete)addressList.SelectedItem).ZipCode;
            selectedAddress.PredictionID = ((AddressAutocomplete)addressList.SelectedItem).PredictionID;

            city.Text = ((AddressAutocomplete)addressList.SelectedItem).City;
            state.Text = ((AddressAutocomplete)addressList.SelectedItem).State;
            zipcode.Text = ((AddressAutocomplete)addressList.SelectedItem).ZipCode;

            return selectedAddress;
        }

        public void resetAddressEntries(Entry unit, Entry city, Entry state, Entry zipcode)
        {
            unit.Text = null;
            city.Text = null;
            state.Text = null;
            zipcode.Text = null;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
