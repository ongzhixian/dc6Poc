﻿namespace Dn6Poc.DocuMgmtPortal.Api.Requests;

public class LoginRequest
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
