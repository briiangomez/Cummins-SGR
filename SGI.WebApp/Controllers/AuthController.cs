using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.Exceptions;
using SGI.Infrastructure.Entities;
using SGI.Infrastructure.Interfaces;
using SGI.WebApp.ApiModels;
using SGI.WebApp.ApiModels.ApiErrors;
using System;
using System.Threading.Tasks;

namespace SGI.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {

        private IMapper mapper;
        private IAuthService authService;
        private ILogger<AuthController> logger;

        public AuthController(IMapper mapper, IAuthService authService, ILogger<AuthController> logger)
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        ///    Performs the Login against the Identity Service.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<TokenApiModel>> Login([FromBody] LoginApiModel login)
        {

            ActionResult<TokenApiModel> result = default(ActionResult<TokenApiModel>);

            try
            {
                Token token =  await authService.LoginAsync(login.UserName, login.Password);
                result = Ok(mapper.Map<TokenApiModel>(token));
            }
            catch (BusinessException business)
            {
                return BadRequest(new BadRequest(business.Message));
            }
            catch (Exception ex)
            {
                logger.LogError(this.GetType().Name, ex);
                return new StatusCodeResult(500);
            }

            return result;

        }

        /// <summary>
        ///    Performs the Login against the Identity Service.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("refresh-token")]
        public async Task<ActionResult<TokenApiModel>> RefreshToken([FromBody] string token)
        {
            ActionResult<TokenApiModel> result = default(ActionResult<TokenApiModel>);

            try
            {
                Token newToken = await authService.RefreshTokenAsync(token);
                result = Ok(mapper.Map<TokenApiModel>(newToken));
            }
            catch (BusinessException business)
            {
                return BadRequest(new BadRequest(business.Message));
            }
            catch (Exception ex)
            {
                logger.LogError(this.GetType().Name, ex);
                return new StatusCodeResult(500);
            }

            return result;

        }
    }
}