using ConferenceManagement.Model;
using Microsoft.AspNetCore.SignalR;

namespace ConferenceManagement.UserHub
{
    public class StatusHub:Hub
    {
        public async Task SendNotification(BookRoom booking)
        {
            await Clients.All.SendAsync("ReceiveNotification", booking.RequestId, booking.UserId, booking.RoomId, booking.Date, booking.TimeSlot, booking.Status);
        }
    }
}
