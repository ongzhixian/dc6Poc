# Testing


## Cheatsheet

NUnit 3.x	        MSTest v2.x.	    xUnit.net 2.x	Comments
[Test]	            [TestMethod]	    [Fact]	                    Marks a test method.
[TestFixture]	    [TestClass]	        n/a	                        Marks a test class.
[SetUp]	            [TestInitialize]	Constructor	                Triggered before every test case.
[TearDown]	        [TestCleanup]	    IDisposable.Dispose	        Triggered after every test case.
[OneTimeSetUp]	    [ClassInitialize]   IClassFixture<T>	        One-time triggered method before test cases start.
[OneTimeTearDown]   [ClassCleanup]	    IClassFixture<T>	        One-time triggered method after test cases end.
[Ignore("reason")]	[Ignore]	        [Fact(Skip="reason")]	    Ignores a test case.
[Property]	        [TestProperty]	    [Trait]	                    Sets arbitrary metadata on a test.
[Theory]	        [DataRow]	        [Theory]	                Configures a data-driven test.
[Category("")]	    [TestCategory("")]	[Trait("Category", "")]     Categorizes the test cases or classes.


## Rhino vs Moq


Mock Type	        Rhino.Mocks	                                            Moq
Strict Mock	        IFoo mock = MockRepository.GenerateStrictMock<IFoo>();	Mock<IFoo> mockWrapper = new Moq.Mock<IFoo>(MockBehavior.Strict);
Dynamic\Loose Mock	IFoo mock = MockRepository.GenerateMock<IFoo>();	    Mock<IFoo> mockWrapper = new Moq.Mock<IFoo>();
                                                                            or
                                                                            Mock<IFoo> mockWrapper = new Moq.Mock<IFoo>(MockBehavior.Loose);
Partial Mock	    IFoo mock = MockRepository.GeneratePartialMock<IFoo>();	Mock<IFoo> mockWrapper = new Moq.Mock<IFoo>() { CallBase = true };

Passing             IFoo mock = MockRepository.GenerateMock<IFoo>(param1, param2); 	
Constructor                                                                 Mock<IFoo> mockWrapper = new Moq.Mock<IFoo>(param1, param2);
Arguments                                                                   or
                                                                            Mock<IFoo> mockWrapper = new Moq.Mock<IFoo>(MockBehavior.Strict, param1, param2);

Setups (methods)
Stub 	            mock.Stub(x => x.SomeMethod()).Return(true); 	        mockWrapper.Setup(x => x.SomeMethod()).Returns(true);
Mock 	            mock.Expect(x => x.SomeMethod()).Return(true); 	        mockWrapper.Setup(x => x.SomeMethod()).Returns(true).Verifiable();

Setups (properties)

Stub                mock.Stub(x => x.SomeProperty).Return(true); 	        mock.Setup(foo => foo.SomeProperty).Returns("bar");
(always return same value) 	

Stub                (must use a callback) 	                                mock.SetupProperty(f => f.SomeProperty);
(returns tracked value)

Stub                (must use a callback) 	                                mock.SetupProperty(f => f.SomeProperty, "bar");
(with initial value; returns tracked value) 	


Mock                mock.Expect(x => x.SomeProperty).Return(true); 	        mock.SetupSet(foo => foo.SomePropertyName).Returns("bar");
(always return same value, create expectation)                              mock.SetupGet(foo => foo.SomeProperty)



Asserting/Verifying

Called 	            mock.AssertWasCalled(x => x.SomeMethod()); 	            mockWrapper.Verify(x => x.SomeMethod());
Called              mock.AssertWasCalled(x => x.SomeMethod(), options => options.Repeat.Times(2) ); 	
a specific number of times                                                  mockWrapper.Verify(x => x.SomeMethod(), Times.Exactly(2));
Not called 	        mock.AssertWasNotCalled(x => x.SomeMethod()); 	        mockWrapper.Verify(x => x.SomeMethod(), Times.Never);


Get 	            mock.AssertWasCalled(x => x.SomeProperty); 	            mockWrapper.VerifyGet(x => x.SomeProperty);
Set 	            mock.AssertWasCalled(x => x.SomeProperty = true); 	    mockWrapper.VerifySet(x => x.SomeProperty);
Not called 	        mock.AssertWasNotCalled(x => x.SomeProperty = true);    mockWrapper.VerifySet(x => { x.SomeProperty = true; }, Times.Never);

