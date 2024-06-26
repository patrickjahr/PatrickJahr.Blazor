using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PatrickJahr.Blazor.BarcodeDetection;

/// <summary>
/// Represents a service for barcode detection.
/// </summary>
public class BarcodeDetectionService
{
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeDetectionService"/> class.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime.</param>
    public BarcodeDetectionService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/PatrickJahr.Blazor.BarcodeDetection/PatrickJahr.Blazor.BarcodeDetection.js"));
    }

    /// <summary>
    /// Checks if barcode detection is supported.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating if barcode detection is supported.</returns>
    public async Task<bool> IsSupportedAsync()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    /// <summary>
    /// Gets the supported barcode formats.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an array of strings representing the supported barcode formats.</returns>
    public async Task<string[]> GetSupportedFormatsAsync()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string[]>("supportedFormats");
    }

    /// <summary>
    /// Detects barcodes in the specified file.
    /// </summary>
    /// <param name="file">JavaScript reference</param>
    /// <param name="formats">Supported barcode formats.</param>
    /// <returns></returns>
    public async Task<string[]> DetectCode(IJSObjectReference file, string[] formats)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string[]>("detectCode", file, formats);
    }

    /// <summary>
    /// Detects barcodes in the specified file.
    /// </summary>
    /// <param name="file">HTML Element reference</param>
    /// <param name="formats">Supported barcode formats.</param>
    /// <returns></returns>
    public async Task<string[]> DetectCode(ElementReference file, string[] formats)
    {
        var module = await _moduleTask.Value;
        var result = await module.InvokeAsync<string[]>("detectCode", file, formats);
        return result;
    }

    /// <summary>
    /// Disposes the resources used by the barcode detection service.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}