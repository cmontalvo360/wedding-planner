

namespace Wedding_Planner.Models
{
    public class GuestList
    {
        public int GuestListId {get; set; }

        public int UserId {get; set; }
        public User User {get; set; }

        public int WeddingId {get; set; }
        public Wedding Wedding {get; set; }
    }
}