using CSLY.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSLY.Models;
using AutoMapper;
using CSLY.Dtos;

namespace CSLY.Controllers.api
{
    public class CategoriesController : ApiController
    {
        protected GenericUnitofWork _unitOfWork = new GenericUnitofWork();


        public IHttpActionResult GetCategories(string query = null)
        {
            var categoryInDb = _unitOfWork.GetRepositoryInstance<Category>()
                .GetAll()
                .ToList();
            if (!string.IsNullOrWhiteSpace(query))
                categoryInDb = categoryInDb.Where(c => c.CategoryName.Contains(query)).ToList();

            return Ok(categoryInDb);
        }

        public IHttpActionResult GetCategory(int id)
        {
            var categoryInDb = _unitOfWork.GetRepositoryInstance<Category>()
                .GetFirstOrDefault(id);
            if(categoryInDb != null && categoryInDb.IsActive == true)
            {
                return Ok(categoryInDb);
            }
            return NotFound();
        }

        [Route("api/Categories/{status}/{pageNumber}")]
        public IHttpActionResult GetCategoriesPagination(int status , int pageNumber)
        {
            var categoryInDb = _unitOfWork.GetRepositoryInstance<Category>()
                .GetAll()
                .Skip(status * (pageNumber - 1))
                .Take(status);

            if (categoryInDb == null)
                return NotFound();
            return Ok(categoryInDb);
        }

        [HttpPost]
        public IHttpActionResult AddCategory(Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            newCategory.IsActive = true;
            _unitOfWork.GetRepositoryInstance<Category>().Add(newCategory);
            _unitOfWork.Complete();
            return Created(new Uri(Request.RequestUri + "/" + newCategory.CategoryId), newCategory);
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory(Category newcategory,int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categoryInDb = _unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(id);
            if(categoryInDb != null)
            {
                if (categoryInDb.IsActive == true)
                {
                    categoryInDb.CategoryName = newcategory.CategoryName;
                    _unitOfWork.Complete();
                    return Created(new Uri(Request.RequestUri + "/" + categoryInDb.CategoryId), categoryInDb);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var categoryId = new Category { CategoryId = id };
            _unitOfWork.GetRepositoryInstance<Category>().Remove(categoryId);
            _unitOfWork.Complete();
            return Ok();
        }

    }
}

