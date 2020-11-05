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
    public partial class AddEstadoGarantiumComponent : ComponentBase
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

        IEnumerable<Sgi.Models.SgiCore.Incidencia> _getIncidenciaForIdIncidenciaResult;
        protected IEnumerable<Sgi.Models.SgiCore.Incidencia> getIncidenciaForIdIncidenciaResult
        {
            get
            {
                return _getIncidenciaForIdIncidenciaResult;
            }
            set
            {
                if (!object.Equals(_getIncidenciaForIdIncidenciaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getIncidenciaForIdIncidenciaResult", NewValue = value, OldValue = _getIncidenciaForIdIncidenciaResult };
                    _getIncidenciaForIdIncidenciaResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Sgi.Models.SgiCore.EstadoGarantium _estadogarantium;
        protected Sgi.Models.SgiCore.EstadoGarantium estadogarantium
        {
            get
            {
                return _estadogarantium;
            }
            set
            {
                if (!object.Equals(_estadogarantium, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "estadogarantium", NewValue = value, OldValue = _estadogarantium };
                    _estadogarantium = value;
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
            var sgiCoreGetIncidenciaResult = await SgiCore.GetIncidencia();
            getIncidenciaForIdIncidenciaResult = sgiCoreGetIncidenciaResult;

            estadogarantium = new Sgi.Models.SgiCore.EstadoGarantium(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.EstadoGarantium args)
        {
            try
            {
                var sgiCoreCreateEstadoGarantiumResult = await SgiCore.CreateEstadoGarantium(estadogarantium);
                DialogService.Close(estadogarantium);
            }
            catch (System.Exception sgiCoreCreateEstadoGarantiumException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new EstadoGarantium!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
