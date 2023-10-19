using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components
{
    public partial class EmployeeCard
    {
        public EmployeeCard()
        {
            Employee = new Employee();
        }

        [Parameter]
        public Employee Employee { get; set; }

        // callback : parent is employee overview
        // we will trigger the parent method from this child
        // will trigger the pop up
        [Parameter]
        public EventCallback<Employee> EmployeeQuickViewClickedFromCard { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void NavigateToDetails(Employee employee)
        {
            NavigationManager.NavigateTo($"employeedetail/{employee.EmployeeId}");
        }

        // life cycles
        protected override void OnInitialized()
        {
            //base.OnInitialized();

            if (string.IsNullOrEmpty(Employee.LastName))
            {
                throw new Exception("Last name cannot be empty");
            }
        }
    }
}
