Describe "Demo" -Tags @("test", "example") {
    Context "asd" {
        It "asd" {
            1 | Should Be 1
        }
    }
}

Describe "Demo2" -Tags @("life", "example") -Skip {
    Context "asd" {
        It "asd" {
            1 | Should Be 1
        }
    }
}

