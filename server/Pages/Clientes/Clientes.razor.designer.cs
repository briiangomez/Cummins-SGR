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
using Sgi.Pages.Clientes;

namespace Sgi.Pages
{
    public partial class ClientesComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.SgiCore.Cliente> grid0;

        IEnumerable<Sgi.Models.SgiCore.Cliente> _getClientesResult;
        protected IEnumerable<Sgi.Models.SgiCore.Cliente> getClientesResult
        {
            get
            {
                return _getClientesResult;
            }
            set
            {
                if (!object.Equals(_getClientesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getClientesResult", NewValue = value, OldValue = _getClientesResult };
                    _getClientesResult = value;
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
            var sgiCoreGetClientesResult = await SgiCore.GetClientes();
            getClientesResult = sgiCoreGetClientesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddCliente>("Add Cliente", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.SgiCore.Cliente args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCliente>("Edit Cliente", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteClienteResult = await SgiCore.DeleteCliente(data.Id);
                    if (sgiCoreDeleteClienteResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteClienteException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Cliente");
            }
        }
    }
}
