namespace ConferenceManagement.Model
{
    public class BookRoom
    {
        public int BookingId { get; set; }
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public string TimeSlot { get; set; }
        public string Status { get; set; } = "Pending";
        
    }
}
