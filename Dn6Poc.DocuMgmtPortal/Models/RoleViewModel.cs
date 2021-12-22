using System.ComponentModel.DataAnnotations;

namespace Dn6Poc.DocuMgmtPortal.Models
{
    public class Role
    {
        public const string SYSTEM_ADMINISTRATOR = "system-administrator";
        public const string FORM_DESIGNER = "form-designer";
        public const string USER = "user";
    }

    public enum ApplicationRoles
    {
        SYSTEM_ADMINISTRATOR,
        FORM_DESIGNER,
        USER
    }

    public class AddRoleFormModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string RoleName { get; set; }

        public AddRoleFormModel(string role)
        {
            Username = string.Empty;
            RoleName = role;
        }

        public AddRoleFormModel()
        {
            Username = string.Empty;
            this.RoleName = string.Empty;
        }
    }
}
