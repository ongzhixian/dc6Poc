# Powershell script to demostrate black box testing

Describe 'Test /api/user'  {

    $base_url = " http://localhost:7071"
    
    $path = "/api/AppUser"

    Context "HTTP POST $base_url$path" {
        
        It "should get a HTTP 201 (Created) on create AppUser record on first call" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "asd";
                "password" = "testpassw0rd"
            } | ConvertTo-Json -Compress

            # Act
            $response = Invoke-WebRequest -Method 'Post' -Uri $url -Headers $headers -Body $body

            # Assert(s)
            $response.StatusCode | Should Be 201 # HTTP 201 Created
            Write-Host "Response: ", $response, $response.StatusCode
        }

        It "should get HTTP 200 on second call" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "testuser";
                "password" = "testpassw0rd"
            } | ConvertTo-Json -Compress

            # Act
            $response = Invoke-WebRequest -Method 'Post' -Uri $url -Headers $headers -Body $body

            # Assert(s)
            $response.StatusCode | Should Be 200
            Write-Host "Response: ", $response, $response.StatusCode
        }
    }

    $path = "/api/AppUser/61b1a7fcf66c04dfe1a830d9"

    Context "HTTP GET $base_url$path" {
        
        It "should get HTTP 200 for existing AppUser record" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            # Act
            $response = Invoke-WebRequest -Method 'Get' -Uri $url -Headers $headers

            # Assert(s)
            $response.StatusCode | Should Be 200
            Write-Host "Response: ", $response, $response.StatusCode

        }
    }

    $path = "/api/AppUser/61b1a7fcf66c04dfe1a830d8"

    Context "HTTP GET $base_url$path" {
        
        It "should get HTTP 404 for non-existing AppUser record" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            # # Act
            # $response = Invoke-WebRequest -Method 'Get' -Uri $url -Headers $headers -SkipHttpErrorCheck

            # # Assert(s)
            # $response.StatusCode | Should Be 404
            # Write-Host "Response: ", $response, $response.StatusCode

            {
                Invoke-WebRequest -Method Get -Uri $url -Headers $headers
            } | Should Throw "Response status code does not indicate success: 404 (Not Found)."

        }
    }

    $path = "/api/AppUser"

    Context "HTTP PUT $base_url$path" {
        
        It "should return HTTP 200 when existing AppUser record is updated" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "testuser";
                "password" = "changedPassword"
            } | ConvertTo-Json -Compress

            # Act
            # $response = Invoke-RestMethod -Method 'Post' -Uri $url -Headers $headers -Body $body
            # The $response object from Invoke-WebRequest is more descriptive (for example, it has the status code)
            $response = Invoke-WebRequest -Method 'Put' -Uri $url -Headers $headers -Body $body

            # Assert(s)
            $response.StatusCode | Should Be 200
            # $response.Length | Should BeGreaterThan  0
            Write-Host "Response: ", $response, $response.StatusCode

            # Note: In newer versions of Pester, there's a BeGreaterOrEqual operator.
            # For list of assertions available in Pester 3.4 see: C:\Program Files\WindowsPowerShell\Modules\Pester\3.4.0\Functions\Assertions            
        }

        It "should return HTTP 404 if it does not exists" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "testuserxx";
                "password" = "changedPassword"
            } | ConvertTo-Json -Compress

            {
                Invoke-WebRequest -Method 'Put' -Uri $url -Headers $headers -Body $body
            } | Should Throw "Response status code does not indicate success: 404 (Not Found)."
        }
    }

    $path = "/api/AppUser"

    Context "HTTP DELETE $base_url$path" {

        It "should return HTTP 404 if record does NOT exists" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "asddd";
            } | ConvertTo-Json -Compress

            {
                Invoke-WebRequest -Method 'Delete' -Uri $url -Headers $headers -Body $body
            } | Should Throw "Response status code does not indicate success: 404 (Not Found)."
        }
        
        It "should return HTTP 200 if it exists" {
            # Arrange
            $url = "$base_url$path"
            $headers = @{
                "Content-Type" = "application/json"
            }

            $body = @{
                "username" = "asd";
            } | ConvertTo-Json -Compress

            # Act
            $response = Invoke-WebRequest -Method 'Delete' -Uri $url -Headers $headers -Body $body

            # Assert(s)
            $response.StatusCode | Should Be 200
            Write-Host "Response: ", $response, $response.StatusCode
        }
    }

}

