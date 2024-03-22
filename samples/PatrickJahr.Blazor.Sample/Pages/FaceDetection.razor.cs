using System.Globalization;
using Microsoft.AspNetCore.Components;
using PatrickJahr.Blazor.FaceDetection;

namespace PatrickJahr.Blazor.Sample.Pages;

public partial class FaceDetection
{
    private FaceDetectionResult[] _faces = Array.Empty<FaceDetectionResult>();
    private readonly List<string> _facesStyle = new();

    private ElementReference? _image;
    private bool _isFaceDetectionSupported;
    [Inject] private FaceDetectionService _faceDetectionService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _isFaceDetectionSupported = await _faceDetectionService.IsSupportedAsync();
        await base.OnInitializedAsync();
    }

    private async Task DetectFaces()
    {
        if (_image.HasValue)
        {
            _faces = await _faceDetectionService.DetectFacesAsync(_image.Value);
            foreach (var face in _faces)
                _facesStyle.Add(
                    $"width: {face.BoundingBox.Width!.Value.ToString(CultureInfo.InvariantCulture)}px; height: {face.BoundingBox.Height!.Value.ToString(CultureInfo.InvariantCulture)}px; top: {face.BoundingBox.Top!.Value.ToString(CultureInfo.InvariantCulture)}px; left: {face.BoundingBox.Left!.Value.ToString(CultureInfo.InvariantCulture)}px");
            await InvokeAsync(StateHasChanged);
        }
    }
}