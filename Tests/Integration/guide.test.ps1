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
