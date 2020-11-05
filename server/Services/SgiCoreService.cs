using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Sgi.Data;
using Sgi.Models;

namespace Sgi
{
    public partial class SgiCoreService
    {
        private readonly SgiCoreContext context;
        private readonly NavigationManager navigationManager;

        public SgiCoreService(SgiCoreContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public async Task ExportClientesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/clientes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/clientes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportClientesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/clientes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/clientes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnClientesRead(ref IQueryable<Models.SgiCore.Cliente> items);

        public async Task<IQueryable<Models.SgiCore.Cliente>> GetClientes(Query query = null)
        {
            var items = context.Clientes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnClientesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnClienteCreated(Models.SgiCore.Cliente item);

        public async Task<Models.SgiCore.Cliente> CreateCliente(Models.SgiCore.Cliente cliente)
        {
            OnClienteCreated(cliente);
            cliente.Id = Guid.NewGuid();
            context.Clientes.Add(cliente);
            context.SaveChanges();

            return cliente;
        }


        public async Task<IncidenciaApi> CreateIncidenciaApi(Sgi.Models.IncidenciaApi entity)
        {
            Models.SgiCore.Incidencia inc = new Models.SgiCore.Incidencia();
            Models.SgiCore.Motor mot = new Models.SgiCore.Motor();
            Models.SgiCore.Estado est = context.Estados.FirstOrDefault(o => o.Descripcion == "En Proceso");
            Models.SgiCore.Cliente cli = new Models.SgiCore.Cliente();
            Models.SgiCore.Falla fall = new Models.SgiCore.Falla();
            Models.SgiCore.EstadoGarantium estadoGarantium = new Models.SgiCore.EstadoGarantium();
            Models.SgiCore.EstadoIncidencium estInc = new Models.SgiCore.EstadoIncidencium();
            inc.Id = Guid.NewGuid();
            inc.NumeroIncidencia = entity.numeroIncidencia;
            inc.NumeroOperacion = entity.numeroOperacion;
            if (entity.fechaIncidencia != null)
            {
                inc.FechaIncidencia = entity.fechaIncidencia.Value;
            }
            if (entity.fechaRegistro != null)
            {
                inc.FechaRegistro = entity.fechaRegistro.Value;
            }
            if (entity.fechaCierre != null)
            {
                inc.FechaCierre = entity.fechaCierre.Value;
            }
            inc.NroReclamoConcesionario = entity.NroReclamoConcesionario;
            inc.NroReclamoCummins = entity.NroReclamoCummins;
            inc.Descripcion = entity.Descripcion;
            inc.LatitudGps = entity.latitudGps;
            inc.LongitudGps = entity.latitudGps;
            inc.PathImagenes = entity.PathImagenes;
            inc.Sintoma = entity.Sintoma;
            inc.EsGarantia = entity.EsGarantia;
            context.Incidencia.Add(inc);
            mot = context.Motors.FirstOrDefault(o => o.NumeroChasis == entity.numeroChasis && o.NumeroMotor == entity.numeroMotor);
            if (mot == null)
            {
                mot = new Models.SgiCore.Motor();
                mot.Id = Guid.NewGuid();
                mot.Equipo = entity.Equipo;
                mot.Modelo = entity.ModeloMotor;
                mot.NumeroChasis = entity.numeroChasis;
                mot.ModeloEquipo = entity.ModeloEquipo;
                mot.NumeroMotor = entity.numeroMotor;
                mot.HsKm = entity.horasTractor;
                context.Motors.Add(mot);
            }
            Models.SgiCore.MotorIncidencium mots = new Models.SgiCore.MotorIncidencium();
            mots.IncidenciaId = inc.Id;
            mots.MotorId = mot.Id;
            mots.Id = Guid.NewGuid();
            if (entity.fechaCompra != null)
            {
                mots.FechaCompra = entity.fechaCompra.Value;
            }
            if (entity.fechaFalla != null)
            {
                mots.FechaFalla = entity.fechaFalla.Value;
            }
            if (entity.fechaInicioGarantia != null)
            {
                mots.FechaInicioGarantia = entity.fechaInicioGarantia.Value;
            }    
            context.MotorIncidencia.Add(mots);

            cli = context.Clientes.FirstOrDefault(o => o.DNI == entity.numeroDocumento);
            if(cli == null)
            {
                cli = new Models.SgiCore.Cliente();
                cli.Id = Guid.NewGuid();
                cli.Nombre = entity.nombreContacto;
                cli.Telefono = entity.telefonoFijoContacto;
                cli.Email = entity.emailContacto;
                cli.Direccion = entity.domicilioContacto;
                cli.Direccion = entity.DireccionInspeccion;
                context.Clientes.Add(cli);
            }
            Models.SgiCore.ClienteIncidencium clii = new Models.SgiCore.ClienteIncidencium();
            clii.ClienteId = cli.Id;
            clii.IncidenciaId = inc.Id;
            clii.Id = Guid.NewGuid();
            context.ClienteIncidencia.Add(clii);
            Models.SgiCore.EstadoIncidencium estt = new Models.SgiCore.EstadoIncidencium();
            estt.EstadoId = est.Id;
            estt.IncidenciaId = inc.Id;
            estt.Id = Guid.NewGuid();
            context.EstadoIncidencia.Add(estt);
            context.SaveChanges();
            return entity;
        }

        public IncidenciaApi ParseIncidencia(Models.SgiCore.Incidencia inc)
        {
            IncidenciaApi incidencia = new IncidenciaApi();
            try
            {
                var motInc = context.MotorIncidencia.FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var mot = context.Motors.FirstOrDefault(o => o.Id == motInc.MotorId);
                var falla = context.Fallas.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var cliInc = context.ClienteIncidencia.FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var cliente = context.Clientes.FirstOrDefault(o => o.Id == cliInc.ClienteId);
                var estadoG = context.EstadoGarantia.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var estInc = context.EstadoIncidencia.OrderByDescending(o => o.Created).FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var estado = context.Estados.FirstOrDefault(o => o.Id == estInc.EstadoId);
                var dealer = context.Dealers.FirstOrDefault(o => o.Id == inc.IdDealer);
                incidencia.Id = inc.Id;
                incidencia.fechaIncidencia = inc.FechaIncidencia;
                incidencia.fechaRegistro = inc.FechaRegistro;
                incidencia.fechaCierre = inc.FechaCierre;
                incidencia.NroReclamoConcesionario = inc.NroReclamoConcesionario.Value;
                incidencia.NroReclamoCummins = inc.NroReclamoCummins.Value;
                incidencia.Descripcion = inc.Descripcion;
                incidencia.DireccionInspeccion = inc.DireccionInspeccion;
                incidencia.latitudGps = (long)inc.LatitudGps;
                incidencia.longitudGps = (long)inc.LongitudGps;
                incidencia.PathImagenes = inc.PathImagenes;
                //incidencia.mostrarEnTv = inc.MostrarEnTv.Value;
                incidencia.numeroOperacion = inc.NumeroOperacion;
                incidencia.numeroIncidencia = inc.NumeroIncidencia;
                incidencia.numeroChasis = mot.NumeroChasis;
                incidencia.numeroMotor = mot.NumeroMotor;
                incidencia.configuracionCorta = inc.ConfiguracionCorta;
                incidencia.fechaPreEntrega = inc.FechaPreEntrega;
                incidencia.horasTractor = mot.HsKm.Value;
                //incidencia.codigoConcesionario = dealer.LocationCode;
                //incidencia.nombreConcesionario = dealer.Name;
                //incidencia.emailConcesionario = dealer.Email;
                //incidencia.telefonoConcesionario = dealer.Phone;
                incidencia.nombreContacto = cliente.Nombre;
                incidencia.tipoDniContacto = cliente.TipoDNI;
                incidencia.numeroDocumento = cliente.DNI;
                incidencia.domicilioContacto = cliente.Direccion;
                incidencia.localidadContacto = cliente.Localidad;
                incidencia.provinciaContacto = cliente.Provincia;
                incidencia.telefonoFijoContacto = cliente.Telefono;
                incidencia.emailContacto = cliente.Email;
                incidencia.latitudGpsContacto = (float)-34.500;
                incidencia.longitudGpsContacto = (float)-35.000;
                //incidencia.idFalla = falla.IdFalla.Value;
                incidencia.nombreFalla = inc.Sintoma;
                //incidencia.observacionesFalla = falla.Observaciones;
                incidencia.idEstadoIncidencia = estado.Codigo;
                incidencia.nombreEstadoIncidencia = estado.Descripcion;
                incidencia.fechaEstadoIncidencia = estInc.Created;
                //incidencia.idEstadoGarantia = estadoG.Codigo;
                //incidencia.nombreEstadoGarantia = estadoG.Nombre;
                //incidencia.observacionesGarantia = estadoG.ObservacionesGarantia;
                //incidencia.observacionesProveedor = estadoG.ObservacionesProveedor;
            }
            catch (Exception ex)
            {
                
            }
            return incidencia;

        }
        public async Task ExportClienteIncidenciaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/clienteincidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/clienteincidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportClienteIncidenciaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/clienteincidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/clienteincidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnClienteIncidenciaRead(ref IQueryable<Models.SgiCore.ClienteIncidencium> items);

        public async Task<IQueryable<Models.SgiCore.ClienteIncidencium>> GetClienteIncidencia(Query query = null)
        {
            var items = context.ClienteIncidencia.AsQueryable();

            items = items.Include(i => i.Cliente);

            items = items.Include(i => i.Incidencia);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnClienteIncidenciaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnClienteIncidenciumCreated(Models.SgiCore.ClienteIncidencium item);

        public async Task<Models.SgiCore.ClienteIncidencium> CreateClienteIncidencium(Models.SgiCore.ClienteIncidencium clienteIncidencium)
        {
            OnClienteIncidenciumCreated(clienteIncidencium);
            clienteIncidencium.Id = Guid.NewGuid();
            context.ClienteIncidencia.Add(clienteIncidencium);
            context.SaveChanges();

            return clienteIncidencium;
        }
        public async Task ExportDealersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/dealers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/dealers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDealersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/dealers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/dealers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDealersRead(ref IQueryable<Models.SgiCore.Dealer> items);

        public async Task<IQueryable<Models.SgiCore.Dealer>> GetDealers(Query query = null)
        {
            var items = context.Dealers.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDealersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDealerCreated(Models.SgiCore.Dealer item);

        public async Task<Models.SgiCore.Dealer> CreateDealer(Models.SgiCore.Dealer dealer)
        {
            OnDealerCreated(dealer);
            dealer.Id = Guid.NewGuid();
            context.Dealers.Add(dealer);
            context.SaveChanges();

            return dealer;
        }
        public async Task ExportEstadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/estados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/estados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEstadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/estados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/estados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEstadosRead(ref IQueryable<Models.SgiCore.Estado> items);

        public async Task<IQueryable<Models.SgiCore.Estado>> GetEstados(Query query = null)
        {
            var items = context.Estados.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnEstadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEstadoCreated(Models.SgiCore.Estado item);

        public async Task<Models.SgiCore.Estado> CreateEstado(Models.SgiCore.Estado estado)
        {
            OnEstadoCreated(estado);
            estado.Id = Guid.NewGuid();
            context.Estados.Add(estado);
            context.SaveChanges();

            return estado;
        }
        public async Task ExportEstadoGarantiaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/estadogarantia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/estadogarantia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEstadoGarantiaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/estadogarantia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/estadogarantia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEstadoGarantiaRead(ref IQueryable<Models.SgiCore.EstadoGarantium> items);

        public async Task<IQueryable<Models.SgiCore.EstadoGarantium>> GetEstadoGarantia(Query query = null)
        {
            var items = context.EstadoGarantia.AsQueryable();

            items = items.Include(i => i.Incidencia);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnEstadoGarantiaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEstadoGarantiumCreated(Models.SgiCore.EstadoGarantium item);

        public async Task<Models.SgiCore.EstadoGarantium> CreateEstadoGarantium(Models.SgiCore.EstadoGarantium estadoGarantium)
        {
            OnEstadoGarantiumCreated(estadoGarantium);
            estadoGarantium.Id = Guid.NewGuid();
            context.EstadoGarantia.Add(estadoGarantium);
            context.SaveChanges();

            return estadoGarantium;
        }
        public async Task ExportEstadoIncidenciaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/estadoincidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/estadoincidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEstadoIncidenciaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/estadoincidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/estadoincidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEstadoIncidenciaRead(ref IQueryable<Models.SgiCore.EstadoIncidencium> items);

        public async Task<IQueryable<Models.SgiCore.EstadoIncidencium>> GetEstadoIncidencia(Query query = null)
        {
            var items = context.EstadoIncidencia.AsQueryable();

            items = items.Include(i => i.Incidencia);

            items = items.Include(i => i.Estado);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnEstadoIncidenciaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEstadoIncidenciumCreated(Models.SgiCore.EstadoIncidencium item);

        public async Task<Models.SgiCore.EstadoIncidencium> CreateEstadoIncidencium(Models.SgiCore.EstadoIncidencium estadoIncidencium)
        {
            OnEstadoIncidenciumCreated(estadoIncidencium);
            estadoIncidencium.Id = Guid.NewGuid();
            context.EstadoIncidencia.Add(estadoIncidencium);
            context.SaveChanges();

            return estadoIncidencium;
        }
        public async Task ExportFallasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/fallas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/fallas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFallasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/fallas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/fallas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFallasRead(ref IQueryable<Models.SgiCore.Falla> items);

        public async Task<IQueryable<Models.SgiCore.Falla>> GetFallas(Query query = null)
        {
            var items = context.Fallas.AsQueryable();

            items = items.Include(i => i.Incidencia);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnFallasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFallaCreated(Models.SgiCore.Falla item);

        public async Task<Models.SgiCore.Falla> CreateFalla(Models.SgiCore.Falla falla)
        {
            OnFallaCreated(falla);
            falla.Id = Guid.NewGuid();
            context.Fallas.Add(falla);
            context.SaveChanges();

            return falla;
        }
        public async Task ExportIncidenciaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/incidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/incidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportIncidenciaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/incidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/incidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnIncidenciaRead(ref IQueryable<Models.SgiCore.Incidencia> items);

        public async Task<IQueryable<Models.SgiCore.Incidencia>> GetIncidencia(Query query = null)
        {
            var items = context.Incidencia.AsQueryable();

            items = items.Include(i => i.Dealer);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnIncidenciaRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task<IQueryable<Sgi.Models.IncidenciaApi>> GetIncidenciaApi(Query query = null)
        {
            var items = context.Incidencia.AsQueryable();

            items = items.Include(i => i.Dealer);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            var apis = new List<Sgi.Models.IncidenciaApi>();
            foreach (var item in items)
            {
                apis.Add(ParseIncidencia(item));
            }
            
            return await Task.FromResult(apis.AsQueryable());
        }

        partial void OnIncidenciaCreated(Models.SgiCore.Incidencia item);

        public async Task<Models.SgiCore.Incidencia> CreateIncidencia(Models.SgiCore.Incidencia incidencia)
        {
            OnIncidenciaCreated(incidencia);
            incidencia.Id = Guid.NewGuid();
            context.Incidencia.Add(incidencia);
            context.SaveChanges();

            return incidencia;
        }
        public async Task ExportMotorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/motors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/motors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMotorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/motors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/motors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMotorsRead(ref IQueryable<Models.SgiCore.Motor> items);

        public async Task<IQueryable<Models.SgiCore.Motor>> GetMotors(Query query = null)
        {
            var items = context.Motors.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnMotorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMotorCreated(Models.SgiCore.Motor item);

        public async Task<Models.SgiCore.Motor> CreateMotor(Models.SgiCore.Motor motor)
        {
            OnMotorCreated(motor);
            motor.Id = Guid.NewGuid();
            context.Motors.Add(motor);
            context.SaveChanges();

            return motor;
        }
        public async Task ExportMotorIncidenciaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/motorincidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/motorincidencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMotorIncidenciaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/motorincidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/motorincidencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMotorIncidenciaRead(ref IQueryable<Models.SgiCore.MotorIncidencium> items);

        public async Task<IQueryable<Models.SgiCore.MotorIncidencium>> GetMotorIncidencia(Query query = null)
        {
            var items = context.MotorIncidencia.AsQueryable();

            items = items.Include(i => i.Motor);

            items = items.Include(i => i.Incidencia);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnMotorIncidenciaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMotorIncidenciumCreated(Models.SgiCore.MotorIncidencium item);

        public async Task<Models.SgiCore.MotorIncidencium> CreateMotorIncidencium(Models.SgiCore.MotorIncidencium motorIncidencium)
        {
            OnMotorIncidenciumCreated(motorIncidencium);
            motorIncidencium.Id = Guid.NewGuid();
            context.MotorIncidencia.Add(motorIncidencium);
            context.SaveChanges();

            return motorIncidencium;
        }
        public async Task ExportPermissionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/permissions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/permissions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPermissionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/permissions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/permissions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPermissionsRead(ref IQueryable<Models.SgiCore.Permission> items);

        public async Task<IQueryable<Models.SgiCore.Permission>> GetPermissions(Query query = null)
        {
            var items = context.Permissions.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPermissionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPermissionCreated(Models.SgiCore.Permission item);

        public async Task<Models.SgiCore.Permission> CreatePermission(Models.SgiCore.Permission permission)
        {
            OnPermissionCreated(permission);

            context.Permissions.Add(permission);
            context.SaveChanges();

            return permission;
        }
        public async Task ExportRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolesRead(ref IQueryable<Models.SgiCore.Role> items);

        public async Task<IQueryable<Models.SgiCore.Role>> GetRoles(Query query = null)
        {
            var items = context.Roles.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoleCreated(Models.SgiCore.Role item);

        public async Task<Models.SgiCore.Role> CreateRole(Models.SgiCore.Role role)
        {
            OnRoleCreated(role);

            context.Roles.Add(role);
            context.SaveChanges();

            return role;
        }
        public async Task ExportRolePermissionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/rolepermissions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/rolepermissions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolePermissionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/rolepermissions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/rolepermissions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolePermissionsRead(ref IQueryable<Models.SgiCore.RolePermission> items);

        public async Task<IQueryable<Models.SgiCore.RolePermission>> GetRolePermissions(Query query = null)
        {
            var items = context.RolePermissions.AsQueryable();

            items = items.Include(i => i.Role);

            items = items.Include(i => i.Permission);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnRolePermissionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRolePermissionCreated(Models.SgiCore.RolePermission item);

        public async Task<Models.SgiCore.RolePermission> CreateRolePermission(Models.SgiCore.RolePermission rolePermission)
        {
            OnRolePermissionCreated(rolePermission);

            context.RolePermissions.Add(rolePermission);
            context.SaveChanges();

            return rolePermission;
        }
        public async Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsersRead(ref IQueryable<Models.SgiCore.User> items);

        public async Task<IQueryable<Models.SgiCore.User>> GetUsers(Query query = null)
        {
            var items = context.Users.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserCreated(Models.SgiCore.User item);

        public async Task<Models.SgiCore.User> CreateUser(Models.SgiCore.User user)
        {
            OnUserCreated(user);

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }
        public async Task ExportUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgicore/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgicore/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUserRolesRead(ref IQueryable<Models.SgiCore.UserRole> items);

        public async Task<IQueryable<Models.SgiCore.UserRole>> GetUserRoles(Query query = null)
        {
            var items = context.UserRoles.AsQueryable();

            items = items.Include(i => i.User);

            items = items.Include(i => i.Role);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUserRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserRoleCreated(Models.SgiCore.UserRole item);

        public async Task<Models.SgiCore.UserRole> CreateUserRole(Models.SgiCore.UserRole userRole)
        {
            OnUserRoleCreated(userRole);

            context.UserRoles.Add(userRole);
            context.SaveChanges();

            return userRole;
        }

        partial void OnClienteDeleted(Models.SgiCore.Cliente item);

        public async Task<Models.SgiCore.Cliente> DeleteCliente(Guid? id)
        {
            var item = context.Clientes
                              .Where(i => i.Id == id)
                              .Include(i => i.ClienteIncidencia)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnClienteDeleted(item);

            context.Clientes.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnClienteGet(Models.SgiCore.Cliente item);

        public async Task<Models.SgiCore.Cliente> GetClienteById(Guid? id)
        {
            var items = context.Clientes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnClienteGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Cliente> CancelClienteChanges(Models.SgiCore.Cliente item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnClienteUpdated(Models.SgiCore.Cliente item);

        public async Task<Models.SgiCore.Cliente> UpdateCliente(Guid? id, Models.SgiCore.Cliente cliente)
        {
            OnClienteUpdated(cliente);

            var item = context.Clientes
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(cliente);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return cliente;
        }

        partial void OnClienteIncidenciumDeleted(Models.SgiCore.ClienteIncidencium item);

        public async Task<Models.SgiCore.ClienteIncidencium> DeleteClienteIncidencium(Guid? id)
        {
            var item = context.ClienteIncidencia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnClienteIncidenciumDeleted(item);

            context.ClienteIncidencia.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnClienteIncidenciumGet(Models.SgiCore.ClienteIncidencium item);

        public async Task<Models.SgiCore.ClienteIncidencium> GetClienteIncidenciumById(Guid? id)
        {
            var items = context.ClienteIncidencia
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Cliente);

            items = items.Include(i => i.Incidencia);

            var item = items.FirstOrDefault();

            OnClienteIncidenciumGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.ClienteIncidencium> CancelClienteIncidenciumChanges(Models.SgiCore.ClienteIncidencium item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnClienteIncidenciumUpdated(Models.SgiCore.ClienteIncidencium item);

        public async Task<Models.SgiCore.ClienteIncidencium> UpdateClienteIncidencium(Guid? id, Models.SgiCore.ClienteIncidencium clienteIncidencium)
        {
            OnClienteIncidenciumUpdated(clienteIncidencium);

            var item = context.ClienteIncidencia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(clienteIncidencium);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return clienteIncidencium;
        }

        partial void OnDealerDeleted(Models.SgiCore.Dealer item);

        public async Task<Models.SgiCore.Dealer> DeleteDealer(Guid? id)
        {
            var item = context.Dealers
                              .Where(i => i.Id == id)
                              .Include(i => i.Incidencia)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDealerDeleted(item);

            context.Dealers.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnDealerGet(Models.SgiCore.Dealer item);

        public async Task<Models.SgiCore.Dealer> GetDealerById(Guid? id)
        {
            var items = context.Dealers
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnDealerGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Dealer> CancelDealerChanges(Models.SgiCore.Dealer item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnDealerUpdated(Models.SgiCore.Dealer item);

        public async Task<Models.SgiCore.Dealer> UpdateDealer(Guid? id, Models.SgiCore.Dealer dealer)
        {
            OnDealerUpdated(dealer);

            var item = context.Dealers
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(dealer);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return dealer;
        }

        partial void OnEstadoDeleted(Models.SgiCore.Estado item);

        public async Task<Models.SgiCore.Estado> DeleteEstado(Guid? id)
        {
            var item = context.Estados
                              .Where(i => i.Id == id)
                              .Include(i => i.EstadoIncidencia)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEstadoDeleted(item);

            context.Estados.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnEstadoGet(Models.SgiCore.Estado item);

        public async Task<Models.SgiCore.Estado> GetEstadoById(Guid? id)
        {
            var items = context.Estados
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnEstadoGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Estado> CancelEstadoChanges(Models.SgiCore.Estado item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnEstadoUpdated(Models.SgiCore.Estado item);

        public async Task<Models.SgiCore.Estado> UpdateEstado(Guid? id, Models.SgiCore.Estado estado)
        {
            OnEstadoUpdated(estado);

            var item = context.Estados
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(estado);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return estado;
        }

        partial void OnEstadoGarantiumDeleted(Models.SgiCore.EstadoGarantium item);

        public async Task<Models.SgiCore.EstadoGarantium> DeleteEstadoGarantium(Guid? id)
        {
            var item = context.EstadoGarantia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEstadoGarantiumDeleted(item);

            context.EstadoGarantia.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnEstadoGarantiumGet(Models.SgiCore.EstadoGarantium item);

        public async Task<Models.SgiCore.EstadoGarantium> GetEstadoGarantiumById(Guid? id)
        {
            var items = context.EstadoGarantia
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Incidencia);

            var item = items.FirstOrDefault();

            OnEstadoGarantiumGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.EstadoGarantium> CancelEstadoGarantiumChanges(Models.SgiCore.EstadoGarantium item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnEstadoGarantiumUpdated(Models.SgiCore.EstadoGarantium item);

        public async Task<Models.SgiCore.EstadoGarantium> UpdateEstadoGarantium(Guid? id, Models.SgiCore.EstadoGarantium estadoGarantium)
        {
            OnEstadoGarantiumUpdated(estadoGarantium);

            var item = context.EstadoGarantia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(estadoGarantium);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return estadoGarantium;
        }

        partial void OnEstadoIncidenciumDeleted(Models.SgiCore.EstadoIncidencium item);

        public async Task<Models.SgiCore.EstadoIncidencium> DeleteEstadoIncidencium(Guid? id)
        {
            var item = context.EstadoIncidencia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEstadoIncidenciumDeleted(item);

            context.EstadoIncidencia.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnEstadoIncidenciumGet(Models.SgiCore.EstadoIncidencium item);

        public async Task<Models.SgiCore.EstadoIncidencium> GetEstadoIncidenciumById(Guid? id)
        {
            var items = context.EstadoIncidencia
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Incidencia);

            items = items.Include(i => i.Estado);

            var item = items.FirstOrDefault();

            OnEstadoIncidenciumGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.EstadoIncidencium> CancelEstadoIncidenciumChanges(Models.SgiCore.EstadoIncidencium item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnEstadoIncidenciumUpdated(Models.SgiCore.EstadoIncidencium item);

        public async Task<Models.SgiCore.EstadoIncidencium> UpdateEstadoIncidencium(Guid? id, Models.SgiCore.EstadoIncidencium estadoIncidencium)
        {
            OnEstadoIncidenciumUpdated(estadoIncidencium);

            var item = context.EstadoIncidencia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(estadoIncidencium);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return estadoIncidencium;
        }

        partial void OnFallaDeleted(Models.SgiCore.Falla item);

        public async Task<Models.SgiCore.Falla> DeleteFalla(Guid? id)
        {
            var item = context.Fallas
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFallaDeleted(item);

            context.Fallas.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnFallaGet(Models.SgiCore.Falla item);

        public async Task<Models.SgiCore.Falla> GetFallaById(Guid? id)
        {
            var items = context.Fallas
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Incidencia);

            var item = items.FirstOrDefault();

            OnFallaGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Falla> CancelFallaChanges(Models.SgiCore.Falla item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnFallaUpdated(Models.SgiCore.Falla item);

        public async Task<Models.SgiCore.Falla> UpdateFalla(Guid? id, Models.SgiCore.Falla falla)
        {
            OnFallaUpdated(falla);

            var item = context.Fallas
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(falla);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return falla;
        }

        partial void OnIncidenciaDeleted(Models.SgiCore.Incidencia item);

        public async Task<Models.SgiCore.Incidencia> DeleteIncidencia(Guid? id)
        {
            var item = context.Incidencia
                              .Where(i => i.Id == id)
                              .Include(i => i.EstadoGarantia)
                              .Include(i => i.Fallas)
                              .Include(i => i.ClienteIncidencia)
                              .Include(i => i.MotorIncidencia)
                              .Include(i => i.EstadoIncidencia)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnIncidenciaDeleted(item);

            context.Incidencia.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnIncidenciaGet(Models.SgiCore.Incidencia item);

        public async Task<Models.SgiCore.Incidencia> GetIncidenciaById(Guid? id)
        {
            var items = context.Incidencia
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Dealer);

            var item = items.FirstOrDefault();

            OnIncidenciaGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Incidencia> CancelIncidenciaChanges(Models.SgiCore.Incidencia item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnIncidenciaUpdated(Models.SgiCore.Incidencia item);

        public async Task<Models.SgiCore.Incidencia> UpdateIncidencia(Guid? id, Models.SgiCore.Incidencia incidencia)
        {
            OnIncidenciaUpdated(incidencia);

            var item = context.Incidencia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(incidencia);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return incidencia;
        }

        partial void OnMotorDeleted(Models.SgiCore.Motor item);

        public async Task<Models.SgiCore.Motor> DeleteMotor(Guid? id)
        {
            var item = context.Motors
                              .Where(i => i.Id == id)
                              .Include(i => i.MotorIncidencia)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMotorDeleted(item);

            context.Motors.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnMotorGet(Models.SgiCore.Motor item);

        public async Task<Models.SgiCore.Motor> GetMotorById(Guid? id)
        {
            var items = context.Motors
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnMotorGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Motor> CancelMotorChanges(Models.SgiCore.Motor item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnMotorUpdated(Models.SgiCore.Motor item);

        public async Task<Models.SgiCore.Motor> UpdateMotor(Guid? id, Models.SgiCore.Motor motor)
        {
            OnMotorUpdated(motor);

            var item = context.Motors
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(motor);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return motor;
        }

        partial void OnMotorIncidenciumDeleted(Models.SgiCore.MotorIncidencium item);

        public async Task<Models.SgiCore.MotorIncidencium> DeleteMotorIncidencium(Guid? id)
        {
            var item = context.MotorIncidencia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMotorIncidenciumDeleted(item);

            context.MotorIncidencia.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnMotorIncidenciumGet(Models.SgiCore.MotorIncidencium item);

        public async Task<Models.SgiCore.MotorIncidencium> GetMotorIncidenciumById(Guid? id)
        {
            var items = context.MotorIncidencia
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Motor);

            items = items.Include(i => i.Incidencia);

            var item = items.FirstOrDefault();

            OnMotorIncidenciumGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.MotorIncidencium> CancelMotorIncidenciumChanges(Models.SgiCore.MotorIncidencium item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnMotorIncidenciumUpdated(Models.SgiCore.MotorIncidencium item);

        public async Task<Models.SgiCore.MotorIncidencium> UpdateMotorIncidencium(Guid? id, Models.SgiCore.MotorIncidencium motorIncidencium)
        {
            OnMotorIncidenciumUpdated(motorIncidencium);

            var item = context.MotorIncidencia
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(motorIncidencium);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return motorIncidencium;
        }

        partial void OnPermissionDeleted(Models.SgiCore.Permission item);

        public async Task<Models.SgiCore.Permission> DeletePermission(Guid? id)
        {
            var item = context.Permissions
                              .Where(i => i.Id == id)
                              .Include(i => i.RolePermissions)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPermissionDeleted(item);

            context.Permissions.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnPermissionGet(Models.SgiCore.Permission item);

        public async Task<Models.SgiCore.Permission> GetPermissionById(Guid? id)
        {
            var items = context.Permissions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnPermissionGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Permission> CancelPermissionChanges(Models.SgiCore.Permission item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPermissionUpdated(Models.SgiCore.Permission item);

        public async Task<Models.SgiCore.Permission> UpdatePermission(Guid? id, Models.SgiCore.Permission permission)
        {
            OnPermissionUpdated(permission);

            var item = context.Permissions
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(permission);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return permission;
        }

        partial void OnRoleDeleted(Models.SgiCore.Role item);

        public async Task<Models.SgiCore.Role> DeleteRole(Guid? id)
        {
            var item = context.Roles
                              .Where(i => i.Id == id)
                              .Include(i => i.RolePermissions)
                              .Include(i => i.UserRoles)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoleDeleted(item);

            context.Roles.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnRoleGet(Models.SgiCore.Role item);

        public async Task<Models.SgiCore.Role> GetRoleById(Guid? id)
        {
            var items = context.Roles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnRoleGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.Role> CancelRoleChanges(Models.SgiCore.Role item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnRoleUpdated(Models.SgiCore.Role item);

        public async Task<Models.SgiCore.Role> UpdateRole(Guid? id, Models.SgiCore.Role role)
        {
            OnRoleUpdated(role);

            var item = context.Roles
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(role);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return role;
        }

        partial void OnRolePermissionDeleted(Models.SgiCore.RolePermission item);

        public async Task<Models.SgiCore.RolePermission> DeleteRolePermission(Guid? id)
        {
            var item = context.RolePermissions
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRolePermissionDeleted(item);

            context.RolePermissions.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnRolePermissionGet(Models.SgiCore.RolePermission item);

        public async Task<Models.SgiCore.RolePermission> GetRolePermissionById(Guid? id)
        {
            var items = context.RolePermissions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Role);

            items = items.Include(i => i.Permission);

            var item = items.FirstOrDefault();

            OnRolePermissionGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.RolePermission> CancelRolePermissionChanges(Models.SgiCore.RolePermission item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnRolePermissionUpdated(Models.SgiCore.RolePermission item);

        public async Task<Models.SgiCore.RolePermission> UpdateRolePermission(Guid? id, Models.SgiCore.RolePermission rolePermission)
        {
            OnRolePermissionUpdated(rolePermission);

            var item = context.RolePermissions
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(rolePermission);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return rolePermission;
        }

        partial void OnUserDeleted(Models.SgiCore.User item);

        public async Task<Models.SgiCore.User> DeleteUser(Guid? id)
        {
            var item = context.Users
                              .Where(i => i.Id == id)
                              .Include(i => i.UserRoles)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(item);

            context.Users.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnUserGet(Models.SgiCore.User item);

        public async Task<Models.SgiCore.User> GetUserById(Guid? id)
        {
            var items = context.Users
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnUserGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.User> CancelUserChanges(Models.SgiCore.User item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnUserUpdated(Models.SgiCore.User item);

        public async Task<Models.SgiCore.User> UpdateUser(Guid? id, Models.SgiCore.User user)
        {
            OnUserUpdated(user);

            var item = context.Users
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(user);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return user;
        }

        partial void OnUserRoleDeleted(Models.SgiCore.UserRole item);

        public async Task<Models.SgiCore.UserRole> DeleteUserRole(Guid? id)
        {
            var item = context.UserRoles
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserRoleDeleted(item);

            context.UserRoles.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnUserRoleGet(Models.SgiCore.UserRole item);

        public async Task<Models.SgiCore.UserRole> GetUserRoleById(Guid? id)
        {
            var items = context.UserRoles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.User);

            items = items.Include(i => i.Role);

            var item = items.FirstOrDefault();

            OnUserRoleGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.SgiCore.UserRole> CancelUserRoleChanges(Models.SgiCore.UserRole item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnUserRoleUpdated(Models.SgiCore.UserRole item);

        public async Task<Models.SgiCore.UserRole> UpdateUserRole(Guid? id, Models.SgiCore.UserRole userRole)
        {
            OnUserRoleUpdated(userRole);

            var item = context.UserRoles
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(userRole);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return userRole;
        }
    }
}
