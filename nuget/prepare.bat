cd ..
nuget restore FluentAssertions.Extension.Json.sln
msbuild FluentAssertions.Extension.Json.sln -p:Configuration="Release"
cd nuget
nuget pack FluentAssertions.Extension.Json.nuspec