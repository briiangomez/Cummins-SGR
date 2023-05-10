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
using BlazorInputFile;
using System.Globalization;
using SGR.Models.Models;
using SGRBlazorApp.Helpers;
using SGR.Models;

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

        public List<IFileListEntry> files = new List<IFileListEntry>();

        public List<string> ArchivoCompra = new List<string>();

        public List<string> DatosAdjuntos = new List<string>();

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IFileUpload fileUpload { get; set; }


        [Inject]
        Microsoft.AspNetCore.Components.NavigationManager navigationManager { get; set; }

        [Inject]
        ISgrService<Dealer> dealerService { get; set; }

        [Inject]
        ISgrService<SGR.Models.Models.Motor> motorService { get; set; }

        [Inject]
        ISgrService<MotorDealer> motordealerService { get; set; }

        [Inject]
        ISgrService<Sintoma> sintomaService { get; set; }

        [Inject]
        ISgrService<ImagenesIncidencium> imagenesService { get; set; }

        [Inject]
        ISgrService<SGR.Models.Models.Incidencia> incidenciaService { get; set; }

        public List<SGR.Models.Models.Motor> MotoresList { get; set; }

        public double? Latitud { get; set; }

        public string Direc = String.Empty;

        public double? Longitud { get; set; }


        [Inject]
        ISgrService<SGRBlazorApp.Data.IncidenciaApi> incidenciaApiService { get; set; }

        public bool CheckBox1Value { get; set; }

        public List<SGR.Models.Models.Sintoma> sims { get; set; }

        IEnumerable<SGR.Models.Models.Dealer> _getDealersForIdDealerResult;

        IEnumerable<SGR.Models.Models.MotorDealer> _getMotorsDealers { get; set; }

        public Dictionary<Guid, double> distances = new Dictionary<Guid, double>();
        protected IEnumerable<SGR.Models.Models.Dealer> getDealersForIdDealerResult
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
        public SGRBlazorApp.Data.IncidenciaApi incidencia { get; set; }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var CertificacionList = await motordealerService.GetAllAsync("MotorDealer/GetMotorDealer");
            var sgiCoreGetDealersResult = await dealerService.GetAllAsync("Dealer/GetDealer");
            MotoresList = await motorService.GetAllAsync("Motor/GetMotor");
            _getMotorsDealers = CertificacionList;
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
                
                string lat = await JSRuntime.InvokeAsync<string>("getValue", "Lat");
                string longi = await JSRuntime.InvokeAsync<string>("getValue", "Long");
                string dire = await JSRuntime.InvokeAsync<string>("getValue", "Dir");
                args.DireccionInspeccion = dire;
                NumberFormatInfo formatProvider = new NumberFormatInfo();
                formatProvider.NumberDecimalSeparator = ".";
                formatProvider.NumberGroupSeparator = ",";
                formatProvider.NumberGroupSizes = new int[] { 2 };
                args.latitudGps = Convert.ToDouble(lat, formatProvider);
                args.longitudGps = Convert.ToDouble(longi, formatProvider);
                args.EsGarantia = CheckBox1Value;
                MotoresList = MotoresList.Where(o => o.Codigo == args.ModeloMotor).ToList();
                distances = GetDistances(args.latitudGps, args.longitudGps);
                if(distances.Count > 0 && CheckBox1Value)
                {
                    args.IdConcesionario = distances.OrderBy(o => o.Value).FirstOrDefault().Key;
                    args.nombreConcesionario = getDealersForIdDealerResult.FirstOrDefault(o => o.Id == args.IdConcesionario.Value).Name;
                    args.codigoConcesionario = getDealersForIdDealerResult.FirstOrDefault(o => o.Id == args.IdConcesionario.Value).LocationCode;
                }
                else
                {
                    args.nombreConcesionario = getDealersForIdDealerResult.FirstOrDefault(o => o.Id == args.IdConcesionario.Value).Name;
                    args.codigoConcesionario = getDealersForIdDealerResult.FirstOrDefault(o => o.Id == args.IdConcesionario.Value).LocationCode;
                }
                if(CheckBox1Value)
                {
                    args.idEstadoIncidencia = 0;
                }
                else
                {
                    args.idEstadoIncidencia = 1;
                }
                if (files.Count == 0)
                    throw new Exception("El archivo es obligatorio");
                var incids = await incidenciaApiService.GetAllAsync("Incidencia/GetIncidenciaApi");
                args.numeroIncidencia = incids.Count() + 1;
                var sgiCoreCreateIncidenciaResult = await incidenciaApiService.SaveAsync("Incidencia/CrearIncidencia",args);
                if (sgiCoreCreateIncidenciaResult.Id == Guid.Empty)
                    throw new Exception("Ocurrio un error generando el Reclamo, por favor intente nuevamente!");
                if (ArchivoCompra.Count > 0)
                {
                    foreach (var item in ArchivoCompra)
                    {
                        //string path = await fileUpload.Upload(item, sgiCoreCreateIncidenciaResult.Id.ToString());
                        ImagenesIncidencium img = new ImagenesIncidencium();
                        img.Imagen = item;
                        img.Tipo = "Factura de Compra";
                        img.IncidenciaId = sgiCoreCreateIncidenciaResult.Id;
                        await imagenesService.SaveAsync("ImagenesIncidencia/CreateImagenesIncidencia", img);
                    }
                }
                else
                {
                    throw new Exception("El archivo es obligatorio");
                }
                if (DatosAdjuntos.Count > 0)
                {
                    foreach (var item in DatosAdjuntos)
                    {
                        //string path = await fileUpload.Upload(item, sgiCoreCreateIncidenciaResult.Id.ToString());
                        ImagenesIncidencium img = new ImagenesIncidencium();
                        img.Imagen = item;
                        img.Tipo = "Datos Adjuntos";
                        img.IncidenciaId = sgiCoreCreateIncidenciaResult.Id;
                        await imagenesService.SaveAsync("ImagenesIncidencia/CreateImagenesIncidencia", img);
                    }
                }
                navigationManager.NavigateTo("/incidenciaDetail/" + sgiCoreCreateIncidenciaResult.Id.ToString());
            }
            catch (System.Exception ex)
            {
                Logger.AddLine(String.Format("Error Crear Incidencia - {0}-{1}-{2}", DateTime.Now.ToString(), ex.Message, ex.StackTrace));
                await JSRuntime.MostrarMensaje("Error!", ex.Message, TipoMensajeSweetAlert.error);
            }
        }

        public Dictionary<Guid,double> GetDistances(double lat, double lon)
        {
            Dictionary<Guid, double> diss = new Dictionary<Guid, double>();
            Guid idMotor = MotoresList.FirstOrDefault().Id;
            var dealerss = _getMotorsDealers.Where(o => o.MotorId == idMotor).Select(o => o.DealerId).ToList();
            getDealersForIdDealerResult = getDealersForIdDealerResult.Where(o => dealerss.Contains(o.Id)).ToList();
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
