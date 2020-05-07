using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using CSLY.Repository;
using CSLY.Models;
using System.Data.Entity;
using CSLY.ViewModel;

namespace CSLY.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        protected GenericUnitofWork dbContext = new GenericUnitofWork();
        public ActionResult Index()
        {
            var products = dbContext.GetRepositoryInstance<Product>()
                .GetAllInclude(p=>p.Category)
                .Where(p=>p.IsActive == true);
            return View(products);
        }

        public ActionResult New()
        {
            ProductCategoryImageViewModel newProduct = new ProductCategoryImageViewModel
            {
                Product = new Product(),
                Category = dbContext.GetRepositoryInstance<Category>()
                .GetAll()
                .Where(c => c.IsActive == true),
            };
            return View(newProduct);
        }

        [HttpPost]
        public ActionResult ProductManager(Product product, HttpPostedFileBase ImageFile)
        {
            if(!ModelState.IsValid)
            {
                ProductCategoryImageViewModel newProduct = new ProductCategoryImageViewModel
                {
                    Product = product,
                    Category = dbContext.GetRepositoryInstance<Category>()
                    .GetAll()
                    .Where(c => c.IsActive == true),
                };
                return View("New", newProduct);
            }
            if(product.ProductId == 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                fileName += DateTime.Now.ToString("yymmssfff") + extension;
                product.ImagePath = "~/Image/" + fileName;
                product.IsActive = true;
                product.CreatedDate = DateTime.Now;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                ImageFile.SaveAs(fileName);
                dbContext.GetRepositoryInstance<Product>().Add(product);            
                dbContext.Complete();
            }
            var productInDb = dbContext.GetRepositoryInstance<Product>().GetFirstOrDefault(product.ProductId);
            productInDb.ProductName = product.ProductName;
            if(product.ImagePath != null)
                productInDb.ImagePath = product.ImagePath;
            productInDb.ModifiedDate = DateTime.Now;
            productInDb.CategoryId = product.CategoryId;
            productInDb.Amount = product.Amount;
            productInDb.Price = product.Price;
            dbContext.Complete();
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var productInDb = dbContext.GetRepositoryInstance<Product>().GetFirstOrDefault(id);
            if (productInDb == null)
                return View("Index");
            ProductCategoryImageViewModel productCategory = new ProductCategoryImageViewModel
            {
                Product = productInDb,
                Category = dbContext.GetRepositoryInstance<Category>().GetAll().Where(c => c.IsActive == true),
            };
            return View("New", productCategory);
        }

        [HttpDelete]
        public ActionResult Delete(Product product)
        {
            var productInDb = dbContext.GetRepositoryInstance<Product>().GetFirstOrDefault(product.ProductId);
            if (productInDb == null)
                return View("Index");
            dbContext.GetRepositoryInstance<Product>().Remove(product);
            dbContext.Complete();
            return View("Index");
        }

    }

}