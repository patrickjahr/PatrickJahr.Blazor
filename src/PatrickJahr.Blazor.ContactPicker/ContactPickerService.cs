using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PatrickJahr.Blazor.ContactPicker;

/// <summary>
/// Represents a service for barcode detection.
/// </summary>
public class ContactPickerService
{
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeDetectionService"/> class.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime.</param>
    public ContactPickerService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/PatrickJahr.Blazor.ContactPicker/PatrickJahr.Blazor.ContactPicker.js"));
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