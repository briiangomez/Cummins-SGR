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
    public partial class EditUserRoleComponent : ComponentBase
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

        Sgi.Models.SgiCore.UserRole _userrole;
        protected Sgi.Models.SgiCore.UserRole userrole
        {
            get
            {
                return _userrole;
            }
            set
            {
                if (!object.Equals(_userrole, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "userrole", NewValue = value, OldValue = _userrole };
                    _userrole = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Sgi.Models.SgiCore.User> _getUsersForUserIdResult;
        protected IEnumerable<Sgi.Models.SgiCore.User> getUsersForUserIdResult
        {
            get
            {
                return _getUsersForUserIdResult;
            }
            set
            {
                if (!object.Equals(_getUsersForUserIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUsersForUserIdResult", NewValue = value, OldValue = _getUsersForUserIdResult };
                    _getUsersForUserIdResult = value;
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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var sgiCoreGetUserRoleByIdResult = await SgiCore.GetUserRoleById(Id);
            userrole = sgiCoreGetUserRoleByIdResult;

            var sgiCoreGetUsersResult = await SgiCore.GetUsers();
            getUsersForUserIdResult = sgiCoreGetUsersResult;

            var sgiCoreGetRolesResult = await SgiCore.GetRoles();
            getRolesForRoleIdResult = sgiCoreGetRolesResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.UserRole args)
        {
            try
            {
                var sgiCoreUpdateUserRoleResult = await SgiCore.UpdateUserRole(Id, userrole);
                DialogService.Close(userrole);
            }
            catch (System.Exception sgiCoreUpdateUserRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update UserRole");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
