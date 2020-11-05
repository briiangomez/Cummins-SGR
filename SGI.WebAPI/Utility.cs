using SGIWebApi.Models;
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
                incidencia.fechaIncidencia = inc.FechaIncidencia;
                incidencia.fechaRegistro = inc.FechaRegistro;
                incidencia.fechaCierre = inc.FechaCierre;
                if(!String.IsNullOrEmpty(inc.Sintoma))
                {
                    incidencia.Sintoma = sintomas.FirstOrDefault(o => o.Codigo == inc.Sintoma).Descripcion;
                }
                if (inc.NroReclamoConcesionario != null)
                {
                    incidencia.NroReclamoConcesionario = inc.NroReclamoConcesionario.Value;
                }
                if (inc.NroReclamoCummins != null)
                {
                    incidencia.NroReclamoCummins = inc.NroReclamoCummins.Value;
                }
                incidencia.Descripcion = inc.Descripcion;
                incidencia.DireccionInspeccion = inc.DireccionInspeccion;
                incidencia.latitudGps = (long)inc.LatitudGps;
                incidencia.longitudGps = (long)inc.LongitudGps;
                incidencia.PathImagenes = inc.PathImagenes;
                if (inc.MostrarEnTv != null)
                {
                    incidencia.mostrarEnTv = inc.MostrarEnTv.Value;
                }
                incidencia.numeroOperacion = inc.NumeroOperacion;
                incidencia.numeroIncidencia = inc.NumeroIncidencia;
                incidencia.numeroChasis = motInc.NumeroChasis;
                incidencia.numeroMotor = mot.NumeroMotor;
                incidencia.configuracionCorta = inc.ConfiguracionCorta;
                incidencia.fechaPreEntrega = inc.FechaPreEntrega;
                if (motInc.HsKm != null)
                {
                    incidencia.horasTractor = motInc.HsKm.Value;
                }
                incidencia.IdConcesionario = dealer.Id;
                incidencia.codigoConcesionario = dealer.LocationCode;
                incidencia.nombreConcesionario = dealer.Name;
                incidencia.emailConcesionario = dealer.Email;
                incidencia.telefonoConcesionario = dealer.Phone;
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
                incidencia.idEstadoIncidencia = estado.Codigo;
                incidencia.nombreEstadoIncidencia = estado.Descripcion;
                incidencia.fechaEstadoIncidencia = estInc.Created;
                incidencia.idEstadoGarantia = estadoG.Codigo;
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
