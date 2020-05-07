using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSLY.Repository;
using CSLY.Models;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using CSLY.ViewModel;
using CSLY.googledriveapi;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using System.Web;

namespace CSLY.Controllers.api
{
    public class ProductsController : ApiController 
    {
        protected GenericUnitofWork dbContext = new GenericUnitofWork();

        public IHttpActionResult GetProducts()
        {
            return Ok(dbContext.GetRepositoryInstance<Product>().
                GetAllInclude(p => p.Category).
                Where(p => p.IsActive == true).ToList());
        }

        public IHttpActionResult GetProduct(int id)
        {
            return Ok(dbContext.GetRepositoryInstance<Product>()
                .GetAllInclude(p=>p.Category)
                .Where(p=>p.IsActive == true && p.ProductId == id));
        }

        [HttpPost]
        public IHttpActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            product.IsActive = true;
            product.CreatedDate = DateTime.Now;
            product.ImagePath = googledrive.FileUpload(product.ImagePath);
            dbContext.GetRepositoryInstance<Product>().Add(product);
            dbContext.Complete();
            return Ok(product);
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(Product product , int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var productInDb = dbContext.GetRepositoryInstance<Product>().GetFirstOrDefault(id);
            if (productInDb == null)
                return NotFound();
            productInDb.ProductName = product.ProductName;
            productInDb.CategoryId = product.CategoryId;
            productInDb.Amount = product.Amount;
            productInDb.Price = product.Price;
            productInDb.ModifiedDate = DateTime.Now;
            if(product.ImagePath != null)
                productInDb.ImagePath = product.ImagePath;
            dbContext.Complete();
            return Ok(productInDb);
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            //var productId = new Product { ProductId = id };
            var productInDb = dbContext.GetRepositoryInstance<Product>().GetFirstOrDefault(id);
            dbContext.GetRepositoryInstance<Product>().Remove(productInDb);
            dbContext.Complete();
            if(productInDb.ImagePath != null)
                googledrive.DeleteFile(productInDb.ImagePath);
            return Ok();
        }

        
    }
}
