using Microsoft.AspNetCore.Authorization;

namespace Dn6Poc.DocuMgmtPortal.Policies;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public MinimumAgeRequirement(int minimumAge) => MinimumAge = minimumAge;

    public int MinimumAge { get; }
}