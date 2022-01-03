using System.ComponentModel.DataAnnotations;

namespace Dn6Poc.DocuMgmtPortal.Models
{
    public class Role
    {
        public const string SystemAdministrator = "system-administrator";
        public const string FormDesigner = "form-designer";
        public const string User = "user";
    }

    public enum ApplicationRoles
    {
        SystemAdministrator,
        FormDesigner,
        User
    }

    public class AddRoleFormModel
    {
        [Required]
        public string Username { get; init; }

        [Required]
        public string RoleName { get; init; }

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
