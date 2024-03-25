# PatrickJahr.Blazor.BarcodeDetection

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/PatrickJahr.Blazor.BarcodeDetection?label=NuGet%20Downloads)](https://www.nuget.org/packages/PatrickJahr.Blazor.BarcodeDetection/)

## Introduction

A Blazor wrapper for the [Barcode Detection API](https://wicg.github.io/shape-detection-api/#barcode-detection-api).

The Barcode Detection API is a part of the [Shape Detection API](https://wicg.github.io/shape-detection-api) and enables the detection of barcodes from an image or a video stream in various formats such as QR, Code 128, EAN-13, and many others.

## Getting started

### Prerequisites

You need .NET 7.0 or newer to use this library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)
[Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

### Platform support

[Platform support for Barcode Detection API](https://caniuse.com/mdn-api_barcodedetector)

### Installation

You can install the package via NuGet using the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package PatrickJahr.Blazor.BarcodeDetection
```

## Usage

The package can be utilized in Blazor projects.

### Add to service collection

To make the BarcodeDetection available on all pages, register it in the `IServiceCollection` in `Program.cs` before the host is built:

```csharp
builder.Services.AddBarcodeDetectionService();
```

### Checking for Browser Support

Before utilizing the Barcode Detection API, you should first verify if the API is supported on the target platform by calling the `IsSupportedAsync()` method. This method returns a boolean indicating whether the Barcode Detection API is supported or not.

```csharp
var isSupported = await barcodeDetectionService.IsSupportedAsync();
if (isSupported)
{
    // enable badging feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```

### Detect Barcode

To detect a barcode, simply call the `DetectCode` method of the BarcodeDetectionService class, which can be added to your component via Dependency Injection. To invoke the method, you need the following parameters:

- First, we need a picture or video to detect a barcode:
  -- `IJSObjectReference` file: An instance of a blob or ImageBitmap, for example
  -- `ElementReference` file: An instance of an HTML element like img or video
- Then, you can set your preferred formats with the `formats` parameter.
  The result is a list of `string` with the detected barcodes.

#### Supported Formats

To find out which formats are supported, simply call the `GetSupportedFormatsAsync` method, which returns a `string[]` with all the formats that are supported.

## Related articles

- [`detect()` Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/BarcodeDetector/detect)
- [`getSupportedFormats()` Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/BarcodeDetector/getSupportedFormats_static)
- [Blog post on web.dev](https://developer.chrome.com/docs/capabilities/shape-detection?hl=de)
- [Browser support on caniuse.com](https://caniuse.com/mdn-api_barcodedetector)

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official PatrickJahr product.
