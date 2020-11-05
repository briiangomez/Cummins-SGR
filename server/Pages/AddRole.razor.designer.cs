using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Sgi.Models.SgiCore;
using Microsoft.EntityFrameworkCore;

namespace Sgi.Pages
{
    public partial class AddRoleComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SgiCoreService SgiCore { get; set; }

        Sgi.Models.SgiCore.Role _role;
        protected Sgi.Models.SgiCore.Role role
        {
            get
            {
                return _role;
            }
            set
            {
                if (!object.Equals(_role, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "role", NewValue = value, OldValue = _role };
                    _role = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            role = new Sgi.Models.SgiCore.Role(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.Role args)
        {
            try
            {
                var sgiCoreCreateRoleResult = await SgiCore.CreateRole(role);
                DialogService.Close(role);
            }
            catch (System.Exception sgiCoreCreateRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Role!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
