using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.DTOs;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SGI.ApplicationCore.Services
{
    public class IncidenciaApiModelService<T> : IModelService<IncidenciaApi> where T : BaseEntity
    {
        private readonly ILogger<ServiceBase<IncidenciaApi>> _logger;
        protected readonly IRepository<Incidencia> _repository;
        protected readonly IRepository<Falla> _Fallarepository;
        protected readonly IRepository<Cliente> _Clienterepository;
        protected readonly IRepository<EstadoGarantia> _EstadoGarantiarepository;
        protected readonly IRepository<Estado> _Estadorepository;
        protected readonly IRepository<Motor> _MotorRepository;
        protected readonly IRepository<EstadoIncidencia> _EstadoIncidenciarepository;
        protected readonly IRepository<ClienteIncidencia> _ClienteIncidenciarepository;
        protected readonly IRepository<MotorIncidencia> _MotorIncidenciaRepository;
        protected readonly IRepository<Dealer> _DealerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncidenciaApiModelService(ILogger<ServiceBase<IncidenciaApi>> logger, IRepository<Incidencia> repository, IRepository<Falla> Fallarepository,
            IRepository<Cliente> Clienterepository, IRepository<EstadoGarantia> EstadoGarantiarepository, IRepository<Estado> Estadorepository,
            IRepository<Motor> MotorRepository, IRepository<Dealer> DealerRepository, IRepository<EstadoIncidencia> EstadoIncidenciarepository,
            IRepository<MotorIncidencia> MotorIncidenciaRepository, IRepository<ClienteIncidencia> ClienteIncidenciarepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _Fallarepository = Fallarepository;
            _Clienterepository = Clienterepository;
            _EstadoGarantiarepository = EstadoGarantiarepository;
            _Estadorepository = Estadorepository;
            _MotorRepository = MotorRepository;
            _DealerRepository = DealerRepository;
            _EstadoIncidenciarepository = EstadoIncidenciarepository;
            _MotorIncidenciaRepository = MotorIncidenciaRepository;
            _ClienteIncidenciarepository = ClienteIncidenciarepository;
            _unitOfWork = unitOfWork;
        }

        public IncidenciaApi GetByNumero(string numero)
        {
            IncidenciaApi incidencia = new IncidenciaApi();
            try
            {
                long nro = Int64.Parse(numero);
                var inc = _repository.GetAll(null, o => o.NumeroIncidencia == nro, null, false).FirstOrDefault();
                incidencia = this.ParseIncidencia(inc);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return incidencia;
        }

        public Task<IncidenciaApi> GetByNumeroAsync(string numero)
        {
            return Task.Factory.StartNew(() => GetByNumero(numero));
        }

        public virtual IncidenciaApi[] GetAllByDate(DateTime desde, DateTime hasta)
        {
            var res = new List<IncidenciaApi>();
            try
            {
                var reps = _repository.GetAll().ToList();

                foreach (var inc in reps)
                {
                    res.Add(this.ParseIncidencia(inc));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return res.ToArray();
        }

        public virtual Task<IncidenciaApi[]> GetAllByDateAsync(DateTime desde, DateTime hasta)
        {
            return Task.Factory.StartNew(() => GetAllByDate(desde, hasta));
        }

        public Guid Insert(IncidenciaApi entity)
        {
            Incidencia inc = new Incidencia();
            Motor mot = new Motor();
            Cliente cli = new Cliente();
            Falla fal = new Falla();
            inc.Id = Guid.NewGuid();
            inc.NumeroIncidencia = entity.numeroIncidencia;
            inc.NumeroOperacion = entity.numeroOperacion;
            if(entity.fechaIncidencia != null)
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
            _repository.Insert(inc);
            mot = _MotorRepository.GetAll(null, o => o.NumeroChasis == entity.numeroChasis && o.NumeroMotor == entity.numeroMotor, null, false).FirstOrDefault();
            if(mot == null)
            {
                mot = new Motor();
                mot.Equipo = entity.Equipo;
                mot.NumeroChasis = entity.numeroChasis;
                mot.ModeloEquipo = entity.ModeloEquipo;
                mot.NumeroMotor = entity.numeroMotor;
                mot.HsKm = entity.horasTractor;
                _MotorRepository.Insert(mot);
            }
            MotorIncidencia mots = new MotorIncidencia();
            mots.IncidenciaId = inc.Id;
            mots.MotorId = mot.Id;
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
            _MotorIncidenciaRepository.Insert(mots);
            _unitOfWork.SaveChanges();
            return inc.Id;
        }

        public virtual async Task<Guid> InsertAsync(IncidenciaApi entity)
        {
            Incidencia inc = new Incidencia();
            await _repository.InsertAsync(inc);
            _unitOfWork.SaveChanges();
            return entity.Id;
        }

        public IncidenciaApi ParseIncidencia(Incidencia inc)
        {
            IncidenciaApi incidencia = new IncidenciaApi();
            try
            {
                var motInc = _MotorIncidenciaRepository.GetAll(null, o => o.IncidenciaId == inc.Id, null, false).FirstOrDefault();
                var mot = _MotorRepository.GetAll(null, o => o.Id == motInc.MotorId, null, false).FirstOrDefault();
                var falla = _Fallarepository.GetAll(null,o => o.IdIncidencia == inc.Id,null,false).FirstOrDefault();
                var cliInc = _ClienteIncidenciarepository.GetAll(null, o => o.IncidenciaId == inc.Id, null, false).FirstOrDefault();
                var cliente = _Clienterepository.GetAll(null, o => o.Id == cliInc.ClienteId, null, false).FirstOrDefault();
                var estadoG = _EstadoGarantiarepository.GetAll(null, o => o.IdIncidencia == inc.Id, null, false).FirstOrDefault();
                var estInc = _EstadoIncidenciarepository.GetAll(null, o => o.IncidenciaId == inc.Id, new List<string> { "Fecha" }, true).FirstOrDefault();
                var estado = _Estadorepository.GetAll(null, o => o.Id == estInc.Id,null,false).FirstOrDefault();
                var dealer = _DealerRepository.Get(inc.IdDealer, null);
                incidencia.Id = inc.Id;
                incidencia.fechaIncidencia = inc.FechaIncidencia;
                incidencia.fechaRegistro = inc.FechaRegistro;
                incidencia.fechaCierre = inc.FechaCierre;
                incidencia.NroReclamoConcesionario = inc.NroReclamoConcesionario;
                incidencia.NroReclamoCummins = inc.NroReclamoCummins;
                incidencia.Descripcion = inc.Descripcion;
                incidencia.DireccionInspeccion = inc.DireccionInspeccion;
                incidencia.latitudGps = (long)inc.LatitudGps;
                incidencia.longitudGps = (long)inc.LongitudGps;
                incidencia.PathImagenes = inc.PathImagenes;
                incidencia.mostrarEnTv = inc.MostrarEnTv;
                incidencia.numeroOperacion = inc.NumeroOperacion;
                incidencia.numeroIncidencia = inc.NumeroIncidencia;
                incidencia.numeroChasis = mot.NumeroChasis;
                incidencia.numeroMotor = mot.NumeroMotor;
                incidencia.configuracionCorta = inc.ConfiguracionCorta;
                incidencia.fechaPreEntrega = inc.FechaPreEntrega;
                incidencia.horasTractor = mot.HsKm;
                incidencia.codigoConcesionario = dealer.LocationCode;
                incidencia.nombreConcesionario = dealer.Name;
                incidencia.emailConcesionario = dealer.Email;
                incidencia.telefonoConcesionario = dealer.Phone;
                incidencia.tipoDniContacto = cliente.TipoDNI;
                incidencia.numeroDocumento = cliente.DNI;
                incidencia.domicilioContacto = cliente.Direccion;
                incidencia.localidadContacto = cliente.Localidad;
                incidencia.provinciaContacto = cliente.Provincia;
                incidencia.telefonoFijoContacto = cliente.Telefono;
                incidencia.emailContacto = cliente.Email;
                incidencia.latitudGpsContacto = cliente.LatitudGpsContacto;
                incidencia.longitudGpsContacto = cliente.LongitudGpsContacto;
                incidencia.idFalla = falla.IdFalla;
                incidencia.nombreFalla = falla.Nombre;
                incidencia.observacionesFalla = falla.Observaciones;
                incidencia.idEstadoIncidencia = estado.Codigo;
                incidencia.nombreEstadoIncidencia = estado.Descripcion;
                incidencia.fechaEstadoIncidencia = estInc.Fecha;
                incidencia.idEstadoGarantia = estadoG.Codigo;
                incidencia.nombreEstadoGarantia = estadoG.Nombre;
                incidencia.observacionesGarantia = estadoG.ObservacionesGarantia;
                incidencia.observacionesProveedor = estadoG.ObservacionesProveedor;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return incidencia;

        }

        public int Count()
        {
            return _repository.Count();
        }

        public Incidencia Get(Guid id, List<string> include = null)
        {
            return _repository.Get(id, include);
        }

        public Task<Incidencia> GetAsync(Guid id, List<string> include = null)
        {
            return _repository.GetAsync(id, include);
        }

        public PagedResult<Incidencia> GetPaged(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<Incidencia, bool>> predicate = null, List<string> orderBy = null)
        {
            return _repository.GetPaged(page, pagesize, include, desc, predicate, orderBy);

        }

        public Task<PagedResult<Incidencia>> GetPagedAsync(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<Incidencia, bool>> predicate = null, List<string> orderBy = null)
        {
            return Task.Factory.StartNew(() => _repository.GetPaged(page, pagesize, include, desc, predicate, orderBy));

        }

        public Incidencia[] GetAll(List<string> include = null, Expression<Func<Incidencia, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            return _repository.GetAll(include, predicate, orderBy, desc);

        }

        public Task<Incidencia[]> GetAllAsync(List<string> include = null, Expression<Func<Incidencia, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            return _repository.GetAllAsync(include, predicate, orderBy, desc);
        }

        public Guid Insert(Incidencia entity)
        {
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
            return entity.Id;
        }

        public virtual async Task<Guid> InsertAsync(Incidencia entity)
        {
            await _repository.InsertAsync(entity);
            _unitOfWork.SaveChanges();
            return entity.Id;
        }

        public void Update(Guid id, Incidencia entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(Guid id, Incidencia entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public PagedResult<IncidenciaApi> GetPaged(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<IncidenciaApi, bool>> predicate = null, List<string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<IncidenciaApi>> GetPagedAsync(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<IncidenciaApi, bool>> predicate = null, List<string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IncidenciaApi[] GetAll(List<string> include = null, Expression<Func<IncidenciaApi, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            throw new NotImplementedException();
        }

        public Task<IncidenciaApi[]> GetAllAsync(List<string> include = null, Expression<Func<IncidenciaApi, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, IncidenciaApi entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, IncidenciaApi entity)
        {
            throw new NotImplementedException();
        }

        public PagedResult<T> GetPaged(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<T>> GetPagedAsync(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public T[] GetAll(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> GetAllAsync(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            throw new NotImplementedException();
        }

        public Guid Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
