using BethanysPieShopHRM.App.Services;
using Microsoft.AspNetCore.Components;


namespace BethanysPieShopHRM.App.Components.Widgets
{
    public partial class EmployeeCountWidget
    {
        public int EmployeeCounter { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //EmployeeCounter = MockDataService.Employees.Count;

            int empCount = (await EmployeeDataService.GetAllEmployees()).Count();
            EmployeeCounter = empCount;
        }
    }
}
