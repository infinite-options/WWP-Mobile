using System;
using System.Collections.ObjectModel;

namespace WWP.Model
{
    public class Appointment
    {
        public string day { get; set; }
        public string fullDate { get; set; }
        public ObservableCollection<Slot> slotColl { get; set; }
        public double slotCollHeight { get; set; }
    }

    public class Slot
    {
        public string imgSource { get; set; }
        public string eventName { get; set; }
        public string time { get; set; }
    }
}
