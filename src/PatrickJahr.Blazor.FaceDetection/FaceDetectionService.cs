using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace PatrickJahr.Blazor.FaceDetection;
public class FaceDetectionService : IAsyncDisposable
{
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

    public FaceDetectionService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/PatrickJahr.Blazor.FaceDetection/PatrickJahr.Blazor.FaceDetection.js"));
    }

    public async Task<bool> IsSupportedAsync()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task<int> DetectFacesAsync(ElementReference element)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<int>("detectFaces", element);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}