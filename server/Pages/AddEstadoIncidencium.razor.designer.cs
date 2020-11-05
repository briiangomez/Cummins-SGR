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
    public partial class AddEstadoIncidenciumComponent : ComponentBase
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

        IEnumerable<Sgi.Models.SgiCore.Incidencia> _getIncidenciaForIncidenciaIdResult;
        protected IEnumerable<Sgi.Models.SgiCore.Incidencia> getIncidenciaForIncidenciaIdResult
        {
            get
            {
                return _getIncidenciaForIncidenciaIdResult;
            }
            set
            {
                if (!object.Equals(_getIncidenciaForIncidenciaIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getIncidenciaForIncidenciaIdResult", NewValue = value, OldValue = _getIncidenciaForIncidenciaIdResult };
                    _getIncidenciaForIncidenciaIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Sgi.Models.SgiCore.Estado> _getEstadosForEstadoIdResult;
        protected IEnumerable<Sgi.Models.SgiCore.Estado> getEstadosForEstadoIdResult
        {
            get
            {
                return _getEstadosForEstadoIdResult;
            }
            set
            {
                if (!object.Equals(_getEstadosForEstadoIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getEstadosForEstadoIdResult", NewValue = value, OldValue = _getEstadosForEstadoIdResult };
                    _getEstadosForEstadoIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Sgi.Models.SgiCore.EstadoIncidencium _estadoincidencium;
        protected Sgi.Models.SgiCore.EstadoIncidencium estadoincidencium
        {
            get
            {
                return _estadoincidencium;
            }
            set
            {
                if (!object.Equals(_estadoincidencium, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "estadoincidencium", NewValue = value, OldValue = _estadoincidencium };
                    _estadoincidencium = value;
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
            getIncidenciaForIncidenciaIdResult = sgiCoreGetIncidenciaResult;

            var sgiCoreGetEstadosResult = await SgiCore.GetEstados();
            getEstadosForEstadoIdResult = sgiCoreGetEstadosResult;

            estadoincidencium = new Sgi.Models.SgiCore.EstadoIncidencium(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.EstadoIncidencium args)
        {
            try
            {
                var sgiCoreCreateEstadoIncidenciumResult = await SgiCore.CreateEstadoIncidencium(estadoincidencium);
                DialogService.Close(estadoincidencium);
            }
            catch (System.Exception sgiCoreCreateEstadoIncidenciumException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new EstadoIncidencium!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
