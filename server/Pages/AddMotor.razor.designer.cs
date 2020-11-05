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
    public partial class AddMotorComponent : ComponentBase
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

        Sgi.Models.SgiCore.Motor _motor;
        protected Sgi.Models.SgiCore.Motor motor
        {
            get
            {
                return _motor;
            }
            set
            {
                if (!object.Equals(_motor, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "motor", NewValue = value, OldValue = _motor };
                    _motor = value;
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
            motor = new Sgi.Models.SgiCore.Motor(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.Motor args)
        {
            try
            {
                var sgiCoreCreateMotorResult = await SgiCore.CreateMotor(motor);
                DialogService.Close(motor);
            }
            catch (System.Exception sgiCoreCreateMotorException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Motor!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
