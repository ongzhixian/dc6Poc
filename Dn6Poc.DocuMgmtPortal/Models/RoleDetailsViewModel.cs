using Dn6Poc.DocuMgmtPortal.MongoEntities;
using System.Text;

namespace Dn6Poc.DocuMgmtPortal.Models
{
    public class RoleDetailsViewModel
    {
        public string RoleName { get; set; }

        public AddRoleFormModel AddRoleForm { get;set; }

        public IEnumerable<User> UserList { get; set; }

        public RoleDetailsViewModel(string role, IEnumerable<User> userList)
        {
            this.RoleName = role;
            this.AddRoleForm = new AddRoleFormModel(role);
            this.UserList = userList;
        }

        public RoleDetailsViewModel()
        {
            this.RoleName = string.Empty;
            this.AddRoleForm = new AddRoleFormModel(string.Empty);
            this.UserList = new List<User>();
        }
    }
}
