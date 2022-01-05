# lern

## Install Angular CLI

npm install -g @angular/cli

npm install -g typescript

## Setup git

$ git config --global user.name "John Doe"

$ git config --global user.email johndoe@example.com

-----------

ng generate component xyz
ng add @angular/material
ng add @angular/pwa
ng test


There are JavaScript frameworks out there that assist in active Web development. 
Let us compare three frameworks that can help in web development immensely. 

Jasmine is an open source testing framework for JavaScript that aims to run on any JavaScript-enabled platform. 
Jasmine is influenced from other testing frameworks such as ScrewUnit, JSSpec, JSpec, and Spec. 

Mocha is a framework with a simple setup for Node.js programs. 
It has an assertion library that throws exceptions on failure, offers lot of plugins and is highly extensible. 

Jest is a Javascript testing framework as well, with a focus on providing a simple framework to the users. 
It works seamlessly with projects using Babel, TypeScript, Node.js, React, Angular, and Vue.js. 

Jasmine so far is missing a test runner whereas Mocha has a testrunner support equipped with an API for setting up the test suite. 
Jest, on the other hand, has an intelligent execution of tests and snapshots that can happen parallelly.


Jasmine - The Default Choice of Angular
Jasmine is a Behavior-Driven Development(BDD) framework that provides a clean, obvious Syntax for writing test cases by making the process easy.

Jest — A Very Fast Testing Library!
Jest provides you with multiple layers on top of Jasmine and it is a very fast testing library that runs tests in parallel. 
It comes with minimum configuration setup, out of box mocking, and assertion support.

Mocha — Highly Flexible Framework
Mocha is a feature-rich JavaScript test framework running on node.js and the browser, making asynchronous testing simple. 
Mocha tests run serially, allowing for flexible and accurate reporting while mapping uncaught exceptions to the correct test cases.
Mocha is a very flexible framework that gives the freedom to choose assertion, mocking libraries based on our requirements, and provides configurable test reporters.

10 Best Practices for Writing Node.js REST APIs
https://blog.risingstack.com/10-best-practices-for-writing-node-js-rest-apis/
Do JWT-Based, Stateless Authentication

https://www.tutorialswebsite.com/how-to-use-cookies-in-angular/

https://blog.logrocket.com/the-perfect-architecture-flow-for-your-next-node-js-project/


----
# Invoke-WebRequest -Uri "https://unpkg.com/react@17.0.2/umd/react.development.js" -OutFile "./react.development.js"

# Get-Content "./react.development.js" -Raw



# $a = [System.Security.Cryptography.SHA384]::Create().ComputeHash((Get-Content "./react.development.js" -Raw -AsByteStream))


# PS> [Convert]::ToBase64String($a)
# sha384-xQwCoNcK/7P3Lpv50IZSEbJdpqbToWEODAUyI/RECaRXmOE2apWt7htari8kvKa/
# sha384-xQwCoNcK/7P3Lpv50IZSEbJdpqbToWEODAUyI/RECaRXmOE2apWt7htari8kvKa/


# $a = [System.Security.Cryptography.SHA384]::Create().ComputeHash((Invoke-WebRequest -Uri "https://unpkg.com/react@17.0.2/umd/react.development.js"))

# Get-Content (Invoke-WebRequest -Uri "https://unpkg.com/react@17.0.2/umd/react.development.js") -Raw -AsByteStream

# [System.Net.WebClient]::new().DownloadData("https://unpkg.com/react@17.0.2/umd/react.development.js")

# $url = "https://unpkg.com/react@17.0.2/umd/react.development.js"
# $url = "https://unpkg.com/react-dom@17/umd/react-dom.development.js"
# $url = "https://unpkg.com/babel-standalone@6/babel.min.js"
# $url = "https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"
# $url = "https://fonts.googleapis.com/css?family=Raleway:400,300,600"
# "sha384-$([Convert]::ToBase64String([System.Security.Cryptography.SHA384]::Create().ComputeHash([System.Net.WebClient]::new().DownloadData($url))))"


