using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSLY.Models;
using CSLY.Dtos;
using CSLY.Repository;
using AutoMapper;

namespace CSLY.Controllers
{
    public class HomeController : Controller
    {
        private GenericUnitofWork dbContext = new GenericUnitofWork();

        public ActionResult Index(string search)
        {
            if(search != null)
            {
                var productDtoBySearch = dbContext.GetRepositoryInstance<Product>().GetFirstOrDefaultByParam(p => p.ProductName == search);
                return View(productDtoBySearch);
            }
            var productDto = dbContext.GetRepositoryInstance<Product>().GetAll().Where(p=>p.IsActive == true && p.Amount >0).Select(Mapper.Map<Product, ProductDto>);
            return View(productDto);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}