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
using SGRBlazorApp.Services;

namespace SGRBlazorApp.Pages
{
    public partial class EditIncidenciaComponent : ComponentBase
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

        Sgi.Models.SgiCore.Incidencia _incidencia;
        protected Sgi.Models.SgiCore.Incidencia incidencia
        {
            get
            {
                return _incidencia;
            }
            set
            {
                if (!object.Equals(_incidencia, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "incidencia", NewValue = value, OldValue = _incidencia };
                    _incidencia = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Sgi.Models.SgiCore.Dealer> _getDealersForIdDealerResult;
        protected IEnumerable<Sgi.Models.SgiCore.Dealer> getDealersForIdDealerResult
        {
            get
            {
                return _getDealersForIdDealerResult;
            }
            set
            {
                if (!object.Equals(_getDealersForIdDealerResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getDealersForIdDealerResult", NewValue = value, OldValue = _getDealersForIdDealerResult };
                    _getDealersForIdDealerResult = value;
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
            var sgiCoreGetIncidenciaByIdResult = await SgiCore.GetIncidenciaById(Id);
            incidencia = sgiCoreGetIncidenciaByIdResult;
            var estIncs = await SgiCore.GetEstadoIncidencia();
            var estInc = estIncs.OrderByDescending(o => o.Created).FirstOrDefault(o => o.IncidenciaId == incidencia.Id);
            incidencia.Aux1 = estInc.Estado.Descripcion;
            var sgiCoreGetDealersResult = await SgiCore.GetDealers();
            getDealersForIdDealerResult = sgiCoreGetDealersResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.Incidencia args)
        {
            try
            {
                var sgiCoreUpdateIncidenciaResult = await SgiCore.UpdateIncidencia(Id, incidencia);
                DialogService.Close(incidencia);
            }
            catch (System.Exception sgiCoreUpdateIncidenciaException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Incidencia");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
