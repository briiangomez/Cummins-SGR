using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using SGRBlazorApp.Services;
using SGRBlazorApp.Data;
using SGRBlazorApp.Interfaces;

namespace SGRBlazorApp.Pages
{
    public partial class AddIncidenciaComponent : ComponentBase
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
        Microsoft.AspNetCore.Components.NavigationManager navigationManager { get; set; }

        [Inject]
        ISgrService<Dealer> dealerService { get; set; }

        [Inject]
        ISgrService<Sintomas> sintomaService { get; set; }

        [Inject]
        ISgrService<SGRBlazorApp.Data.Incidencia> incidenciaService { get; set; }

        [Inject]
        ISgrService<SGRBlazorApp.Data.IncidenciaApi> incidenciaApiService { get; set; }

        public bool CheckBox1Value { get; set; }

        public List<SGRBlazorApp.Data.Sintomas> sims { get; set; }

        IEnumerable<SGRBlazorApp.Data.Dealer> _getDealersForIdDealerResult;

        public Dictionary<Guid, double> distances = new Dictionary<Guid, double>();
        protected IEnumerable<SGRBlazorApp.Data.Dealer> getDealersForIdDealerResult
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

        SGRBlazorApp.Data.IncidenciaApi _incidencia;
        protected SGRBlazorApp.Data.IncidenciaApi incidencia
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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var sgiCoreGetDealersResult = await dealerService.GetAllAsync("Dealer/GetDealer");
            getDealersForIdDealerResult = sgiCoreGetDealersResult;
            var incids = await incidenciaApiService.GetAllAsync("Incidencia/GetIncidenciaApi");
            incidencia = new SGRBlazorApp.Data.IncidenciaApi(){};
            incidencia.numeroIncidencia = incids.ToList().Count + 1;
            incidencia.fechaIncidencia = DateTime.Now;
            sims = await sintomaService.GetAllAsync("Sintoma/GetSintoma");
            sims = sims.OrderBy(o => o.Descripcion).ToList();

        }


        public const double EarthRadius = 6371;
        public double GetDistance(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {
            double distance = 0;
            double Lat = (Latitude2 - Latitude1) * (Math.PI / 180);
            double Lon = (Longitude2 - Longitude1) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(Latitude1 * (Math.PI / 180)) * Math.Cos(Latitude2 * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return distance;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SGRBlazorApp.Data.IncidenciaApi args)
        {
            try
            {
                distances = GetDistances(args.latitudGps, args.longitudGps);
                if(distances.Count > 0)
                {
                    args.IdConcesionario = distances.OrderBy(o => o.Value).FirstOrDefault().Key;
                    args.nombreConcesionario = getDealersForIdDealerResult.FirstOrDefault(o => o.Id == args.IdConcesionario.Value).Name;
                    args.codigoConcesionario = getDealersForIdDealerResult.FirstOrDefault(o => o.Id == args.IdConcesionario.Value).LocationCode;
                }
                var sgiCoreCreateIncidenciaResult = await incidenciaApiService.SaveAsync("Incidencia/CrearIncidencia",args);
                navigationManager.NavigateTo("/incidenciaDetail/" + sgiCoreCreateIncidenciaResult.Id.ToString());
            }
            catch (System.Exception sgiCoreCreateIncidenciaException)
            {
                    //NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Incidencia!");
            }
        }

        public Dictionary<Guid,double> GetDistances(double lat, double lon)
        {
            Dictionary<Guid, double> diss = new Dictionary<Guid, double>();
            foreach (var item in getDealersForIdDealerResult.ToList())
            {
                if(item.LongitudGps != null && item.LatitudGps != null)
                {
                    double dis = GetDistance(lat, lon, item.LatitudGps.Value, item.LongitudGps.Value);
                    diss.Add(item.Id, dis);
                }
            }

            return diss;
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            navigationManager.NavigateTo("/incidencia");
        }
    }
}
