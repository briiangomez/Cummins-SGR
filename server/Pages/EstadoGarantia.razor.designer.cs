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
    public partial class EstadoGarantiaComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.SgiCore.EstadoGarantium> grid0;

        IEnumerable<Sgi.Models.SgiCore.EstadoGarantium> _getEstadoGarantiaResult;
        protected IEnumerable<Sgi.Models.SgiCore.EstadoGarantium> getEstadoGarantiaResult
        {
            get
            {
                return _getEstadoGarantiaResult;
            }
            set
            {
                if (!object.Equals(_getEstadoGarantiaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getEstadoGarantiaResult", NewValue = value, OldValue = _getEstadoGarantiaResult };
                    _getEstadoGarantiaResult = value;
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
            var sgiCoreGetEstadoGarantiaResult = await SgiCore.GetEstadoGarantia();
            getEstadoGarantiaResult = sgiCoreGetEstadoGarantiaResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddEstadoGarantium>("Add Estado Garantium", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.SgiCore.EstadoGarantium args)
        {
            var dialogResult = await DialogService.OpenAsync<EditEstadoGarantium>("Edit Estado Garantium", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteEstadoGarantiumResult = await SgiCore.DeleteEstadoGarantium(data.Id);
                    if (sgiCoreDeleteEstadoGarantiumResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteEstadoGarantiumException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete EstadoGarantium");
            }
        }
    }
}
