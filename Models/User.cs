using System.Collections.Generic;

namespace Wedding_Planner.Models
{
    public class User
    {
        public int UserId {get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }
        public string Email {get; set; }
        public string Password {get; set; }
        public List<GuestList> Invitations {get; set; }
        public User()
        {
            Invitations = new List<GuestList>();
        }
    }
}