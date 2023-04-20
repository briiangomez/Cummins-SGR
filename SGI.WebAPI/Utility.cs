using SGIWebApi.Models;
using SGR.Models;
using SGR.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGIWebApi
{
    public class Utility
    {
        public static IncidenciaApi ParseIncidencia(Incidencia inc, SGIDbContext _context)
        {
            IncidenciaApi incidencia = new IncidenciaApi();
            try
            {
                var motInc = _context.MotorIncidencia.FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var mot = _context.Equipos.FirstOrDefault(o => o.Id == motInc.MotorId);
                var falla = _context.Fallas.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var cliente = _context.Clientes.FirstOrDefault(o => o.Id == inc.IdCliente);
                var estadoG = _context.EstadoGarantia.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var estInc = _context.EstadoIncidencia.OrderByDescending(o => o.Created).FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var estado = _context.Estados.FirstOrDefault(o => o.Id == estInc.EstadoId);
                var dealer = _context.Dealers.FirstOrDefault(o => o.Id == inc.IdDealer);
                var sintomas = _context.Sintomas.ToList();
                incidencia.Id = inc.Id;
                incidencia.numeroDocumento = cliente.Dni;
                incidencia.fechaIncidencia = inc.FechaIncidencia;

                incidencia.fechaRegistro = inc.FechaRegistro;
                incidencia.fechaCompra = motInc.FechaCompra;
                incidencia.nombreContactoCliente = cliente.Contacto;
                incidencia.emailContactoCliente = cliente.Aux1;
                incidencia.telefonoContactoCliente = cliente.Aux2;
                if (inc.FechaCierre != null)
                {
                    incidencia.fechaCierre = inc.FechaCierre.Value;
                }
                if (!String.IsNullOrEmpty(inc.Sintoma))
                {
                    
                    incidencia.codigoSintoma = 0;
                    if (sintomas.FirstOrDefault(o => o.Descripcion == inc.Sintoma || o.Codigo == inc.Sintoma) != null)
                    {
                        incidencia.Sintoma = sintomas.FirstOrDefault(o => o.Descripcion == inc.Sintoma || o.Codigo == inc.Sintoma).Descripcion;
                        incidencia.codigoSintoma = Int32.Parse(sintomas.FirstOrDefault(o => o.Descripcion == inc.Sintoma || o.Codigo == inc.Sintoma).Codigo);
                    }
                }
                if (motInc.MotorId != null)
                {
                    incidencia.IdMotor = motInc.MotorId;
                }
                //if (inc.NroReclamoCummins != null)
                //{
                //    incidencia.NroReclamoCummins = inc.NroReclamoCummins.Value;
                //}
                //incidencia.Descripcion = inc.Descripcion;
                incidencia.DireccionInspeccion = inc.DireccionInspeccion;
                if (inc.LatitudGps != null)
                {
                    incidencia.latitudGps = inc.LatitudGps.Value; 
                }
                if (inc.LongitudGps != null)
                {
                    incidencia.longitudGps = inc.LongitudGps.Value; 
                }
                incidencia.PathImagenes = inc.PathImagenes;
                if (inc.MostrarEnTv != null)
                {
                    incidencia.mostrarEnTv = inc.MostrarEnTv.Value;
                }
                if (estadoG.IdEstadoGarantia != null)
                {
                    incidencia.idEstadoGarantia = estadoG.IdEstadoGarantia.Value;
                }
                if (inc.EsGarantia != null)
                {
                    incidencia.EsGarantia = inc.EsGarantia.Value;
                    incidencia.Garantia = inc.EsGarantia.Value ? "Si" : "No";
                }
                else
                {
                    incidencia.Garantia = "No";
                }
                incidencia.numeroOperacion = inc.NumeroOperacion;
                incidencia.numeroIncidencia = inc.NroIncidencia;
                incidencia.numeroChasis = motInc.NumeroChasis;
                incidencia.numeroMotor = motInc.NumeroMotor;
                incidencia.Equipo = mot.Equipo1;
                if(mot.Oemid != null)
                    incidencia.IdOem = mot.Oemid.Value;
                incidencia.ModeloEquipo = motInc.ModeloEquipo;
                incidencia.configuracionCorta = inc.ConfiguracionCorta;
                incidencia.fechaPreEntrega = inc.FechaPreEntrega;
                if (motInc.HsKm != null)
                {
                    incidencia.horasTractor = motInc.HsKm.Value;
                }
                if (motInc.FechaInicioGarantia != null)
                {
                    incidencia.fechaInicioGarantia = motInc.FechaInicioGarantia.Value;
                }
                if (motInc.FechaFalla != null)
                {
                    incidencia.fechaFalla = motInc.FechaFalla.Value;
                }
                if(mot.MotorId != null)
                {
                    incidencia.ModeloMotor = _context.Motors.Find(mot.MotorId).Codigo;
                }
                incidencia.telefonoCelularContacto = cliente.Celular;
                incidencia.ObservacionesIncidencia = inc.Aux1;
                incidencia.codigoConcesionario = dealer.LocationCode;
                incidencia.nombreConcesionario = dealer.Name;
                incidencia.emailConcesionario = dealer.Email;
                incidencia.telefonoConcesionario = dealer.Phone;
                incidencia.IdConcesionario = dealer.Id;
                incidencia.nombreContacto = cliente.Nombre;
                incidencia.tipoDniContacto = cliente.TipoDni;
                incidencia.numeroDocumento = cliente.Dni;
                incidencia.domicilioContacto = cliente.Direccion;
                incidencia.localidadContacto = cliente.Localidad;
                incidencia.provinciaContacto = cliente.Provincia;
                incidencia.telefonoFijoContacto = cliente.Telefono;
                incidencia.emailContacto = cliente.Email;
                incidencia.latitudGpsContacto = (float)cliente.LatitudGpsContacto;
                incidencia.longitudGpsContacto = (float)cliente.LongitudGpsContacto;
                incidencia.idFalla = (long)falla.IdFalla;
                incidencia.nombreFalla = falla.Nombre;
                incidencia.observacionesFalla = falla.Observaciones;
                incidencia.nombreEstadoIncidencia = estado.Descripcion;
                if (estado.Descripcion == "Anulada")
                {
                    incidencia.observacionesAnulada = estInc.Observacion;
                }
                incidencia.idEstadoIncidencia = estado.Codigo;
                //incidencia.nombreEstadoIncidencia = inc.Aux2;
                if (!String.IsNullOrEmpty(inc.Aux3))
                {
                    incidencia.idEstadoIncidencia = Int32.Parse(inc.Aux3);
                }
                incidencia.fechaEstadoIncidencia = estInc.Created;
                if (estadoG.IdEstadoGarantia != null)
                {
                    incidencia.idEstadoGarantia = estadoG.IdEstadoGarantia.Value;
                }
                incidencia.nombreEstadoGarantia = estadoG.Nombre;
                incidencia.observacionesGarantia = estadoG.ObservacionesGarantia;
                incidencia.observacionesProveedor = estadoG.ObservacionesProveedor;
            }
            catch (Exception ex)
            {
                Logger.AddLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), ex.Message,ex.StackTrace));
            }
            return incidencia;

        }
    }
}
