using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Thinktecture.Blazor.WebShare;
using Thinktecture.Blazor.WebShare.Models;

namespace Thinktecture.Blazor.Sample.Pages
{
    public partial class Index
    {
        [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] private WebShareService _webShareService { get; set; } = default!;

        private IJSObjectReference? _module;

        private bool _isWebShareSupported = false;
        private bool _canShareBasicData = false;
        private bool _canShareFileData = false;

        protected override async Task OnInitializedAsync()
        {
            _isWebShareSupported = await _webShareService.IsSupportedAsync();
            _canShareBasicData = await _webShareService.CanShareAsync(new WebShareDataModel
            {
                Title = "Test 1",
                Text = "Lorem ipsum dolor...",
                Url = "https://thinktecture.com"
            });            
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/Index.razor.js");
                var file = await _module.InvokeAsync<IJSObjectReference>("generateSampleFile");
                _canShareFileData = await _webShareService.CanShareAsync(new WebShareDataModel
                {
                    Files = new[] {file}
                });

                await InvokeAsync(StateHasChanged);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task ShareData()
        {
            var file = await _module.InvokeAsync<IJSObjectReference>("generateSampleFile");
            await _webShareService.ShareAsync(new WebShareDataModel
            {
                Title = "Test 1",
                Text = "Lorem ipsum dolor...",
                Url = "https://thinktecture.com",
                Files = new[] { file }
            });
        }
    }
}