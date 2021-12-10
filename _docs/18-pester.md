# Pester

```powershell: Example in demo.test.ps1
Describe "Demo" -Tags @("test", "example") {
    Context "asd" {
        It "asd" {
            1 | Should Be 1
        }
    }
}

Describe "Demo2" -Tags @("life", "example") {
    Context "asd" {
        It "asd" {
            1 | Should Be 1
        }
    }
}
```

```powershell: -Skip is use in It-clauses !
Describe "HTTP GET $path" -Tags @("HTTP", "GET", $path) {
    Context "pass no parameters" {
        It "return a status code of 200" -Skip {
            1 | Should Be 1
        }
    }
}
```

```cmd:Output; note the exclamation mark - that denotes a skipped test
Describing HTTP GET /api/AppUser
   Context pass no parameters
    [!] return a status code of 200 16ms
```

## Running

Runs both test
invoke-pester .\demo.test.ps1 -TestName @("Demo","demo2")
invoke-pester .\demo.test.ps1 -Tag "example"
invoke-pester .\demo.test.ps1 -Tag @("test", "life")

Run "Demo"
invoke-pester .\demo.test.ps1 -Tag "test"
invoke-pester .\demo.test.ps1 -TestName "Demo"


## Guidance

1.  Describe    (the test scenario)         -- I am testing ... HTTP GET /country
2.  Context     (condition; when-clauses)   -- When I       ... do not pass parameters 
3.  It          (test; should-clauses)      -- It should    ... return a status code of 200

Describe "HTTP GET /country" -Tags @("HTTP", "GET", "/api/country") {
    Context "asd" {
        It "asd" {
            1 | Should Be 1
        }
    }
}

## Parameters

```powershell:method 1: path variable replacement
$path = "/api/AppUser/{id}"
$path = "/api/AppUser/61b1a7fcf66c04dfe1a830d9"
$url = "$base_url$path"
```

```powershell:method 2: placeholder replacement
$path = "/api/AppUser/{id}"
$url = "$base_url$path".Replace("{id}", "61b1a7fcf66c04dfe1a830d9")
```


```powershell:method 3: placeholder substitution; note the change from {id} to {0} (C# convention)
$path = "/api/AppUser/{0}"
$url = "$base_url$path" -f "61b1a7fcf66c04dfe1a830d9"
```


This does not work well; 
Just listing it here for reference.
Problem: Write-Host "/api/AppUser/$id" displays "/api/AppUser/" ) 
```powershell: interesting -- ExpandString (PowerShell string interpolation?)
$path = "/api/AppUser/$id"
$id = '61b1a7fcf66c04dfe1a830d9'
$string = $ExecutionContext.InvokeCommand.ExpandString($message)
```

# Full example

```
# 1.  Describe    (the test scenario)         -- I am testing ... HTTP GET /country
# 2.  Context     (condition; when-clauses)   -- When I       ... do not pass parameters 
# 3.  It          (test; should-clauses)      -- It should    ... return a status code of 200

# Rewrite of the AppUser API into a better standard.

$base_url = " http://localhost:7071"

# Tests
# POST   /api/AppUser
# GET    /api/AppUser
# PUT    /api/AppUser
# DELETE /api/AppUser

$path = "/api/AppUser"

Describe "HTTP POST $path" -Tags @("HTTP", "POST", $path) {
    Context "pass valid body" {

        $url = "$base_url$path"
        $headers = @{
            "Content-Type" = "application/json"
        }
        $body = @{
            "username" = "testuser";
            "password" = "testpassw0rd"
        } | ConvertTo-Json -Compress

        $response = Invoke-WebRequest -Method 'Post' -Uri $url -Headers $headers -Body $body

        It "return status code 201" {
            $response.StatusCode | Should Be 201 # HTTP 201 Created
        }
    }
}

$path = "/api/AppUser"

Describe "HTTP GET $path" -Tags @("HTTP", "GET", $path) {
    
    Context "pass no parameters" {

        # Arrange
        $url = "$base_url$path"
        $headers = @{
            "Content-Type" = "application/json"
        }

        # Act
        $response = { Invoke-WebRequest -Method 'Get' -Uri $url -Headers $headers }

        # Assert(s)
        It "return a status code of 404" {
            $response | Should Throw "Response status code does not indicate success: 404 (Not Found)."
            # Write-Host "Response: ", $response, $response.StatusCode
        }
    }
}

$path = "/api/AppUser/{id}"

Describe "HTTP GET $path" -Tags @("HTTP", "GET", $path) {

    Context "pass an existing id as parameter" {

        $path = "/api/AppUser/61b1a7fcf66c04dfe1a830d9"
        $url = "$base_url$path"
        $headers = @{
            "Content-Type" = "application/json"
        }

        $response = Invoke-WebRequest -Method 'Get' -Uri $url -Headers $headers
        
        It "return a status code of 200" {
            $response.StatusCode | Should Be 200
        }
    }

    Context "pass an non-existing id as parameter" {

        $path = "/api/AppUser/61b1a7fcf66c04dfe1a830d8"
        $url = "$base_url$path"
        $headers = @{
            "Content-Type" = "application/json"
        }

        $response = { Invoke-WebRequest -Method 'Get' -Uri $url -Headers $headers }
        
        It "return a status code of 404" {
            $response | Should Throw "Response status code does not indicate success: 404 (Not Found)."
        }
    }

}

```


