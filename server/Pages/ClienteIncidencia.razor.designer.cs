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
    public partial class ClienteIncidenciaComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.SgiCore.ClienteIncidencium> grid0;

        IEnumerable<Sgi.Models.SgiCore.ClienteIncidencium> _getClienteIncidenciaResult;
        protected IEnumerable<Sgi.Models.SgiCore.ClienteIncidencium> getClienteIncidenciaResult
        {
            get
            {
                return _getClienteIncidenciaResult;
            }
            set
            {
                if (!object.Equals(_getClienteIncidenciaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getClienteIncidenciaResult", NewValue = value, OldValue = _getClienteIncidenciaResult };
                    _getClienteIncidenciaResult = value;
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
            var sgiCoreGetClienteIncidenciaResult = await SgiCore.GetClienteIncidencia();
            getClienteIncidenciaResult = sgiCoreGetClienteIncidenciaResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddClienteIncidencium>("Add Cliente Incidencium", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.SgiCore.ClienteIncidencium args)
        {
            var dialogResult = await DialogService.OpenAsync<EditClienteIncidencium>("Edit Cliente Incidencium", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteClienteIncidenciumResult = await SgiCore.DeleteClienteIncidencium(data.Id);
                    if (sgiCoreDeleteClienteIncidenciumResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteClienteIncidenciumException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete ClienteIncidencium");
            }
        }
    }
}
