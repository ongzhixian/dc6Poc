namespace Dn6Poc.DocuMgmtPortal.Models
{
    public class Role
    {
        public const string SYSTEM_ADMINISTRATOR = "System administrator";
        public const string FORM_DESIGNER = "Form designer";
        public const string USER = "User";
    }

    public class AddRoleFormModel
    {
        public string Username { get; set; }
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
