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
    public partial class RolePermissionsComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.SgiCore.RolePermission> grid0;

        IEnumerable<Sgi.Models.SgiCore.RolePermission> _getRolePermissionsResult;
        protected IEnumerable<Sgi.Models.SgiCore.RolePermission> getRolePermissionsResult
        {
            get
            {
                return _getRolePermissionsResult;
            }
            set
            {
                if (!object.Equals(_getRolePermissionsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getRolePermissionsResult", NewValue = value, OldValue = _getRolePermissionsResult };
                    _getRolePermissionsResult = value;
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
            var sgiCoreGetRolePermissionsResult = await SgiCore.GetRolePermissions();
            getRolePermissionsResult = sgiCoreGetRolePermissionsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddRolePermission>("Add Role Permission", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.SgiCore.RolePermission args)
        {
            var dialogResult = await DialogService.OpenAsync<EditRolePermission>("Edit Role Permission", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteRolePermissionResult = await SgiCore.DeleteRolePermission(data.Id);
                    if (sgiCoreDeleteRolePermissionResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteRolePermissionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete RolePermission");
            }
        }
    }
}
