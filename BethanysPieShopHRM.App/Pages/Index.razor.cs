using BethanysPieShopHRM.App.Components.Widgets;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class Index
    {
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
    }
}
