using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PatrickJahr.Blazor.BarcodeDetection;

namespace PatrickJahr.Blazor.Sample.Pages;
public partial class BarcodeDetection 
{
    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
    [Inject] private BarcodeDetectionService _barcodeDetectionService { get; set; } = default!;

    private ElementReference? _barcode;
    private IJSObjectReference? _module;
    private bool _isSupported;
    private string[] _barcodes = Array.Empty<string>();
    private string result = String.Empty;

    protected override async Task OnInitializedAsync()
    {
        _isSupported = await _barcodeDetectionService.IsSupportedAsync();
        if (_isSupported) {
            _barcodes = await _barcodeDetectionService.GetSupportedFormatsAsync();
        }
        _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/BarcodeDetection.razor.js");
        await base.OnInitializedAsync();
    }

    private async Task DetectBarCodes()
    {
        var barcodes = new List<string>();
        if (_barcode is not null)
        {
            var codes = await _barcodeDetectionService.DetectCodeByElement(_barcode.Value, _barcodes);
            barcodes.AddRange(codes);
        }
        else
        {
            _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/BarcodeDetection.razor.js");

            var blobPromise = await _module.InvokeAsync<IJSObjectReference>("getBarcodeBlobPromise");
            var codes = await _barcodeDetectionService.DetectCode(blobPromise, _barcodes);
            barcodes.AddRange(codes);
        }
        Console.WriteLine(barcodes);
        result = barcodes.Count > 0 ? string.Join(",", barcodes) : "No barcodes found";
        Console.WriteLine(result);
        StateHasChanged();
    }
}