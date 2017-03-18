using DocArcService.Interfaces;
using DocArcService.Models;
using DocArcService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace DocArcService.Controllers
{
    [System.Web.Http.Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService _service = new UserService();

        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> CreateUser()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }

                var result = await _service.CreateUserAsync(Request.Content);

                if (result)
                {
                    return Ok(result);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteUserById(string UserId)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }

                var result = _service.DeleteUserByIdAsync(UserId).Result;

                if (result)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteUserByProviderName(string providerName)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }

                var result = _service.DeleteUserByProviderNameAsync(providerName).Result;

                if (result)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}