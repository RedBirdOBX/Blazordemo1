using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components
{
    public partial class QuickViewPopUp
    {
        private Employee? _employee;

        [Parameter]
        public Employee? Employee { get; set; }

        protected override void OnParametersSet()
        {
            _employee = Employee;
        }

        public void Close()
        {
            _employee = null;
        }
    }
}


//The " = default!; " part of the code is just to remove the null warning check added in the newer versions of .NET.

//It's used mostly on properties that you are expecting to be injected into a component/class via [Inject] or [Parameter]. This is because you "know" that it will be provided by the .NET framework when a component is created, but since it's a property that's a class (eg - Employee vs int) and isn't explicitly set in a constructor, intellisense will mark it with a "null warning" since technically its default value when constructed is "null", we as the devs just "know" it will be injected though.

//That " = default!; " is shorthand for telling intellisense that "this is not expected to be default when created".