using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloazorSample.Components
{
    public class ConfirmModelBoxBase : ComponentBase
    {
        //[Parameter] // You don't need 
        public bool ShowConfirmationBox { get; set; }

        //[Parameter] // You don't need
        //public EventCallback<bool> ShowConfirmationBoxChanged { get; set; }


        public void Show()
        {
            ShowConfirmationBox = true;

            StateHasChanged();
        }
    }
}
