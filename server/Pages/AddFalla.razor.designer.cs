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
    public partial class AddFallaComponent : ComponentBase
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

        Sgi.Models.SgiCore.Falla _falla;
        protected Sgi.Models.SgiCore.Falla falla
        {
            get
            {
                return _falla;
            }
            set
            {
                if (!object.Equals(_falla, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "falla", NewValue = value, OldValue = _falla };
                    _falla = value;
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

            falla = new Sgi.Models.SgiCore.Falla(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.Falla args)
        {
            try
            {
                var sgiCoreCreateFallaResult = await SgiCore.CreateFalla(falla);
                DialogService.Close(falla);
            }
            catch (System.Exception sgiCoreCreateFallaException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Falla!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
