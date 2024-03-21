using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using KristofferStrube.Blazor.FileSystemAccess;
using KristofferStrube.Blazor.FileSystem;
using PatrickJahr.Blazor.BarcodeDetection;

namespace PatrickJahr.Blazor.Sample.Pages;
public partial class BarcodeDetection
{
    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
    [Inject] private BarcodeDetectionService _barcodeDetectionService { get; set; } = default!;
    [Inject] private IFileSystemAccessService _fileSystemAccessService { get; set; } = default!;

    private ElementReference? _barcode;
    private ElementReference? _video;
    private IJSObjectReference? _module;
    private bool _isSupported;
    private string[] _barcodes = Array.Empty<string>();
    private string result = String.Empty;
    private string videoResult = String.Empty;
    private string imageResult = String.Empty;
    private bool showVideo = false;

    private static FilePickerAcceptType[] _acceptedTypes = new FilePickerAcceptType[]
    {
        new FilePickerAcceptType
        {
            Accept = new Dictionary<string, string[]>
            {
                { "image/png", new[] {".png" } }
            }
        }
    };

    private OpenFilePickerOptionsStartInWellKnownDirectory _openFilePickerOptions = new OpenFilePickerOptionsStartInWellKnownDirectory
    {
        Multiple = false,
        StartIn = WellKnownDirectory.Pictures,
        Types = _acceptedTypes
    };

    protected override async Task OnInitializedAsync()
    {
        _isSupported = await _barcodeDetectionService.IsSupportedAsync();
        if (_isSupported)
        {
            _barcodes = await _barcodeDetectionService.GetSupportedFormatsAsync();
        }
        await base.OnInitializedAsync();
    }

    private async Task DetectBarCodes()
    {
        var barcodes = new List<string>();
        if (_barcode is not null)
        {
            var codes = await _barcodeDetectionService.DetectCode(_barcode.Value, _barcodes);
            barcodes.AddRange(codes);
            result = barcodes.Count > 0 ? string.Join(",", barcodes) : "No barcodes found";
            StateHasChanged();
        }
    }

    private async Task DetectContentsByVideo()
    {
        showVideo = true;
        await InvokeAsync(() => StateHasChanged());

        var barcodes = new List<string>();
        if (_video is not null)
        {
            var codes = await _barcodeDetectionService.DetectCode(_video.Value, _barcodes);
            barcodes.AddRange(codes);
            videoResult = barcodes.Count > 0 ? string.Join(",", barcodes) : "No content found";
            showVideo = false;
            await InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task SelectFileAndDetecBarcode()
    {
        FileSystemFileHandle? _fileHandle = null;
        try
        {
            var fileHandles = await _fileSystemAccessService.ShowOpenFilePickerAsync(_openFilePickerOptions);
            _fileHandle = fileHandles.Single();
        }
        catch (JSException ex)
        {
            // Handle Exception or cancelation of File Access prompt
            Console.WriteLine(ex);
        }
        finally
        {
            if (_fileHandle is not null)
            {
                var file = await _fileHandle.GetFileAsync();
                var codes = await _barcodeDetectionService.DetectCode(file.JSReference, _barcodes);
                imageResult = codes.Length > 0 ? string.Join(",", codes) : "No content found";
                await InvokeAsync(() => StateHasChanged());
                await file.JSReference.DisposeAsync();
                await _fileHandle.DisposeAsync();
            }

        }
    }
}