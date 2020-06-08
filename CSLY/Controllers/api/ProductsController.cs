using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CSLY.Repository;
using CSLY.Models;
using CSLY.MyAuthorizations;
using AutoMapper;
using CSLY.Dtos;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CSLY.Controllers.api
{
    [JwtAuthorize]
    public class ProductsController : ApiController 
    {
        private GenericUnitofWork dbContext = new GenericUnitofWork();
        public static Cloudinary cloudinary;

        public const string CLOUD_NAME = "dcrllmnai";
        public const string API_KEY = "581332317551619";
        public const string API_SECRET = "Jm_ZH12L6l2RURuM37zqmjwGwBo";

        public IHttpActionResult GetProducts()
        {
            if (GetUserRole().Equals("Admin"))
            {
               return Ok(dbContext.GetRepositoryInstance<Product>().
               GetAllInclude(p => p.Category).
               Where(p => p.IsActive == true).ToList());
            }
            else if (GetUserRole().Equals("User"))
            {
                return Ok(dbContext.GetRepositoryInstance<Product>().
                GetAllInclude(p => p.Category).
                Where(p => p.IsActive == true && p.Amount > 0).ToList()
                .Select(Mapper.Map<Product, ProductDto>));
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        public IHttpActionResult GetRandomProductsDto()
        {
            return Ok(dbContext.GetRepositoryInstance<Product>().
                GetAllInclude(p => p.Category).
                Where(p => p.IsActive == true).ToList()
                .Select(Mapper.Map<Product, ProductDto>)
                .OrderBy(r => Guid.NewGuid()).Take(6));
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

            //upload image to cloudinary and return image url then save to image Path
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(product.ImagePath)
                };
                //new image path on cloudinary
                product.ImagePath = cloudinary.Upload(uploadParams).Url.ToString();
            }
            catch (Exception e)
            {
                return NotFound();
            }
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
            return Ok();
        }

        private string GetUserRole()
        {
            var path = HttpContext.Current.Server.MapPath("/loginRole.json");
            string userRole;
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject myObject = (JObject)JToken.ReadFrom(reader);
                IEnumerable<JProperty> myProperties = myObject.Properties();
                JProperty role = myProperties.FirstOrDefault();
                userRole = (string)role.Value.First;
            }
            return userRole;
        }
    }
}
