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
    public partial class AddMotorIncidenciumComponent : ComponentBase
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

        IEnumerable<Sgi.Models.SgiCore.Motor> _getMotorsForMotorIdResult;
        protected IEnumerable<Sgi.Models.SgiCore.Motor> getMotorsForMotorIdResult
        {
            get
            {
                return _getMotorsForMotorIdResult;
            }
            set
            {
                if (!object.Equals(_getMotorsForMotorIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMotorsForMotorIdResult", NewValue = value, OldValue = _getMotorsForMotorIdResult };
                    _getMotorsForMotorIdResult = value;
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

        Sgi.Models.SgiCore.MotorIncidencium _motorincidencium;
        protected Sgi.Models.SgiCore.MotorIncidencium motorincidencium
        {
            get
            {
                return _motorincidencium;
            }
            set
            {
                if (!object.Equals(_motorincidencium, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "motorincidencium", NewValue = value, OldValue = _motorincidencium };
                    _motorincidencium = value;
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
            var sgiCoreGetMotorsResult = await SgiCore.GetMotors();
            getMotorsForMotorIdResult = sgiCoreGetMotorsResult;

            var sgiCoreGetIncidenciaResult = await SgiCore.GetIncidencia();
            getIncidenciaForIncidenciaIdResult = sgiCoreGetIncidenciaResult;

            motorincidencium = new Sgi.Models.SgiCore.MotorIncidencium(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.MotorIncidencium args)
        {
            try
            {
                var sgiCoreCreateMotorIncidenciumResult = await SgiCore.CreateMotorIncidencium(motorincidencium);
                DialogService.Close(motorincidencium);
            }
            catch (System.Exception sgiCoreCreateMotorIncidenciumException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new MotorIncidencium!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
