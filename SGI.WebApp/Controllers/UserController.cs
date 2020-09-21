using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Exceptions;
using SGI.ApplicationCore.Interfaces;
using SGI.WebApp.ApiModels;
using SGI.WebApp.ApiModels.ApiErrors;
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
    public class UserController : BaseController<User>
    {

        IServiceBase<User> userService;

        public UserController(IServiceBase<User> service, IMapper mapper, ILogger<UserController> logger) : base(service, mapper, logger)
        {
            userService = service;
        }

        // GET api/user
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ActionResult<IEnumerable<UserApiModel>>> Get([FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<UserApiModel>(include, filterBy, orderBy, desc);

        // GET api/user
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{pagesize}")]
        public Task<ActionResult<PagedResultViewModel<UserApiModel>>> Get(int page, int pagesize, [FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<UserApiModel>(page, pagesize, include, filterBy, orderBy, desc);

        // GET api/user/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<ActionResult<UserApiModel>> Get(Guid id, [FromQuery] List<string> include = null) => Query<UserApiModel>(id, include);

        // POST api/user
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserApiModel value)
        {
            try
            {
                User current = await Task.Factory.StartNew(() => Mapper.Map<User>(value));
                Guid result = await Service.InsertAsync(current);
                return Ok(result);
            }
            catch (BusinessException business)
            {
                return BadRequest(new BadRequest(business.Message));
            }
            catch (Exception ex)
            {
                return NotFound(new InternalServerError(ex.Message));
            }

        }
        // PUT api/user/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult> Put(Guid id, [FromBody] UserApiModel value) => Update(id, value);

        // DELETE api/user/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(Guid id)
        {

            return Drop(id);
        }

        protected override Expression<Func<User, bool>> BuildFilterExpression(string filterBy)
         => (User x) => x.UserName.Contains(filterBy, StringComparison.CurrentCultureIgnoreCase);
    }
}