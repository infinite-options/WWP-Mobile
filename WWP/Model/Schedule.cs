using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Experimentation.Models
{
    public class Schedule : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string day { get; set; }
        public string row { get; set; }
        public ObservableCollection<PickerTimeHour> startHour { get; set; }
        public ObservableCollection<PickerTimeMinute> startMinute { get; set; }
        public ObservableCollection<PickerTime> startTime { get; set; }
        public ObservableCollection<PickerTimeHour> endHour { get; set; }
        public ObservableCollection<PickerTimeMinute> endMinute { get; set; }
        public ObservableCollection<PickerTime> endTime { get; set; }

        public Color colorValue { get; set; }
        public double opacityValue { get; set; }
        public bool isEnabledValue { get; set; }

        public Color updateColorValue
        {
            set
            {
                colorValue = value;
                PropertyChanged(this, new PropertyChangedEventArgs("colorValue"));
            }
        }
        public double updateOpacityValue
        {
            set
            {
                opacityValue = value;
                PropertyChanged(this, new PropertyChangedEventArgs("opacityValue"));
            }
        }
        public bool updateIsEnabledValue
        {
            set
            {
                isEnabledValue = value;
                PropertyChanged(this, new PropertyChangedEventArgs("isEnabledValue"));
            }
        }

    }
}
