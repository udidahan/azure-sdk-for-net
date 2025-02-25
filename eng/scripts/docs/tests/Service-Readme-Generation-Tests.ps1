<#
```
Invoke-Pester -Output Detailed $PSScriptRoot/Service-Readme-Generation-Tests.ps1

We are testing the Get-dotnet-OnboardedDocsMsPackagesForMoniker function in Docs-ToC.sp1
```
#>

Import-Module Pester

BeforeAll {
    . $PSScriptRoot/../../../common/scripts/common.ps1
}

# Test plan:
# 1. Tests on sucessfully building the onboarding package and its package info map.
# 2. Tests on passing error csv file.
# 3. Tests on missing metadata json file.
Describe "Get-OnboardedDocsMsPackagesForMoniker" -Tag "UnitTest" {
    # Passed cases
    It "Build the map of onboarding package to package info" -TestCases @(
        @{ DocRepoLocation = "$PSScriptRoot/inputs"; moniker="latest" }
        @{ DocRepoLocation = "$PSScriptRoot/inputs"; moniker="preview" }
    ) {
        $onboardingPackages = Get-dotnet-OnboardedDocsMsPackagesForMoniker -DocRepoLocation $DocRepoLocation -moniker $moniker
        foreach ($package in $onboardingPackages.GetEnumerator()) {
            $package.Key | Should -Be $package.Value.Name
            (ConvertTo-Json $package.Value) | Should -Be (Get-Content "$DocRepoLocation/metadata/$moniker/$($package.Key).json" -Raw)
        }
    }

    # Failed cases
    It "Failed to parse package info" -TestCases @(
        @{ DocRepoLocation = "$PSScriptRoot/inputs/exceptions"; moniker="latest" }
        @{ DocRepoLocation = "$PSScriptRoot/inputs/exceptions"; moniker="preview" }
    ) {
        $onboardingPackages = Get-dotnet-OnboardedDocsMsPackagesForMoniker -DocRepoLocation $DocRepoLocation -moniker $moniker 2> $null
        $onboardingPackages.Value | Should -BeNullOrEmpty
    }
}
