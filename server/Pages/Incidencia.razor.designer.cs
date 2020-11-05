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
    public partial class IncidenciaComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.IncidenciaApi> grid0;

        IEnumerable<Sgi.Models.IncidenciaApi> _getIncidenciaResult;
        protected IEnumerable<Sgi.Models.IncidenciaApi> getIncidenciaResult
        {
            get
            {
                return _getIncidenciaResult;
            }
            set
            {
                if (!object.Equals(_getIncidenciaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getIncidenciaResult", NewValue = value, OldValue = _getIncidenciaResult };
                    _getIncidenciaResult = value;
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
            var sgiCoreGetIncidenciaResult = await SgiCore.GetIncidenciaApi();
            getIncidenciaResult = sgiCoreGetIncidenciaResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddIncidencia>("Add Incidencia", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.IncidenciaApi args)
        {
            var dialogResult = await DialogService.OpenAsync<EditIncidencia>("Edit Incidencia", new Dictionary<string, object>() { {"Id", args.Id} });

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteIncidenciaResult = await SgiCore.DeleteIncidencia(data.Id);
                    if (sgiCoreDeleteIncidenciaResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteIncidenciaException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Incidencia");
            }
        }
    }
}
