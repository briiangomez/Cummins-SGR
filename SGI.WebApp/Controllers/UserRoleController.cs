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
    public class UserRoleController : BaseController<UserRole>
    {
        public UserRoleController(IServiceBase<UserRole> service, IMapper mapper, ILogger<UserRoleController> logger) : base(service, mapper, logger)
        {

        }

        // GET api/userrole
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ActionResult<IEnumerable<UserRoleApiModel>>> Get([FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<UserRoleApiModel>(include, filterBy, orderBy, desc);

        // GET api/userrole
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{pagesize}")]
        public Task<ActionResult<PagedResultViewModel<UserRoleApiModel>>> Get(int page, int pagesize, [FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<UserRoleApiModel>(page, pagesize, include, filterBy, orderBy, desc);

        // GET api/userrole/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<ActionResult<UserRoleApiModel>> Get(Guid id, [FromQuery] List<string> include = null) => Query<UserRoleApiModel>(id, include);

        // POST api/userrole
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> Post([FromBody] UserRoleApiModel value) => Insert(value);

        // PUT api/userrole/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult> Put(Guid id, [FromBody] UserRoleApiModel value) => Update(id, value);


        // DELETE api/userrole/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(Guid id) => Drop(id);

        protected override Expression<Func<UserRole, bool>> BuildFilterExpression(string filterBy)
         => (UserRole x) => x.User.UserName.Contains(filterBy) || x.Role.Name.Contains(filterBy);

    }
}