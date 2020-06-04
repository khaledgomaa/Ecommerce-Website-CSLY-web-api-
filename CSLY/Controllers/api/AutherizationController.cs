using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSLY.Models;
using CSLY.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace CSLY.Controllers.api
{
    public class AutherizationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ValidateToken()
        {
            var requestHeader = Request;
            var headers = requestHeader.Headers;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (headers.Contains("Authorization"))
            {
                string token = headers.GetValues("Authorization").FirstOrDefault();
                if (token != null)
                {
                    string tokenUsername = TokenManager.ValidateToken(token);
                    if (tokenUsername == null && UserManager.FindByName(tokenUsername) == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                    return Ok(tokenUsername);
                }
            }
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
