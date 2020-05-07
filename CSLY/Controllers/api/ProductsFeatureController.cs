using CSLY.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSLY.Models;

namespace CSLY.Controllers.api
{
    public class ProductsFeatureController : ApiController
    {
        protected GenericUnitofWork dbContext = new GenericUnitofWork();


        public IHttpActionResult GetTop10ProductsInEachCategory()
        {
            var productInDb = dbContext.GetRepositoryInstance<Product>()
                .GetAllInclude(p=>p.Category)
                .GroupBy(p => p.Category.CategoryName)
                .OrderBy(c => c.Key);
            return Ok(productInDb);
        }
    }
}
