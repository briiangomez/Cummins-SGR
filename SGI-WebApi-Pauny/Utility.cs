using SGI_WebApi_Pauny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SGI_WebApi_Pauny
{
    public class Utility
    {

        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        public static IncidenciaApi ParseIncidencia(Incidencia inc, SGIDbContext _context)
        {
            IncidenciaApi incidencia = new IncidenciaApi();
            try
            {
                var motInc = _context.MotorIncidencias.FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var mot = _context.Motors.FirstOrDefault(o => o.Id == motInc.MotorId);
                var falla = _context.Fallas.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var cliente = _context.Clientes.FirstOrDefault(o => o.Id == inc.IdCliente);
                var estadoG = _context.EstadoGarantias.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var estInc = _context.EstadoIncidencias.OrderByDescending(o => o.Created).FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var estado = _context.Estados.FirstOrDefault(o => o.Id == estInc.EstadoId);
                var dealer = _context.Dealers.FirstOrDefault(o => o.Id == inc.IdDealer);
                var sintomas = _context.Sintomas.ToList();
                incidencia.Id = inc.Id;
                incidencia.numeroDocumento = cliente.Dni;
                incidencia.fechaIncidencia = inc.FechaIncidencia;
                incidencia.fechaRegistro = inc.FechaRegistro;
                incidencia.fechaCompra = motInc.FechaCompra;
                //if(inc.FechaCierre != null)
                //{
                //    incidencia.fe = inc.FechaCierre.Value;
                //}
                if (!String.IsNullOrEmpty(inc.Sintoma))
                {
                    incidencia.Sintoma = inc.Sintoma;
                    incidencia.codigoSintoma = 0;
                    if(sintomas.FirstOrDefault(o => o.Descripcion == inc.Sintoma) != null)
                    {
                        incidencia.codigoSintoma = Int32.Parse(sintomas.FirstOrDefault(o => o.Descripcion == inc.Sintoma).Codigo);
                    }
                }
                //if (inc.NroReclamoConcesionario != null)
                //{
                //    incidencia.NroReclamoConcesionario = inc.NroReclamoConcesionario.Value;
                //}
                //if (inc.NroReclamoCummins != null)
                //{
                //    incidencia.NroReclamoCummins = inc.NroReclamoCummins.Value;
                //}
                //incidencia.Descripcion = inc.Descripcion;
                //incidencia.DireccionInspeccion = inc.DireccionInspeccion;
                incidencia.latitudGps = (long)inc.LatitudGps;
                incidencia.longitudGps = (long)inc.LongitudGps;
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
                }
                incidencia.numeroOperacion = inc.NumeroOperacion;
                incidencia.numeroIncidencia = inc.NumeroIncidencia;
                incidencia.numeroChasis = motInc.NumeroChasis;
                incidencia.numeroMotor = mot.NumeroMotor;
                incidencia.Equipo = mot.Equipo;
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
                incidencia.ModeloMotor = mot.Modelo;
                incidencia.telefonoCelularContacto = cliente.Celular;
                incidencia.ObservacionesIncidencia = inc.Aux1;
                incidencia.codigoConcesionario = dealer.LocationCode;
                incidencia.nombreConcesionario = dealer.Name;
                incidencia.emailConcesionario = dealer.Email;
                incidencia.telefonoConcesionario = dealer.Phone;
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
                incidencia.nombreEstadoIncidenciaSGI = estado.Descripcion;
                if(estado.Descripcion == "Anulada")
                {
                    incidencia.observacionesAnulada = estInc.Observacion;
                }
                incidencia.idEstadoIncidenciaSGI = estado.Codigo;
                incidencia.nombreEstadoIncidencia = inc.Aux2;
                if(!String.IsNullOrEmpty(inc.Aux3))
                {
                    incidencia.idEstadoIncidencia = Int32.Parse(inc.Aux3);
                }
                incidencia.fechaEstadoIncidencia = estInc.Created;
                if (estadoG.IdEstadoGarantia != null )
                {
                    incidencia.idEstadoGarantia = estadoG.IdEstadoGarantia.Value; 
                }
                incidencia.nombreEstadoGarantia = estadoG.Nombre;
                incidencia.observacionesGarantia = estadoG.ObservacionesGarantia;
                incidencia.observacionesProveedor = estadoG.ObservacionesProveedor;
            }
            catch (Exception ex)
            {
            }
            return incidencia;

        }
    }
}
