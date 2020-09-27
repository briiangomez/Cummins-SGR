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
    public class DealerController : BaseController<Dealer>
    {
        public DealerController(IServiceBase<Dealer> service, IMapper mapper, ILogger<DealerController> logger) : base(service, mapper, logger)
        {

        }

        // GET api/Dealer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ActionResult<IEnumerable<DealerApiModel>>> Get([FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<DealerApiModel>(include, filterBy, orderBy, desc);

        // GET api/Dealer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{pagesize}")]
        public Task<ActionResult<PagedResultViewModel<DealerApiModel>>> Get(int page, int pagesize, [FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<DealerApiModel>(page, pagesize, include, filterBy, orderBy, desc);

        // GET api/Dealer/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<ActionResult<DealerApiModel>> Get(Guid id, [FromQuery] List<string> include = null) => Query<DealerApiModel>(id, include);

        // POST api/Dealer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> Post([FromBody] DealerApiModel value) => Insert(value);

        // PUT api/Dealer/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult> Put(Guid id, [FromBody] DealerApiModel value) => Update(id, value);


        // DELETE api/Dealer/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(Guid id) => Drop(id);

        protected override Expression<Func<Dealer, bool>> BuildFilterExpression(string filterBy)
         => (Dealer x) => x.Name.Contains(filterBy);

    }
}