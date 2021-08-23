using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace WWP.Model
{
    public class AvailableTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    
        public string availableTime { get; set; }
        public Color color { get; set; }

        public Color updateColor
        {
            set
            {
                color = value;
                PropertyChanged(this, new PropertyChangedEventArgs("color"));
            }
        }
        
    }
}
