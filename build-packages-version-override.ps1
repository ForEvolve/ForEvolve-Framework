Write-Verbose "SourceBranch: $($Env:Build_SourceBranch)"
Write-Verbose "SourceBranchName: $($Env:Build_SourceBranchName)"
Write-Verbose "PR PullRequestId: $($Env:System_PullRequest_PullRequestId)"
Write-Verbose "PR SourceBranch: $($Env:System_PullRequest_SourceBranch)"
Write-Verbose "PR TargetBranch: $($Env:System_PullRequest_TargetBranch)"
Write-Verbose "xMyBuildNumber: $($Env:xMyBuildNumber)"
Write-Verbose "xPackAdditionalBuildProperties: $($Env:xPackAdditionalBuildProperties)"

function Update-VersionNumbers($version) {
    $myBuildNumber = "$($version)-CI-$($Env:Build_BuildID)"
    Write-Host "##vso[task.setvariable variable=MyBuildNumber;]$myBuildNumber"
    Write-Host "##vso[task.setvariable variable=PackAdditionalBuildProperties;]PackageVersion=$myBuildNumber"
}

function Show-VersionNumbers() {
    Write-Output "MyBuildNumber: $Env:MyBuildNumber"
    # Write-Output "PackAdditionalBuildProperties: $Env:PackAdditionalBuildProperties"
}

Write-Output "Version number before starting"
Show-VersionNumbers

if ($Env:Build_Reason -eq "PullRequest") {
    $source = $Env:System_PullRequest_SourceBranch
    $target = $Env:System_PullRequest_TargetBranch

    $prefix = "release/"
    $prefixLength = $prefix.length
    if ($source.StartsWith($prefix)) {
        $v = $source.Substring($prefixLength)
        Write-Verbose "Updating version numbers to $v using source branch $source"
        Update-VersionNumbers $v
        Write-Output "Updated version numbers to $v using source branch $source"
    }
    elseif ($target.StartsWith($prefix)) {
        $v = $target.Substring($prefixLength)
        Write-Verbose "Updating version numbers to $v using target branch $target"
        Update-VersionNumbers $v
        Write-Output "Updated version numbers to $v using target branch $target"
    }
    
    Write-Output "Updated version number"
    Show-VersionNumbers
}