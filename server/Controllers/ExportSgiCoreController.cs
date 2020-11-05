using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sgi.Data;

namespace Sgi
{
    public partial class ExportSgiCoreController : ExportController
    {
        private readonly SgiCoreContext context;

        public ExportSgiCoreController(SgiCoreContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/SgiCore/clientes/csv")]
        [HttpGet("/export/SgiCore/clientes/csv(fileName='{fileName}')")]
        public FileStreamResult ExportClientesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Clientes, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/clientes/excel")]
        [HttpGet("/export/SgiCore/clientes/excel(fileName='{fileName}')")]
        public FileStreamResult ExportClientesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Clientes, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/clienteincidencia/csv")]
        [HttpGet("/export/SgiCore/clienteincidencia/csv(fileName='{fileName}')")]
        public FileStreamResult ExportClienteIncidenciaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.ClienteIncidencia, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/clienteincidencia/excel")]
        [HttpGet("/export/SgiCore/clienteincidencia/excel(fileName='{fileName}')")]
        public FileStreamResult ExportClienteIncidenciaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.ClienteIncidencia, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/dealers/csv")]
        [HttpGet("/export/SgiCore/dealers/csv(fileName='{fileName}')")]
        public FileStreamResult ExportDealersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Dealers, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/dealers/excel")]
        [HttpGet("/export/SgiCore/dealers/excel(fileName='{fileName}')")]
        public FileStreamResult ExportDealersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Dealers, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/estados/csv")]
        [HttpGet("/export/SgiCore/estados/csv(fileName='{fileName}')")]
        public FileStreamResult ExportEstadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Estados, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/estados/excel")]
        [HttpGet("/export/SgiCore/estados/excel(fileName='{fileName}')")]
        public FileStreamResult ExportEstadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Estados, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/estadogarantia/csv")]
        [HttpGet("/export/SgiCore/estadogarantia/csv(fileName='{fileName}')")]
        public FileStreamResult ExportEstadoGarantiaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.EstadoGarantia, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/estadogarantia/excel")]
        [HttpGet("/export/SgiCore/estadogarantia/excel(fileName='{fileName}')")]
        public FileStreamResult ExportEstadoGarantiaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.EstadoGarantia, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/estadoincidencia/csv")]
        [HttpGet("/export/SgiCore/estadoincidencia/csv(fileName='{fileName}')")]
        public FileStreamResult ExportEstadoIncidenciaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.EstadoIncidencia, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/estadoincidencia/excel")]
        [HttpGet("/export/SgiCore/estadoincidencia/excel(fileName='{fileName}')")]
        public FileStreamResult ExportEstadoIncidenciaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.EstadoIncidencia, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/fallas/csv")]
        [HttpGet("/export/SgiCore/fallas/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFallasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Fallas, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/fallas/excel")]
        [HttpGet("/export/SgiCore/fallas/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFallasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Fallas, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/incidencia/csv")]
        [HttpGet("/export/SgiCore/incidencia/csv(fileName='{fileName}')")]
        public FileStreamResult ExportIncidenciaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Incidencia, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/incidencia/excel")]
        [HttpGet("/export/SgiCore/incidencia/excel(fileName='{fileName}')")]
        public FileStreamResult ExportIncidenciaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Incidencia, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/motors/csv")]
        [HttpGet("/export/SgiCore/motors/csv(fileName='{fileName}')")]
        public FileStreamResult ExportMotorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Motors, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/motors/excel")]
        [HttpGet("/export/SgiCore/motors/excel(fileName='{fileName}')")]
        public FileStreamResult ExportMotorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Motors, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/motorincidencia/csv")]
        [HttpGet("/export/SgiCore/motorincidencia/csv(fileName='{fileName}')")]
        public FileStreamResult ExportMotorIncidenciaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.MotorIncidencia, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/motorincidencia/excel")]
        [HttpGet("/export/SgiCore/motorincidencia/excel(fileName='{fileName}')")]
        public FileStreamResult ExportMotorIncidenciaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.MotorIncidencia, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/permissions/csv")]
        [HttpGet("/export/SgiCore/permissions/csv(fileName='{fileName}')")]
        public FileStreamResult ExportPermissionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Permissions, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/permissions/excel")]
        [HttpGet("/export/SgiCore/permissions/excel(fileName='{fileName}')")]
        public FileStreamResult ExportPermissionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Permissions, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/roles/csv")]
        [HttpGet("/export/SgiCore/roles/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Roles, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/roles/excel")]
        [HttpGet("/export/SgiCore/roles/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Roles, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/rolepermissions/csv")]
        [HttpGet("/export/SgiCore/rolepermissions/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRolePermissionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.RolePermissions, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/rolepermissions/excel")]
        [HttpGet("/export/SgiCore/rolepermissions/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRolePermissionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.RolePermissions, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/users/csv")]
        [HttpGet("/export/SgiCore/users/csv(fileName='{fileName}')")]
        public FileStreamResult ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Users, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/users/excel")]
        [HttpGet("/export/SgiCore/users/excel(fileName='{fileName}')")]
        public FileStreamResult ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Users, Request.Query), fileName);
        }
        [HttpGet("/export/SgiCore/userroles/csv")]
        [HttpGet("/export/SgiCore/userroles/csv(fileName='{fileName}')")]
        public FileStreamResult ExportUserRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.UserRoles, Request.Query), fileName);
        }

        [HttpGet("/export/SgiCore/userroles/excel")]
        [HttpGet("/export/SgiCore/userroles/excel(fileName='{fileName}')")]
        public FileStreamResult ExportUserRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.UserRoles, Request.Query), fileName);
        }
    }
}
