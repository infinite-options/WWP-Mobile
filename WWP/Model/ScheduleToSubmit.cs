using System;
using System.Collections.Generic;

namespace Experimentation.Models
{
    public class ScheduleToSubmit
    {
        public List<string[]> sunday { get; set; }
        public List<string[]> monday { get; set; }
        public List<string[]> tuesday { get; set; }
        public List<string[]> wednesday { get; set; }
        public List<string[]> thursday { get; set; }
        public List<string[]> friday { get; set; }
        public List<string[]> saturday { get; set; }
    }
}
