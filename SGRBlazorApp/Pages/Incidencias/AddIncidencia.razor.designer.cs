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
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        Microsoft.AspNetCore.Components.NavigationManager navigationManager { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }


        public bool CheckBox1Value { get; set; }

        public List<SGRBlazorApp.Data.Sintomas> sims { get; set; }

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
            var sgiCoreGetDealersResult = await SgiCore.GetDealers();
            getDealersForIdDealerResult = sgiCoreGetDealersResult;
            var incids = await SgiCore.GetIncidencia();
            incidencia = new SGRBlazorApp.Data.IncidenciaApi(){};
            incidencia.numeroIncidencia = incids.ToList().Count + 1;
            incidencia.fechaIncidencia = DateTime.Now;
            sims = new List<SGRBlazorApp.Data.Sintomas>()
            {
                new SGRBlazorApp.Data.Sintomas() { ID = "Aceite Lubricante Contaminado", Sintoma = "Aceite Lubricante Contaminado" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Aceite Lubricante o de la Transmisión en el Refrigerante", Sintoma = "Aceite Lubricante o de la Transmisión en el Refrigerante" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Aceleración o Respuesta Deficientes del Motor", Sintoma = "Aceleración o Respuesta Deficientes del Motor" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Alta Presión del Aceite Lubricante", Sintoma = "Alta Presión del Aceite Lubricante" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Alternador del Sistema de Carga Sobrecargando", Sintoma = "Alternador del Sistema de Carga Sobrecargando" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Baja Potencia de Motor", Sintoma = "Baja Potencia de Motor" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Combustible en el Aceite Lubricante", Sintoma = "Combustible en el Aceite Lubricante" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Combustible o Aceite Lubricante Fugando del Múltiple de Escape", Sintoma = "Combustible o Aceite Lubricante Fugando del Múltiple de Escape" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Consumo de Aceite Lubricante Excesivo", Sintoma = "Consumo de Aceite Lubricante Excesivo" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Consumo de Combustible Excesivo", Sintoma = "Consumo de Combustible Excesivo" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Contaminación del Refrigerante", Sintoma = "Contaminación del Refrigerante" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Detonación de Combustible", Sintoma = "Detonación de Combustible" },
                new SGRBlazorApp.Data.Sintomas() { ID = "El Compresor de Aire Bombea Aceite Lubricante en Exceso hacia el Sistema de Aire", Sintoma = "El Compresor de Aire Bombea Aceite Lubricante en Exceso hacia el Sistema de Aire" },
                new SGRBlazorApp.Data.Sintomas() { ID = "El Compresor de Aire No Bombea Aire", Sintoma = "El Compresor de Aire No Bombea Aire" },
                new SGRBlazorApp.Data.Sintomas() { ID = "El Compresor de Aire No Deja de Bombear", Sintoma = "El Compresor de Aire No Deja de Bombear" },
                new SGRBlazorApp.Data.Sintomas() { ID = "El Compresor de Aire No Mantiene la Presión de Aire Adecuada (No Bombea Continuamente)", Sintoma = "El Compresor de Aire No Mantiene la Presión de Aire Adecuada (No Bombea Continuamente)" },
                new SGRBlazorApp.Data.Sintomas() { ID = "El Compresor de Aire Se Cicla Frecuentemente", Sintoma = "El Compresor de Aire Se Cicla Frecuentemente" },
                new SGRBlazorApp.Data.Sintomas() { ID = "", Sintoma = "" },
                new SGRBlazorApp.Data.Sintomas() { ID = "Otro", Sintoma = "Otro" },
            };


        }

        protected async System.Threading.Tasks.Task Form0Submit(SGRBlazorApp.Data.IncidenciaApi args)
        {
            try
            {
                args.IdConcesionario = Guid.Parse("69935E47-46F5-4710-8636-F5B1EE9204A1");
                var sgiCoreCreateIncidenciaResult = await SgiCore.CreateIncidenciaApi(args);
                navigationManager.NavigateTo("/mapResult");
            }
            catch (System.Exception ex)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Incidencia!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            navigationManager.NavigateTo("/incidencia");
        }
    }
}
