using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dn6Poc.DocuMgmtPortal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Dn6Poc.DocuMgmtPortal.Services;
using Dn6Poc.DocuMgmtPortal.Api;
using Microsoft.AspNetCore.Mvc;
using Dn6Poc.DocuMgmtPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics.CodeAnalysis;
using Dn6Poc.DocuMgmtPortal.Tests.Helpers;

namespace Dn6Poc.DocuMgmtPortal.Controllers.Tests;

[TestClass()]
public class LoginControllerTests
{
    Mock<ILogger<AuthenticationController>> mockLogger = new Mock<ILogger<AuthenticationController>>();
    Mock<ILoginService> mockLoginService = new Mock<ILoginService>();

    [TestInitialize]
    public void BeforeTest()
    {
        mockLogger = new Mock<ILogger<AuthenticationController>>();
        mockLoginService = new Mock<ILoginService>();
    }

    [TestMethod()]
    public void LoginController_IsNotNullTest()
    {
        LoginController controller = new LoginController(
            mockLogger.Object,
            mockLoginService.Object
            );

        Assert.IsNotNull(controller);
    }

    [TestMethod()]
    public void LoginController_LoggerArgumentNullExceptionTest()
    {
        ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() =>
            new LoginController(
                null,
                mockLoginService.Object
            )
        );

        Assert.IsNotNull(exception);
        Assert.IsNotNull(exception.ParamName);
        Assert.AreEqual<string>("logger", exception.ParamName);
    }

    [TestMethod()]
    public void LoginController_LoginServiceArgumentNullExceptionTest()
    {
        ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() =>
            new LoginController(
                mockLogger.Object,
                null
            )
        );

        Assert.IsNotNull(exception);
        Assert.IsNotNull(exception.ParamName);
        Assert.AreEqual<string>("loginService", exception.ParamName);
    }

    [TestMethod()]
    public void Index_ViewTest()
    {
        LoginController controller = new LoginController(
            mockLogger.Object,
            mockLoginService.Object
            );

        ActionResult result = controller.Index();

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(LoginViewModel));
    }

    [TestMethod()]
    public async Task AuthenticateAsync_RedirectToActionResultTestAsync()
    {
        mockLoginService.Setup(m => m.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<string>()))
            .ReturnsAsync("SomeJwtString");

        LoginViewModel model = new LoginViewModel();

        LoginController controller = new LoginController(
            mockLogger.Object,
            mockLoginService.Object
            );

        var features = new FeatureCollection();
        DefaultSessionFeature defaultSessionFeature = new DefaultSessionFeature();

        features.Set(new DefaultSessionFeature().Session);
        controller.ControllerContext.HttpContext = new DefaultHttpContext(features);
        controller.ControllerContext.HttpContext.Session = new SessionSubstitue();
        
        ActionResult result = await controller.AuthenticateAsync(model);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
    }

    [TestMethod()]
    public async Task AuthenticateAsync_ForbidTestAsync()
    {
        mockLoginService.Setup(m => m.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<string>()))
            .ReturnsAsync(string.Empty);

        LoginViewModel model = new LoginViewModel();

        LoginController controller = new LoginController(
            mockLogger.Object,
            mockLoginService.Object
            );

        ActionResult result = await controller.AuthenticateAsync(model);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ForbidResult));
    }
}
