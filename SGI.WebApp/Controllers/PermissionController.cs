using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Interfaces;
using SGI.ApplicationCore.Services;
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
    public class PermissionController : BaseController<Permission>
    {
        public PermissionController(IServiceBase<Permission> service, IMapper mapper, ILogger<PermissionController> logger) : base(service, mapper, logger)
        {

        }

        // GET api/permission
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ActionResult<IEnumerable<PermissionApiModel>>> Get([FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<PermissionApiModel>(include, filterBy, orderBy, desc);

        // GET api/permission
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{pagesize}")]
        public Task<ActionResult<PagedResultViewModel<PermissionApiModel>>> Get(int page, int pagesize, [FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<PermissionApiModel>(page, pagesize, include, filterBy, orderBy, desc);

        // GET api/permission/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<ActionResult<PermissionApiModel>> Get(Guid id, [FromQuery] List<string> include = null) => Query<PermissionApiModel>(id, include);

        // POST api/permission
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> Post([FromBody] PermissionApiModel value) => Insert(value);

        // PUT api/permission/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult> Put(Guid id, [FromBody] PermissionApiModel value) => Update(id, value);


        // DELETE api/permission/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(Guid id) => Drop(id);

        protected override Expression<Func<Permission, bool>> BuildFilterExpression(string filterBy)
         => (Permission x) => x.Name.Contains(filterBy);

    }
}