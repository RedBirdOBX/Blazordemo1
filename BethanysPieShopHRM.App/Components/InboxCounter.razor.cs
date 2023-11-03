using BethanysPieShopHRM.App.Models;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components
{
    public partial class InboxCounter
    {
        public int MessageCount { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }


        protected override void OnInitialized()
        {
            MessageCount = 47; 
            ApplicationState.NumberOfMessages = MessageCount;
        }
    }
}
