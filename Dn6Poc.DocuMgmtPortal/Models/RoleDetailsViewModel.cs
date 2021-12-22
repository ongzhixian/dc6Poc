using System.Text;

namespace Dn6Poc.DocuMgmtPortal.Models
{
    public class RoleDetailsViewModel
    {
        public string RoleName { get; set; }

        public AddRoleFormModel AddRoleForm { get;set; }

        public RoleDetailsViewModel(string role)
        {
            this.RoleName = role;
            this.AddRoleForm = new AddRoleFormModel(role);
        }

        public RoleDetailsViewModel()
        {
            this.RoleName = string.Empty;
            this.AddRoleForm = new AddRoleFormModel(string.Empty);
        }
    }
}
