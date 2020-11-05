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
    public partial class UsersComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.SgiCore.User> grid0;

        IEnumerable<Sgi.Models.SgiCore.User> _getUsersResult;
        protected IEnumerable<Sgi.Models.SgiCore.User> getUsersResult
        {
            get
            {
                return _getUsersResult;
            }
            set
            {
                if (!object.Equals(_getUsersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUsersResult", NewValue = value, OldValue = _getUsersResult };
                    _getUsersResult = value;
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
            var sgiCoreGetUsersResult = await SgiCore.GetUsers();
            getUsersResult = sgiCoreGetUsersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddUser>("Add User", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.SgiCore.User args)
        {
            var dialogResult = await DialogService.OpenAsync<EditUser>("Edit User", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteUserResult = await SgiCore.DeleteUser(data.Id);
                    if (sgiCoreDeleteUserResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteUserException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete User");
            }
        }
    }
}
