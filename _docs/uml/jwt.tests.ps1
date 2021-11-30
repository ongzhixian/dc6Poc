# Powershell script to demostrate black box testing
$token = ""
Describe 'Test /jwt/token/sign' {

    $base_url = "http://localhost:7200"
    
    Context "HTTP GET $base_url/jwt/token/sign" {
        
        It "should return a JWT" {
            # Arrange
            $url = "$base_url/jwt/token/sign"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "testuser";
                "password" = "testpassw0rd"
            } | ConvertTo-Json -Compress

            # Act
            # $response = Invoke-RestMethod -Method 'Post' -Uri $url -Headers $headers -Body $body
            # The $response object from Invoke-WebRequest is more descriptive (for example, it has the status code)
            $response = Invoke-WebRequest -Method Get -Uri $url -Headers $headers

            # Assert(s)
            $response.StatusCode | Should Be 200
            $token = $response.Content
            Write-Host $token
            # $response.Length | Should BeGreaterThan  0
            # Write-Host "Response: ", $response, $response.StatusCode
            

            # Note: In newer versions of Pester, there's a BeGreaterOrEqual operator.
            # For list of assertions available in Pester 3.4 see: C:\Program Files\WindowsPowerShell\Modules\Pester\3.4.0\Functions\Assertions            
        }
    }

    Context "HTTP GET $base_url/country" {
        
        It "should return a list of countries" {
            # Arrange
            

            $url = "$base_url/country"
            $headers = @{
                "Content-Type" = "application/json"
                "Authorization" = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiTXkgTmFtZSIsImlkIjoiMTIzNCIsImlhdCI6MTYzODI1NDQyOCwiZXhwIjoxNjM4MjU1MzI4fQ.kLj4FBnJqiysiqWHtinh1lpmfLxWxHz9NPpGb54njt0"
            }

            $body = @{
                "username" = "testuser";
                "password" = "testpassw0rd"
            } | ConvertTo-Json -Compress

            # Act
            Write-Host "TOKEN [$token]"
            $response = Invoke-WebRequest -Method Get -Uri $url -Headers $headers

            # Assert(s)
            $response.StatusCode | Should Be 200
            
        }
    }

    
    Context "HTTP GET non-existing URL ($base_url/nonExistingUrl)" {
        
        It "should return HTTP 401 (Unauthorized)" {
            # Arrange
            $url = "$base_url/nonExistingUrl"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "testuser";
                "password" = "testpassw0rd"
            } | ConvertTo-Json -Compress
            # $responseStatusCode = 0
            

            # Act
            # Invoke-WebRequest will throw an exception if the URL doesn't exist
            
            # Method 1: Use try-catch
            # try {
            #     $response = Invoke-WebRequest -Method Get -Uri $url -Headers $headers    
            # }
            # catch {
            #     $responseStatusCode = $_.Exception.Response.StatusCode.Value__
            # }

            # Method 1: Assert(s)
            # $responseStatusCode | Should Be 404

            # Method 2: (PS7+ only): Using `-SkipHttpErrorCheck` will skip the error check and return the response object
            # $response = Invoke-WebRequest -Method Get -Uri $url -Headers $headers -SkipHttpErrorCheck

            # Method 2: Assert(s)
            # $response.StatusCode | Should Be 404

            # Method 3: Should Throw
            # {
            #     Invoke-WebRequest -Method Get -Uri $url -Headers $headers
            # } | Should Throw "Response status code does not indicate success: 404 (Not Found)."

            {
                Invoke-WebRequest -Method Get -Uri $url -Headers $headers
            } | Should Throw "Response status code does not indicate success: 401 (Unauthorized)."

        
        }
    }
}