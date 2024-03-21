using Microsoft.AspNetCore.Components;
using PatrickJahr.Blazor.FaceDetection;

namespace PatrickJahr.Blazor.Sample.Pages
{
    public partial class FaceDetection
    {
        [Inject] private FaceDetectionService _faceDetectionService { get; set; } = default!;

        private ElementReference? _image;
        private bool _isFaceDetectionSupported;

        protected override async Task OnInitializedAsync()
        {
            _isFaceDetectionSupported = await _faceDetectionService.IsSupportedAsync();
            await base.OnInitializedAsync();
        }

        private async Task DetectFaces()
        {
            if (_image.HasValue)
            {
                var count = await _faceDetectionService.DetectFacesAsync(_image.Value);
                Console.WriteLine($"{count} faces detected");
            }
        }
    }
}