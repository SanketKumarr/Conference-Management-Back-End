using System.ComponentModel.DataAnnotations;

namespace ConferenceManagement.Model
{
    public class User
    {
        public int UserId { get; set; }



        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.", MinimumLength = 1)]
        public string Name { get; set; }



        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, ErrorMessage = "Password must be between 6 and 50 characters.", MinimumLength = 6)]
        public string Password { get; set; }



        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(50, ErrorMessage = "Designation must be between 1 and 50 characters.", MinimumLength = 1)]
        public string Designation { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
