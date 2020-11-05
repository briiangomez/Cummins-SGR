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
    public partial class MotorIncidenciaComponent : ComponentBase
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
        protected RadzenGrid<Sgi.Models.SgiCore.MotorIncidencium> grid0;

        IEnumerable<Sgi.Models.SgiCore.MotorIncidencium> _getMotorIncidenciaResult;
        protected IEnumerable<Sgi.Models.SgiCore.MotorIncidencium> getMotorIncidenciaResult
        {
            get
            {
                return _getMotorIncidenciaResult;
            }
            set
            {
                if (!object.Equals(_getMotorIncidenciaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMotorIncidenciaResult", NewValue = value, OldValue = _getMotorIncidenciaResult };
                    _getMotorIncidenciaResult = value;
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
            var sgiCoreGetMotorIncidenciaResult = await SgiCore.GetMotorIncidencia();
            getMotorIncidenciaResult = sgiCoreGetMotorIncidenciaResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddMotorIncidencium>("Add Motor Incidencium", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Sgi.Models.SgiCore.MotorIncidencium args)
        {
            var dialogResult = await DialogService.OpenAsync<EditMotorIncidencium>("Edit Motor Incidencium", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("¿Esta seguro de eliminar el siguiente Registro?") == true)
                {
                    var sgiCoreDeleteMotorIncidenciumResult = await SgiCore.DeleteMotorIncidencium(data.Id);
                    if (sgiCoreDeleteMotorIncidenciumResult != null) {
                        await grid0.Reload();
}
                }
            }
            catch (System.Exception sgiCoreDeleteMotorIncidenciumException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete MotorIncidencium");
            }
        }
    }
}
