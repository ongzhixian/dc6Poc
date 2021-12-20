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
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }


        [Required]
        public string Email { get; set; }

        //public UserStatus UserStatus { get; set; }
    }
}
