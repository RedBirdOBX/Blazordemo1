using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages.PlugIns
{
    public partial class Message
    {
        [Parameter]
        public string MessageContent { get; set; }
    }
}
