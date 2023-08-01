namespace ConferenceManagement.Model
{
    public class BookRoom
    {
        public int BookingId { get; set; }
        public string RequestId { get; set; } 
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string Date { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; }
    }
}
