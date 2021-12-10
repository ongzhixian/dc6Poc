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
