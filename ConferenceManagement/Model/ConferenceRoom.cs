namespace ConferenceManagement.Model
{
    public class ConferenceRoom
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public bool IsAVRoom { get; set; }
        public string Image { get; set; }
    }
}
