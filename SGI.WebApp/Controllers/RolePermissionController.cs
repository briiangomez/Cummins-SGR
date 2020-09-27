using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Interfaces;
using SGI.WebApp.ApiModels;
using SGI.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGI.WebApp.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class RolePermissionController : BaseController<RolePermission>
    {
        public RolePermissionController(IServiceBase<RolePermission> service, IMapper mapper, ILogger<RolePermissionController> logger) : base(service, mapper, logger)
        {

        }

        // GET api/rolepermission
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ActionResult<IEnumerable<RolePermissionApiModel>>> Get([FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<RolePermissionApiModel>(include, filterBy, orderBy, desc);

        // GET api/rolepermission
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{pagesize}")]
        public Task<ActionResult<PagedResultViewModel<RolePermissionApiModel>>> Get(int page, int pagesize, [FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<RolePermissionApiModel>(page, pagesize, include, filterBy, orderBy, desc);

        // GET api/rolepermission/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<ActionResult<RolePermissionApiModel>> Get(Guid id, [FromQuery] List<string> include = null) => Query<RolePermissionApiModel>(id, include);

        // POST api/rolepermission
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> Post([FromBody] RolePermissionApiModel value) => Insert(value);

        // PUT api/rolepermission/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult> Put(Guid id, [FromBody] RolePermissionApiModel value) => Update(id, value);


        // DELETE api/rolepermission/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(Guid id) => Drop(id);

        protected override Expression<Func<RolePermission, bool>> BuildFilterExpression(string filterBy)
         => (RolePermission x) => x.Permission.Name.Contains(filterBy) || x.Role.Name.Contains(filterBy);

    }
}