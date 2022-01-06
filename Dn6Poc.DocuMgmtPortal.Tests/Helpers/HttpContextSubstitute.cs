using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Dn6Poc.DocuMgmtPortal.Tests.Helpers;

internal class HttpContextFactory
{

    public HttpContextFactory()
    {
        var a = new DefaultHttpContext();
        
    }

}
