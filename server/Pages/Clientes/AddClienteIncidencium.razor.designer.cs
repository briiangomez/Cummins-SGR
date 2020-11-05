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
    public partial class AddClienteIncidenciumComponent : ComponentBase
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

        IEnumerable<Sgi.Models.SgiCore.Cliente> _getClientesForClienteIdResult;
        protected IEnumerable<Sgi.Models.SgiCore.Cliente> getClientesForClienteIdResult
        {
            get
            {
                return _getClientesForClienteIdResult;
            }
            set
            {
                if (!object.Equals(_getClientesForClienteIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getClientesForClienteIdResult", NewValue = value, OldValue = _getClientesForClienteIdResult };
                    _getClientesForClienteIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

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

        Sgi.Models.SgiCore.ClienteIncidencium _clienteincidencium;
        protected Sgi.Models.SgiCore.ClienteIncidencium clienteincidencium
        {
            get
            {
                return _clienteincidencium;
            }
            set
            {
                if (!object.Equals(_clienteincidencium, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "clienteincidencium", NewValue = value, OldValue = _clienteincidencium };
                    _clienteincidencium = value;
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
            getClientesForClienteIdResult = sgiCoreGetClientesResult;

            var sgiCoreGetIncidenciaResult = await SgiCore.GetIncidencia();
            getIncidenciaForIncidenciaIdResult = sgiCoreGetIncidenciaResult;

            clienteincidencium = new Sgi.Models.SgiCore.ClienteIncidencium(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.ClienteIncidencium args)
        {
            try
            {
                var sgiCoreCreateClienteIncidenciumResult = await SgiCore.CreateClienteIncidencium(clienteincidencium);
                DialogService.Close(clienteincidencium);
            }
            catch (System.Exception sgiCoreCreateClienteIncidenciumException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new ClienteIncidencium!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
