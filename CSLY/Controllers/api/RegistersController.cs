using CSLY.Models;
using CSLY.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;

namespace CSLY.Controllers.api
{
    public class RegistersController : ApiController
    {

        private GenericUnitofWork dbContext = new GenericUnitofWork();
        //to access AspNetusers table
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult RegisterUser(RegisterationModel userData)
        {
            //Add password from userData into AccountInfo table then 

            if (!ModelState.IsValid || userData.Password != userData.ConfirmedPassword)
                return BadRequest();


            AccountInfo accountInfo = new AccountInfo()
            { Email = userData.Email , PhoneNumber = userData.PhoneNumber };
            dbContext.GetRepositoryInstance<AccountInfo>()
                .Add(accountInfo);
           
            //check if user exist in aspnetusers or not
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var checkIfUserExist = userManager.FindByName(userData.UserName);
            if (checkIfUserExist != null)
                return BadRequest();

            dbContext.Complete();

            //the id generated from this process will be
            //added tp AspNetusers clientId field as a foreign key and username too 

            var user = new ApplicationUser
            { Client_Id = accountInfo.Id, UserName = userData.UserName };

            var check = userManager.Create(user, userData.Password);
            if (!check.Succeeded)
                return BadRequest();

            userManager.AddToRole(user.Id, "User");
            return Ok(userData);
        }
    }
}
