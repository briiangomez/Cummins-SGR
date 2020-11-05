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
    public partial class EditRolePermissionComponent : ComponentBase
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

        [Parameter]
        public dynamic Id { get; set; }

        Sgi.Models.SgiCore.RolePermission _rolepermission;
        protected Sgi.Models.SgiCore.RolePermission rolepermission
        {
            get
            {
                return _rolepermission;
            }
            set
            {
                if (!object.Equals(_rolepermission, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rolepermission", NewValue = value, OldValue = _rolepermission };
                    _rolepermission = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Sgi.Models.SgiCore.Role> _getRolesForRoleIdResult;
        protected IEnumerable<Sgi.Models.SgiCore.Role> getRolesForRoleIdResult
        {
            get
            {
                return _getRolesForRoleIdResult;
            }
            set
            {
                if (!object.Equals(_getRolesForRoleIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getRolesForRoleIdResult", NewValue = value, OldValue = _getRolesForRoleIdResult };
                    _getRolesForRoleIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Sgi.Models.SgiCore.Permission> _getPermissionsForPermissionIdResult;
        protected IEnumerable<Sgi.Models.SgiCore.Permission> getPermissionsForPermissionIdResult
        {
            get
            {
                return _getPermissionsForPermissionIdResult;
            }
            set
            {
                if (!object.Equals(_getPermissionsForPermissionIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPermissionsForPermissionIdResult", NewValue = value, OldValue = _getPermissionsForPermissionIdResult };
                    _getPermissionsForPermissionIdResult = value;
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
            var sgiCoreGetRolePermissionByIdResult = await SgiCore.GetRolePermissionById(Id);
            rolepermission = sgiCoreGetRolePermissionByIdResult;

            var sgiCoreGetRolesResult = await SgiCore.GetRoles();
            getRolesForRoleIdResult = sgiCoreGetRolesResult;

            var sgiCoreGetPermissionsResult = await SgiCore.GetPermissions();
            getPermissionsForPermissionIdResult = sgiCoreGetPermissionsResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.RolePermission args)
        {
            try
            {
                var sgiCoreUpdateRolePermissionResult = await SgiCore.UpdateRolePermission(Id, rolepermission);
                DialogService.Close(rolepermission);
            }
            catch (System.Exception sgiCoreUpdateRolePermissionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update RolePermission");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
