using System;
using System.ComponentModel;

namespace Experimentation.Models
{
    public class ItemToBring : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public bool isChecked { get; set; }
        public string title { get; set; }

        public bool isCheckedUpdate
        {
            set { isChecked = value; PropertyChanged(this, new PropertyChangedEventArgs("isChecked")); }
        }
    }
}
