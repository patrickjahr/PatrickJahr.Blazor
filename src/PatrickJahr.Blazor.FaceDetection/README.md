# PatrickJahr.Blazor.FaceDetection

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/PatrickJahr.Blazor.FaceDetection?label=NuGet%20Downloads)](https://www.nuget.org/packages/PatrickJahr.Blazor.FaceDetection/)

## Introduction

A Blazor wrapper for the [Face Detection API](https://wicg.github.io/shape-detection-api/#face-detection-api).

The Face Detection API is a part of the [Shape Detection API](https://wicg.github.io/shape-detection-api) and enables the detection of faces from an image.

## Getting started

### Prerequisites

You need .NET 7.0 or newer to use this library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)
[Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

### Platform support

This is a preview image. If you want to use it in chome, you have to enable the experimantel features.
TBD

### Installation

You can install the package via NuGet using the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package PatrickJahr.Blazor.FaceDetection
```

## Usage

The package can be utilized in Blazor projects.

### Add to service collection

To make the FaceDetection available on all pages, register it in the `IServiceCollection` in `Program.cs` before the host is built:

```csharp
builder.Services.AddFaceDetectionService();
```

### Checking for Browser Support

Before utilizing the Face Detection API, you should first verify if the API is supported on the target platform by calling the `IsSupportedAsync()` method. This method returns a boolean indicating whether the Face Detection API is supported or not.

```csharp
var isSupported = await faceDetectionService.IsSupportedAsync();
if (isSupported)
{
    // enable badging feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```

### Detect Face

To detect a Face, simply call the `DetectFacesAsync` method of the FaceDetectionService class, which can be added to your component via Dependency Injection. To invoke the method, you need the following parameters:

- First, we need a picture or video to detect a Face:
  -- `IJSObjectReference` file: An instance of a blob or ImageBitmap, for example.
  -- `ElementReference` file: An instance of an HTML element like img.

  The result is a list of detected faces. You get information where the face was detected on the given image.

## Related articles

TBD

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official PatrickJahr product.
