using BethanysPieShopHRM.App.Components.Widgets;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class Index
    {
        [Inject]
        public IConfiguration Config { get; set; }


        public Index()
        {
            // har coded but could be dynamic
            Widgets = new List<Type>
            {
                typeof(EmployeeCountWidget),
                typeof(InboxWidget)
            };
        }

        public List<Type> Widgets { get; set; }

        protected override Task OnInitializedAsync()
        {
            var ans = Config["Message"];

            return base.OnInitializedAsync();
        }
    }
}
