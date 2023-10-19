using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class Counter
    {
        // example of child component using a method name / delegate of a parent
        // triggering a parent event

        [Parameter]
        public EventCallback<MouseEventArgs> UpdateOverviewMessage { get; set; }
    }
}
