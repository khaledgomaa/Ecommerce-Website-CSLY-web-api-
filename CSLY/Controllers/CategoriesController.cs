using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSLY.Repository;
using CSLY.Models;
using AutoMapper;

namespace CSLY.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Admin
        protected GenericUnitofWork _unitOfWork = new GenericUnitofWork();

        #region CategoryActions

        public ActionResult Index()
        {
            IEnumerable<Category> allCategories = _unitOfWork.GetRepositoryInstance<Category>().GetAll().Where(i => i.IsActive == true);
            return View(allCategories);
        }

        public ActionResult New()
        {
            Category category = new Category();
            return View(category);
        }

        [HttpPost]
        public ActionResult CategoryManager(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View("New",category);
            }
            if(category.CategoryId != 0)
            {
                var categoryInDb = _unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(category.CategoryId);
                categoryInDb.CategoryName = category.CategoryName;
                _unitOfWork.Complete();
            }
            else
            {
                category.IsActive = true;
                _unitOfWork.GetRepositoryInstance<Category>().Add(category);
                _unitOfWork.Complete();
            }
            
            return RedirectToAction("Index", "Categories");
        }

        public ActionResult Edit(int id)
        {
            var category = _unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(id);
            if(category != null)
            {
                return View("New", category);
            }
            return HttpNotFound();
        }

        public ActionResult Delete(int id)
        {
            var category = _unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(id);
            if (category != null)
            {
                _unitOfWork.GetRepositoryInstance<Category>().Remove(category);
                _unitOfWork.Complete();
            }
            return RedirectToAction("Index", "Categories");
        }

        #endregion
    }
}