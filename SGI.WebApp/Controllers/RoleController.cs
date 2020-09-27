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
    public class RoleController : BaseController<Role>
    {
        public RoleController(IServiceBase<Role> service, IMapper mapper, ILogger<RoleController> logger) : base(service, mapper, logger)
        {

        }

        // GET api/role
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ActionResult<IEnumerable<RoleApiModel>>> Get([FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<RoleApiModel>(include, filterBy, orderBy, desc);

        // GET api/role
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{pagesize}")]
        public Task<ActionResult<PagedResultViewModel<RoleApiModel>>> Get(int page, int pagesize, [FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<RoleApiModel>(page, pagesize, include, filterBy, orderBy, desc);

        // GET api/role/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<ActionResult<RoleApiModel>> Get(Guid id, [FromQuery] List<string> include = null) => Query<RoleApiModel>(id, include);

        // POST api/role
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> Post([FromBody] RoleApiModel value) => Insert(value);

        // PUT api/role/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult> Put(Guid id, [FromBody] RoleApiModel value) => Update(id, value);


        // DELETE api/role/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(Guid id) => Drop(id);

        protected override Expression<Func<Role, bool>> BuildFilterExpression(string filterBy)
         => (Role x) => x.Name.Contains(filterBy);

    }
}