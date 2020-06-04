using System;
using System.Web;
using CSLY.Controllers.api;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using CSLY.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.UI;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace CSLY.MyAuthorizations
{

    public class JwtAuthorize : AuthorizeAttribute 
    {
        protected override bool IsAuthorized(HttpActionContext httpContext)
        {
            JwtController jwtactions = new JwtController();
            var request = httpContext.Request;
            var headers = request.Headers;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (headers.Contains("Authorization"))
            {
                string token = headers.GetValues("Authorization").FirstOrDefault();
                if (token != null)
                {
                    string tokenUsername = jwtactions.ValidateToken(token);

                    if (tokenUsername == null)
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    else if (UserManager.FindByName(tokenUsername) == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        var userRole = UserManager.GetRoles(UserManager.FindByName(tokenUsername).Id);
                        var path = HttpContext.Current.Server.MapPath("/loginRole.json");
                        JObject userData = new JObject(
                            new JProperty("Role", userRole));

                        using (StreamWriter file = File.CreateText(path))
                        using (JsonTextWriter writer = new JsonTextWriter(file))
                        {
                            userData.WriteTo(writer);
                        }

                        return true;
                    }
                }
                return false;
            }
            return false;
        }
    }
}