Verify Mocks only 	mock.VerifyAllExpectations(); 	                        mockWrapper.Verify();
Verify Mocks and Stubs 	(not available) 	                                mockWrapper.VerifyAll();



Change how many times to use the mock 	
                    use .Repeat():                                          use .SetupSequence():

                    mock.Expect(x => x.SomeProperty)                        mockWrapper.SetupSequence(x => x.SomeMethod())
                    .Repeat.Times(2)                                        .Returns(true)
                    .Return(true); 	                                        .Returns(true)
                                                                            .Throws( new Exception("Called too many times"));
Ignore arguments 	
                    use .IgnoreArguments():                                 use argument constraints:

                    mock.Expect(x => x.SomeMethod("param"))                 mockWrapper.Setup(x => x.SomeMethod(It.IsAny<string>()))
                    .IgnoreArguments()                                      .Returns(true);
                    .Return(true); 	


# Mocking notes

https://stackoverflow.com/questions/47198341/how-to-unit-test-httpcontext-signinasync
https://stackoverflow.com/questions/48873454/no-service-for-type-microsoft-aspnetcore-mvc-has-been-registered/48875448
https://stackoverflow.com/questions/7601972/what-is-the-best-way-to-test-a-redirecttoaction/9886309
https://coderedirect.com/questions/527099/asp-net-core-2-0-signinasync-returns-exception-value-cannot-be-null-provider


```cs
DefaultHttpContext MakeHttpContext()
{
    // Need mock of IAuthenticationService to handle HttpContext.SignIn
    var mockAuthenticationService = new Mock<IAuthenticationService>();

    mockAuthenticationService
        .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
        .Returns(Task.CompletedTask);

    // Need mock of IUrlHelperFactory to properly handle RedirectToAction
    var mockUrlHelperFactory = new Mock<IUrlHelperFactory>();

    // Setup mock service provider

    var serviceProviderMock = new Mock<IServiceProvider>();

    serviceProviderMock
        .Setup(_ => _.GetService(typeof(IAuthenticationService)))
        .Returns(mockAuthenticationService.Object);

    serviceProviderMock
        .Setup(_ => _.GetService(typeof(IUrlHelperFactory)))
        .Returns(mockUrlHelperFactory.Object);

    return new DefaultHttpContext()
    {
        RequestServices = serviceProviderMock.Object
    };
}
```


```cs:Setup options monitor
[TestMethod()]
public async Task IndexAsyncTestAsync()
{
    Mock<ILogger<LoginController>> mockLogger = new Mock<ILogger<LoginController>>();
    Mock<IAuthenticationApiService> mockAuthenticationApiService = new Mock<IAuthenticationApiService>();
    Mock<IJwtService> mockJwtService = new Mock<IJwtService>();

    //Mock<IOptionsMonitor<JwtSettings>> mockOptionsMonitor = new Mock<IOptionsMonitor<JwtSettings>>();
    //mockOptionsMonitor.Setup(m => m.Get("jwt")).Returns(new JwtSettings
    //{
    //    SecretKey = "placeHolderSecretKey",
    //    ValidIssuer = "placeHolderValidIssuer",
    //    ValidAudience = "placeHolderValidAudience"
    //});

    mockJwtService.Setup(m => m.GetClaims("jwt")).Returns(new List<Claim>());

    OperationResult<LoginResponse> operationResult = new(true, new LoginResponse()
    {
        ExpiryDateTime = DateTime.UtcNow,
        Jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
    });

    mockAuthenticationApiService.Setup(a => a.IsValidCredentialsAsync(It.IsAny<LoginViewModel>())).ReturnsAsync(operationResult);

    LoginController loginController = new LoginController(
        mockLogger.Object,
        mockAuthenticationApiService.Object,
        mockJwtService.Object
        );

    IActionResult? result = await loginController.IndexAsync(new LoginViewModel());

    Assert.IsNotNull(result);
    Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
    Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);

}
```

# Reference


https://www.automatetheplanet.com/mstest-cheat-sheet/#tab-con-8

https://hamidmosalla.com/2017/08/03/moq-working-with-setupget-verifyget-setupset-verifyset-setupproperty/



https://www.jetbrains.com/help/dotcover/Running_Coverage_Analysis_from_the_Command_LIne.html#to-install-dotcover-console-runner-as-a-net-global-tool
https://www.jetbrains.com/help/resharper/ReSharper_Command_Line_Tools.html#overview-video


