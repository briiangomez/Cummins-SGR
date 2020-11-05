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
    public partial class EditEstadoComponent : ComponentBase
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

        Sgi.Models.SgiCore.Estado _estado;
        protected Sgi.Models.SgiCore.Estado estado
        {
            get
            {
                return _estado;
            }
            set
            {
                if (!object.Equals(_estado, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "estado", NewValue = value, OldValue = _estado };
                    _estado = value;
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
            var sgiCoreGetEstadoByIdResult = await SgiCore.GetEstadoById(Id);
            estado = sgiCoreGetEstadoByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.Estado args)
        {
            try
            {
                var sgiCoreUpdateEstadoResult = await SgiCore.UpdateEstado(Id, estado);
                DialogService.Close(estado);
            }
            catch (System.Exception sgiCoreUpdateEstadoException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Estado");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
