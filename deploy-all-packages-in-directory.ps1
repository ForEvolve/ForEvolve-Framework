param(
    [Alias('k')]
    [string]$Key,
    [Alias('s')]
    [string]$sourceUri = 'https://api.nuget.org/v3/index.json '
) 

Get-ChildItem . -filter *.nupkg | Foreach-Object { 
    dotnet nuget push $_.FullName -k $Key -s $sourceUri
}