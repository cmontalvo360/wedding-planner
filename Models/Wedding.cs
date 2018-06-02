using System;
using System.Collections.Generic;

namespace Wedding_Planner.Models
{
    public class Wedding
    {
        public int WeddingId {get; set; }
        public string WedderOne {get; set; }
        public string WedderTwo {get; set; }
        public DateTime WeddDate {get; set; }
        public string WedAddress {get; set; }
        public int CreatorId {get; set; }

        public List<GuestList> Attendees {get; set; }
        public Wedding()
        {
            Attendees = new List<GuestList>();
        }
    }
}