using BethanysPieShopHRM.App.Models;
using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeOverview
    {
        public List<Employee>? Employees = new List<Employee>();

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }


        // you can use a private field or a public prop to send data to UI.
        private string _title = "This is the employee overview page.";
        private string _dateTime = DateTime.Now.ToString("F");
        private string _btnMessage = string.Empty;
        private string _triggerMsg = string.Empty;
        private string _pageTitle = "Foo";
        private Employee? _selectedEmployee;

        // override to override the **default** razor component life-cycle OnInitialized method.
        protected override async Task OnInitializedAsync()
        {
            //base.OnInitialized();
            //Employees = MockDataService.Employees;


            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();

        }

        // demo of using a DOM event handler like onclick
        private void UpdateDateTime()
        {
            _dateTime = DateTime.Now.ToString("F");
        }

        // MouseEventArgs automatically passed for OnClick
        private void UpdateBtnMessage(MouseEventArgs e, int buttonNumber)
        {
            _btnMessage = $"button {buttonNumber} was clicked";
        }

        private void UpdateMessageOnOverview(MouseEventArgs e)
        {
            _triggerMsg = "Parent method was triggered";
        }

        // an example of passing in multiple args
        private void MultipleArgsTest(MouseEventArgs e, string arg)
        {
            _triggerMsg = arg;
        }

        public void ShowQuickViewPopUpFromOverview(Employee selectedEmployee)
        {
            _selectedEmployee = selectedEmployee;
        }
    }
}
