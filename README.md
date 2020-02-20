uploadcare-dotnet
===============

[![Build Status](https://okolobaxa.visualstudio.com/uploadcare-dotnet/_apis/build/status/okolobaxa.uploadcare-dotnet?branchName=master)](https://okolobaxa.visualstudio.com/uploadcare-dotnet/_build/latest?definitionId=1&branchName=master)
[![NuGet version](http://img.shields.io/nuget/v/UploadcareCSharp.svg)](https://www.nuget.org/packages/UploadcareCSharp/)&nbsp;

C# library for Uploadcare. Uploadcare is a content delivery platform that optimizes web performance for developers, startups and large enterprises.

Supported features:

- Full Uploadcare API v0.5 (file, group, project and webhook, face detection)
- CDN path builder
- File uploads from disk, byteArray, and URL
- Signed uploads
- Simple and signed auth

TODO:
- Multi-part uploads
- Throttling

## Nuget
Latest stable version is available from [NuGet Gallery](https://www.nuget.org/packages/UploadcareCSharp/)

## Installation

Using the [.NET Core command-line interface (CLI) tools][dotnet-core-cli-tools]:

```sh
dotnet add package UploadcareCSharp
```

Using the [NuGet Command Line Interface (CLI)][nuget-cli]:

```sh
nuget install UploadcareCSharp
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package UploadcareCSharp
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on *Manage NuGet Packages...*
4. Click on the *Browse* tab and search for "UploadcareCSharp".
5. Click on the UploadcareCSharp package, select the appropriate version in the
   right-tab and click *Install*.


## Examples
### Basic API Usage

```csharp
var client = UploadcareClient.DemoClientWithSignedAuth();

var project = await client.Projects.GetAsync();
var file = await client.Files.GetAsync("85b5644f-e692-4855-9db0-8c5a83096e25");
```

### Building CDN URLs

```csharp
var file = await client.Files.GetAsync("85b5644f-e692-4855-9db0-8c5a83096e25");
var builder = new CdnPathBuilder(file)
        .ResizeWidth(200)
        .CropCenter(200, 200)
        .Grayscale();
        
var url = builder.Build();
```
### File uploads

```csharp
var file = new FileInfo("Lenna.png");

try
{
  var uploader = new FileUploader(client);
  var result = await uploader.Upload(file);
  
  Console.Writeline(result.FileId);
} 
catch (UploadFailureException ex) 
{
    Console.Writeline("Upload failed :(");
}
```
For any requests, bug or comments, please [open an issue][issues] or [submit a pull request][pulls].

[issues]: https://github.com/okolobaxa/uploadcare-dotnet/issues/new
[nuget-cli]: https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference
[package-manager-console]: https://docs.microsoft.com/en-us/nuget/tools/package-manager-console
[pulls]: https://github.com/okolobaxa/uploadcare-dotnet/pulls
[dotnet-core-cli-tools]: https://docs.microsoft.com/en-us/dotnet/core/tools/