```Unit tests with mocking
# 1.  Describe    (the test scenario)         -- I am testing ... HTTP GET /country
# 2.  Context     (condition; when-clauses)   -- When I       ... do not pass parameters 
# 3.  It          (test; should-clauses)      -- It should    ... return a status code of 200

# Rewrite of the AppUser API into a better standard.

$base_url = " http://localhost:7071"

Describe "HTTP GET $path" -Tags @("HTTP", "GET", $path) {

    Context "pass an existing id as parameter" {

        $path = "/api/AppUser/61b1a7fcf66c04dfe1a830d9"
        $url = "$base_url$path"
        $headers = @{
            "Content-Type" = "application/json"
        }

        Mock -CommandName Invoke-WebRequest -Verifiable -MockWith {
            return @{
                StatusCode = 200
            }
        } -ParameterFilter {
            $url -match $url -and $Method -eq "Get"
        }

        $response = Invoke-WebRequest -Method 'Get' -Uri $url -Headers $headers
        
        It "return a status code of 200" {
            $response.StatusCode | Should Be 200
        }
    }

    Context "pass an non-existing id as parameter" {

        $path = "/api/AppUser/61b1a7fcf66c04dfe1a830d8"
        $url = "$base_url$path"
        $headers = @{
            "Content-Type" = "application/json"
        }

        Mock -CommandName Invoke-WebRequest -Verifiable -MockWith {
            Throw "Response status code does not indicate success: 404 (Not Found)."
        } -ParameterFilter {
            $url -match $url -and $Method -eq "Get"
        }

        $response = { Invoke-WebRequest -Method 'Get' -Uri $url -Headers $headers }
        
        It "return a status code of 404" {
            $response | Should Throw "Response status code does not indicate success: 404 (Not Found)."
        }
    }

}

```


## Reference: HttpResponseMessage object (for mocking response)

Fields of response object:

System.Net.Http.HttpResponseMessage BaseResponse {get;set;}
string Content {get;}
System.Text.Encoding Encoding {get;}
System.Collections.Generic.Dictionary[string,System.Collections.Generic.IEnumerable[string]] Headers {get;}
Microsoft.PowerShell.Commands.WebCmdletElementCollection Images {get;}
Microsoft.PowerShell.Commands.WebCmdletElementCollection InputFields {get;}
Microsoft.PowerShell.Commands.WebCmdletElementCollection Links {get;}
string RawContent {get;set;}
long RawContentLength {get;}
System.IO.MemoryStream RawContentStream {get;}
System.Collections.Generic.Dictionary[string,string] RelationLink {get;}
int StatusCode {get;}
string StatusDescription {get;}

# Reference
https://powershellexplained.com/2017-01-13-powershell-variable-substitution-in-strings/
