﻿using BethanysPieShopHRM.App.Models;
using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared.Domain;
using BethanysPieShopHRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeDetail
    {
        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeDataService? EmployeeDataService { get; set; }

        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        protected override async Task OnInitializedAsync()
        {
            //Employee = MockDataService.Employees.Where(e => e.EmployeeId == int.Parse(EmployeeId)).FirstOrDefault();

            try
            {
                Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
                Employee.FirstName += "-foo";

                if (Employee.Latitude.HasValue && Employee.Longitude.HasValue)
                {
                    MapMarkers = new List<Marker>
                    {
                        new Marker()
                        {
                            Description = $"{Employee.FirstName} {Employee.LastName}",
                            ShowPopup = false,
                            X = Employee.Longitude.Value,
                            Y = Employee.Latitude.Value
                        }
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
