using BethanysPieShopHRM.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BethanysPieShopHRM.ComponentLibrary
{
    public partial class Map2
    {
        public string ElementId = $"map-{Guid.NewGuid():D}";

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public int Zoom { get; set; }

        [Parameter]
        public List<Marker> Markers { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("deliveryMap.showOrUpdate", ElementId, Markers);
        }
    }
}
