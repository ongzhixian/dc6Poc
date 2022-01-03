using System.ComponentModel.DataAnnotations;

namespace Dn6Poc.DocuMgmtPortal.Models
{
    public enum UserStatus
    {
        [Display(Name = "Pending activation")]
        PendingActivation = 0,

        Active = 1,

        Locked = 2,

        Suspended = 3
    }

    public class AddUserViewModel
    {
        [Required]
        public string Username { get; init; } = string.Empty;

        [Required]
        public string FirstName { get; init; } = string.Empty;

        [Required]
        public string LastName { get; init; } = string.Empty;

        [Required]
        public string Password { get; init; } = string.Empty;


        [Required]
        public string Email { get; init; } = string.Empty;

        //public UserStatus UserStatus { get; set; }
    }
}
