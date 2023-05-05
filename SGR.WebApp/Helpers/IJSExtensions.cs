using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBBlazorApp.Helpers
{
    public static class IJSExtensions
    {
        /// <summary>
        /// Muestra solo un mensaje
        /// </summary>
        /// <param name="js"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static ValueTask<object> MostrarMensaje(this IJSRuntime js, string mensaje)
        {
            return js.InvokeAsync<object>("Swal.fire", mensaje);
        }

        //Muestra un mensaje pero con titulo y tipo de mensaje
        public static ValueTask<object> MostrarMensaje(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert tipo)
        {
            return js.InvokeAsync<object>("Swal.fire", titulo, mensaje, tipo.ToString());
        }

        public async static Task<bool> Confirmar(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert tipo)
        {
            return await js.InvokeAsync<bool>("CustomConfirm", titulo, mensaje, tipo.ToString());
        }
    }


    public enum TipoMensajeSweetAlert
    { 
      question, warning, error, success, info
    }

}
