using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace WWP.Model
{
    public class Hobbies : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name { get; set; }
        Color color;
        public Color bgColor
        {
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("bgColor");
                }
            }
            get
            {
                return color;
            }

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