if(Get-Member -inputobject $var -name "Property" -Membertype Properties){
#Property exists
}

------------------------

var app = angular.module('MySocektApp', ['ngMaterial', 'LocalStorageModule', 'btford.socket-io']);

app.service('SocketService', ['socketFactory', function SocketService(socketFactory) {
    return socketFactory({
        ioSocket: io.connect('http://localhost:3000')
    });
}]);

app.controller('homeController', function($scope, localStorageService, SocketService) {

    $scope.array = [];
    $scope.message = {};
    SocketService.emit('room', { roomId: "temp" });

    $scope.add = function() {
        SocketService.emit('toBackEnd', {roomId:'temp', data: $scope.message, date: new Date() })
        $scope.array.push({ data: $scope.message, date: new Date() })
    }

    SocketService.on('message', function(msg) {
        $scope.array.push(msg)
    });

})


Given
When 
Then


"EnableSqlParameterLogging": true,


services.AddDbContext<ContosoPetsContext>(options =>
	options.UseSqlServer(builder.ConnectionString))
	 .EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging")));



. <(wget -q -O - https://aka.ms/persist-data-ef-core-setup)

The following variables are used in this module:

srcWorkingDirectory: /home/zhixian/aspnet-learn/src
setupWorkingDirectory: /home/zhixian/aspnet-learn/setup
sqlConnectionString: Data Source=azsql718433028.database.windows.net;Initial Catalog=ContosoPetsAuth;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

sqlUsername: SqlUser
sqlPassword: Pass.8394.word

instrumentationKey (for Application Insights): 7ce4e481-e590-4bc8-bba9-8ec031c985af
appId (for Application Insights): 578c1489-105b-4e37-a78f-2261ae099a6f
apiKey (for Application Insights): 5q2yjhte40fonagv07ubmbz4yrebsuxr3r41tbwl

db is an alias for sqlcmd -U SqlUser -P Pass.8394.word -S azsql718433028.database.windows.net -d ContosoPetsAuth

,
        {
            "name": "Launch tutorial-app",
            "runtimeExecutable": "npm",
            "args": ["start", "--workspace=tutorial-app"],
            "request": "launch",
            "skipFiles": [
                "<node_internals>/**"
            ],
            "type": "pwa-node"
        }

https://developer.mozilla.org/en-US/docs/Learn/Server-side/Express_Nodejs

<Navbar bg="light" expand="lg">
  <Container>
    <Navbar.Brand href="#home">React-Bootstrap</Navbar.Brand>
    <Navbar.Toggle aria-controls="basic-navbar-nav" />
    <Navbar.Collapse id="basic-navbar-nav">
      <Nav className="me-auto">
        <Nav.Link href="#home">Home</Nav.Link>
        <Nav.Link href="#link">Link</Nav.Link>
        <NavDropdown title="Dropdown" id="basic-nav-dropdown">
          <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.2">Another action</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
          <NavDropdown.Divider />
          <NavDropdown.Item href="#action/3.4">Separated link</NavDropdown.Item>
        </NavDropdown>
      </Nav>

    </Navbar.Collapse>
  </Container>
</Navbar>

openssl dgst -sha384 -binary FILENAME.js | openssl base64 -A

Found hint at c:\Users\zhixian\AppData\Roaming\Code\User\globalStorage\ms-edgedevtools.vscode-edge-devtools\node_modules\hint\dist\src\lib\index.js
Error loading "hint" package from "d:\src\github\nodePoc"
Trying to load shared version
Found hint at c:\Users\zhixian\AppData\Roaming\Code\User\globalStorage\ms-edgedevtools.vscode-edge-devtools\node_modules\hint\dist\src\lib\index.js


<!-- include ts-browser -->
<script type="text/javascript" src="https://unpkg.com/ts-browser"></script>

<!-- include your TypeScript file -->
<script type="text/typescript" src="your/typescript/file.ts"></script>


	react-router-dom.js
	https://unpkg.com/browse/react-router-dom@4.2.2/umd/react-router-dom.js
	https://unpkg.com/browse/react-router-dom@4.2.2/umd/react-router-dom.min.js
	
https://medium.com/@Idan_Co/angular-print-service-290651c721f9
https://javascript.plainenglish.io/what-is-ngxs-and-how-to-use-it-in-angular-f975f5cfac40
https://blog.knoldus.com/ngrx-vs-ngxs-state-management-techniques/
https://serengetitech.com/tech/handling-xml-data-in-net/
https://blog.knoldus.com/category/blockchain/
https://blog.knoldus.com/implementing-bots-in-daml-application/
https://blog.knoldus.com/getting-started-with-daml/
https://daml.com/

https://notiz.dev/blog/create-a-component-library-for-angular-and-the-web

https://simplernerd.com/js-budgets-error-warning/
https://stackoverflow.com/questions/53995948/warning-in-budgets-maximum-exceeded-for-initial/53995996
https://applitools.com/blog/migrating-protractor-tests-angular/


https://igv.org/
https://github.com/igvteam/igv.js/

https://jestjs.io/docs/expect

https://jestjs.io/docs/setup-teardown#one-time-setup
https://www.softwaretestinghelp.com/jest-testing-tutorial/

https://swagger.io/tools/swaggerhub/hosted-api-documentation/?utm_medium=ppco&utm_source=bing&utm_term=%2Bapi%20%2Bdocumentation&utm_content=&utm_campaign=SEM_SwaggerHub_NB_APAC_ENG_PHR_Prospecting&gclsrc=aw.ds&msclkid=3023f0979df31c30b3829100b75938fa
https://medium.com/consonance/building-an-angular-library-with-the-angular-cli-version-6-384ee85933ad
https://github.com/angular/angular-cli/issues/11071

https://www.c-sharpcorner.com/article/communication-between-angulars-components/

https://docs.microsoft.com/en-us/samples/browse/
https://docs.microsoft.com/en-us/learn/
https://github.com/github/copilot-docs/blob/main/docs/visualstudiocode/gettingstarted.md#getting-started-with-github-copilot-in-visual-studio-code

https://www.learmoreseekmore.com/2021/07/ngrx-v12-angular-application-state-management-using-ngrx-store.html
https://www.learmoreseekmore.com/2019/10/angular-state-management-with-ngrx.html#:~:text=NgRx%20provides%20state%20management%20to%20an%20application%2C%20where,the%20components%20consuming%20that%20data%20get%20updated%20instantly.
https://ngserve.io/ngrx-tutorial-understanding-the-redux-pattern/
https://github.com/Naveen512/AngularNgrxStoreSample/blob/master/src/app/stores/cakes/cakes.reducer.ts
https://instercloud.com/blog/how-to-manage-application-state-in-an-angular-9-app-using-ngrx
https://stackoverflow.com/questions/54297317/angular-ngrx-effects-how-to-pass-a-parameter

https://www.learmoreseekmore.com/2021/07/ngrx-v12-angular-application-state-management-using-ngrx-store.html
https://offering.solutions/blog/articles/2020/05/29/authentication-in-angular-with-ngrx-and-asp.net-core/
https://andrewhalil.com/2021/02/21/implementing-crud-operations-with-ngrx-store/
https://baldur.gitbook.io/angular/ngrx/whats-ngrx/authentication-guard-with-ngrx
https://www.gistia.com/insights/authentication-in-angular-with-ngrx-part-ii
https://www.gistia.com/insights/authentication-in-angular-with-ngrx-part-i
https://mherman.org/blog/authentication-in-angular-with-ngrx/
https://auth0.com/blog/ngrx-authentication-tutorial/
https://www.tektutorialshub.com/angular/angular-httpclient-http-interceptor/?__cf_chl_jschl_tk__=wGkZI5m1a5EhgyEu3wnl.Oa7XfbfuxrZ8X1nohrYAmE-1636418836-0-gaNycGzNDL0#http-interceptor-example
https://angular.io/guide/observables#creating-observables


https://ngrx.io/guide/effects#handling-errors
https://brianflove.com/2020-06-01/route-params-ngrx-store/
https://medium.com/simars/ngrx-router-store-reduce-select-route-params-6baff607dd9


https://www.learmoreseekmore.com/2021/07/ngrx-v12-an-angular-state-management-using-ngrx-data.html
https://developers.google.com/books/docs/v1/reference/volumes/list?apix_params=%7B%22q%22%3A%22frank%20herbert%22%7D
https://developers.google.com/books/docs/v1/getting_started

https://dotnetcoretutorials.com/2021/11/10/single-file-apps-in-net-6/
https://jasonwatmore.com/post/2020/10/17/angular-10-basic-http-authentication-tutorial-example#auth-guard-ts
https://www.tutorialspoint.com/angular8/angular8_reactive_programming.htm

https://developers.redhat.com/blog/2019/01/14/building-a-node-js-service-using-the-api-first-approach#
https://www.youtube.com/watch?v=HktWin_LPf4

https://blog.codemagic.io/how-to-develop-and-distribute-ios-apps-without-mac-with-flutter-codemagic/
https://www.andrewhoefling.com/Blog/Post/how-to-develop-ios-without-a-mac
https://nicksnettravels.builttoroam.com/ios-dev-no-mac/

https://www.raywenderlich.com/7357-ios-app-with-kotlin-native-getting-started
https://offering.solutions/blog/articles/2018/04/23/starting-with-angular-and-microsofts-face-recognition-api/
https://offering.solutions/blog/articles/2021/11/11/loading-configuration-before-your-angular-app-starts/

https://github.com/dotnet/reactive
https://github.com/dotnet/reactive
https://anthonygiretti.com/2021/08/03/introducing-c-10-record-struct/
https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/observability
https://docs.microsoft.com/en-us/dotnet/architecture/containerized-lifecycle/design-develop-containerized-apps/design-docker-applications
https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview
https://developers.redhat.com/blog/2019/01/14/building-a-node-js-service-using-the-api-first-approach#

https://jsnlog.com/Documentation/HowTo/Angular2Logging
https://www.timsommer.be/log-javascript-client-side-events-in-your-server-side-logs/

https://dotnetcoretutorials.com/2021/11/10/single-file-apps-in-net-6/
https://medium.com/@chrisbautistaaa/server-sent-events-in-angular-node-908830cc29aa


-------------------

https://relevant.software/blog/ai-usage-for-wealth-management-and-financial-planning/
https://www.whitesourcesoftware.com/free-developer-tools/blog/npm-vs-yarn-which-should-you-choose/

https://github.com/brianmaher84/PlantUML_Notepad-_UDL
https://plantuml.com/running

java -jar plantuml.jar file1 file2 file3

https://ialab.it.monash.edu/webcola/examples/sucrosebreakdown.html
https://i.imgur.com/plNRs6Q.jpg
https://swimlane.github.io/ngx-graph/


https://www.npmjs.com/package/express-jwt

https://www.positronx.io/express-cors-tutorial/

Access to resource at 'http://localhost:3000/my-endpoint' from origin 
'http://localhost:4200' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.


https://blog.mapbox.com/how-i-built-a-wind-map-with-webgl-b63022b5537f
http://www.weather.gov.sg/home/


dbdev-tim
h0m2C2q1vxWvMwFk

mongodb:minitools:ConnectionString mongodb+srv://dbdev-tim:h0m2C2q1vxWvMwFk@cluster0.vwfie.mongodb.net/myFirstDatabase?retryWrites=true&w=majority

dotnet user-secrets --project .\MiniTools.Web\ set mongodb:minitools:ConnectionString "mongodb+srv://dbdev-tim:h0m2C2q1vxWvMwFk@cluster0.vwfie.mongodb.net/minitools?retryWrites=true&w=majority"

dotnet user-secrets --project .\MiniTools.Web\ set JWT:ValidAudience "https://localhost:7001/"
dotnet user-secrets --project .\MiniTools.Web\ set JWT:ValidIssuer "https://localhost:7001/"
dotnet user-secrets --project .\MiniTools.Web\ set JWT:SecretKey "secret09172301287"



[JsonSerializer(typeof(Person))
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationOptions.Default,
	PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class PersonJsonContext: JsonSerializerContext
 
 
JsonSerializer.Serialize(person, PersonJsonContext.Default.Person)
JsonSerializer.deserialize(text, PersonJsonContext.Default.Person)



EmitCompilerGeneratedFile


<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>


dotnet add .\MiniTools.SourceGenerators\ package Microsoft.CodeAnalysis.CSharp
dotnet add .\MiniTools.SourceGenerators\ package Microsoft.CodeAnalysis.Analyzers

NativeMemoryArray — A library that takes full advantage of the .NET 6 API to handle huge data of over 2GB
https://www.reddit.com/r/csharp/comments/rmu0qw/nativememoryarray_a_library_that_takes_full/

Stay safe with your units! Advanced units of measure in .NET.
https://whiteblackgoose.medium.com/stay-safe-with-your-units-advanced-units-of-measure-in-net-f7d8b02af87e


https://github.com/DavidWengier/SourceGeneratorTemplate

▶ https://github.com/chsienki/kittitas

▶ https://sourcegen.dev/

BsonClassMap.RegisterClassMap<CD>(cm =>
{
   cm.AutoMap();
   cm.GetMemberMap(c => c.Artist).SetElementName("artist");
   cm.GetMemberMap(c => c.Title).SetElementName("title");
   cm.GetMemberMap(c => c.Category).SetElementName("category");
});


https://brahimkamel.wordpress.com/2016/05/28/configure-camel-case-resolver-for-mongodb-c-driver/




https://telera-cosmos-db.documents.azure.com:443/
PRIMARY KEY
9G807ZjJVH2BXLINCOh7nIT2UM03rLQySonc1uiYx7EzvRa5H6gTP2RP3uxmZ5lmRs8llgWn9KoeEJuZd7zocg==
SECONDARY KEY
22EbEh5wg6eSRCarHtHWJrmLB82SpH5674UGcfQ1aUbzXZWWMHynNDg4891sBklSG8xsDxY8x0fWnEhrcQBA2A==
PRIMARY CONNECTION STRING
AccountEndpoint=https://telera-cosmos-db.documents.azure.com:443/;AccountKey=9G807ZjJVH2BXLINCOh7nIT2UM03rLQySonc1uiYx7EzvRa5H6gTP2RP3uxmZ5lmRs8llgWn9KoeEJuZd7zocg==;
SECONDARY CONNECTION STRING
AccountEndpoint=https://telera-cosmos-db.documents.azure.com:443/;AccountKey=22EbEh5wg6eSRCarHtHWJrmLB82SpH5674UGcfQ1aUbzXZWWMHynNDg4891sBklSG8xsDxY8x0fWnEhrcQBA2A==;


dbadmin
36qteNZ56B8TmNM
{
    "db_uri": "mongodb+srv://dbadmin:36qteNZ56B8TmNM@cluster0.p8q7e.mongodb.net/myFirstDatabase?retryWrites=true&w=majority"
}

# ZX Custom
ako.ps1
*.test.ps1
MyReference-TravelApi


mongoDb:safeTravel = mongodb+srv://dbadmin:36qteNZ56B8TmNM@cluster0.p8q7e.mongodb.net/safe_travel?retryWrites=true&w=majority

username
password
host		@cluster0.p8q7e.mongodb.net
database	/safe_travel
options		?retryWrites=true&w=majority

Another “advanced” pattern that can be achieved is to register multiple concrete implementations for an interface. 
These can later be injected as an IEnumerable of that interface. In this post, we’ll explore a quick example of how we can do that.



Transient objects are always different; a new instance is provided to every controller and every service.
Scoped objects are the same within a request, but different across different requests.
Singleton objects are the same for every object and every request.

  services.AddSingleton<IEnricher, LocationEnricher>();
    services.AddSingleton<IEnricher, DateEnricher>();
IEnumerable<IEnricher> enrichers)



https://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/connecting/
It is recommended to store a MongoClient instance in a global place, either as a static variable or in an IoC container with a singleton lifetime.
The implementation of IMongoDatabase provided by a MongoClient is thread-safe and is safe to be stored globally or in an IoC container.
The implementation of IMongoCollection<TDocument> ultimately provided by a MongoClient is thread-safe and is safe to be stored globally or in an IoC container.



================================


https://www.c-sharpcorner.com/article/consuming-asp-net-web-api-rest-service-in-asp-net-mvc-using-http-client/
https://www.tutorialsteacher.com/webapi/consume-web-api-post-method-in-aspnet-mvc


https://zetcode.com/csharp/httpclient/

https://stackoverflow.com/questions/37371264/invalidoperationexception-unable-to-resolve-service-for-type-microsoft-aspnetc


https://www.azurefromthetrenches.com/capturing-and-tracing-all-http-requests-in-c-and-net/



accessToken
refreshToken
Role vs Group



https://pragmaticwebsecurity.com/articles/oauthoidc/refresh-token-protection-implications.html
https://auth0.com/blog/refresh-tokens-what-are-they-and-when-to-use-them/
https://dev.to/moe23/refresh-jwt-with-refresh-tokens-in-asp-net-core-5-rest-api-step-by-step-3en5
https://jasonwatmore.com/post/2021/06/15/net-5-api-jwt-authentication-with-refresh-tokens
https://jasonwatmore.com/post/2020/05/25/aspnet-core-3-api-jwt-authentication-with-refresh-tokens
https://www.yogihosting.com/jwt-refresh-token-aspnet-core/
https://codewithmukesh.com/blog/refresh-tokens-in-aspnet-core/
https://www.google.com/search?client=firefox-b-d&q=asp.net++jwt+refresh+token
https://alimozdemir.medium.com/asp-net-core-jwt-and-refresh-token-with-httponly-cookies-b1b96c849742
https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0


https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-6.0


https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client#create-and-initialize-httpclient

https://www.geeksforgeeks.org/how-to-align-navbar-items-to-the-right-in-bootstrap-4/
https://www.studytonight.com/bootstrap/how-to-align-bootstrap-5-navbar-items-to-the-right


https://refactoring.guru/design-patterns/factory-method/csharp/example


    unpkg
        https://unpkg.com/grapesjs
        https://unpkg.com/grapesjs/dist/css/grapes.min.css
    cdnjs
        https://cdnjs.cloudflare.com/ajax/libs/grapesjs/0.12.17/grapes.min.js
        https://cdnjs.cloudflare.com/ajax/libs/grapesjs/0.12.17/css/grapes.min.css
		


https://vuido.mimec.org/introduction
https://github.com/andlabs/libui


https://github.com/szimek/signature_pad
https://rehansaeed.com/

-------------------------



Opwn

https://devblogs.microsoft.com/scripting/getting-started-with-pester/
https://www.cryingcloud.com/blog/2017/08/08/part-3-more-tests-this-time-with-pesters-mock
https://github.com/PowerShell/PowerShell/blob/master/docs/testing-guidelines/WritingPesterTests.md
https://petertheautomator.com/2020/09/07/verify-invoke-webrequest-statuscode-404-in-pester/
https://stackoverflow.com/questions/19122378/powershell-web-request-without-throwing-exception-on-4xx-5xx

https://offering.solutions/blog/articles/2020/05/18/authentication-and-authorization-with-angular-and-asp.net-core-using-oidc-and-oauth2/
https://offering.solutions/blog/articles/2020/05/29/authentication-in-angular-with-ngrx-and-asp.net-core/
https://stackoverflow.com/questions/48091395/angular4-ngrx-is-it-good-idea-to-keep-auth-state-in-ngrx-store
https://www.codemag.com/article/1805021/Security-in-Angular-Part-1
https://www.toptal.com/angular/angular-6-jwt-authentication
https://swimlane.github.io/ngx-graph/#introduction
https://indepth.dev/posts/1206/understanding-the-magic-behind-ngrx-effects



https://dotnetcoretutorials.com/2018/01/03/setting-private-nuget-server-part-1-intro-server-setup/





https://www.cdc.gov/coronavirus/2019-ncov/travelers/map-and-travel-notices.html
https://www.iatatravelcentre.com/world.php
https://www.kayak.sg/travel-restrictions
https://www.unwto.org/covid-19-travel-restrictions
https://www.e-unwto.org/
https://safetravel.ica.gov.sg/health
https://www.notarise.gov.sg/

https://blog.logrocket.com/the-perfect-architecture-flow-for-your-next-node-js-project/
https://www.toptal.com/nodejs/secure-rest-api-in-nodejs
https://developerhowto.com/2018/12/29/build-a-rest-api-with-node-js-and-express-js/
https://reactjs.org/

https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli#connection-string
https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency

https://fireship.io/lessons/angular-animations-examples/
https://www.yearofmoo.com/2017/06/new-wave-of-animation-features.html
http://www.scala-js.org/doc/tutorial/
https://github.com/andreaferretti/paths-js#demos
http://andreaferretti.github.io/paths-js-react-demo/
https://animejs.com/
https://www.toptal.com/developers/css3maker/font-face.html

https://tburleson-layouts-demos.firebaseapp.com/#/grid

https://www.toptal.com/developers/css3maker/font-face.html
https://www.w3schools.com/css/css3_animations.asp
https://greensock.com/get-started/
https://stackblitz.com/edit/angular-parallax-effect?file=src%2Fapp%2Fparallax.directive.ts
https://betterprogramming.pub/build-beautiful-page-transitions-in-angular-28e40fa7e3bf
https://www.w3schools.com/howto/howto_css_parallax.asp
https://www.freecodecamp.org/news/beautiful-page-transitions-in-angular/
https://jasonwatmore.com/post/2019/11/04/angular-8-router-animation-tutorial-example




https://devkimchi.com/2020/07/01/5-ways-injecting-multiple-instances-of-same-interface-on-aspnet-core/

https://www.c-sharpcorner.com/blogs/register-generic-interface-in-asp-net-core
https://stackoverflow.com/questions/56143613/inject-generic-interface-in-net-core

https://www.stevejgordon.co.uk/asp-net-core-dependency-injection-how-to-register-generic-types
https://www.stevejgordon.co.uk/asp-net-core-dependency-injection-registering-multiple-implementations-interface



https://www.meziantou.net/avoid-performance-issue-with-jsonserializer-by-reusing-the-same-instance-of-json.htm
https://www.newtonsoft.com/json/help/html/Performance.htm
https://dotnetcoretutorials.com/2020/01/25/what-those-benchmarks-of-system-text-json-dont-mention/
https://marcroussy.com/2020/08/17/deserialization-with-system-text-json/
https://marcroussy.com/2020/11/02/serialization-with-system-text-json/

https://docs.microsoft.com/en-us/aspnet/core/web-api/route-to-code?view=aspnetcore-6.0#notable-missing-features-compared-to-web-api
https://www.learnrazorpages.com/razor-pages/routing
LinkGenerator 
https://www.stevejgordon.co.uk/asp-net-core-dependency-injection-registering-multiple-implementations-interface



https://asusualcoding.wordpress.com/2020/09/25/how-to-register-generic-interfaces-in-net-core/
https://blog.rsuter.com/dotnet-dependency-injection-way-to-work-around-missing-named-registrations/
https://dejanstojanovic.net/aspnet/2018/december/registering-multiple-implementations-of-the-same-interface-in-aspnet-core/
https://devkimchi.com/2020/07/01/5-ways-injecting-multiple-instances-of-same-interface-on-aspnet-core/







-----------------------

https://github.com/ekalinin/nodeenv

https://blog.logrocket.com/build-native-ui-components-react-native/

Nuxt - The Intuitive Vue Framework
https://developer.okta.com/blog/2020/01/27/best-nodejs-testing-tools

https://testing-library.com/docs/
https://medium.com/swlh/the-easiest-way-to-start-using-swagger-in-node-js-54326864e74f
https://rapidapi.com/blog/build-rest-api-node-js/
http://restify.com/docs/home/
https://github.com/swagger-api/swagger-codegen

https://www.atdatabases.org/blog/2021/02/05/node-js-in-memory-database



------------------

logEvent.Properties.GetValueOrDefault("SourceContext").ToString();


251394

az group list | convertfrom-json | select name
az appservice plan list | convertfrom-json | select name
az webapp list | convertfrom-json | select name

az webapp create -g telera-resource-group -p telera-app-service-plan -n mini-tools

az webapp list-runtimes 

az webapp up --name mini-tools


az webapp up --sku F1 --name MyFirstAzureWebApp --os-type Windows

az webapp up --sku F1 --name mini-tools-app --os-type Windows --runtime "DOTNET:6.0" --resource-group telera-resource-group --plan telera-app-service-plan --location southeastasia

az webapp deploy --name mini-tools-app --resource-group telera-resource-group --src-path deploy.zip --type zip

az webapp create --name mini-tools --plan telera-app-service-plan --resource-group telera-resource-group --runtime "DOTNET:6.0"


D:\src\github

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0#automatically-log-scope-with-spanid-traceid-parentid-baggage-and-tags
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-logging/?view=aspnetcore-6.0
https://thecodeblogger.com/2021/05/13/configure-logging-using-appsettings-json-in-net-applications/

https://docs.microsoft.com/en-us/aspnet/mvc/overview/security/
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
https://bytelanguage.net/2021/07/30/net-6-jwt-authentication-in-minimal-web-api/


https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-6.0
https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
https://bradwilson.typepad.com/blog/2010/01/input-validation-vs-model-validation-in-aspnet-mvc.html

https://andrewlock.net/preventing-mass-assignment-or-over-posting-in-asp-net-core/
https://stackoverflow.com/questions/37854866/how-to-get-the-id-of-a-document-using-c-sharp-driver-mongodb/37855645
https://github.com/serilog/serilog-extensions-logging/issues/117
https://lurumad.github.io/problem-details-an-standard-way-for-specifying-errors-in-http-api-responses-asp.net-core

https://codeopinion.com/

https://stackoverflow.com/questions/937668/how-do-i-support-etags-in-asp-net-mvc
https://stackoverflow.com/questions/937668/how-do-i-support-etags-in-asp-net-mvc
https://stackoverflow.com/questions/937668/how-do-i-support-etags-in-asp-net-mvc
https://www.singular.co.nz/2007/12/shortguid-a-shorter-and-url-friendly-guid-in-c-sharp/
https://www.singular.co.nz/2007/12/shortguid-a-shorter-and-url-friendly-guid-in-c-sharp/
https://www.singular.co.nz/2007/12/shortguid-a-shorter-and-url-friendly-guid-in-c-sharp/
https://codeopinion.com/
https://codeopinion.com/


4,294,967,296
2,147,483,647

https://methodpoet.com/free-resharper-alternatives/
https://github.com/JosefPihrt/Roslynator
https://github.com/DotNetAnalyzers/StyleCopAnalyzers
https://github.com/SonarSource/sonarlint-visualstudio
https://github.com/code-cracker/code-cracker
https://github.com/codecadwallader/codemaid
https://github.com/security-code-scan/security-code-scan

https://visualstudiomagazine.com/articles/2021/05/07/testing-tools.aspx

	https://www.zeta-resource-editor.com/index.html

dotnet watch dotcover test  --dcReportType=HTML --dcOutput=dotcover.html  --project .\Dn6Poc.DocuMgmtPortal.Tests\
