using Dn6Poc.DocuMgmtPortal.Models;

namespace Dn6Poc.DocuMgmtPortal.Api.Requests;

public class AddUserRequest : AddUserViewModel
{
    public AddUserRequest() { }

    public AddUserRequest(AddUserViewModel model) 
    {
        this.Username = model.Username;
        this.Password = model.Password;
        this.LastName = model.LastName;
        this.FirstName = model.FirstName;
        this.Email = model.Email;
    }

}

public class UpdateUserStatusRequest
{
    public string Id { get; set; }

    public UserStatus UserStatus { get; set; }
}

