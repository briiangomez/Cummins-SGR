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
    public partial class AddDealerComponent : ComponentBase
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

        Sgi.Models.SgiCore.Dealer _dealer;
        protected Sgi.Models.SgiCore.Dealer dealer
        {
            get
            {
                return _dealer;
            }
            set
            {
                if (!object.Equals(_dealer, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "dealer", NewValue = value, OldValue = _dealer };
                    _dealer = value;
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
            dealer = new Sgi.Models.SgiCore.Dealer(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.Dealer args)
        {
            try
            {
                var sgiCoreCreateDealerResult = await SgiCore.CreateDealer(dealer);
                DialogService.Close(dealer);
            }
            catch (System.Exception sgiCoreCreateDealerException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Dealer!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
