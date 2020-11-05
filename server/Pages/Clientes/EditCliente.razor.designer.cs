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
    public partial class EditClienteComponent : ComponentBase
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

        Sgi.Models.SgiCore.Cliente _cliente;
        protected Sgi.Models.SgiCore.Cliente cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                if (!object.Equals(_cliente, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "cliente", NewValue = value, OldValue = _cliente };
                    _cliente = value;
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
            var sgiCoreGetClienteByIdResult = await SgiCore.GetClienteById(Id);
            cliente = sgiCoreGetClienteByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.Cliente args)
        {
            try
            {
                var sgiCoreUpdateClienteResult = await SgiCore.UpdateCliente(Id, cliente);
                DialogService.Close(cliente);
            }
            catch (System.Exception sgiCoreUpdateClienteException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Cliente");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
