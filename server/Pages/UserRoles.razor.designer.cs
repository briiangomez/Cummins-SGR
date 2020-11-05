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
    public partial class UserRolesComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.SgiCore.UserRole> grid0;

        IEnumerable<Sgi.Models.SgiCore.UserRole> _getUserRolesResult;
        protected IEnumerable<Sgi.Models.SgiCore.UserRole> getUserRolesResult
        {
            get
            {
                return _getUserRolesResult;
            }
            set
            {
                if (!object.Equals(_getUserRolesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUserRolesResult", NewValue = value, OldValue = _getUserRolesResult };
                    _getUserRolesResult = value;
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
            var sgiCoreGetUserRolesResult = await SgiCore.GetUserRoles();
            getUserRolesResult = sgiCoreGetUserRolesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddUserRole>("Add User Role", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.SgiCore.UserRole args)
        {
            var dialogResult = await DialogService.OpenAsync<EditUserRole>("Edit User Role", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteUserRoleResult = await SgiCore.DeleteUserRole(data.Id);
                    if (sgiCoreDeleteUserRoleResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteUserRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete UserRole");
            }
        }
    }
}
