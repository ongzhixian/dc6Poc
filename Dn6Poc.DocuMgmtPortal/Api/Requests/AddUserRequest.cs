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
    public string Id { get; init; } = string.Empty;

    public UserStatus UserStatus { get; init; }
}

public enum UserRoleAction
{
    Add,
    Remove
}
public class UserRoleActionRequest
{
    public UserRoleAction Action { get;set; }
    public string Id { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}