uploadcare-csharp
===============

[![Build Status](https://travis-ci.org/okolobaxa/uploadcare-csharp.svg)](https://travis-ci.org/okolobaxa/uploadcare-csharp)[![Build status](https://ci.appveyor.com/api/projects/status/4fa72wwet494g2e5/branch/master?svg=true)](https://ci.appveyor.com/project/okolobaxa/uploadcare-csharp/branch/master)

This is a C# library for Uploadcare.

Supported features:

- Part of file and project API v0.3
- Paginated resources fetched as `List<T>`
- CDN path builder
- File uploads from disk, byteArray, and URL

## Nuget
Latest stable version is available from [NuGet Gallery](https://www.nuget.org/packages/UploadcareCSharp/)

## Examples
### Basic API Usage

```csharp
Client client = new Client("publickey", "privatekey");
Project project = client.GetProject();
Collaborator owner = project.GetOwner();
```

### Building CDN URLs

```csharp
UploadcareFile file = client.GetFile("85b5644f-e692-4855-9db0-8c5a83096e25");
CdnPathBuilder builder = file.CdnPath()
        .ResizeWidth(200)
        .CropCenter(200, 200)
        .Grayscale();
Uri url = Urls.Cdn(builder);
```
### File uploads

```csharp
Client client = Client.DemoClient();
var file = new FileInfo("test.png");

try
{
  var uploader = new FileUploader(_client, file);
  var result = uploader.Upload(EStoreType.DoNotStore);
  Console.Writeline(result.FileId);
} 
catch (UploadFailureException ex) 
{
    Console.Writeline("Upload failed :(");
}
```
