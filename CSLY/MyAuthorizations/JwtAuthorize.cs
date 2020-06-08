using System;
using System.Web;
using CSLY.Controllers.api;
using System.Linq;
using Microsoft.AspNet.Identity;
using CSLY.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;
using System.Web.Http.Controllers;
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

             var UserManager = new UserManager<ApplicationUser>
                                (new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var request = httpContext.Request;
            var headers = request.Headers;

            bool isUserAuthorized = false;

            if (headers.Contains("Authorization"))
            {
                string token = headers.GetValues("Authorization").FirstOrDefault();
                if (token != null)
                {
                    string tokenUsername = jwtactions.ValidateToken(token);

                    isUserAuthorized =
                         (tokenUsername == null || UserManager.FindByName(tokenUsername) == null) ? false : true;

                    if (isUserAuthorized)
                    {
                        WriteUserRoleToFile(UserManager
                            .GetRoles(UserManager.FindByName(tokenUsername).Id));
                    }
                }
            }
            return isUserAuthorized;
        }

        private void WriteUserRoleToFile(dynamic userRole)
        {
            var path = HttpContext.Current.Server.MapPath("/loginRole.json");
            JObject userData = new JObject(
                new JProperty("Role", userRole));

            using (StreamWriter file = File.CreateText(path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                userData.WriteTo(writer);
            }
        }
    }
}